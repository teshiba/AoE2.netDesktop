namespace AoE2NetDesktop.Form;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Utility.Forms;

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

    private readonly List<LeaderboardView> leaderboardViews = new() {
        new(IndexRM1v1, "1v1 RM", LeaderboardId.RM1v1, Color.Blue),
        new(IndexRMTeam, "Team RM", LeaderboardId.RMTeam, Color.Indigo),
        new(IndexDM1v1, "1v1 DM", LeaderboardId.DM1v1, Color.DarkGreen),
        new(IndexDMTeam, "Team DM", LeaderboardId.DMTeam, Color.SeaGreen),
        new(IndexEW1v1, "1v1 EW", LeaderboardId.EW1v1, Color.Red),
        new(IndexEWTeam, "Team EW", LeaderboardId.EWTeam, Color.OrangeRed),
        new(IndexUnranked, "Unranked", LeaderboardId.Unranked, Color.SlateGray),
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
        => PlayerRate = new PlayerRateFormsPlot(formsPlotPlayerRate, leaderboardViews, FontSize);

    private void UpdateStatisticsTabGraph()
        => PlayerRate.Plot(Controler.PlayerRatingHistories);

    private async Task UpdateListViewStatisticsAsync()
    {
        listViewStatistics.UseWaitCursor = true;

        var leaderboards = await Controler.ReadLeaderBoardAsync();
        if(leaderboards.Count == LeaderboardIdCount) {
            listViewStatistics.BeginUpdate();
            listViewStatistics.Items.Clear();

            var listviewItems = new ListViewItem[LeaderboardIdCount];
            foreach(var item in leaderboardViews) {
                listviewItems[item.Index] = CtrlHistory.CreateListViewItem(leaderboards[item.LeaderboardId], item);
            }

            listViewStatistics.Items.AddRange(listviewItems);
            listViewStatistics.EndUpdate();
        } else {
            Debug.Print("UpdateListViewStatistics ERROR.");
        }

        listViewStatistics.UseWaitCursor = false;
    }

    private void ListViewStatistics_KeyDown(object sender, KeyEventArgs e)
    {
        if((e.KeyCode == Keys.A) && e.Control) {
            foreach(ListViewItem item in listViewStatistics.Items) {
                item.Selected = true;
            }
        }
    }

    private void ListViewStatistics_ItemChecked(object sender, ItemCheckedEventArgs e)
        => PlayerRate.Plots[(LeaderboardId)e.Item.Tag].IsVisible = e.Item.Checked;

    private void FormsPlotPlayerRate_MouseMove(object sender, MouseEventArgs e)
        => PlayerRate.UpdateHighlight();
}
