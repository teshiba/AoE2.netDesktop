namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

using AoE2NetDesktopTests.TestData;
using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class FormMainTests
{
    [TestMethod]
    public void OnChangePropertyOpacityTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var expVal = 0.5;
        var inputVal = (decimal)expVal * 100;

        // Act
        TestUtilityExt.SetSettings("MainFormOpacityPercent", inputVal);

        // Assert
        Assert.AreEqual(expVal, testClass.Opacity);
    }

    [TestMethod]
    [DataRow(true, FormBorderStyle.None, FormBorderStyle.None)]
    [DataRow(true, FormBorderStyle.Sizable, FormBorderStyle.None)]
    [DataRow(false, FormBorderStyle.None, FormBorderStyle.Sizable)]
    [DataRow(false, FormBorderStyle.Sizable, FormBorderStyle.Sizable)]
    public void OnChangeIsHideTitleTitleTest(bool isHide, FormBorderStyle currentFormBorderStyle, FormBorderStyle expVal)
    {
        // Arrange
        var testClass = new FormMainPrivate() {
            FormBorderStyle = currentFormBorderStyle,
        };

        // Act
        TestUtilityExt.SetSettings("MainFormIsHideTitle", isHide);

        // Assert
        Assert.AreEqual(expVal, testClass.FormBorderStyle);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void OnChangePropertyIsAlwaysOnTopTest(bool value)
    {
        // Arrange
        var testClass = new FormMainPrivate();

        // Act
        TestUtilityExt.SetSettings("MainFormIsAlwaysOnTop", value);

        // Assert
        Assert.AreEqual(value, testClass.TopMost);
    }

    [TestMethod]
    public void SetChromaKeyTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var expVal = ColorTranslator.FromHtml("#123456");

        // Act
        TestUtilityExt.SetSettings("ChromaKey", "#123456");

        // Assert
        Assert.AreEqual(expVal, testClass.BackColor);
    }

    [TestMethod]
    public void SetChromaKeyTestInvalidColorName()
    {
        // Arrange
        var testClass = new FormMainPrivate();

        // Act
        TestUtilityExt.SetSettings("ChromaKey", "invalidColorName");

        // Assert
        Assert.AreEqual("Control", testClass.BackColor.Name);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void OnChangePropertyIsTransparencyTest(bool value)
    {
        // Arrange
        var testClass = new FormMainPrivate();
        TestUtilityExt.SetSettings("ChromaKey", "#123456");

        Color expVal;
        if(value) {
            expVal = ColorTranslator.FromHtml("#123456");
        } else {
            expVal = default;
        }

        // Act
        TestUtilityExt.SetSettings("MainFormIsTransparency", value);

        // Assert
        Assert.AreEqual(expVal, testClass.TransparencyKey);
    }

    [TestMethod]
    public void OnChangePropertyIsTransparencyTestInvalidChromaKeyString()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        TestUtilityExt.SetSettings("ChromaKey", "invalidColorName");
        Color expVal = default;

        // Act
        TestUtilityExt.SetSettings("MainFormIsTransparency", true);

        // Assert
        Assert.AreEqual(expVal, testClass.TransparencyKey);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void OnChangePropertyDrawHighQualityTest(bool value)
    {
        // Arrange
        _ = new FormMainPrivate();

        // Act
        TestUtilityExt.SetSettings("DrawHighQuality", value);

        // Assert
        Assert.AreEqual(value, DrawEx.DrawHighQuality);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void OnChangeIsAutoReloadLastMatchTest(bool value)
    {
        // Arrange
        var testClass = new FormMainPrivate();

        // Act
        TestUtilityExt.SetSettings("IsAutoReloadLastMatch", value);

        // Assert
        Assert.AreEqual(value, testClass.LastMatchLoader.Enabled);
    }

    [TestMethod]
    public void OnTimerAsyncTestAsyncIsAoE2deActive()
    {
        // Arrange
        CtrlMain.IntervalSec = 1;
        CtrlMain.SystemApi = new SystemApiStub(1);
        var testClass = new FormMainPrivate();
        testClass.labelAoE2DEActive.Text = string.Empty;

        // Act
        testClass.LastMatchLoader.Start();

        while(testClass.LastMatchLoader.Enabled) {
            _ = Task.Delay(500);
        }

        _ = testClass.Awaiter.WaitAsync("OnTimerAsync").ConfigureAwait(false);

        // Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void OnTimerAsyncTestAsyncIsNotAoE2deActive()
    {
        // Arrange
        CtrlMain.IntervalSec = 1;
        CtrlMain.SystemApi = new SystemApiStub(0);
        var testClass = new FormMainPrivate();
        testClass.labelAoE2DEActive.Text = string.Empty;

        // Act
        testClass.LastMatchLoader.Start();

        while(testClass.LastMatchLoader.Enabled) {
            _ = Task.Delay(500);
        }

        _ = testClass.Awaiter.WaitAsync("OnTimerAsync").ConfigureAwait(false);

        // Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void RedrawLastMatchAsyncTestSetLastHistory()
    {
        // Arrange
        Match ret = null;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            testClass.labelGameId.Text = $"GameID : --------";
            ret = await testClass.RedrawLastMatchAsync(TestData.AvailableUserProfileId);
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual("00000002", ret.MatchId);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void RedrawLastMatchAsyncTestSetLastMatch()
    {
        // Arrange
        Match ret = null;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            ret = await testClass.RedrawLastMatchAsync(TestData.AvailableUserProfileIdWithoutHistory);
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual("00000003", ret.MatchId);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void RedrawLastMatchAsyncTestSameGameID()
    {
        // Arrange
        string actMatch = string.Empty;
        var expMatch = string.Empty;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");
            expMatch = CtrlMain.DisplayedMatch.MatchId;
            actMatch = (await testClass.RedrawLastMatchAsync(TestData.AvailableUserProfileId)).MatchId;
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(expMatch, actMatch);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void GameTimerTest()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            testClass.GameTimer.Start();
            await testClass.Awaiter.WaitAsync("OnTimerGame");
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void GameTimerTestDisplayedMatchNull()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            // Act
            CtrlMain.DisplayedMatch = null;
            testClass.GameTimer.Start();
            await testClass.Awaiter.WaitAsync("OnTimerGame");
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void GameTimerTestIsNotHandleCreated()
    {
        // Arrange
        var done = false;
        var testClass = new FormMainPrivate();

        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_Shown");

            // Act
            testClass.GameTimer.Start();
            done = true;
            testClass.Close();
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.AreEqual(DisplayStatus.Closing, testClass.DisplayStatus);
    }
}
