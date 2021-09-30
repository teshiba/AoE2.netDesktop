using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using AoE2NetDesktop.Tests;
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
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormHistoryTestOpenAoE2NetProfileToolStripMenuItem_Click()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var toolStripMenuItem = testClass.GetControl<ToolStripMenuItem>("openAoE2NetProfileToolStripMenuItem");
            var listViewMatchedPlayers = testClass.GetControl<ListView>("listViewMatchedPlayers");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlHistory");
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlMain.SelectedIndex = 1;
                listViewMatchedPlayers.Items[0].Selected = true;
                toolStripMenuItem.PerformClick();
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormHistoryTestContextMenuStripMatchedPlayers_Opening()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var contextMenuStripMatchedPlayers = testClass.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
            var listViewMatchedPlayers = testClass.GetControl<ListView>("listViewMatchedPlayers");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlHistory");
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlMain.SelectedIndex = 1;
                listViewMatchedPlayers.Items[0].Selected = true;
                contextMenuStripMatchedPlayers.Show(listViewMatchedPlayers, listViewMatchedPlayers.Items[0].Position);
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormHistoryTestContextMenuStripMatchedPlayers_OpeningCancel()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var contextMenuStripMatchedPlayers = testClass.GetControl<ContextMenuStrip>("contextMenuStripMatchedPlayers");
            var listViewMatchedPlayers = testClass.GetControl<ListView>("listViewMatchedPlayers");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlHistory");
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlMain.SelectedIndex = 1;
                listViewMatchedPlayers.Items[0].Selected = true;
                contextMenuStripMatchedPlayers.Show(listViewMatchedPlayers, listViewMatchedPlayers.Location);
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormHistoryTestMouseMoveNull()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var arg = new object[] {
                new ScottPlot.FormsPlot(),
                new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0)
            };

            var done = false;

            // Act
            testClass.Shown += async (sender, e) => {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.Invoke("FormsPlotPlayerRate_MouseMove", arg);
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        [DataRow(Orientation.Horizontal)]
        [DataRow(Orientation.Vertical)]
        public void FormHistoryTestSplitContainerPlayers_DoubleClick(Orientation orientation)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var arg = new object[] {
                new SplitContainer(),
                new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0)
            };
            var splitContainerPlayers = testClass.GetControl<SplitContainer>("splitContainerPlayers");
            splitContainerPlayers.Orientation = orientation;
            var done = false;

            // Act
            testClass.Shown += async (sender, e) => {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.Invoke("SplitContainerPlayers_DoubleClick", arg);
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormHistoryTestListViewStatistics_ItemChecked()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            var listViewStatistics = testClass.GetControl<ListView>("listViewStatistics");
            var tabControlHistory = testClass.GetControl<TabControl>("tabControlHistory");
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                tabControlHistory.SelectedIndex = 2;
                listViewStatistics.Items[0].Checked = true;
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }
    }
}