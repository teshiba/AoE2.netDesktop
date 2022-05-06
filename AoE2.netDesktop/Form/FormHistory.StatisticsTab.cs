namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using LibAoE2net;

    /// <summary>
    /// Statistics Tab of FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        private const int IndexRM1v1 = 0;
        private const int IndexRMTeam = 1;
        private const int IndexEW1v1 = 2;
        private const int IndexEWTeam = 3;
        private const int IndexUnranked = 4;
        private const int IndexDM1v1 = 5;
        private const int IndexDMTeam = 6;

        private const int FontSize = 16;

        private readonly Dictionary<LeaderboardId, Color> leaderboardColorList = new () {
            { LeaderboardId.RM1v1, Color.Blue },
            { LeaderboardId.RMTeam, Color.Indigo },
            { LeaderboardId.DM1v1, Color.DarkGreen },
            { LeaderboardId.DMTeam, Color.SeaGreen },
            { LeaderboardId.EW1v1, Color.Red },
            { LeaderboardId.EWTeam, Color.OrangeRed },
            { LeaderboardId.Unranked, Color.SlateGray },
        };

        /// <summary>
        /// Gets count of LeaderboardId.
        /// </summary>
        /// <remarks>exclude <see cref="LeaderboardId.Undefined"/>.</remarks>
        public static int LeaderboardIdCount => Enum.GetNames(typeof(LeaderboardId)).Length - 1;

        /// <summary>
        /// Gets or sets Rate history plot object.
        /// </summary>
        public PlayerRateFormsPlot PlayerRate { get; set; }

        private void InitStatisticsTab()
        {
            PlayerRate = new PlayerRateFormsPlot(formsPlotPlayerRate, leaderboardColorList, FontSize);
        }

        private void UpdateStatisticsTabGraph()
        {
            PlayerRate.Plot(Controler.PlayerMatchHistory, Controler.ProfileId);
        }

        private async Task UpdateListViewStatisticsAsync()
        {
            listViewStatistics.UseWaitCursor = true;

            var leaderboards = await Controler.ReadLeaderBoardAsync();
            if (leaderboards.Count == LeaderboardIdCount) {
                listViewStatistics.BeginUpdate();
                listViewStatistics.Items.Clear();

                var listviewItems = new ListViewItem[LeaderboardIdCount];
                listviewItems[IndexRM1v1] = CtrlHistory.CreateListViewItem("1v1 RM", LeaderboardId.RM1v1, leaderboards, leaderboardColorList);
                listviewItems[IndexRMTeam] = CtrlHistory.CreateListViewItem("Team RM", LeaderboardId.RMTeam, leaderboards, leaderboardColorList);
                listviewItems[IndexDM1v1] = CtrlHistory.CreateListViewItem("1v1 DM", LeaderboardId.DM1v1, leaderboards, leaderboardColorList);
                listviewItems[IndexDMTeam] = CtrlHistory.CreateListViewItem("Team DM", LeaderboardId.DMTeam, leaderboards, leaderboardColorList);
                listviewItems[IndexEW1v1] = CtrlHistory.CreateListViewItem("1v1 EW", LeaderboardId.EW1v1, leaderboards, leaderboardColorList);
                listviewItems[IndexEWTeam] = CtrlHistory.CreateListViewItem("Team EW", LeaderboardId.EWTeam, leaderboards, leaderboardColorList);
                listviewItems[IndexUnranked] = CtrlHistory.CreateListViewItem("Unranked", LeaderboardId.Unranked, leaderboards, leaderboardColorList);

                listViewStatistics.Items.AddRange(listviewItems);
                listViewStatistics.EndUpdate();
            } else {
                Debug.Print("UpdateListViewStatistics ERROR.");
            }

            listViewStatistics.UseWaitCursor = false;
        }

        private void ListViewStatistics_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.A) && e.Control) {
                foreach (ListViewItem item in listViewStatistics.Items) {
                    item.Selected = true;
                }
            }
        }

        private void ListViewStatistics_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            PlayerRate.Plots[(LeaderboardId)e.Item.Tag].IsVisible = e.Item.Checked;
        }

        private void FormsPlotPlayerRate_MouseMove(object sender, MouseEventArgs e)
        {
            PlayerRate.UpdateHighlight();
        }
    }
}
