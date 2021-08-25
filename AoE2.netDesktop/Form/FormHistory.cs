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
            formsPlotWinRate1v1EachMap.Plot.Title("1v1 Random Map Count");
            formsPlotWinRate1v1EachMap.Plot.YLabel("Map");
            formsPlotWinRate1v1EachMap.Plot.XLabel("Win / Total Game count");
            formsPlotRate1v1.Plot.Title("1v1 Random Map Rate");
            formsPlotRate1v1.Plot.YLabel("Rate");
            formsPlotRate1v1.Plot.XLabel("Date");

            formsPlotMapRateTeam.Configuration.LockHorizontalAxis = true;
            formsPlotMapRateTeam.Configuration.LockVerticalAxis = true;
            formsPlotWinRateTeamEachMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRateTeamEachMap.Configuration.LockVerticalAxis = true;
            formsPlotWinRateTeamEachMap.Plot.Title("Team Random Map Count");
            formsPlotWinRateTeamEachMap.Plot.YLabel("Map");
            formsPlotWinRateTeamEachMap.Plot.XLabel("Win / Total Game count");
            formsPlotRateTeam.Plot.Title("Team Random Map Rate");
            formsPlotRateTeam.Plot.YLabel("Rate");
            formsPlotRateTeam.Plot.XLabel("Date");
        }

        /// <inheritdoc/>
        protected override CtrlHistory Controler { get => (CtrlHistory)base.Controler; }

        private static ListViewItem GetLeaderboard(string leaderboardName, Leaderboard leaderboard)
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

        private async void FormHistory_ShownAsync(object sender, System.EventArgs e)
        {
            await UpdateGraph();

            if (await Controler.ReadLeaderBoardAsync()) {
                UpdateListViewStatistics();
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }
        }

        private async Task UpdateGraph()
        {
            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                UpdateListViewHistory();
                UpdateRate1v1();
                UpdateRateTeam();
                UpdateWinRateEachMap(LeaderBoardId.OneVOneRandomMap, formsPlotWinRate1v1EachMap.Plot);
                UpdateWinRateEachMap(LeaderBoardId.TeamRandomMap, formsPlotWinRateTeamEachMap.Plot);
                UpdateMapRate(LeaderBoardId.OneVOneRandomMap, formsPlotMapRate1v1.Plot);
                UpdateMapRate(LeaderBoardId.TeamRandomMap, formsPlotMapRateTeam.Plot);
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }
        }

        private void UpdateListViewStatistics()
        {
            listViewStatistics.Items.Add(GetLeaderboard("1v1 RM", Controler.Leaderboards[LeaderBoardId.OneVOneRandomMap]));
            listViewStatistics.Items.Add(GetLeaderboard("Team RM", Controler.Leaderboards[LeaderBoardId.TeamRandomMap]));
            listViewStatistics.Items.Add(GetLeaderboard("1v1 DM", Controler.Leaderboards[LeaderBoardId.OneVOneDeathmatch]));
            listViewStatistics.Items.Add(GetLeaderboard("Team DM", Controler.Leaderboards[LeaderBoardId.TeamDeathmatch]));
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
            plot.Legend();
        }

        private void UpdateWinRateEachMap(LeaderBoardId leaderBoardId, Plot plot)
        {
            var rateWin = new Dictionary<string, double>();
            var rateLose = new Dictionary<string, double>();

            foreach (var item in Controler.PlayerMatchHistory) {
                if (item.LeaderboardId == leaderBoardId) {
                    var player = Controler.GetSelectedPlayer(item);
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
            plot.Legend(location: Alignment.UpperRight);
            plot.YTicks(rateWin.Keys.ToArray());
            plot.SetAxisLimits(xMin: 0, yMin: -1);
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
            if (scatterPlot1v1 != null) {
                // determine point nearest the cursor
                (double mouseCoordX, double mouseCoordY) = formsPlotRate1v1.GetMouseCoordinates();
                double xyRatio = formsPlotRate1v1.Plot.XAxis.Dims.PxPerUnit / formsPlotRate1v1.Plot.YAxis.Dims.PxPerUnit;
                (double pointX, double pointY, int pointIndex) = scatterPlot1v1.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

                // place the highlight over the point of interest
                highlightedPoint1v1.Xs[0] = pointX;
                highlightedPoint1v1.Ys[0] = pointY;
                highlightedPoint1v1.Label = $"Rate:{pointY}";
                tooltip1v1.Label = $"Rate:{pointY} {DateTime.FromOADate(pointX)}";
                tooltip1v1.X = pointX;
                tooltip1v1.Y = pointY;

                // render if the highlighted point chnaged
                if (lastHighlightedIndex1v1 != pointIndex) {
                    lastHighlightedIndex1v1 = pointIndex;
                    formsPlotRate1v1.Render();
                }
            }
        }

        private void FormsPlotRateTeam_MouseMove(object sender, MouseEventArgs e)
        {
            if (scatterPlotTeam != null) {
                // determine point nearest the cursor
                (double mouseCoordX, double mouseCoordY) = formsPlotRateTeam.GetMouseCoordinates();
                double xyRatio = formsPlotRateTeam.Plot.XAxis.Dims.PxPerUnit / formsPlotRateTeam.Plot.YAxis.Dims.PxPerUnit;
                (double pointX, double pointY, int pointIndex) = scatterPlotTeam.GetPointNearest(mouseCoordX, mouseCoordY, xyRatio);

                // place the highlight over the point of interest
                highlightedPointTeam.Xs[0] = pointX;
                highlightedPointTeam.Ys[0] = pointY;
                highlightedPointTeam.Label = $"Rate:{pointY}";
                tooltipTeam.Label = $"Rate:{pointY} {DateTime.FromOADate(pointX)}";
                tooltipTeam.X = pointX;
                tooltipTeam.Y = pointY;

                // render if the highlighted point chnaged
                if (lastHighlightedIndexTeam != pointIndex) {
                    lastHighlightedIndexTeam = pointIndex;
                    formsPlotRateTeam.Render();
                }
            }
        }
    }
}
