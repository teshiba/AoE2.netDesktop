namespace AoE2NetDesktop.Form;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Utility.Forms;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

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

        InitListViewMatchHistorySorter();

        WinRateStat = new WinRatePlot(formsPlotWinRate, FontSize);
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
        if(SelectedLeaderboard != LeaderboardId.Undefined) {
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
        WinRateStat.Plot(Controler.PlayerMatchHistory, Controler.ProfileId, SelectedLeaderboard, SelectedDataSource);
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
