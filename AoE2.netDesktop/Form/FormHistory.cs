namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AoE2NetDesktop.From;
    using LibAoE2net;
    using ScottPlot;
    using ScottPlot.Plottable;

    /// <summary>
    /// FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        private ScatterPlot highlightedPoint1v1;
        private ScatterPlot highlightedPointTeam;
        private Tooltip tooltip1v1;
        private Tooltip tooltipTeam;
        private ScatterPlot scatterPlot1v1;
        private ScatterPlot scatterPlotTeam;
        private int lastHighlightedIndex1v1 = -1;
        private int lastHighlightedIndexTeam = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHistory"/> class.
        /// </summary>
        /// <param name="ctrlHistory">A class that inherits FormControler.</param>
        public FormHistory(CtrlHistory ctrlHistory)
            : base(ctrlHistory)
        {
            InitializeComponent();

            formsPlotMapRate1v1.Configuration.LockHorizontalAxis = true;
            formsPlotMapRate1v1.Configuration.LockVerticalAxis = true;
            formsPlotWinRate1v1EachMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRate1v1EachMap.Configuration.LockVerticalAxis = true;
            formsPlotCiv1v1.Configuration.LockHorizontalAxis = true;
            formsPlotCiv1v1.Configuration.LockVerticalAxis = true;
            formsPlotWinRate1v1EachMap.Plot.Title("1v1 Random Map Count");
            formsPlotWinRate1v1EachMap.Plot.YLabel("Map");
            formsPlotWinRate1v1EachMap.Plot.XLabel("Win / Total Game count");
            formsPlotRate1v1.Plot.Title("1v1 Random Map Rate");
            formsPlotRate1v1.Plot.YLabel("Rate");
            formsPlotRate1v1.Plot.XLabel("Date");
            formsPlotCiv1v1.Plot.Title("1v1 Random Map");
            formsPlotCiv1v1.Plot.YLabel("Civilization Name");
            formsPlotCiv1v1.Plot.XLabel("Win / Total Game Count");

            formsPlotMapRateTeam.Configuration.LockHorizontalAxis = true;
            formsPlotMapRateTeam.Configuration.LockVerticalAxis = true;
            formsPlotWinRateTeamEachMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRateTeamEachMap.Configuration.LockVerticalAxis = true;
            formsPlotCivTeam.Configuration.LockHorizontalAxis = true;
            formsPlotCivTeam.Configuration.LockVerticalAxis = true;
            formsPlotWinRateTeamEachMap.Plot.Title("Team Random Map Count");
            formsPlotWinRateTeamEachMap.Plot.YLabel("Map");
            formsPlotWinRateTeamEachMap.Plot.XLabel("Win / Total Game count");
            formsPlotRateTeam.Plot.Title("Team Random Map Rate");
            formsPlotRateTeam.Plot.YLabel("Rate");
            formsPlotRateTeam.Plot.XLabel("Date");
            formsPlotCivTeam.Plot.Title("Team Random Map");
            formsPlotCivTeam.Plot.YLabel("Civilization Name");
            formsPlotCivTeam.Plot.XLabel("Win / Total Game Count");

            formsPlotCountry.Configuration.LockHorizontalAxis = true;
            formsPlotCountry.Configuration.LockVerticalAxis = true;
            formsPlotCountry.Plot.Title("Player's country");
            formsPlotCountry.Plot.YLabel("Country");
            formsPlotCountry.Plot.XLabel("Game count");
        }

        /// <inheritdoc/>
        protected override CtrlHistory Controler { get => (CtrlHistory)base.Controler; }

        private static ListViewItem GetLeaderboardListViewItem(string leaderboardName, Leaderboard leaderboard)
        {
            var ret = new ListViewItem(leaderboardName);

            ret.SubItems.Add(leaderboard.Rank.ToString());
            ret.SubItems.Add(leaderboard.Rating.ToString());
            ret.SubItems.Add(leaderboard.HighestRating.ToString());
            ret.SubItems.Add(leaderboard.Games.ToString());
            ret.SubItems.Add(leaderboard.Wins.ToString());
            ret.SubItems.Add(leaderboard.Losses.ToString());
            if (leaderboard.Games == 0) {
                ret.SubItems.Add("00.0%");
            } else {
                var winRate = (double)leaderboard.Wins / leaderboard.Games * 100;
                ret.SubItems.Add($"{winRate:F1}%");
            }

            return ret;
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

        private static void UpdateStackedBarGraph(Plot plot, List<string> ticks, List<double> lowerData, List<double> upperDate)
        {
            var winList = lowerData.ToArray();
            var loseList = upperDate.ToArray();

            // to simulate stacking Lose on Win, shift Lose up by Win
            double[] stackedList = new double[loseList.Length];
            for (int i = 0; i < loseList.Length; i++) {
                stackedList[i] = winList[i] + loseList[i];
            }

            // plot the bar charts in reverse order (highest first)
            var barWin = plot.AddBar(stackedList);
            var barLose = plot.AddBar(winList);
            barWin.Orientation = ScottPlot.Orientation.Horizontal;
            barLose.Orientation = ScottPlot.Orientation.Horizontal;
            barWin.ShowValuesAboveBars = true;
            barLose.ShowValuesAboveBars = true;
            plot.YTicks(ticks.ToArray());
            plot.SetAxisLimits(xMin: 0, yMin: -1);
        }

        private static void UpdatePointerOfRateGraph(FormsPlot formsPlot, ScatterPlot highlightedPoint, ScatterPlot scatterPlot, Tooltip tooltip, ref int lastHighlightedIndex)
        {
            if (scatterPlot != null) {
                // determine point nearest the cursor
                (double mouseCoordX, double mouseCoordY) = formsPlot.GetMouseCoordinates();
                var xyRatio = formsPlot.Plot.XAxis.Dims.PxPerUnit / formsPlot.Plot.YAxis.Dims.PxPerUnit;
                (double pointX, double pointY, int pointIndex) = scatterPlot.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

                // place the highlight over the point of interest
                highlightedPoint.Xs[0] = pointX;
                highlightedPoint.Ys[0] = pointY;
                highlightedPoint.Label = $"Rate:{pointY}";
                tooltip.Label = $"Rate:{pointY} {DateTime.FromOADate(pointX)}";
                tooltip.X = pointX;
                tooltip.Y = pointY;

                // render if the highlighted point chnaged
                if (lastHighlightedIndex != pointIndex) {
                    lastHighlightedIndex = pointIndex;
                    formsPlot.Render();
                }
            }
        }

        private static Diplomacy CheckDiplomacy(Player selectedPlayer, Player player)
        {
            var ret = Diplomacy.Enemy;

            if (selectedPlayer.Color % 2 == player.Color % 2) {
                ret = Diplomacy.Ally;
            }

            return ret;
        }

        private async void FormHistory_ShownAsync(object sender, System.EventArgs e)
        {
            await UpdatePlayerMatchHistory();
            await UpdateListViewStatistics();
        }

        private async Task UpdatePlayerMatchHistory()
        {
            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                UpdateListViewHistory();
                UpdateRate1v1();
                UpdateRateTeam();
                UpdateListViewMatchedPlayers();
                UpdateCountry();
                UpdateWinRateEachMap(LeaderBoardId.OneVOneRandomMap, formsPlotWinRate1v1EachMap.Plot);
                UpdateWinRateEachMap(LeaderBoardId.TeamRandomMap, formsPlotWinRateTeamEachMap.Plot);
                UpdateMapRate(LeaderBoardId.OneVOneRandomMap, formsPlotMapRate1v1.Plot);
                UpdateMapRate(LeaderBoardId.TeamRandomMap, formsPlotMapRateTeam.Plot);
                UpdateWinRateEachCivilization(LeaderBoardId.OneVOneRandomMap, formsPlotCiv1v1.Plot);
                UpdateWinRateEachCivilization(LeaderBoardId.TeamRandomMap, formsPlotCivTeam.Plot);
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }
        }

        private void UpdateCountry()
        {
            var countryList = new Dictionary<string, double>();
            var plot = formsPlotCountry.Plot;

            foreach (var match in Controler.PlayerMatchHistory) {
                var selectedPlayer = Controler.GetSelectedPlayer(match);
                foreach (var player in match.Players) {
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
                if (CountryCode.ISO31661alpha2.TryGetValue(x, out var countryName)) {
                    countryName = "N/A";
                }

                return countryName;
            });
            plot.YTicks(countryNames.ToArray());
            plot.SetAxisLimits(xMin: 0, yMin: -1);

            var bar = plot.AddBar(countryList.Values.ToArray());
            bar.Orientation = ScottPlot.Orientation.Horizontal;
            bar.ShowValuesAboveBars = true;
        }

        private async Task UpdateListViewStatistics()
        {
            if (await Controler.ReadLeaderBoardAsync()) {
                listViewStatistics.Items.Add(GetLeaderboardListViewItem("1v1 RM", Controler.Leaderboards[LeaderBoardId.OneVOneRandomMap]));
                listViewStatistics.Items.Add(GetLeaderboardListViewItem("Team RM", Controler.Leaderboards[LeaderBoardId.TeamRandomMap]));
                listViewStatistics.Items.Add(GetLeaderboardListViewItem("1v1 DM", Controler.Leaderboards[LeaderBoardId.OneVOneDeathmatch]));
                listViewStatistics.Items.Add(GetLeaderboardListViewItem("Team DM", Controler.Leaderboards[LeaderBoardId.TeamDeathmatch]));
            } else {
                Debug.Print("UpdateListViewStatistics ERROR.");
            }
        }

        private void UpdateRate1v1()
        {
            scatterPlot1v1 = UpdateRatePlot(LeaderBoardId.OneVOneRandomMap, formsPlotRate1v1.Plot);
            highlightedPoint1v1 = formsPlotRate1v1.Plot.AddPoint(0, 0);
            highlightedPoint1v1.Color = Color.Red;
            highlightedPoint1v1.MarkerSize = 10;
            highlightedPoint1v1.MarkerShape = MarkerShape.openCircle;
            tooltip1v1 = formsPlotRate1v1.Plot.AddTooltip(label: "Rate", x: -1000, y: -1000);
            tooltip1v1.LabelPadding = 0;
            tooltip1v1.FillColor = Color.White;
            tooltip1v1.Font.Size = 16;
            tooltip1v1.Font.Bold = true;
            formsPlotRate1v1.Render();
        }

        private void UpdateRateTeam()
        {
            scatterPlotTeam = UpdateRatePlot(LeaderBoardId.TeamRandomMap, formsPlotRateTeam.Plot);
            highlightedPointTeam = formsPlotRateTeam.Plot.AddPoint(0, 0);
            highlightedPointTeam.Color = Color.Red;
            highlightedPointTeam.MarkerSize = 10;
            highlightedPointTeam.MarkerShape = MarkerShape.openCircle;
            tooltipTeam = formsPlotRateTeam.Plot.AddTooltip(label: "Rate", x: -1000, y: -1000);
            tooltipTeam.LabelPadding = 0;
            tooltipTeam.FillColor = Color.White;
            tooltipTeam.Font.Size = 16;
            tooltipTeam.Font.Bold = true;
            formsPlotRateTeam.Render();
        }

        private void UpdateMapRate(LeaderBoardId leaderBoardId, Plot plot)
        {
            var mapRate = new Dictionary<string, double>();

            foreach (var item in Controler.PlayerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    if (!mapRate.ContainsKey(item.GetMapName())) {
                        mapRate.Add(item.GetMapName(), 0);
                    }

                    mapRate[item.GetMapName()]++;
                }
            }

            var pie = plot.AddPie(mapRate.Values.ToArray());
            pie.SliceLabels = mapRate.Keys.ToArray();
            pie.ShowPercentages = true;
            pie.ShowValues = true;
            pie.ShowLabels = true;
        }

        private void UpdateListViewMatchedPlayers()
        {
            var playerList = new Dictionary<string, PlayerInfo>();

            foreach (var match in Controler.PlayerMatchHistory) {
                var selectedPlayer = Controler.GetSelectedPlayer(match);
                foreach (var player in match.Players) {
                    if (player != selectedPlayer) {
                        if (!playerList.ContainsKey(player.Name)) {
                            playerList.Add(player.Name, new PlayerInfo());
                        }

                        playerList[player.Name].Country = player.Country;

                        switch (match.LeaderboardId) {
                        case LeaderBoardId.OneVOneRandomMap:
                            playerList[player.Name].Rate1v1RM = player.Rating;
                            playerList[player.Name].Games1v1++;
                            break;
                        case LeaderBoardId.TeamRandomMap:
                            playerList[player.Name].RateTeamRM = player.Rating;
                            playerList[player.Name].GamesTeam++;

                            switch (CheckDiplomacy(selectedPlayer, player)) {
                            case Diplomacy.Ally:
                                playerList[player.Name].GamesAlly++;
                                break;
                            case Diplomacy.Enemy:
                                playerList[player.Name].GamesEnemy++;
                                break;
                            default:
                                break;
                            }

                            break;
                        default:
                            break;
                        }

                        playerList[player.Name].LastDate = match.GetOpenedTime();
                    }
                }
            }

            foreach (var player in playerList) {
                var listviewItem = new ListViewItem(player.Key);
                listviewItem.SubItems.Add(player.Value.Country);
                listviewItem.SubItems.Add(player.Value.Rate1v1RM.ToString());
                listviewItem.SubItems.Add(player.Value.RateTeamRM.ToString());
                listviewItem.SubItems.Add(player.Value.GamesTeam.ToString());
                listviewItem.SubItems.Add(player.Value.GamesAlly.ToString());
                listviewItem.SubItems.Add(player.Value.GamesEnemy.ToString());
                listviewItem.SubItems.Add(player.Value.Games1v1.ToString());
                listviewItem.SubItems.Add(player.Value.LastDate.ToString());
                listViewMatchedPlayers.Items.Add(listviewItem);
            }
        }

        private void UpdateWinRateEachCivilization(LeaderBoardId leaderBoardId, Plot plot)
        {
            var rateWin = new Dictionary<string, double>();
            var rateLose = new Dictionary<string, double>();

            foreach (var item in Controler.PlayerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = Controler.GetSelectedPlayer(item);
                    AddWonRate(rateWin, rateLose, player.Won, player.GetCivName());
                }
            }

            UpdateStackedBarGraph(plot, rateWin.Keys.ToList(), rateWin.Values.ToList(), rateLose.Values.ToList());
        }

        private void UpdateWinRateEachMap(LeaderBoardId leaderBoardId, Plot plot)
        {
            var rateWin = new Dictionary<string, double>();
            var rateLose = new Dictionary<string, double>();

            foreach (var item in Controler.PlayerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = Controler.GetSelectedPlayer(item);
                    AddWonRate(rateWin, rateLose, player.Won, item.GetMapName());
                }
            }

            UpdateStackedBarGraph(plot, rateWin.Keys.ToList(), rateWin.Values.ToList(), rateLose.Values.ToList());
        }

        private void UpdateListViewHistory()
        {
            foreach (var item in Controler.PlayerMatchHistory) {
                var player = Controler.GetSelectedPlayer(item);
                var listViewItem = new ListViewItem(item.GetMapName());

                listViewItem.SubItems.Add((player.Rating?.ToString() ?? "----")
                                        + (player.RatingChange?.Contains('-') ?? true ? string.Empty : "+")
                                        + player.RatingChange?.ToString());

                if (player.Won == null) {
                    listViewItem.SubItems.Add("---");
                } else {
                    var won = (bool)player.Won;
                    listViewItem.SubItems.Add(won ? "o" : string.Empty);
                }

                listViewItem.SubItems.Add(player.GetCivName());
                listViewItem.SubItems.Add(player.Color.ToString() ?? "-");
                listViewItem.SubItems.Add(item.GetOpenedTime().ToString());
                listViewItem.SubItems.Add(item.Version);

                if (player.Rating != null) {
                    switch (item.LeaderboardId) {
                    case LeaderBoardId.OneVOneRandomMap:
                        listViewHistory1v1.Items.Add(listViewItem);
                        break;
                    case LeaderBoardId.TeamRandomMap:
                        listViewHistoryTeam.Items.Add(listViewItem);
                        break;
                    default:
                        break;
                    }
                }
            }
        }

        private ScatterPlot UpdateRatePlot(LeaderBoardId leaderBoardId, Plot plot)
        {
            var dateList = new List<DateTime>();
            var rateList = new List<double>();

            foreach (var item in Controler.PlayerMatchHistory) {
                var player = Controler.GetSelectedPlayer(item);
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
            candlePlot.ColorUp = Color.Red;
            candlePlot.ColorDown = Color.Green;
            var bol = candlePlot.GetBollingerBands(7);
            plot.AddScatterLines(bol.xs, bol.sma, Color.Blue);

            return scatterPlot;
        }

        private void FormsPlotRate1v1_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePointerOfRateGraph(formsPlotRate1v1, highlightedPoint1v1, scatterPlot1v1, tooltip1v1, ref lastHighlightedIndex1v1);
        }

        private void FormsPlotRateTeam_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePointerOfRateGraph(formsPlotRateTeam, highlightedPointTeam, scatterPlotTeam, tooltipTeam, ref lastHighlightedIndexTeam);
        }
    }
}
