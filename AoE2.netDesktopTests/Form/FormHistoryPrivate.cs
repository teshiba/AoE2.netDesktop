using LibAoE2net;
using AoE2NetDesktop.Tests;
using System.Windows.Forms;

namespace AoE2NetDesktop.Form.Tests
{
    public partial class FormHistoryTests
    {
        private class FormHistoryPrivate : FormHistory
        {
            public ContextMenuStrip contextMenuStripMatchedPlayers;
            public ListView listViewStatistics;
            public ListView listViewMatchedPlayers;
            public SplitContainer splitContainerPlayers;
            public ToolStripMenuItem openAoE2NetProfileToolStripMenuItem;
            public TabControl tabControlHistory;
            public ScottPlot.FormsPlot formsPlotPlayerRate;

            public FormHistoryPrivate()
                : base(TestData.AvailableUserProfileId)
            {
                AoE2net.ComClient = new TestHttpClient();
                contextMenuStripMatchedPlayers = this.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
                listViewStatistics = this.GetControl<ListView>("listViewStatistics");
                listViewMatchedPlayers = this.GetControl<ListView>("listViewMatchedPlayers");
                splitContainerPlayers = this.GetControl<SplitContainer>("splitContainerPlayers");
                tabControlHistory = this.GetControl<TabControl>("tabControlHistory");
                openAoE2NetProfileToolStripMenuItem = this.GetControl<ToolStripMenuItem>("openAoE2NetProfileToolStripMenuItem");
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
        }
    }
}