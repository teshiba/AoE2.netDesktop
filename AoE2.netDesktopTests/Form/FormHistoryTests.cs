namespace AoE2NetDesktop.Form.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Windows.Forms;

using static System.Windows.Forms.ListView;

[TestClass]
public partial class FormHistoryTests
{
#pragma warning disable VSTHRD101 // Avoid unsupported async delegates
    [TestMethod]
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

    [TestMethod]
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

    [TestMethod]
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

    [TestMethod]
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

    [TestMethod]
    public void FormHistoryTestMouseMoveNull()
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var arg = new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0);
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.FormsPlotPlayerRate_MouseMove(arg);
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
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
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.SplitContainerPlayers_DoubleClick(arg);
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
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

    [TestMethod]
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
            testClass.ListViewStatistics_KeyDown(new KeyEventArgs(keys));
            Assert.AreEqual(expVal, testClass.listViewStatistics.SelectedItems.Count);
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormHistoryTestOpenHistoryToolStripMenuItem_Click()
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
            testClass.listViewMatchedPlayers.Focus();
            testClass.OpenHistoryToolStripMenuItem_Click(new EventArgs());
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormHistoryTestListViewMatchedPlayers_MouseDoubleClick()
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.ListViewMatchedPlayers_MouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 0, 0, 0, 0));
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormHistoryTestListViewMatchedPlayersColumnClick()
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.ListViewMatchedPlayers_ColumnClick(new ColumnClickEventArgs(0));
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormHistoryTestListViewMatchHistory_ColumnClick()
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.ListViewMatchHistory_ColumnClick(new ColumnClickEventArgs(0));
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(true, "player2", 1)]
    [DataRow(false, "player2", 0)]
    [DataRow(true, "Player2", 1)]
    [DataRow(false, "Player2", 1)]
    public void FormHistoryTestTextBoxFindName_TextChanged(bool ignoreCase, string expFindName, int expFindCount)
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;
        ListViewItemCollection actVal = null;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.checkBoxIgnoreCase.Checked = ignoreCase;
            testClass.textBoxFindName.Text = expFindName;
            actVal = testClass.listViewMatchedPlayers.Items;
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
        Assert.AreEqual(expFindCount, actVal.Count);
    }

    [TestMethod]
    public void FormHistoryTestListViewFilterCountory_ItemChecked()
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.listViewFilterCountry.Visible = true;
            testClass.listViewFilterCountry.Items[0].Focused = true;
            testClass.listViewFilterCountry.Items[0].Checked = true;
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void FormHistoryTestCheckBoxFilter_CheckedChanged(bool check)
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.checkBoxSetFilter.Checked = !check;
            testClass.checkBoxSetFilter.Checked = check;
            testClass.Close();
            done = true;

            Assert.AreEqual(check, testClass.listViewFilterCountry.Visible);
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void FormHistoryTestCheckBoxCountryFilter_CheckedChanged(bool check)
    {
        // Arrange
        var testClass = new FormHistoryPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
            testClass.listViewFilterCountry.Items[0].Checked = true;
            testClass.checkBoxEnableCountryFilter.Checked = !check;
            testClass.checkBoxEnableCountryFilter.Checked = check;
            testClass.Close();
            done = true;

            Assert.AreEqual(check, testClass.checkBoxEnableCountryFilter.Checked);
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }
#pragma warning restore VSTHRD101 // Avoid unsupported async delegates
}
