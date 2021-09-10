namespace AoE2NetDesktop.Form
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
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

            formsPlotRate1v1.Plot.Title("1v1 Random Map Rate");
            formsPlotRate1v1.Plot.YLabel("Rate");
            formsPlotRate1v1.Plot.XLabel("Date");
            formsPlotRate1v1.Render();

            formsPlotRateTeam.Plot.Title("Team Random Map Rate");
            formsPlotRateTeam.Plot.YLabel("Rate");
            formsPlotRateTeam.Plot.XLabel("Date");
            formsPlotRateTeam.Render();

            formsPlotWinRate1v1Map.Configuration.LockHorizontalAxis = true;
            formsPlotWinRate1v1Map.Configuration.LockVerticalAxis = true;
            formsPlotWinRate1v1Map.Plot.Title("1v1 Random Map Count");
            formsPlotWinRate1v1Map.Plot.YLabel("Map");
            formsPlotWinRate1v1Map.Plot.XLabel("Win / Total Game count");
            formsPlotWinRate1v1Map.Render();

            formsPlotWinRateTeamMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRateTeamMap.Configuration.LockVerticalAxis = true;
            formsPlotWinRateTeamMap.Plot.Title("Team Random Map Count");
            formsPlotWinRateTeamMap.Plot.YLabel("Map");
            formsPlotWinRateTeamMap.Plot.XLabel("Win / Total Game count");
            formsPlotWinRateTeamMap.Render();

            formsPlotCiv1v1.Configuration.LockHorizontalAxis = true;
            formsPlotCiv1v1.Configuration.LockVerticalAxis = true;
            formsPlotCiv1v1.Plot.Title("1v1 Random Map");
            formsPlotCiv1v1.Plot.YLabel("Civilization Name");
            formsPlotCiv1v1.Plot.XLabel("Win / Total Game Count");
            formsPlotCiv1v1.Render();

            formsPlotCivTeam.Configuration.LockHorizontalAxis = true;
            formsPlotCivTeam.Configuration.LockVerticalAxis = true;
            formsPlotCivTeam.Plot.Title("Team Random Map");
            formsPlotCivTeam.Plot.YLabel("Civilization Name");
            formsPlotCivTeam.Plot.XLabel("Win / Total Game Count");
            formsPlotCivTeam.Render();

            formsPlotCountry.Configuration.LockHorizontalAxis = true;
            formsPlotCountry.Configuration.LockVerticalAxis = true;
            formsPlotCountry.Plot.Title("Player's country");
            formsPlotCountry.Plot.YLabel("Country");
            formsPlotCountry.Plot.XLabel("Game count");
            formsPlotCountry.Render();
        }

        /// <inheritdoc/>
        protected override CtrlHistory Controler { get => (CtrlHistory)base.Controler; }

        private async Task UpdateListViewStatistics()
        {
            var leaderboards = await Controler.ReadLeaderBoardAsync();
            if (leaderboards.Count == 4) {
                listViewStatistics.Items.Clear();
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("1v1 RM", leaderboards[LeaderBoardId.OneVOneRandomMap]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("Team RM", leaderboards[LeaderBoardId.TeamRandomMap]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("1v1 DM", leaderboards[LeaderBoardId.OneVOneDeathmatch]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("Team DM", leaderboards[LeaderBoardId.TeamDeathmatch]));
            } else {
                Debug.Print("UpdateListViewStatistics ERROR.");
            }
        }

        private void UpdateListViewMatchedPlayers()
        {
            foreach (var player in Controler.MatchedPlayerInfos) {
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

        private void UpdateListViewHistory()
        {
            var listViewitems = Controler.CreateListViewHistory();
            listViewHistory1v1.Items.AddRange(listViewitems[LeaderBoardId.OneVOneRandomMap]);
            listViewHistoryTeam.Items.AddRange(listViewitems[LeaderBoardId.TeamRandomMap]);
        }

        private void UpdateGraphs()
        {
            var dataPloter = Controler.DataPloter;
            dataPloter.PlotWinRateMap(LeaderBoardId.OneVOneRandomMap, formsPlotWinRate1v1Map.Plot);
            dataPloter.PlotWinRateMap(LeaderBoardId.TeamRandomMap, formsPlotWinRateTeamMap.Plot);
            dataPloter.PlotWinRateCivilization(LeaderBoardId.OneVOneRandomMap, formsPlotCiv1v1.Plot);
            dataPloter.PlotWinRateCivilization(LeaderBoardId.TeamRandomMap, formsPlotCivTeam.Plot);
            dataPloter.PlotPlayedPlayerCountry(formsPlotCountry.Plot);

            var plotTeam = dataPloter.PlotRate(LeaderBoardId.TeamRandomMap, formsPlotRateTeam.Plot);
            if (plotTeam != null) {
                plotHighlightTeam = new PlotHighlight(formsPlotRateTeam, plotTeam);
                plotHighlightTeam.UpdateHighlight();
            }

            var plot1v1 = dataPloter.PlotRate(LeaderBoardId.OneVOneRandomMap, formsPlotRate1v1.Plot);
            if (plot1v1 != null) {
                plotHighlight1v1 = new PlotHighlight(formsPlotRate1v1, plot1v1);
                plotHighlight1v1.UpdateHighlight();
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void FormHistory_ShownAsync(object sender, EventArgs e)
        {
            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                UpdateListViewHistory();
                UpdateListViewMatchedPlayers();
                UpdateGraphs();
                formsPlotWinRate1v1Map.Render();
                formsPlotWinRateTeamMap.Render();
                formsPlotCiv1v1.Render();
                formsPlotCivTeam.Render();
                formsPlotCountry.Render();
                formsPlotRateTeam.Render();
                formsPlotRate1v1.Render();
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }

            await UpdateListViewStatistics();

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

        private void OpenAoE2NetProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedItems = listViewMatchedPlayers.SelectedItems;

            if (selectedItems.Count != 0) {
                Controler.OpenProfile(selectedItems[0].Text);
            }
        }

        private void ContextMenuStripMatchedPlayers_Opening(object sender, CancelEventArgs e)
        {
            var location = new Point(contextMenuStripMatchedPlayers.Left, contextMenuStripMatchedPlayers.Top);
            var point = listViewMatchedPlayers.PointToClient(location);
            var item = listViewMatchedPlayers.HitTest(point).Item;
            if (item?.Bounds.Contains(point) ?? false) {
                openAoE2NetProfileToolStripMenuItem.Visible = true;
            } else {
                e.Cancel = true;
            }
        }
    }
}
