using LibAoE2net;
using AoE2NetDesktop.Tests;
using System.Windows.Forms;
using System;
using AoE2NetDesktop.LibAoE2Net.Functions;

namespace AoE2NetDesktop.Form.Tests
{
    public partial class FormHistoryTests
    {
        private class FormHistoryPrivate : FormHistory
        {
            public ContextMenuStrip contextMenuStripMatchedPlayers;
            public ListView listViewStatistics;
            public ListView listViewMatchedPlayers;
            public ListView listViewMatchHistory;
            public SplitContainer splitContainerPlayers;
            public ToolStripMenuItem openAoE2NetProfileToolStripMenuItem;
            public ToolStripMenuItem openHistoryToolStripMenuItem;
            public TabControl tabControlHistory;
            public ScottPlot.FormsPlot formsPlotPlayerRate;

            public FormHistoryPrivate()
                : base(TestData.AvailableUserProfileId)
            {
                AoE2net.ComClient = new TestHttpClient();
                contextMenuStripMatchedPlayers = this.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
                listViewStatistics = this.GetControl<ListView>("listViewStatistics");
                listViewMatchedPlayers = this.GetControl<ListView>("listViewMatchedPlayers");
                listViewMatchHistory = this.GetControl<ListView>("listViewMatchHistory");
                splitContainerPlayers = this.GetControl<SplitContainer>("splitContainerPlayers");
                tabControlHistory = this.GetControl<TabControl>("tabControlHistory");
                openAoE2NetProfileToolStripMenuItem = this.GetControl<ToolStripMenuItem>("openAoE2NetProfileToolStripMenuItem");
                openHistoryToolStripMenuItem = this.GetControl<ToolStripMenuItem>("openHistoryToolStripMenuItem");
                formsPlotPlayerRate = this.GetControl<ScottPlot.FormsPlot>("formsPlotPlayerRate");
            }

            public void FormsPlotPlayerRateOnMouseMove(MouseEventArgs arg)
            {
                this.Invoke("FormsPlotPlayerRate_MouseMove", formsPlotPlayerRate, arg);
            }

            public void SplitContainerPlayersOnDoubleClick(MouseEventArgs arg)
            {
                this.Invoke("SplitContainerPlayers_DoubleClick", splitContainerPlayers, arg);
            }

            public void ListViewStatisticsOnKeyDown(KeyEventArgs keys)
            {
                this.Invoke("ListViewStatistics_KeyDown", listViewStatistics, keys);
            }

            public void OpenHistoryToolStripMenuItemOnClick(EventArgs e)
            {
                this.Invoke("OpenHistoryToolStripMenuItem_Click", openHistoryToolStripMenuItem, e);
            }

            public void ListViewMatchedPlayersOnMouseDoubleClick(MouseEventArgs e)
            {
                this.Invoke("ListViewMatchedPlayers_MouseDoubleClick", listViewMatchedPlayers, e);
            }

            public void ListViewMatchedPlayersColumnClick(ColumnClickEventArgs e)
            {
                this.Invoke("ListViewMatchedPlayers_ColumnClick", listViewMatchedPlayers, e);
            }

            public void ListViewMatchHistoryOnColumnClick(ColumnClickEventArgs e)
            {
                this.Invoke("ListViewMatchHistory_ColumnClick", listViewMatchHistory, e);
            }
        }
    }
}