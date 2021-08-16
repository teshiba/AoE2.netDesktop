namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }
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
            formsPlotRate.Plot.AddScatter(xs1v1, rateList1v1.ToArray());
            formsPlotRate.Plot.AddScatter(xsTeam, rateListTeam.ToArray());
        }
    }
}
