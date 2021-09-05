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
        /// Plot played player country.
        /// </summary>
        /// <param name="plot">target plot.</param>
        public void PlotPlayedPlayerCountry(Plot plot)
        {
            var countryList = new Dictionary<string, double>();

            foreach (var match in playerMatchHistory) {
                foreach (var player in match.Players) {
                    var selectedPlayer = match.GetPlayer(profileId);
                    if (player != selectedPlayer) {
                        var country = player.Country ?? "N/A";
                        if (!countryList.ContainsKey(country)) {
                            countryList.Add(country, 0);
                        }

                        countryList[country]++;
                    }
                }
            }

            var countryNames = countryList.Keys.Select(x =>
            {
                if (!CountryCode.ISO31661alpha2.TryGetValue(x, out var countryName)) {
                    countryName = "N/A";
                }

                return countryName;
            });

            var bar = plot.AddBar(countryList.Values.ToArray());
            bar.Orientation = Orientation.Horizontal;
            bar.ShowValuesAboveBars = true;

            plot.YTicks(countryNames.ToArray());
            plot.SetAxisLimits(xMin: 0, yMin: -1);
        }

        /// <summary>
        /// Plot win rate each civilization.
        /// </summary>
        /// <param name="leaderBoardId">target leader board.</param>
        /// <param name="plot">target plot object.</param>
        public void PlotWinRateEachCivilization(LeaderBoardId leaderBoardId, Plot plot)
        {
            var rateWin = new Dictionary<string, double>();
            var rateLose = new Dictionary<string, double>();

            foreach (var item in playerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = item.GetPlayer(profileId);
                    AddWonRate(rateWin, rateLose, player.Won, player.GetCivName());
                }
            }

            UpdateStackedBarGraph(plot, rateWin.Keys.ToList(), rateWin.Values.ToList(), rateLose.Values.ToList());
        }

        /// <summary>
        /// Plot win rate each map.
        /// </summary>
        /// <param name="leaderBoardId">target leader board.</param>
        /// <param name="plot">target plot object.</param>
        public void PlotWinRateEachMap(LeaderBoardId leaderBoardId, Plot plot)
        {
            var rateWin = new Dictionary<string, double>();
            var rateLose = new Dictionary<string, double>();

            foreach (var item in playerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = item.GetPlayer(profileId);
                    AddWonRate(rateWin, rateLose, player.Won, item.GetMapName());
                }
            }

            UpdateStackedBarGraph(plot, rateWin.Keys.ToList(), rateWin.Values.ToList(), rateLose.Values.ToList());
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
                plot.SetViewLimits(xs.Min() - 10, xs.Max() + 10, rateList.Min() - 10, rateList.Max() + 10);
                plot.XAxis.TickLabelFormat("yyyy/MM/dd", dateTimeFormat: true);
                plot.XAxis.ManualTickSpacing(1, ScottPlot.Ticks.DateTimeUnit.Month);
                plot.XAxis.TickLabelStyle(rotation: 45);

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

                scatterPlot = plot.AddScatter(xs, rateList.ToArray(), lineStyle: LineStyle.Dot);
                var candlePlot = plot.AddCandlesticks(ohlc.ToArray());
                candlePlot.ColorUp = Color.Red;
                candlePlot.ColorDown = Color.Green;

                try {
                    var bol = candlePlot.GetBollingerBands(ohlc.Count < 7 ? 2 : 7);
                    plot.AddScatterLines(bol.xs, bol.sma, Color.Blue);
                } catch (ArgumentException e) {
                    Debug.Print($" Plot Rate ERROR. {e.Message} {e.StackTrace}");
                }
            }

            return scatterPlot;
        }

        private static void UpdateStackedBarGraph(Plot plot, List<string> ticks, List<double> lowerData, List<double> upperDate)
        {
            if (plot is null) {
                throw new ArgumentNullException(nameof(plot));
            }

            if (ticks is null) {
                throw new ArgumentNullException(nameof(ticks));
            }

            if ((lowerData?.Count == upperDate?.Count) && (lowerData?.Count != 0)) {
                var winList = lowerData.ToArray();
                var loseList = upperDate.ToArray();

                // to simulate stacking Lose on Win, shift Lose up by Win
                double[] stackedList = new double[loseList.Length];
                for (int i = 0; i < loseList.Length; i++) {
                    stackedList[i] = winList[i] + loseList[i];
                }

                // plot the bar charts in reverse order (highest first)
                var barLose = plot.AddBar(stackedList);
                var barWin = plot.AddBar(winList);
                barWin.Orientation = Orientation.Horizontal;
                barLose.Orientation = Orientation.Horizontal;
                barWin.ShowValuesAboveBars = true;
                barLose.ShowValuesAboveBars = true;
                barWin.FillColor = Color.Green;
                barLose.FillColor = Color.IndianRed;
                plot.YTicks(ticks.ToArray());
                plot.SetAxisLimits(xMin: 0, yMin: -1);
            }
        }

        private static void AddWonRate(Dictionary<string, double> rateWin, Dictionary<string, double> rateLose, bool? playerWon, string key)
        {
            if (playerWon != null) {
                if (!rateWin.ContainsKey(key)) {
                    rateWin.Add(key, 0);
                    rateLose.Add(key, 0);
                }

                if ((bool)playerWon) {
                    rateWin[key]++;
                } else {
                    rateLose[key]++;
                }
            }
        }
    }
}
