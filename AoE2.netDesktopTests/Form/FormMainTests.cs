namespace AoE2NetDesktop.Form.Tests;

using System;
using System.Drawing;
using System.Windows.Forms;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Tests;

using LibAoE2net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public partial class FormMainTests
{
    [TestMethod]
    [TestCategory("GUI")]
    [Ignore]
    public void FormMainTestGUI()
    {
        AoE2net.ComClient = new TestHttpClient();
        var testClass = new FormMain(Language.en);
        testClass.ShowDialog();
    }

    [TestMethod]
    [TestCategory("GUI")]
    [Ignore]
    public void FormMainTestGUI1v1()
    {
        var testHttpClient = new TestHttpClient() {
            PlayerLastMatchUri = "playerLastMatchaoe2de1v1.json",
        };

        AoE2net.ComClient = testHttpClient;
        var testClass = new FormMain(Language.en);
        testClass.ShowDialog();
    }

#pragma warning disable VSTHRD101 // Avoid unsupported async delegates
    [TestMethod]
    public void FormMainTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();

        TestUtilityExt.SetSettings(testClass, "SelectedIdType", IdType.Steam);
        TestUtilityExt.SetSettings(testClass, "ProfileId", 1);
        TestUtilityExt.SetSettings(testClass, "WindowLocationMain", new Point(0, 0));
        TestUtilityExt.SetSettings(testClass, "WindowSizeMain", new Size(1330, 350));
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
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
    public void FormMainException_UpdateToolStripMenuItem_ClickAsyncTest()
    {
        // Arrange
        var expVal = string.Empty;
        var testClass = new FormMainPrivate();
        TestUtilityExt.SetSettings(testClass, "SelectedIdType", IdType.Steam);

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");

            testClass.httpClient.ForceHttpRequestException = true;

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

            // Assert
            Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();
    }

    [TestMethod]
    public void FormMainTestGetInvalidPlayerColor()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        testClass.httpClient.PlayerLastMatchUri = "playerLastMatchInvalidPlayerColor.json";
        TestUtilityExt.SetSettings(testClass, "SelectedIdType", IdType.Steam);

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");

            testClass.updateToolStripMenuItem.PerformClick();
            await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

            // Assert
            Assert.IsTrue(testClass.labelErrText.Text.Contains($"invalid player.Color[{null}]"));

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();
    }

    [TestMethod]
    public void FormMainTestTabControlMain_KeyDownF5()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
            testClass.httpClient.ForceHttpRequestException = true;
            testClass.FormMain_KeyDown(Keys.F5);
            done = true;

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(Keys.Right, Keys.Alt, Keys.Shift, 1, 0)]
    [DataRow(Keys.Right, Keys.Alt, Keys.None, 10, 0)]
    [DataRow(Keys.Right, Keys.None, Keys.None, 0, 0)]
    [DataRow(Keys.Left, Keys.Alt, Keys.Shift, -1, 0)]
    [DataRow(Keys.Left, Keys.Alt, Keys.None, -10, 0)]
    [DataRow(Keys.Left, Keys.None, Keys.None, 0, 0)]
    [DataRow(Keys.Up, Keys.Alt, Keys.Shift, 0, -1)]
    [DataRow(Keys.Up, Keys.Alt, Keys.None, 0, -10)]
    [DataRow(Keys.Up, Keys.None, Keys.None, 0, 0)]
    [DataRow(Keys.Down, Keys.Alt, Keys.Shift, 0, 1)]
    [DataRow(Keys.Down, Keys.Alt, Keys.None, 0, 10)]
    [DataRow(Keys.Down, Keys.None, Keys.None, 0, 0)]
    public void FormMainTestTabControlMain_KeyDownWindowResize(Keys keys, Keys alt, Keys shift, int width, int height)
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");

            testClass.Size = new Size(
                testClass.MinimumSize.Width + 100,
                testClass.MinimumSize.Height + 100);

            var expSize = testClass.Size;
            expSize.Width += width;
            expSize.Height += height;

            testClass.FormMain_KeyDown(keys | alt | shift);
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
    public void FormMainTestTabControlMain_KeyDownOtherKey()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
            testClass.httpClient.ForceHttpRequestException = true;
            testClass.FormMain_KeyDown(Keys.F4);
            done = true;

            // Assert
            Assert.IsFalse(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));

            // CleanUp
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
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
        testClass.httpClient.PlayerLastMatchUri = "FileNameDoesNotExist.json";
        var expVal = string.Empty;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");

            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");

            testClass.Close();

            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
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
}
