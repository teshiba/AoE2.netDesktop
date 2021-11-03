namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using LibAoE2net;

    /// <summary>
    /// FormHistory class.
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

        private readonly Dictionary<LeaderboardId, Color> leaderboardColor = new () {
            { LeaderboardId.RM1v1, Color.Blue },
            { LeaderboardId.RMTeam, Color.Indigo },
            { LeaderboardId.DM1v1, Color.DarkGreen },
            { LeaderboardId.DMTeam, Color.SeaGreen },
            { LeaderboardId.EW1v1, Color.Red },
            { LeaderboardId.EWTeam, Color.OrangeRed },
            { LeaderboardId.Unranked, Color.SlateGray },
        };

        private Dictionary<LeaderboardId, List<ListViewItem>> listViewHistory;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormHistory"/> class.
        /// </summary>
        /// <param name="profileId">user profile ID.</param>
        public FormHistory(int profileId)
            : base(new CtrlHistory(profileId))
        {
            InitializeComponent();

            formsPlotPlayerRate.Plot.Title("Player Rate");
            formsPlotPlayerRate.Plot.YLabel("Rate");
            formsPlotPlayerRate.Plot.XLabel("Date");
            formsPlotPlayerRate.Render();

            comboBoxLeaderboard.Enabled = false;
            comboBoxLeaderboard.Items.AddRange(CtrlHistory.GetLeaderboardStrings());

            comboBoxDataSource.Enabled = false;
            comboBoxDataSource.Items.AddRange(CtrlHistory.GetDataSourceStrings());

            PlayerCountryStat = new PlayerCountryPlot(formsPlotCountry);
            WinRateStat = new WinRatePlot(formsPlotWinRate);
            PlayerRate = new PlayerRateFormsPlot(formsPlotPlayerRate, leaderboardColor);

            InitListviewSorter();
        }

        /// <summary>
        /// Gets count of LeaderboardId.
        /// </summary>
        /// <remarks>exclude <see cref="LeaderboardId.Undefined"/>.</remarks>
        public static int LeaderboardIdCount => Enum.GetNames(typeof(LeaderboardId)).Length - 1;

        /// <summary>
        /// Gets or sets playerHistory plot object.
        /// </summary>
        public PlayerCountryPlot PlayerCountryStat { get; set; }

        /// <summary>
        /// Gets or sets Rate team history plot object.
        /// </summary>
        public PlayerRateFormsPlot PlayerRate { get; set; }

        /// <summary>
        /// Gets or sets Win rate stat plot object.
        /// </summary>
        public WinRatePlot WinRateStat { get; set; }

        /// <summary>
        /// Gets selected data source of graph.
        /// </summary>
        public LeaderboardId SelectedLeaderboard => CtrlHistory.GetLeaderboardId(comboBoxLeaderboard.Text);

        /// <summary>
        /// Gets selected data source of graph.
        /// </summary>
        public DataSource SelectedDataSource => CtrlHistory.GetDataSource(comboBoxDataSource.Text);

        /// <inheritdoc/>
        protected override CtrlHistory Controler => (CtrlHistory)base.Controler;

        private static void SortByColumn(ListView listView, ColumnClickEventArgs e)
        {
            var listViewItemComparer = (ListViewItemComparer)listView.ListViewItemSorter;
            listViewItemComparer.Column = e.Column;
            listView.Sort();
        }

        private void InitListviewSorter()
        {
            var sorterMatchHistory = new ListViewItemComparer {
                Column = 5,
                ColumnModes = new ComparerMode[]
                {
                    ComparerMode.String,
                    ComparerMode.Integer,
                    ComparerMode.String,
                    ComparerMode.String,
                    ComparerMode.Integer,
                    ComparerMode.DateTime,
                },
            };

            var sorterMatchedPlayers = new ListViewItemComparer {
                Column = 8,
                ColumnModes = new ComparerMode[]
                {
                    ComparerMode.String,
                    ComparerMode.String,
                    ComparerMode.String,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.Integer,
                    ComparerMode.DateTime,
                },
            };

            listViewMatchedPlayers.ListViewItemSorter = sorterMatchedPlayers;
            listViewMatchHistory.ListViewItemSorter = sorterMatchHistory;
        }

        private async Task UpdateListViewStatisticsAsync()
        {
            listViewStatistics.UseWaitCursor = true;

            var leaderboards = await Controler.ReadLeaderBoardAsync();
            if (leaderboards.Count == LeaderboardIdCount) {
                listViewStatistics.BeginUpdate();
                listViewStatistics.Items.Clear();

                ListViewItem[] listviewItems = new ListViewItem[LeaderboardIdCount];
                listviewItems[IndexRM1v1] = CtrlHistory.CreateListViewItem("1v1 RM", LeaderboardId.RM1v1, leaderboards, leaderboardColor);
                listviewItems[IndexRMTeam] = CtrlHistory.CreateListViewItem("Team RM", LeaderboardId.RMTeam, leaderboards, leaderboardColor);
                listviewItems[IndexDM1v1] = CtrlHistory.CreateListViewItem("1v1 DM", LeaderboardId.DM1v1, leaderboards, leaderboardColor);
                listviewItems[IndexDMTeam] = CtrlHistory.CreateListViewItem("Team DM", LeaderboardId.DMTeam, leaderboards, leaderboardColor);
                listviewItems[IndexEW1v1] = CtrlHistory.CreateListViewItem("1v1 EW", LeaderboardId.EW1v1, leaderboards, leaderboardColor);
                listviewItems[IndexEWTeam] = CtrlHistory.CreateListViewItem("Team EW", LeaderboardId.EWTeam, leaderboards, leaderboardColor);
                listviewItems[IndexUnranked] = CtrlHistory.CreateListViewItem("Unranked", LeaderboardId.Unranked, leaderboards, leaderboardColor);

                listViewStatistics.Items.AddRange(listviewItems);
                listViewStatistics.EndUpdate();
            } else {
                Debug.Print("UpdateListViewStatistics ERROR.");
            }

            listViewStatistics.UseWaitCursor = false;
        }

        private void UpdateListViewPlayers()
        {
            var listViewItems = new List<ListViewItem>();

            listViewMatchedPlayers.BeginUpdate();
            listViewMatchedPlayers.Items.Clear();
            foreach (var player in Controler.MatchedPlayerInfos) {
                var listviewItem = new ListViewItem(player.Key);
                listviewItem.SubItems.Add(player.Value.Country);
                listviewItem.SubItems.Add(player.Value.RateRM1v1.ToString());
                listviewItem.SubItems.Add(player.Value.RateRMTeam.ToString());
                listviewItem.SubItems.Add(player.Value.GamesTeam.ToString());
                listviewItem.SubItems.Add(player.Value.GamesAlly.ToString());
                listviewItem.SubItems.Add(player.Value.GamesEnemy.ToString());
                listviewItem.SubItems.Add(player.Value.Games1v1.ToString());
                listviewItem.SubItems.Add(player.Value.LastDate.ToString());
                listViewItems.Add(listviewItem);
            }

            // When calling Add of ListViewItemCollection frequently in foreach etc.,
            // it takes too much time in the ListViewItemSorte, so AddRange is called once instead.
            listViewMatchedPlayers.Items.AddRange(listViewItems.ToArray());

            listViewMatchedPlayers.EndUpdate();
        }

        private void UpdateGraphs()
        {
            PlayerCountryStat.Plot(Controler.PlayerMatchHistory, Controler.ProfileId);
            PlayerRate.Plot(Controler.PlayerMatchHistory, Controler.ProfileId);
            UpdateWinRateGraph();
        }

        private void UpdateWinRateGraph()
        {
            formsPlotWinRate.Plot.YLabel(comboBoxDataSource.Text);
            WinRateStat.Plot(Controler.PlayerMatchHistory, Controler.ProfileId, SelectedLeaderboard, SelectedDataSource);
            formsPlotWinRate.Render();
        }

        private void UpdateListViewMatchHistory()
        {
            listViewMatchedPlayers.BeginUpdate();
            listViewMatchHistory.Items.Clear();
            if (SelectedLeaderboard != LeaderboardId.Undefined) {
                listViewMatchHistory.Items.AddRange(listViewHistory[SelectedLeaderboard].ToArray());
            }

            listViewMatchedPlayers.EndUpdate();
        }

        private void OpenSelectedPlayerProfile()
        {
            var selectedItems = listViewMatchedPlayers.SelectedItems;

            if (selectedItems.Count != 0) {
                Controler.OpenProfile(selectedItems[0].Text);
            }
        }

        private void OpenSelectedPlayerHistory()
        {
            var selectedItems = listViewMatchedPlayers.SelectedItems;

            if (selectedItems.Count != 0) {
                Controler.OpenHistory(selectedItems[0].Text);
            }
        }

        private void SaveWindowPosition()
        {
            Settings.Default.WindowLocationHistory = new Point(Left, Top);
            Settings.Default.WindowSizeHistory = new Size(Width, Height);
        }

        private void RestoreWindowPosition()
        {
            Top = Settings.Default.WindowLocationHistory.Y;
            Left = Settings.Default.WindowLocationHistory.X;
            Width = Settings.Default.WindowSizeHistory.Width;
            Height = Settings.Default.WindowSizeHistory.Height;
        }

        ///////////////////////////////////////////////////////////////////////
        // event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void FormHistory_ShownAsync(object sender, EventArgs e)
        {
            tabControlHistory.SelectedIndex = Settings.Default.SelectedIndexTabControlHistory;

            tabControlHistory.UseWaitCursor = true;

            if (await Controler.ReadPlayerMatchHistoryAsync()) {
                listViewHistory = Controler.CreateListViewHistory();
                comboBoxLeaderboard.SelectedIndex = Settings.Default.SelectedIndexComboBoxLeaderboard;
                comboBoxDataSource.SelectedIndex = Settings.Default.SelectedIndexComboBoxDataSource;
                comboBoxLeaderboard.Enabled = true;
                comboBoxDataSource.Enabled = true;
                UpdateListViewPlayers();
                UpdateGraphs();
                formsPlotWinRate.Render();
                formsPlotCountry.Render();
                formsPlotPlayerRate.Render();
            } else {
                Debug.Print("ReadPlayerMatchHistoryAsync ERROR.");
            }

            tabControlHistory.UseWaitCursor = false;

            await UpdateListViewStatisticsAsync();

            UseWaitCursor = false;
            Awaiter.Complete();
        }

        private void OpenAoE2NetProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedPlayerProfile();
        }

        private void ComboBoxLeaderboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedIndexComboBoxLeaderboard = comboBoxLeaderboard.SelectedIndex;
            UpdateListViewMatchHistory();
            UpdateWinRateGraph();
        }

        private void ComboBoxDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedIndexComboBoxDataSource = comboBoxDataSource.SelectedIndex;
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

        private void FormHistory_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowPosition();
            Settings.Default.Save();
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            RestoreWindowPosition();
        }

        private void TabControlHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedIndexTabControlHistory = tabControlHistory.SelectedIndex;
        }

        private void SplitContainerPlayers_DoubleClick(object sender, EventArgs e)
        {
            switch (splitContainerPlayers.Orientation) {
            case Orientation.Horizontal:
                splitContainerPlayers.Orientation = Orientation.Vertical;
                PlayerCountryStat.Orientation = ScottPlot.Orientation.Horizontal;
                break;
            case Orientation.Vertical:
                splitContainerPlayers.Orientation = Orientation.Horizontal;
                PlayerCountryStat.Orientation = ScottPlot.Orientation.Vertical;
                break;
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

        private void ListViewStatistics_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.A) && e.Control) {
                foreach (ListViewItem item in listViewStatistics.Items) {
                    item.Selected = true;
                }
            }
        }

        private void OpenHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedPlayerHistory();
        }

        private void ListViewMatchedPlayers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenSelectedPlayerHistory();
        }

        private void ListViewMatchedPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortByColumn((ListView)sender, e);
        }

        private void ListViewMatchHistory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortByColumn((ListView)sender, e);
        }
    }
}
