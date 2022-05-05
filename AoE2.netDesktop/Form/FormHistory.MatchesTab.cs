namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using LibAoE2net;

    /// <summary>
    /// Matches Tab of FormHistory class.
    /// </summary>
    public partial class FormHistory : ControllableForm
    {
        private Dictionary<LeaderboardId, List<ListViewItem>> listViewHistory;

        /// <summary>
        /// Gets or sets Win rate stat plot object.
        /// </summary>
        public WinRatePlot WinRateStat { get; set; }

        /// <summary>
        /// Gets selected data source of graph.
        /// </summary>
        public DataSource SelectedDataSource => CtrlHistory.GetDataSource(comboBoxDataSource.Text);

        /// <summary>
        /// Gets selected data source of graph.
        /// </summary>
        public LeaderboardId SelectedLeaderboard => CtrlHistory.GetLeaderboardId(comboBoxLeaderboard.Text);

        private void InitMatchesTab()
        {
            comboBoxLeaderboard.Enabled = false;
            comboBoxLeaderboard.Items.AddRange(CtrlHistory.GetLeaderboardStrings());

            comboBoxDataSource.Enabled = false;
            comboBoxDataSource.Items.AddRange(CtrlHistory.GetDataSourceStrings());

            SetPlotStyleFormsPlotWinRate();
            InitListViewMatchHistorySorter();

            WinRateStat = new WinRatePlot(formsPlotWinRate);
        }

        private void SetPlotStyleFormsPlotWinRate()
        {
            formsPlotWinRate.Plot.YAxis.TickLabelStyle(fontSize: FontSize);
            formsPlotWinRate.Plot.XAxis.TickLabelStyle(fontSize: FontSize);
            formsPlotWinRate.Plot.XAxis.LabelStyle(fontSize: FontSize + 3);
            formsPlotWinRate.Plot.YAxis.LabelStyle(fontSize: FontSize + 3);
        }

        private void InitListViewMatchHistorySorter()
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
            listViewMatchHistory.ListViewItemSorter = sorterMatchHistory;
        }

        private void UpdateListViewMatchHistory()
        {
            listViewMatchHistory.BeginUpdate();
            listViewMatchHistory.Items.Clear();
            if (SelectedLeaderboard != LeaderboardId.Undefined) {
                listViewMatchHistory.Items.AddRange(listViewHistory[SelectedLeaderboard].ToArray());
            }

            listViewMatchHistory.EndUpdate();
        }

        private void UpdateMatchesTabView()
        {
            listViewHistory = Controler.CreateListViewHistory();

            comboBoxLeaderboard.SelectedIndex = Settings.Default.SelectedIndexComboBoxLeaderboard;
            comboBoxLeaderboard.Enabled = true;
            comboBoxDataSource.SelectedIndex = Settings.Default.SelectedIndexComboBoxDataSource;
            comboBoxDataSource.Enabled = true;

            UpdateMatchesTabGraph();
        }

        private void UpdateMatchesTabGraph()
        {
            formsPlotWinRate.Plot.YLabel(comboBoxDataSource.Text);
            WinRateStat.Plot(Controler.PlayerMatchHistory, Controler.ProfileId, SelectedLeaderboard, SelectedDataSource);
            formsPlotWinRate.Render();
        }

        ///////////////////////////////////////////////////////////////////////
        // event handlers
        ///////////////////////////////////////////////////////////////////////

        private void ListViewMatchHistory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            SortByColumn((ListView)sender, e);
        }

        private void ComboBoxLeaderboard_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedIndexComboBoxLeaderboard = comboBoxLeaderboard.SelectedIndex;
            UpdateListViewMatchHistory();
            UpdateMatchesTabGraph();
        }

        private void ComboBoxDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedIndexComboBoxDataSource = comboBoxDataSource.SelectedIndex;
            UpdateMatchesTabGraph();
        }
    }
}
