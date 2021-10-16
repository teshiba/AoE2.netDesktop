﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Windows.Forms;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public partial class FormHistoryTests
    {
        [TestMethod()]
        public void FormHistoryTest()
        {
            // Arrange
            var expVal = string.Empty;

            var testClass = new FormHistoryPrivate();
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
            var testClass = new FormHistoryPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.tabControlHistory.SelectedIndex = 1;
                testClass.listViewMatchedPlayers.Items[0].Selected = true;
                testClass.openAoE2NetProfileToolStripMenuItem.PerformClick();
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
            var testClass = new FormHistoryPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.tabControlHistory.SelectedIndex = 1;
                testClass.listViewMatchedPlayers.Items[0].Selected = true;
                testClass.contextMenuStripMatchedPlayers.Show(testClass.listViewMatchedPlayers, testClass.listViewMatchedPlayers.Items[0].Position);
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
            var testClass = new FormHistoryPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.tabControlHistory.SelectedIndex = 1;
                testClass.listViewMatchedPlayers.Items[0].Selected = true;
                testClass.contextMenuStripMatchedPlayers.Show(testClass.listViewMatchedPlayers, testClass.listViewMatchedPlayers.Location);
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
            var testClass = new FormHistoryPrivate();
            var arg = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
            var done = false;

            // Act
            testClass.Shown += async (sender, e) => {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.FormsPlotPlayerRateOnMouseMove(arg);
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
            var testClass = new FormHistoryPrivate();
            var arg = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
            var done = false;
            testClass.splitContainerPlayers.Orientation = orientation;

            // Act
            testClass.Shown += async (sender, e) => {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.SplitContainerPlayersOnDoubleClick(arg);
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
            var testClass = new FormHistoryPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.tabControlHistory.SelectedIndex = 2;
                testClass.listViewStatistics.Items[0].Checked = true;
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        [DataRow(Keys.A | Keys.Control, 7)]
        [DataRow(Keys.A, 0)]
        [DataRow(Keys.B, 0)]
        public void FormHistoryTestListViewStatistics_KeyDown(Keys keys, int expVal)
        {
            // Arrange
            var testClass = new FormHistoryPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.ListViewStatisticsOnKeyDown(new KeyEventArgs(keys));
                Assert.AreEqual(expVal, testClass.listViewStatistics.SelectedItems.Count);
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }
    }
}