namespace AoE2NetDesktop.Form
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AoE2NetDesktop.From;
    using LibAoE2net;

    /// <summary>
    /// FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        private PlotHighlight plotHighlightTeam;
        private PlotHighlight plotHighlight1v1;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHistory"/> class.
        /// </summary>
        /// <param name="profileId">user profile ID.</param>
        public FormHistory(int profileId)
            : base(new CtrlHistory(profileId))
        {
            InitializeComponent();
            formsPlotMapRate1v1.Configuration.LockHorizontalAxis = true;
            formsPlotMapRate1v1.Configuration.LockVerticalAxis = true;

            formsPlotMapRateTeam.Configuration.LockHorizontalAxis = true;
            formsPlotMapRateTeam.Configuration.LockVerticalAxis = true;

            formsPlotWinRateTeamEachMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRateTeamEachMap.Configuration.LockVerticalAxis = true;
            formsPlotWinRateTeamEachMap.Plot.Title("1v1 Random Map Count");
            formsPlotWinRateTeamEachMap.Plot.YLabel("Map");
            formsPlotWinRateTeamEachMap.Plot.XLabel("Win / Total Game count");

            formsPlotWinRate1v1EachMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRate1v1EachMap.Configuration.LockVerticalAxis = true;
            formsPlotWinRate1v1EachMap.Plot.Title("1v1 Random Map Count");
            formsPlotWinRate1v1EachMap.Plot.YLabel("Map");
            formsPlotWinRate1v1EachMap.Plot.XLabel("Win / Total Game count");

            formsPlotRate1v1.Plot.Title("1v1 Random Map Rate");
            formsPlotRate1v1.Plot.YLabel("Rate");
            formsPlotRate1v1.Plot.XLabel("Date");

            formsPlotRateTeam.Plot.Title("1v1 Random Map Rate");
            formsPlotRateTeam.Plot.YLabel("Rate");
            formsPlotRateTeam.Plot.XLabel("Date");
        }

        /// <inheritdoc/>
        protected override CtrlHistory Controler { get => (CtrlHistory)base.Controler; }

        private void UpdateListView(PlayerMatchHistory matches, int profileId)
        {
            foreach (var item in matches) {
                var player = item.GetPlayer(profileId);
                var listViewItem = new ListViewItem(item.GetMapName());
                listViewItem.SubItems.Add(CtrlHistory.GetRatingString(player));
                listViewItem.SubItems.Add(CtrlHistory.GetWinMarkerString(player.Won));
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
                    }
                }
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void FormHistory_ShownAsync(object sender, EventArgs e)
        {
            bool ret = await Controler.ReadPlayerMatchHistoryAsync();

            if (ret) {
                UpdateListView(Controler.PlayerMatchHistory, Controler.ProfileId);

                var dataPlot = new DataPlot(Controler.PlayerMatchHistory, Controler.ProfileId);
                dataPlot.PlotWinRateEachMap(LeaderBoardId.OneVOneRandomMap, formsPlotWinRate1v1EachMap.Plot);
                dataPlot.PlotWinRateEachMap(LeaderBoardId.TeamRandomMap, formsPlotWinRateTeamEachMap.Plot);

                var plotTeam = dataPlot.PlotRate(LeaderBoardId.TeamRandomMap, formsPlotRateTeam.Plot);
                var plot1v1 = dataPlot.PlotRate(LeaderBoardId.OneVOneRandomMap, formsPlotRate1v1.Plot);
                plotHighlightTeam = new PlotHighlight(formsPlotRateTeam, plotTeam);
                plotHighlight1v1 = new PlotHighlight(formsPlotRate1v1, plot1v1);
                plotHighlight1v1.UpdateHighlight();
                plotHighlightTeam.UpdateHighlight();
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }

            Awaiter.Complete();
        }

        private void FormsPlotRate1v1_MouseMove(object sender, MouseEventArgs e)
        {
            plotHighlight1v1?.UpdateHighlight();
        }

        private void FormsPlotRateTeam_MouseMove(object sender, MouseEventArgs e)
        {
            plotHighlightTeam?.UpdateHighlight();
        }
    }
}
