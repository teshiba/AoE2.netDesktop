namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using AoE2NetDesktop.From;
    using LibAoE2net;
    using ScottPlot;

    /// <summary>
    /// FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHistory"/> class.
        /// </summary>
        /// <param name="ctrlHistory">A class that inherits FormControler.</param>
        public FormHistory(CtrlHistory ctrlHistory)
            : base(ctrlHistory)
        {
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override CtrlHistory Controler { get => (CtrlHistory)base.Controler; }

        private async void FormHistory_ShownAsync(object sender, System.EventArgs e)
        {
            bool ret = await Controler.ReadPlayerMatchHistoryAsync();

            if (ret) {
                UpdateListView();
                UpdateRatePlot();
                UpdateWinRateEachMap(LeaderBoardId.OneVOneRandomMap, formsPlotWinRate1v1EachMap.Plot);
                UpdateWinRateEachMap(LeaderBoardId.TeamRandomMap, formsPlotWinRateTeamEachMap.Plot);
                UpdateMapRate(LeaderBoardId.OneVOneRandomMap, formsPlotMapRate1v1.Plot);
                UpdateMapRate(LeaderBoardId.TeamRandomMap, formsPlotMapRateTeam.Plot);
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }
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
            plot.Legend(location: Alignment.UpperRight);
            plot.YTicks(rateWin.Keys.ToArray());
        }

        private void UpdateListView()
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

        private void UpdateRatePlot()
        {
            var dateList1v1 = new List<DateTime>();
            var rateList1v1 = new List<double>();
            var dateListTeam = new List<DateTime>();
            var rateListTeam = new List<double>();

            foreach (var item in Controler.PlayerMatchHistory) {
                var player = Controler.GetSelectedPlayer(item);
                if (player.Rating != null) {
                    int rate = (int)player.Rating;
                    switch (item.LeaderboardId) {
                    case LeaderBoardId.OneVOneRandomMap:
                        rateList1v1.Add(rate);
                        dateList1v1.Add(item.GetOpenedTime());
                        break;
                    case LeaderBoardId.TeamRandomMap:
                        rateListTeam.Add(rate);
                        dateListTeam.Add(item.GetOpenedTime());
                        break;
                    default:
                        break;
                    }
                }
            }

            var xs1v1 = dateList1v1.Select(x => x.ToOADate()).ToArray();
            var xsTeam = dateListTeam.Select(x => x.ToOADate()).ToArray();
            formsPlotRate.Plot.XAxis.DateTimeFormat(true);

            var ohlc1v1 = new List<OHLC>();
            var ohlcTeam = new List<OHLC>();
            var oneDay = new TimeSpan(3, 0, 0, 0);

            ohlc1v1.Add(new OHLC(rateList1v1[0], rateList1v1[0], rateList1v1[0], rateList1v1[0], dateList1v1[0], oneDay));
            for (int i = 1; i < xs1v1.Length; i++) {
                if (ohlc1v1.Last().DateTime.Date != dateList1v1[i].Date) {
                    ohlc1v1.Add(new OHLC(rateList1v1[i], rateList1v1[i], rateList1v1[i], rateList1v1[i], dateList1v1[i], oneDay));
                } else {
                    if (ohlc1v1.Last().High < rateList1v1[i]) {
                        ohlc1v1.Last().High = rateList1v1[i];
                    }

                    if (rateList1v1[i] < ohlc1v1.Last().Low) {
                        ohlc1v1.Last().Low = rateList1v1[i];
                    }

                    ohlc1v1.Last().Close = rateList1v1[i];
                }
            }

            ohlcTeam.Add(new OHLC(rateListTeam[0], rateListTeam[0], rateListTeam[0], rateListTeam[0], dateListTeam[0], oneDay));
            for (int i = 1; i < xsTeam.Length; i++) {
                if (ohlcTeam.Last().DateTime.Date != dateListTeam[i].Date) {
                    ohlcTeam.Add(new OHLC(rateListTeam[i], rateListTeam[i], rateListTeam[i], rateListTeam[i], dateListTeam[i], oneDay));
                } else {
                    if (ohlcTeam.Last().High < rateListTeam[i]) {
                        ohlcTeam.Last().High = rateListTeam[i];
                    }

                    if (rateListTeam[i] < ohlcTeam.Last().Low) {
                        ohlcTeam.Last().Low = rateListTeam[i];
                    }

                    ohlcTeam.Last().Close = rateListTeam[i];
                }
            }

            formsPlotRate.Plot.AddScatterLines(xs1v1, rateList1v1.ToArray(), lineStyle: LineStyle.Dot);
            formsPlotRate.Plot.AddScatterLines(xsTeam, rateListTeam.ToArray(), lineStyle: LineStyle.Dot);

            var candlePlot1v1 = formsPlotRate.Plot.AddCandlesticks(ohlc1v1.ToArray());
            var candlePlotTeam = formsPlotRate.Plot.AddCandlesticks(ohlcTeam.ToArray());

            var bol1v1 = candlePlot1v1.GetBollingerBands(7);
            var bolTeam = candlePlotTeam.GetBollingerBands(7);

            formsPlotRate.Plot.AddScatterLines(bol1v1.xs, bol1v1.sma, Color.Blue);
            formsPlotRate.Plot.AddScatterLines(bolTeam.xs, bolTeam.sma, Color.Blue);
        }
    }
}
