namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using LibAoE2net;
    using ScottPlot;
    using ScottPlot.Plottable;

    /// <summary>
    /// Player rate graph.
    /// </summary>
    public class PlayerRatePlot
    {
        private readonly FormsPlot formsPlot;

        private ScatterPlot scatterPlot = new (new double[] { double.NegativeInfinity }, new double[] { double.NegativeInfinity });
        private ScatterPlot scatterLines = new (new double[] { double.NegativeInfinity }, new double[] { double.NegativeInfinity });
        private FinancePlot candlesticks = new ();
        private PlotHighlight highlightPlot = new (new FormsPlot(), new ScatterPlot(new double[] { double.NegativeInfinity }, new double[] { double.NegativeInfinity }));

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRatePlot"/> class.
        /// </summary>
        /// <param name="formsPlot">Parent FormsPlot.</param>
        public PlayerRatePlot(FormsPlot formsPlot)
        {
            this.formsPlot = formsPlot;
        }

        /// <summary>
        /// Gets minimum value of X.
        /// </summary>
        public double? MinX => scatterPlot.Xs.Min() == double.NegativeInfinity ? null : scatterPlot.Xs.Min();

        /// <summary>
        /// Gets minimum value of Y.
        /// </summary>
        public double? MinY => scatterPlot.Ys.Min() == double.NegativeInfinity ? null : scatterPlot.Ys.Min();

        /// <summary>
        /// Gets maximum value of X.
        /// </summary>
        public double? MaxX => scatterPlot.Xs.Max() == double.NegativeInfinity ? null : scatterPlot.Xs.Max();

        /// <summary>
        /// Gets maximum value of Y.
        /// </summary>
        public double? MaxY => scatterPlot.Ys.Max() == double.NegativeInfinity ? null : scatterPlot.Ys.Max();

        /// <summary>
        /// Gets or sets a value indicating whether the plot is visible.
        /// </summary>
        public bool IsVisible {
            get => scatterPlot.IsVisible;
            set {
                scatterPlot.IsVisible = value;
                scatterLines.IsVisible = value;
                candlesticks.IsVisible = value;
                highlightPlot.IsVisible = value;
                formsPlot.Render();
            }
        }

        /// <summary>
        /// Update Highlight.
        /// </summary>
        public void UpdateHighlight()
        {
            highlightPlot.Update();
        }

        /// <summary>
        /// Plot player rate.
        /// </summary>
        /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
        /// <param name="profileId">Profile ID.</param>
        /// <param name="leaderBoardId">LeaderBoard Type.</param>
        public void Plot(PlayerMatchHistory playerMatchHistory, int profileId, LeaderboardId leaderBoardId)
        {
            var dateList = new List<DateTime>();
            var rateList = new List<double>();

            foreach (var item in playerMatchHistory) {
                var player = item.GetPlayer(profileId);
                if (player.Rating != null) {
                    int rate = (int)player.Rating;
                    if (item.LeaderboardId == leaderBoardId) {
                        rateList.Add(rate);
                        dateList.Add(item.GetOpenedTime());
                    }
                }
            }

            if (dateList.Count != 0) {
                formsPlot.Plot.Remove(scatterPlot);
                formsPlot.Plot.Remove(candlesticks);
                formsPlot.Plot.Remove(scatterLines);

                var xs = dateList.Select(x => x.ToOADate()).ToArray();

                var ohlc = new List<OHLC>();
                var oneDay = new TimeSpan(1, 0, 0, 0);

                ohlc.Add(new OHLC(rateList[0], rateList[0], rateList[0], rateList[0], dateList[0], oneDay));
                for (int i = 1; i < xs.Length; i++) {
                    if (ohlc.Last().DateTime.Date != dateList[i].Date) {
                        ohlc.Add(new OHLC(rateList[i], rateList[i], rateList[i], rateList[i], dateList[i], oneDay));
                    } else {
                        if (ohlc.Last().High < rateList[i]) {
                            ohlc.Last().High = rateList[i];
                        }

                        if (rateList[i] < ohlc.Last().Low) {
                            ohlc.Last().Low = rateList[i];
                        }

                        ohlc.Last().Close = rateList[i];
                    }
                }

                scatterPlot = formsPlot.Plot.AddScatter(xs, rateList.ToArray(), lineStyle: LineStyle.Dot);
                candlesticks = formsPlot.Plot.AddCandlesticks(ohlc.ToArray());
                candlesticks.ColorUp = Color.Red;
                candlesticks.ColorDown = Color.Green;

                try {
                    var bol = candlesticks.GetBollingerBands(7);
                    scatterLines = formsPlot.Plot.AddScatterLines(bol.xs, bol.sma, Color.Blue);
                } catch (ArgumentException e) {
                    Debug.Print($" Plot Rate ERROR. {e.Message} {e.StackTrace}");
                }

                highlightPlot = new PlotHighlight(formsPlot, scatterPlot);
                highlightPlot.Update();
            }

            formsPlot.Render();
        }
    }
}