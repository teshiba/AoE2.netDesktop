using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.From.Tests;
using System.Windows.Forms;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class FormHistoryTests
    {
        [TestMethod()]
        public void FormHistoryTest()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormHistoryTestOpenAoE2NetProfileToolStripMenuItem_Click()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var toolStripMenuItem = testClass.GetControl<ToolStripMenuItem>("openAoE2NetProfileToolStripMenuItem");
            var listViewMatchedPlayers = testClass.GetControl<ListView>("listViewMatchedPlayers");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlFormHistory");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlMain.SelectedIndex = 2;
                listViewMatchedPlayers.Items[0].Selected = true;
                toolStripMenuItem.PerformClick();
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormHistoryTestContextMenuStripMatchedPlayers_Opening()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var contextMenuStripMatchedPlayers = testClass.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
            var listViewMatchedPlayers = testClass.GetControl<ListView>("listViewMatchedPlayers");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlFormHistory");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlMain.SelectedIndex = 2;
                listViewMatchedPlayers.Items[0].Selected = true;
                contextMenuStripMatchedPlayers.Show(listViewMatchedPlayers, listViewMatchedPlayers.Items[0].Position);
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormHistoryTestContextMenuStripMatchedPlayers_OpeningCancel()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var contextMenuStripMatchedPlayers = testClass.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
            var listViewMatchedPlayers = testClass.GetControl<ListView>("listViewMatchedPlayers");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlFormHistory");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlMain.SelectedIndex = 2;
                listViewMatchedPlayers.Items[0].Selected = true;
                contextMenuStripMatchedPlayers.Show(listViewMatchedPlayers, listViewMatchedPlayers.Location);
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        [DataRow("FormsPlotRate1v1_MouseMove", true)]
        [DataRow("FormsPlotRateTeam_MouseMove", true)]
        [DataRow("FormsPlotRate1v1_MouseMove", false)]
        [DataRow("FormsPlotRateTeam_MouseMove", false)]
        public void FormHistoryTestMouseMoveNull(string method, bool isNull)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var arg = new object[] {
                new ScottPlot.FormsPlot(),
                new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0)
            };

            // Act
            testClass.Shown += async (sender, e) => {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                if (isNull) {
                    testClass.SetField<PlotHighlight>("plotHighlight1v1", null);
                    testClass.SetField<PlotHighlight>("plotHighlightTeam", null);
                }
                testClass.Invoke(method, arg);
                testClass.Close();
            };

            testClass.ShowDialog();
        }
    }
}