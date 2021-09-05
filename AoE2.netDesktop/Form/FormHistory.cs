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

    /// <summary>
    /// FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        private PlotHighlight plotHighlightTeam;
        private PlotHighlight plotHighlight1v1;
        private Dictionary<string, PlayerInfo> matchedPlayerList;

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
            formsPlotWinRate1v1EachMap.Configuration.LockHorizontalAxis = true;
            formsPlotWinRate1v1EachMap.Configuration.LockVerticalAxis = true;
            formsPlotCiv1v1.Configuration.LockHorizontalAxis = true;
            formsPlotCiv1v1.Configuration.LockVerticalAxis = true;

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

        private static Diplomacy CheckDiplomacy(Player selectedPlayer, Player player)
        {
            var ret = Diplomacy.Enemy;

            if (selectedPlayer.Color % 2 == player.Color % 2) {
                ret = Diplomacy.Ally;
            }

            return ret;
        }

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

        private void UpdateListViewMatchedPlayers(PlayerMatchHistory matches, int profileId)
        {
            matchedPlayerList = new Dictionary<string, PlayerInfo>();

            foreach (var match in matches) {
                var selectedPlayer = match.GetPlayer(profileId);
                foreach (var player in match.Players) {
                    if (player != selectedPlayer) {
                        if (!matchedPlayerList.ContainsKey(player.Name)) {
                            matchedPlayerList.Add(player.Name, new PlayerInfo());
                        }

                        matchedPlayerList[player.Name].Country = player.Country;
                        matchedPlayerList[player.Name].ProfileId = player.ProfilId;

                        switch (match.LeaderboardId) {
                        case LeaderBoardId.OneVOneRandomMap:
                            matchedPlayerList[player.Name].Rate1v1RM = player.Rating;
                            matchedPlayerList[player.Name].Games1v1++;
                            break;
                        case LeaderBoardId.TeamRandomMap:
                            matchedPlayerList[player.Name].RateTeamRM = player.Rating;
                            matchedPlayerList[player.Name].GamesTeam++;

                            switch (CheckDiplomacy(selectedPlayer, player)) {
                            case Diplomacy.Ally:
                                matchedPlayerList[player.Name].GamesAlly++;
                                break;
                            case Diplomacy.Enemy:
                                matchedPlayerList[player.Name].GamesEnemy++;
                                break;
                            default:
                                break;
                            }

                            break;
                        default:
                            break;
                        }

                        matchedPlayerList[player.Name].LastDate = match.GetOpenedTime();
                    }
                }
            }

            foreach (var player in matchedPlayerList) {
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

        private void UpdateListViewHistory(PlayerMatchHistory matches, int profileId)
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
            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                UpdateListViewHistory(Controler.PlayerMatchHistory, Controler.ProfileId);
                UpdateListView(Controler.PlayerMatchHistory, Controler.ProfileId);
                UpdateListViewMatchedPlayers(Controler.PlayerMatchHistory, Controler.ProfileId);

                var dataPlot = new DataPlot(Controler.PlayerMatchHistory, Controler.ProfileId);
                dataPlot.PlotWinRateEachMap(LeaderBoardId.OneVOneRandomMap, formsPlotWinRate1v1EachMap.Plot);
                dataPlot.PlotWinRateEachMap(LeaderBoardId.TeamRandomMap, formsPlotWinRateTeamEachMap.Plot);
                dataPlot.PlotWinRateEachCivilization(LeaderBoardId.OneVOneRandomMap, formsPlotCiv1v1.Plot);
                dataPlot.PlotWinRateEachCivilization(LeaderBoardId.TeamRandomMap, formsPlotCivTeam.Plot);
                dataPlot.PlotPlayedPlayerCountry(formsPlotCountry.Plot);

                var plotTeam = dataPlot.PlotRate(LeaderBoardId.TeamRandomMap, formsPlotRateTeam.Plot);
                if (plotTeam != null) {
                    plotHighlightTeam = new PlotHighlight(formsPlotRateTeam, plotTeam);
                    plotHighlightTeam.UpdateHighlight();
                }

                var plot1v1 = dataPlot.PlotRate(LeaderBoardId.OneVOneRandomMap, formsPlotRate1v1.Plot);
                if (plot1v1 != null) {
                    plotHighlight1v1 = new PlotHighlight(formsPlotRate1v1, plot1v1);
                    plotHighlight1v1.UpdateHighlight();
                }
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
                var profileId = matchedPlayerList[selectedItems[0].Text].ProfileId;
                if (profileId != null) {
                    AoE2net.OpenAoE2net((int)profileId);
                } else {
                    MessageBox.Show($"{selectedItems[0]} has null profile ID.");
                }
            }
        }

        private void ContextMenuStripMatchedPlayers_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Point point = listViewMatchedPlayers.PointToClient(System.Windows.Forms.Cursor.Position);
            ListViewItem item = listViewMatchedPlayers.HitTest(point).Item;
            if (item?.Bounds.Contains(point) ?? false) {
                openAoE2NetProfileToolStripMenuItem.Visible = true;
            } else {
                e.Cancel = true;
            }
        }
    }
}
