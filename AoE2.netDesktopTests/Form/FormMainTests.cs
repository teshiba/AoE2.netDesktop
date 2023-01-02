namespace AoE2NetDesktop.Form.Tests;

using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;

using AoE2NetDesktopTests.TestData;
using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public partial class FormMainTests
{
    [TestInitialize]
    public void InitTest()
    {
        TestUtilityExt.SetSettings("SelectedIdType", IdType.Steam);
        TestUtilityExt.SetSettings("ProfileId", 1);
        TestUtilityExt.SetSettings("WindowLocationMain", new Point(0, 0));
        TestUtilityExt.SetSettings("WindowSizeMain", new Size(1330, 350));
        TestUtilityExt.SetSettings("VisibleGameTime", true);
    }

    [TestMethod]
    [TestCategory("GUI")]
    [Ignore]
    public void FormMainTestGUI()
    {
        var testClass = new FormMain(Language.en);
        testClass.ShowDialog();
    }

    [TestMethod]
    [TestCategory("GUI")]
    [Ignore]
    public void FormMainTestGUI1v1()
    {
        var testHttpClient = new TestHttpClient() {
            PlayerMatchHistoryUri = "playerMatchHistoryaoe2de1v1.json",
        };

        AoE2net.ComClient = testHttpClient;
        var testClass = new FormMain(Language.en);
        testClass.ShowDialog();

        testHttpClient.PlayerMatchHistoryUri = null;
    }

#pragma warning disable VSTHRD101 // Avoid unsupported async delegates
    [TestMethod]
    public void FormMainTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            await testClass.Awaiter.WaitAsync("LabelNameP1_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP2_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP3_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP4_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP5_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP6_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP7_Paint");
            await testClass.Awaiter.WaitAsync("LabelNameP8_Paint");

            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormMainTest1v1OddColor()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        testClass.httpClient.PlayerMatchHistoryUri = "playerMatchHistoryaoe2de1v1OddColor.json";
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await WaitPaintAsync(testClass);

            // Assert
            Assert.AreEqual("1", testClass.label1v1ColorP1.Text);
            Assert.AreEqual("2", testClass.label1v1ColorP2.Text);
            Assert.AreEqual("Player2", testClass.labelName1v1P1.Text);
            Assert.AreEqual("Player100", testClass.labelName1v1P2.Text);

            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // Cleanup
        testClass.httpClient.PlayerMatchHistoryUri = null;
    }

    [TestMethod]
    public void FormMainTest1v1EvenColor()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        testClass.httpClient.PlayerMatchHistoryUri = "playerMatchHistoryaoe2de1v1.json";
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await WaitPaintAsync(testClass);

            // Assert
            Assert.AreEqual("1", testClass.label1v1ColorP1.Text);
            Assert.AreEqual("2", testClass.label1v1ColorP2.Text);
            Assert.AreEqual("Player100", testClass.labelName1v1P1.Text);
            Assert.AreEqual("Player2", testClass.labelName1v1P2.Text);

            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // Cleanup
        testClass.httpClient.PlayerMatchHistoryUri = null;
    }

    [TestMethod]
    public void FormMainException_UpdateToolStripMenuItem_ClickAsyncTest()
    {
        // Arrange
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", false);
        var expVal = string.Empty;
        var testClass = new FormMainPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.httpClient.ForceHttpRequestException = true;

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

            // Assert
            Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceHttpRequestException = false;
    }

    [TestMethod]
    public void UpdateToolStripMenuItem_ClickAsyncTestIsAutoReloadLastMatchTrue()
    {
        // Arrange
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", true);
        CtrlMain.IsReloadingByTimer = false;
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");
            while(testClass.LastMatchLoader.Enabled == false) {
                Debug.Print("waiting LastMatchLoader is enabled...");
                await Task.Delay(100);
            }

            // Assert
            Assert.IsTrue(testClass.LastMatchLoader.Enabled);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void UpdateToolStripMenuItem_ClickAsyncTestIsAutoReloadLastMatchFalse()
    {
        // Arrange
        TestUtilityExt.SetSettings("SelectedIdType", IdType.Steam);
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", false);
        CtrlMain.IsReloadingByTimer = false;
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

            // Assert
            Assert.IsFalse(testClass.LastMatchLoader.Enabled);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(DisplayStatus.Clearing)]
    [DataRow(DisplayStatus.Redrawing)]
    [DataRow(DisplayStatus.Closing)]
    [DataRow(DisplayStatus.Cleared)]
    [DataRow(DisplayStatus.RedrawingPrevMatch)]
    public void UpdateToolStripMenuItem_ClickAsyncTestInvalidOperationException(DisplayStatus displayStatus)
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();
        var expVal = $"Invalid displayStatus: {displayStatus}";

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.DisplayStatus = displayStatus;
            testClass.RequestMatchView = 1;

            testClass.updateToolStripMenuItem.PerformClick();

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(expVal, testClass.labelErrText.Text);
    }

    [TestMethod]
    public void UpdateToolStripMenuItem_ClickAsyncTestIsReloadingByTimerTrue()
    {
        // Arrange
        TestUtilityExt.SetSettings("SelectedIdType", IdType.Steam);
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", false);
        CtrlMain.IsReloadingByTimer = true;
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

            // Assert
            Assert.IsFalse(testClass.LastMatchLoader.Enabled);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormMainTestGetInvalidPlayerColor()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();
        testClass.httpClient.PlayerMatchHistoryUri = "playerMatchHistoryaoe2deInvalidPlayerColor.json";

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

            // Assert
            Assert.IsTrue(testClass.labelErrText.Text.Contains($"invalid player.Color[{null}]"));

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // Cleanup
        testClass.httpClient.PlayerMatchHistoryUri = null;
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownF5()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            await testClass.FormMain_KeyDownAsync(Keys.F5);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    [Ignore]
    [DataRow(Keys.Right, Keys.Alt, Keys.Shift, 1, 0)]
    [DataRow(Keys.Right, Keys.Alt, Keys.None, 10, 0)]
    [DataRow(Keys.Left, Keys.Alt, Keys.Shift, -1, 0)]
    [DataRow(Keys.Left, Keys.Alt, Keys.None, -10, 0)]
    [DataRow(Keys.Up, Keys.Alt, Keys.Shift, 0, -1)]
    [DataRow(Keys.Up, Keys.Alt, Keys.None, 0, -10)]
    [DataRow(Keys.Up, Keys.None, Keys.None, 0, 0)]
    [DataRow(Keys.Down, Keys.Alt, Keys.Shift, 0, 1)]
    [DataRow(Keys.Down, Keys.Alt, Keys.None, 0, 10)]
    [DataRow(Keys.Down, Keys.None, Keys.None, 0, 0)]
    public void FormMainTestTabControlMain_KeyDownWindowResize(Keys keys, Keys alt, Keys shift, int width, int height)
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.Size = new Size(
                testClass.MinimumSize.Width + 100,
                testClass.MinimumSize.Height + 100);

            var expSize = testClass.Size;
            expSize.Width += width;
            expSize.Height += height;

            await testClass.FormMain_KeyDownAsync(keys | alt | shift);
            done = true;

            // Assert
            Assert.AreEqual(expSize, testClass.Size);

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownShiftSpace()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();
        var expVal = !TestUtilityExt.GetSettings<bool>("MainFormIsHideTitle");

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            // Act
            await testClass.FormMain_KeyDownAsync(Keys.Space | Keys.Shift);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(expVal, TestUtilityExt.GetSettings<bool>("MainFormIsHideTitle"));
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownAltSpace()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();
        var expVal = TestUtilityExt.GetSettings<bool>("MainFormIsHideTitle");

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            // Act
            await testClass.FormMain_KeyDownAsync(Keys.Space | Keys.Alt);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.IsFalse(TestUtilityExt.GetSettings<bool>("MainFormIsHideTitle"));
    }

    [TestMethod]
    [DataRow(Keys.Right, DisplayStatus.Shown, 0, 0, true, 0)]
    [DataRow(Keys.Right, DisplayStatus.Shown, 1, 0, true, 0)]
    [DataRow(Keys.Right, DisplayStatus.Shown, 0, 0, false, 0)]
    [DataRow(Keys.Right, DisplayStatus.Shown, 1, 0, false, 0)]
    [DataRow(Keys.Right, DisplayStatus.RedrawingPrevMatch, 0, 0, true, 0)]
    [DataRow(Keys.Right, DisplayStatus.RedrawingPrevMatch, 1, 0, true, 0)]
    [DataRow(Keys.Right, DisplayStatus.RedrawingPrevMatch, 0, 0, false, 0)]
    [DataRow(Keys.Right, DisplayStatus.RedrawingPrevMatch, 1, 0, false, 0)]
    [DataRow(Keys.Left, DisplayStatus.Shown, 0, 0, true, 1)]
    [DataRow(Keys.Left, DisplayStatus.Shown, 1, 0, true, 2)]
    [DataRow(Keys.Left, DisplayStatus.Shown, 0, 0, false, 1)]
    [DataRow(Keys.Left, DisplayStatus.Shown, 1, 0, false, 2)]
    [DataRow(Keys.Left, DisplayStatus.RedrawingPrevMatch, 0, 0, true, 1)]
    [DataRow(Keys.Left, DisplayStatus.RedrawingPrevMatch, 1, 0, true, 2)]
    [DataRow(Keys.Left, DisplayStatus.RedrawingPrevMatch, 0, 0, false, 1)]
    [DataRow(Keys.Left, DisplayStatus.RedrawingPrevMatch, 1, 0, false, 2)]
    [DataRow(Keys.Right, DisplayStatus.RedrawingPrevMatch, 3, 4, false, 2)]
    [DataRow(Keys.Left, DisplayStatus.RedrawingPrevMatch, 98, 98, false, 98)]
    [DataRow(Keys.Right, DisplayStatus.RedrawingPrevMatch, 1, 1, false, 0)]
    public void FormMainTestTabControlMain_KeyDownSelectMatch(
        Keys key, DisplayStatus displayStatus, int requestMatchView, int currentMatchView, bool started, int expRequestMatchView)
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.DisplayStatus = displayStatus;
            testClass.RequestMatchView = requestMatchView;
            testClass.CurrentMatchView = currentMatchView;

            if(started) {
                testClass.ProgressBar.Start();
            } else {
                testClass.ProgressBar.Stop();
            }

            // Act
            await testClass.FormMain_KeyDownAsync(key);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(expRequestMatchView, testClass.RequestMatchView);
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownSelectMatchError()
    {
        // Arrange
        TestUtilityExt.SetSettings("SteamId", "00000000000000003");
        TestUtilityExt.SetSettings("SelectedIdType", IdType.Steam);
        var done = false;
        int expRequestMatchView = 0;
        var testClass = new FormMainPrivate();

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.DisplayStatus = DisplayStatus.RedrawingPrevMatch;
            testClass.RequestMatchView = 2;
            testClass.CurrentMatchView = expRequestMatchView;

            // Act
            await testClass.FormMain_KeyDownAsync(Keys.Left);

            // CleanUp
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(expRequestMatchView, testClass.RequestMatchView);
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownSelectMatchException()
    {
        // Arrange
        var done = false;
        int expRequestMatchView = 0;
        var testClass = new FormMainPrivate();

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.DisplayStatus = DisplayStatus.RedrawingPrevMatch;
            testClass.RequestMatchView = 2;
            testClass.httpClient.ForceHttpRequestException = true;

            // Act
            await testClass.FormMain_KeyDownAsync(Keys.Left);

            // CleanUp
            testClass.httpClient.ForceHttpRequestException = false;
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(expRequestMatchView, testClass.RequestMatchView);
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownOtherKey()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.httpClient.ForceHttpRequestException = true;
            await testClass.FormMain_KeyDownAsync(Keys.F4);
            done = true;

            // Assert
            Assert.IsFalse(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceHttpRequestException = false;
    }

    [TestMethod]
    public void FormMainTestControls_MouseDown()
    {
        // Arrange
        var done = false;
        var expPoint = new Point(10, 20);
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.Controls_MouseDown(new MouseEventArgs(MouseButtons.Left, 0, expPoint.X, expPoint.Y, 0));
            done = true;

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.AreEqual(expPoint, testClass.MouseDownPoint);
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormMainTestControls_MouseMove()
    {
        // Arrange
        var expTop = 0;
        var expLeft = 0;
        var done = false;
        var movePoint = new Point(30, 50);
        var orgPoint = new Point(10, 20);
        var testClass = new FormMainPrivate {
            MouseDownPoint = orgPoint,
        };

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            expTop = testClass.Top + (movePoint.Y - orgPoint.Y);
            expLeft = testClass.Left + (movePoint.X - orgPoint.X);
            testClass.Controls_MouseMove(new MouseEventArgs(MouseButtons.Left, 0, movePoint.X, movePoint.Y, 0));
            done = true;

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.AreEqual(expTop, testClass.Top);
        Assert.AreEqual(expLeft, testClass.Left);
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormMainTestFormMain_MouseClick()
    {
        // Arrange
        var done = false;
        var point = new Point(10, 20);
        var testClass = new FormMainPrivate {
            MouseDownPoint = point,
        };

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            Assert.IsFalse(testClass.contextMenuStripMain.Created);
            testClass.FormMain_MouseClick(new MouseEventArgs(MouseButtons.Right, 0, point.X, point.Y, 0));
            Assert.IsTrue(testClass.contextMenuStripMain.Created);
            done = true;

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormMainFormMain_LoadAsyncTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        testClass.httpClient.PlayerMatchHistoryUri = "FileNameDoesNotExist.json";
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // Cleanup
        testClass.httpClient.PlayerMatchHistoryUri = null;
    }

    [TestMethod]
    public void FormMainTestFormMain_LoadAsyncException()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        testClass.httpClient.ForceException = true;
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceException = false;
    }

    [TestMethod]
    public void FormMainTestFormMain_FormClosingWithFormHistoryOpening()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.CtrlSettings.ShowMyHistory();
            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }
#pragma warning restore VSTHRD101 // Avoid unsupported async delegates

    [TestMethod]
    public void SettingsToolStripMenuItem_ClickTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();

        // Act
        testClass.SettingsToolStripMenuItem_Click(testClass, e);

        // Assert
    }

    [TestMethod]
    public void OpenSettingsTestFormSettingsIsNull()
    {
        // Arrange
        var testClass = new FormMainPrivate {
            FormSettings = null,
        };

        // Act
        testClass.OpenSettings();

        // Assert
        Assert.IsTrue(testClass.FormSettings.Visible);
    }

    [TestMethod]
    public void OpenSettingsTestFormSettingsIsDisposed()
    {
        // Arrange
        var testClass = new FormMainPrivate {
            FormSettings = null,
        };

        // Act
        testClass.OpenSettings();
        testClass.FormSettings.Dispose();
        Assert.IsFalse(testClass.FormSettings.Visible);
        testClass.OpenSettings();

        // Assert
        Assert.IsTrue(testClass.FormSettings.Visible);
    }

    [TestMethod]
    public void LabelName_DoubleClickTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();
        var label = new Label() {
            Tag = new Player() {
                Name = "Player1",
                ProfilId = TestData.AvailableUserProfileId,
            },
        };

        // Act
        testClass.LabelName_DoubleClick(label, e);

        // Assert
        // nothing to do.
    }

    [TestMethod]
    public void LabelName_DoubleClickTestPlayerInvalid()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();
        var label = new Label() {
            Tag = new Player() {
                ProfilId = null,
            },
        };

        // Act
        testClass.LabelName_DoubleClick(label, e);

        // Assert
        Assert.IsTrue(testClass.labelErrText.Text.Contains("invalid player"));
    }

    [TestMethod]
    public void LabelName_DoubleClickTestPlayerNull()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();
        var label = new Label() {
            Tag = null,
        };

        // Act
        testClass.LabelName_DoubleClick(label, e);

        // Assert
        // nothing to do.
    }

    [TestMethod]
    public void ShowMyHistoryHToolStripMenuItem_ClickTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();

        // Act
        testClass.ShowMyHistoryHToolStripMenuItem_Click(testClass, e);

        // Assert
        Assert.IsTrue(testClass.CtrlSettings.FormMyHistory.Visible);
    }

    [TestMethod]
    public void ExitToolStripMenuItem_ClickTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();

        // Act
        testClass.Show();
        Assert.IsTrue(testClass.Visible);
        testClass.ExitToolStripMenuItem_Click(testClass, e);

        // Assert
        Assert.IsFalse(testClass.Visible);
    }

    [TestMethod]

    // reloading
    [DataRow(true, null, DisplayStatus.Shown, DisplayStatus.Redrawing)]

    // Skip reloading
    [DataRow(true, null, DisplayStatus.Uninitialized, DisplayStatus.Uninitialized)]
    [DataRow(true, 1L, DisplayStatus.Shown, DisplayStatus.Shown)]
    [DataRow(false, null, DisplayStatus.Shown, DisplayStatus.Shown)]
    public void FormMain_ActivatedTest(
        bool isAutoReload, long? finished, DisplayStatus displayStatus, DisplayStatus expDisplayStatus)
    {
        // Arrange
        CtrlMain.IsReloadingByTimer = false;
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", isAutoReload);
        CtrlMain.DisplayedMatch = new Match() {
            Finished = finished,
        };
        var testClass = new FormMainPrivate {
            DisplayStatus = displayStatus,
        };

        testClass.FormMain_Activated(new EventArgs());

        // Assert
        Assert.AreEqual(expDisplayStatus, testClass.DisplayStatus);
    }

    [TestMethod]
    public void FormMain_ActivatedTestDisplayedMatchNull()
    {
        // Arrange
        CtrlMain.DisplayedMatch = null;

        CtrlMain.IsReloadingByTimer = false;
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", true);
        var testClass = new FormMainPrivate {
            DisplayStatus = DisplayStatus.Uninitialized,
        };

        testClass.FormMain_Activated(new EventArgs());

        // Assert
        Assert.AreEqual(DisplayStatus.Uninitialized, testClass.DisplayStatus);
    }

    [TestMethod]
    public void PictureBoxMap_DoubleClickTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();

        // Act
        testClass.Show();
        testClass.PictureBoxMap_DoubleClick(testClass.pictureBoxMap, e);

        // Assert
        // nothing to do.
    }

    [TestMethod]
    public void PictureBoxMap1v1_DoubleClickTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var e = new EventArgs();

        // Act
        testClass.Show();
        testClass.PictureBoxMap1v1_DoubleClick(testClass.pictureBoxMap, e);

        // Assert
        // nothing to do.
    }

    private static async Task WaitPaintAsync(FormMainPrivate testClass)
    {
        await testClass.Awaiter.WaitAsync("FormMain_Shown");
        await testClass.Awaiter.WaitAsync("LabelRate1v1P2_Paint");
        await testClass.Awaiter.WaitAsync("LabelWins1v1P2_Paint");
        await testClass.Awaiter.WaitAsync("LabelLoses1v1P2_Paint");
        await testClass.Awaiter.WaitAsync("LabelLoses1v1P1_Paint");
        await testClass.Awaiter.WaitAsync("LabelWins1v1P1_Paint");
        await testClass.Awaiter.WaitAsync("LabelRate1v1P1_Paint");
        await testClass.Awaiter.WaitAsync("LabelRate1v1_Paint");
        await testClass.Awaiter.WaitAsync("LabelWins1v1_Paint");
        await testClass.Awaiter.WaitAsync("LabelLoses1v1_Paint");
        await testClass.Awaiter.WaitAsync("LabelName1v1P1_Paint");
        await testClass.Awaiter.WaitAsync("LabelName1v1P2_Paint");
    }
}
