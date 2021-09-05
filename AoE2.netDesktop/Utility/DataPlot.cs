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
    /// Data Plot by ScottPlot.
    /// </summary>
    public class DataPlot
    {
        private readonly PlayerMatchHistory playerMatchHistory;
        private readonly int profileId;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPlot"/> class.
        /// </summary>
        /// <param name="playerMatchHistory">playerMatchHistory.</param>
        /// <param name="profileId">Profile ID.</param>
        public DataPlot(PlayerMatchHistory playerMatchHistory, int profileId)
        {
            this.playerMatchHistory = playerMatchHistory;
            this.profileId = profileId;
        }

        /// <summary>
        /// Plot Game and Win Rate Each Map.
        /// </summary>
        /// <param name="leaderBoardId">LeaderBoard Type.</param>
        /// <param name="plot">target Plot object.</param>
        public void PlotWinRateEachMap(LeaderBoardId leaderBoardId, Plot plot)
        {
            var rateWin = new Dictionary<string, double>();
            var rateLose = new Dictionary<string, double>();

            foreach (var item in playerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = item.GetPlayer(profileId);
                    if (player.Won != null) {
                        if (!rateWin.ContainsKey(item.GetMapName())) {
                            rateWin.Add(item.GetMapName(), 0);
                            rateLose.Add(item.GetMapName(), 0);
                        }

                        if ((bool)player.Won) {
                            rateWin[item.GetMapName()]++;
                        } else {
                            rateLose[item.GetMapName()]++;
                        }
                    }
                }
            }

            var winList = rateWin.Values.ToArray();
            var loseList = rateLose.Values.ToArray();

            // to simulate stacking Lose on Win, shift Lose up by Win
            var stackedList = new List<double>();
            for (int i = 0; i < loseList.Length; i++) {
                stackedList.Add(winList[i] + loseList[i]);
            }

            if (stackedList.Count != 0) {
                var barWin = plot.AddBar(stackedList.ToArray());
                var barLose = plot.AddBar(winList);
                barWin.Orientation = Orientation.Horizontal;
                barLose.Orientation = Orientation.Horizontal;
                barWin.ShowValuesAboveBars = true;
                barLose.ShowValuesAboveBars = true;
                plot.Legend(location: Alignment.UpperRight);
                plot.YTicks(rateWin.Keys.ToArray());
                plot.SetAxisLimits(xMin: 0, yMin: -1);
            }
        }

        /// <summary>
        /// Plot Player rate.
        /// </summary>
        /// <param name="leaderBoardId">LeaderBoard Type.</param>
        /// <param name="plot">target Plot object.</param>
        /// <returns>Scatter plot data.</returns>
        public ScatterPlot PlotRate(LeaderBoardId leaderBoardId, Plot plot)
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

            var xs = dateList.Select(x => x.ToOADate()).ToArray();
            plot.SetViewLimits(xs.Min() - 10, xs.Max() + 10, rateList.Min() - 10, rateList.Max() + 10);
            plot.XAxis.TickLabelFormat("yyyy/MM/dd", dateTimeFormat: true);
            plot.XAxis.ManualTickSpacing(1, ScottPlot.Ticks.DateTimeUnit.Month);
            plot.XAxis.TickLabelStyle(rotation: 45);

            var ohlc = new List<OHLC>();
            var oneDay = new TimeSpan(2, 0, 0, 0);

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

            var scatterPlot = plot.AddScatter(xs, rateList.ToArray(), lineStyle: LineStyle.Dot);
            var candlePlot = plot.AddCandlesticks(ohlc.ToArray());

            try {
                var bol = candlePlot.GetBollingerBands(7);
                plot.AddScatterLines(bol.xs, bol.sma, Color.Blue);
            } catch (ArgumentException e) {
                Debug.Print($" Plot Rate ERROR. {e.Message} {e.StackTrace}");
            }

            return scatterPlot;
        }
    }
}
