namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
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
        private Dictionary<LeaderBoardId, List<ListViewItem>> listViewitems;

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

            formsPlotCountry.Configuration.LockHorizontalAxis = true;
            formsPlotCountry.Configuration.LockVerticalAxis = true;
            formsPlotCountry.Plot.Title("Player's country");
            formsPlotCountry.Plot.YLabel("Country");
            formsPlotCountry.Plot.XLabel("Game count");
            formsPlotCountry.Render();

            formsPlotWinRate.Configuration.LockHorizontalAxis = true;
            formsPlotWinRate.Configuration.LockVerticalAxis = true;
            formsPlotWinRate.Plot.Title("Game and win count");
            formsPlotWinRate.Plot.Layout(top: 40); // for Data source comboBox
            formsPlotWinRate.Plot.YLabel("---");
            formsPlotWinRate.Plot.XLabel("Win / Total Game count");
            formsPlotWinRate.Render();

            comboBoxLeaderboard.Enabled = false;
            comboBoxDataSource.Enabled = false;
            comboBoxLeaderboard.Items.AddRange(CtrlHistory.GetLeaderboardStrings());
            comboBoxDataSource.Items.AddRange(CtrlHistory.GetDataSourceStrings());
        }

        /// <summary>
        /// Gets count of LeaderboardId.
        /// </summary>
        /// <remarks>exclude <see cref="LeaderBoardId.Undefined"/>.</remarks>
        public static int LeaderboardIdCount => Enum.GetNames(typeof(LeaderBoardId)).Length - 1;

        /// <summary>
        /// Gets selected data source of graph.
        /// </summary>
        public LeaderBoardId SelectedLeaderboard => CtrlHistory.GetLeaderboardId(comboBoxLeaderboard.Text);

        /// <summary>
        /// Gets selected data source of graph.
        /// </summary>
        public DataSource SelectedDataSource => CtrlHistory.GetDataSource(comboBoxDataSource.Text);

        /// <inheritdoc/>
        protected override CtrlHistory Controler => (CtrlHistory)base.Controler;

        private async Task UpdateListViewStatistics()
        {
            var leaderboards = await Controler.ReadLeaderBoardAsync();
            if (leaderboards.Count == LeaderboardIdCount) {
                listViewMatchedPlayers.BeginUpdate();
                listViewStatistics.Items.Clear();
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("1v1 RM", leaderboards[LeaderBoardId.OneVOneRandomMap]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("Team RM", leaderboards[LeaderBoardId.TeamRandomMap]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("1v1 DM", leaderboards[LeaderBoardId.OneVOneDeathmatch]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("Team DM", leaderboards[LeaderBoardId.TeamDeathmatch]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("1v1 EW", leaderboards[LeaderBoardId.OneVOneEmpireWars]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("Team EW", leaderboards[LeaderBoardId.TeamEmpireWars]));
                listViewStatistics.Items.Add(CtrlHistory.CreateListViewLeaderboard("Unranked", leaderboards[LeaderBoardId.Unranked]));
                listViewMatchedPlayers.EndUpdate();
            } else {
                Debug.Print("UpdateListViewStatistics ERROR.");
            }
        }

        private void UpdateListViewPlayers()
        {
            listViewMatchedPlayers.BeginUpdate();
            listViewMatchedPlayers.Items.Clear();
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

            listViewMatchedPlayers.EndUpdate();
        }

        private void UpdateGraphs()
        {
            Controler.DataPloter.PlotPlayedPlayerCountry(formsPlotCountry.Plot);
            UpdateWinRateGraph();
            UpdatePlayerRate();
        }

        private void UpdatePlayerRate()
        {
            var dataPloter = Controler.DataPloter;

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

        private void UpdateListViewMatchHistory()
        {
            listViewMatchedPlayers.BeginUpdate();
            listViewMatchHistory.Items.Clear();
            if (SelectedLeaderboard != LeaderBoardId.Undefined) {
                listViewMatchHistory.Items.AddRange(listViewitems[SelectedLeaderboard].ToArray());
            }

            listViewMatchedPlayers.EndUpdate();
        }

        private void UpdateWinRateGraph()
        {
            formsPlotWinRate.Plot.YLabel(comboBoxDataSource.Text);
            Controler.DataPloter.PlotWinRate(SelectedLeaderboard, SelectedDataSource, formsPlotWinRate.Plot);
            formsPlotWinRate.Render();
        }

        private void OpenSelectedPlayerFrofile()
        {
            var selectedItems = listViewMatchedPlayers.SelectedItems;

            if (selectedItems.Count != 0) {
                Controler.OpenProfile(selectedItems[0].Text);
            }
        }

        ///////////////////////////////////////////////////////////////////////
        // event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void FormHistory_ShownAsync(object sender, EventArgs e)
        {
            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                listViewitems = Controler.CreateListViewHistory();
                comboBoxLeaderboard.SelectedIndex = 0;
                comboBoxDataSource.SelectedIndex = 0;
                comboBoxLeaderboard.Enabled = true;
                comboBoxDataSource.Enabled = true;
                UpdateListViewPlayers();
                UpdateGraphs();
                formsPlotWinRate.Render();
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
            OpenSelectedPlayerFrofile();
        }

        private void ComboBoxLeaderboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateListViewMatchHistory();
            UpdateWinRateGraph();
        }

        private void ComboBoxDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateWinRateGraph();
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
