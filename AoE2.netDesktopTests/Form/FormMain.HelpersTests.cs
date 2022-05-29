namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.User32;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class FormMainTests
{
    [TestMethod]
    public void OnChangePropertyTestException()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var propertySettings = new PropertySettings();
        var e = new PropertyChangedEventArgs(string.Empty);

        // Assert
        var ex = Assert.ThrowsException<TargetInvocationException>(() =>
        {
            // Act
            testClass.OnChangeProperty(propertySettings, e);
        });

        Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.InnerException.GetType());
    }

    [TestMethod]
    public void OnChangePropertyOpacityTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var expVal = 0.5;
        var propertySettings = new PropertySettings() {
            Opacity = expVal,
        };
        var e = new PropertyChangedEventArgs(nameof(propertySettings.Opacity));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

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
        var propertySettings = new PropertySettings() {
            IsHideTitle = isHide,
        };
        var e = new PropertyChangedEventArgs(nameof(propertySettings.IsHideTitle));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

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
        var propertySettings = new PropertySettings() {
            IsAlwaysOnTop = value,
        };
        var e = new PropertyChangedEventArgs(nameof(propertySettings.IsAlwaysOnTop));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

        // Assert
        Assert.AreEqual(value, testClass.TopMost);
    }

    [TestMethod]
    public void SetChromaKeyTest()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var propertySettings = new PropertySettings() {
            ChromaKey = "#123456",
        };
        var expVal = ColorTranslator.FromHtml(propertySettings.ChromaKey);
        var e = new PropertyChangedEventArgs(nameof(propertySettings.ChromaKey));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

        // Assert
        Assert.AreEqual(expVal, testClass.BackColor);
    }

    [TestMethod]
    public void SetChromaKeyTestInvalidColorName()
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var propertySettings = new PropertySettings() {
            ChromaKey = "invalidColorName",
        };
        var e = new PropertyChangedEventArgs(nameof(propertySettings.ChromaKey));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

        // Assert
        Assert.AreEqual("Control", testClass.BackColor.Name);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void OnChangeIsTransparencyTest(bool value)
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var propertySettings = new PropertySettings() {
            IsTransparency = value,
            ChromaKey = "#123456",
        };
        testClass.CtrlSettings.PropertySetting.ChromaKey = propertySettings.ChromaKey;

        Color expVal;
        if(value) {
            expVal = ColorTranslator.FromHtml(propertySettings.ChromaKey);
        } else {
            expVal = default;
        }

        var e = new PropertyChangedEventArgs(nameof(propertySettings.IsTransparency));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

        // Assert
        Assert.AreEqual(expVal, testClass.TransparencyKey);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void OnChangePropertyDrawHighQualityTest(bool value)
    {
        // Arrange
        var testClass = new FormMainPrivate();
        var propertySettings = new PropertySettings() {
            DrawHighQuality = value,
        };
        var e = new PropertyChangedEventArgs(nameof(propertySettings.DrawHighQuality));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

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
        var propertySettings = new PropertySettings() {
            IsAutoReloadLastMatch = value,
        };
        var e = new PropertyChangedEventArgs(nameof(propertySettings.IsAutoReloadLastMatch));

        // Act
        testClass.OnChangeProperty(propertySettings, e);

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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
            testClass.labelGameId.Text = $"GameID: --------";
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
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
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
        Match actMatch = null;
        var done = false;
        var testClass = new FormMainPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormMain_LoadAsync");
            testClass.labelGameId.Text = $"GameID: 00000002";
            actMatch = await testClass.RedrawLastMatchAsync(TestData.AvailableUserProfileId);
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.IsNull(actMatch);
    }
}
