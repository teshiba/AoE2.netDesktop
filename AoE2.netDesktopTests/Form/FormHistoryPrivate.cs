namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktopTests.TestData;
using AoE2NetDesktopTests.TestUtility;

using System;
using System.Windows.Forms;

public partial class FormHistoryTests
{
    private class FormHistoryPrivate : FormHistory
    {
        public ContextMenuStrip contextMenuStripMatchedPlayers;
        public ListView listViewStatistics;
        public ListView listViewMatchedPlayers;
        public ListView listViewMatchHistory;
        public ListView listViewFilterCountry;
        public TextBox textBoxFindName;
        public CheckBox checkBoxIgnoreCase;
        public CheckBox checkBoxSetFilter;
        public CheckBox checkBoxEnableCountryFilter;
        public SplitContainer splitContainerPlayers;
        public ToolStripMenuItem openAoE2NetProfileToolStripMenuItem;
        public ToolStripMenuItem openHistoryToolStripMenuItem;
        public TabControl tabControlHistory;
        public ScottPlot.FormsPlot formsPlotPlayerRate;

        public FormHistoryPrivate()
            : base(TestData.AvailableUserProfileId)
        {
            contextMenuStripMatchedPlayers = this.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
            listViewStatistics = this.GetControl<ListView>("listViewStatistics");
            listViewMatchedPlayers = this.GetControl<ListView>("listViewMatchedPlayers");
            listViewMatchHistory = this.GetControl<ListView>("listViewMatchHistory");
            listViewFilterCountry = this.GetControl<ListView>("listViewFilterCountry");
            textBoxFindName = this.GetControl<TextBox>("textBoxFindName");
            checkBoxIgnoreCase = this.GetControl<CheckBox>("checkBoxIgnoreCase");
            checkBoxSetFilter = this.GetControl<CheckBox>("checkBoxSetFilter");
            checkBoxEnableCountryFilter = this.GetControl<CheckBox>("checkBoxEnableCountryFilter");
            splitContainerPlayers = this.GetControl<SplitContainer>("splitContainerPlayers");
            tabControlHistory = this.GetControl<TabControl>("tabControlHistory");
            openAoE2NetProfileToolStripMenuItem = this.GetControl<ToolStripMenuItem>("openAoE2NetProfileToolStripMenuItem");
            openHistoryToolStripMenuItem = this.GetControl<ToolStripMenuItem>("openHistoryToolStripMenuItem");
            formsPlotPlayerRate = this.GetControl<ScottPlot.FormsPlot>("formsPlotPlayerRate");
        }

        public void FormsPlotPlayerRate_MouseMove(MouseEventArgs arg)
        {
            this.Invoke("FormsPlotPlayerRate_MouseMove", formsPlotPlayerRate, arg);
        }

        public void SplitContainerPlayers_DoubleClick(MouseEventArgs arg)
        {
            this.Invoke("SplitContainerPlayers_DoubleClick", splitContainerPlayers, arg);
        }

        public void ListViewStatistics_KeyDown(KeyEventArgs keys)
        {
            this.Invoke("ListViewStatistics_KeyDown", listViewStatistics, keys);
        }

        public void OpenHistoryToolStripMenuItem_Click(EventArgs e)
        {
            this.Invoke("OpenHistoryToolStripMenuItem_Click", openHistoryToolStripMenuItem, e);
        }

        public void ListViewMatchedPlayers_MouseDoubleClick(MouseEventArgs e)
        {
            this.Invoke("ListViewMatchedPlayers_MouseDoubleClick", listViewMatchedPlayers, e);
        }

        public void ListViewMatchedPlayers_ColumnClick(ColumnClickEventArgs e)
        {
            this.Invoke("ListViewMatchedPlayers_ColumnClick", listViewMatchedPlayers, e);
        }

        public void ListViewMatchHistory_ColumnClick(ColumnClickEventArgs e)
        {
            this.Invoke("ListViewMatchHistory_ColumnClick", listViewMatchHistory, e);
        }
    }
}
