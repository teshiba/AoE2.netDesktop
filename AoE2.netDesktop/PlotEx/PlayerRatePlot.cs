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
        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerRatePlot"/> class.
        /// </summary>
        /// <param name="formsPlot">Parent FormsPlot.</param>
        public PlayerRatePlot(FormsPlot formsPlot)
        {
            FormsPlot = formsPlot;
        }

        /// <summary>
        /// Gets parent formsPlot.
        /// </summary>
        protected FormsPlot FormsPlot { get; }

        /// <summary>
        /// Plot Player rate.
        /// </summary>
        /// <param name="playerMatchHistory">PlayerMatchHistory.</param>
        /// <param name="profileId">Profile ID.</param>
        /// <param name="leaderBoardId">LeaderBoard Type.</param>
        /// <returns>Scatter plot data.</returns>
        public ScatterPlot Plot(PlayerMatchHistory playerMatchHistory, int profileId, LeaderBoardId leaderBoardId)
        {
            var dateList = new List<DateTime>();
            var rateList = new List<double>();
            ScatterPlot scatterPlot = null;

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
                var xs = dateList.Select(x => x.ToOADate()).ToArray();
                FormsPlot.Plot.Clear();
                FormsPlot.Plot.SetOuterViewLimits(xs.Min() - 10, xs.Max() + 10, rateList.Min() - 10, rateList.Max() + 10);
                FormsPlot.Plot.XAxis.TickLabelFormat("yyyy/MM/dd", dateTimeFormat: true);
                FormsPlot.Plot.XAxis.ManualTickSpacing(1, ScottPlot.Ticks.DateTimeUnit.Month);
                FormsPlot.Plot.XAxis.TickLabelStyle(rotation: 45);

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

                scatterPlot = FormsPlot.Plot.AddScatter(xs, rateList.ToArray(), lineStyle: LineStyle.Dot);
                var candlePlot = FormsPlot.Plot.AddCandlesticks(ohlc.ToArray());
                candlePlot.ColorUp = Color.Red;
                candlePlot.ColorDown = Color.Green;

                try {
                    var bol = candlePlot.GetBollingerBands(7);
                    FormsPlot.Plot.AddScatterLines(bol.xs, bol.sma, Color.Blue);
                } catch (ArgumentException e) {
                    Debug.Print($" Plot Rate ERROR. {e.Message} {e.StackTrace}");
                }
            }

            FormsPlot.Render();

            return scatterPlot;
        }
    }
}