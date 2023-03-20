namespace AoE2NetDesktop.Form.Tests;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

using AoE2NetDesktopTests.TestData;
using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public partial class FormSettingsTests
{
    private static IEnumerable<object[]> OnErrorHandlerTestData => new List<object[]>
    {
        new object[] { new HttpRequestException("404"), NetStatus.InvalidRequest },
        new object[] { new HttpRequestException(string.Empty), NetStatus.ServerError },
        new object[] { new TaskCanceledException(), NetStatus.ComTimeout },
    };

#pragma warning disable VSTHRD101 // Avoid unsupported async delegates
    [TestMethod]
    public void FormSettingsTestPictureBoxChromaKey_Click()
    {
        // Arrange
        var expVal = Color.FromArgb(255, 255, 0, 0);
        var done = false;
        var testClass = new FormSettingsPrivate {
            ColorDialog = new ColorDialogEx {
                Color = expVal,
                Opening = () => false,
            },
        };

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.PictureBoxChromaKey_Click(e);

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.AreEqual(expVal, ColorTranslator.FromHtml(SettingsRefs.Get<string>("ChromaKey")));
        Assert.AreEqual(expVal, testClass.pictureBoxChromaKey.BackColor);
        Assert.AreEqual(expVal, ColorTranslator.FromHtml(testClass.textBoxChromaKey.Text));
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestCheckBoxDrawQuality_CheckedChanged()
    {
        // Arrange
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.checkBoxDrawQuality.Checked = true;

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(SettingsRefs.Get<bool>("DrawHighQuality"));
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestCheckBoxAutoReloadLastMatch_CheckedChanged()
    {
        // Arrange
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.checkBoxAutoReloadLastMatch.Checked = true;

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(SettingsRefs.Get<bool>("IsAutoReloadLastMatch"));
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestCheckBoxVisibleGameTime_CheckedChanged()
    {
        // Arrange
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.checkBoxVisibleGameTime.Checked = true;

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(SettingsRefs.Get<bool>("VisibleGameTime"));
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow("123456", "#123456")]
    [DataRow("#123456", "#123456")]
    public void FormSettingsTestTextBoxChromaKey_Leave(string keyValue, string expValTextBox)
    {
        // Arrange
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.textBoxChromaKey.Text = keyValue;
            testClass.TextBoxChromaKey_Leave(new EventArgs());

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.AreEqual(expValTextBox, testClass.textBoxChromaKey.Text);
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(10)]
    [DataRow(100)]
    public void FormSettingsTestUpDownOpacity_ValueChanged(int expVal)
    {
        // Arrange
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.httpClient.ForceHttpRequestException = true;
            testClass.upDownOpacity.Value = expVal;

            // Assert
            Assert.AreEqual(expVal, SettingsRefs.Get<decimal>("MainFormOpacityPercent"));

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceHttpRequestException = false;
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(101)]
    public void FormSettingsTestUpDownOpacity_ValueChangedOutOfRange(int expVal)
    {
        // Arrange
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.httpClient.ForceHttpRequestException = true;

            // Assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                testClass.upDownOpacity.Value = expVal;
            });

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceHttpRequestException = false;
    }

    [TestMethod]
    public void FormSettingsTestExceptionFormMain_LoadHttpRequestException()
    {
        // Arrange
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormSettingsPrivate();
        testClass.httpClient.ForceHttpRequestException = true;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");

            // Assert
            Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Server Error"));

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceHttpRequestException = false;
    }

    [TestMethod]
    public void FormSettingsTestExceptionFormSettings_LoadAsyncNetStatusTimeout()
    {
        // Arrange
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormSettingsPrivate();
        testClass.httpClient.ForceTaskCanceledException = true;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");

            // Assert
            Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Timeout"));

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceTaskCanceledException = false;
    }

    [TestMethod]
    public void FormSettingsTestSetInvalidSteamId()
    {
        // Arrange
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.textBoxSettingSteamId.Text = "0";
            testClass.radioButtonSteamID.Checked = true;
            testClass.buttonSetId.PerformClick();
            await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

            // Assert
            Assert.AreEqual($"   Name: {testClass.InvalidSteamIdString}", testClass.labelSettingsName.Text);
            Assert.AreEqual($"Country: N/A", testClass.labelSettingsCountry.Text);

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestSameLastMatchAndMatchHistory0()
    {
        SettingsRefs.Set("ProfileId", 100);
        SettingsRefs.Set("SelectedIdType", IdType.Profile);
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.buttonSetId.PerformClick();
            await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

            // Assert
            Assert.AreEqual($"   Name: Player100", testClass.labelSettingsName.Text);
            Assert.AreEqual($"Country: Japan", testClass.labelSettingsCountry.Text);

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestNonMatchHistory()
    {
        SettingsRefs.Set("ProfileId", TestData.AvailableUserProfileIdWithoutHistory);
        SettingsRefs.Set("SelectedIdType", IdType.Profile);
        var expVal = string.Empty;
        var done = false;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.buttonSetId.PerformClick();
            await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

            // Assert
            Assert.AreEqual($"   Name: Player100", testClass.labelSettingsName.Text);
            Assert.AreEqual($"Country: Japan", testClass.labelSettingsCountry.Text);

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(IdType.Steam)]
    [DataRow(IdType.Profile)]
    public void FormSettingsTestButtonSetId_Click(IdType idType)
    {
        SettingsRefs.Set("SelectedIdType", idType);
        var testClass = new FormSettingsPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.buttonSetId.PerformClick();
            await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

            // Assert
            Assert.AreEqual("   Name: Player100", testClass.labelSettingsName.Text);
            Assert.AreEqual("Country: Japan", testClass.labelSettingsCountry.Text);

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestButtonSetId_ClickAsyncCatchException()
    {
        // Arrange
        var testClass = new FormSettingsPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.httpClient.ForceException = true;
            testClass.buttonSetId.PerformClick();
            await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

            // Assert
            Assert.IsTrue(testClass.labelErrText.Text.Contains("Force Exception"));

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);

        // CleanUp
        testClass.httpClient.ForceException = false;
    }

    [TestMethod]
    public void FormSettingsTestCheckBoxHideTitle_CheckedChanged()
    {
        // Arrange
        var testClass = new FormSettingsPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            testClass.checkBoxHideTitle.Checked = true;
            testClass.checkBoxHideTitle.Checked = false;

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
    }

    [TestMethod]
    [DataRow(IdType.Steam, TestData.AvailableUserSteamId, "Online")]
    [DataRow(IdType.Profile, TestData.AvailableUserProfileIdString, "Online")]
    [DataRow(IdType.Profile, TestData.AvailableUserProfileIdWithoutSteamIdString, "Online")]
    [DataRow(IdType.Profile, TestData.NotFoundUserProfileIdString, "Server Error")]
    public void ReloadProfileAsyncTest(IdType idtype, string idText, string expNetStatus)
    {
        // Arrange
        var testClass = new FormSettingsPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");
            await testClass.Awaiter.WaitAsync("ReloadProfileAsync");
            testClass.ReloadProfileAsync(idtype, idText);
            await testClass.Awaiter.WaitAsync("ReloadProfileAsync");

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);
        Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains(expNetStatus));
        Assert.IsTrue(testClass.groupBoxPlayer.Enabled);
    }

    [TestMethod]
    public void FormSettings_LoadAsyncTestException()
    {
        // Arrange
        var testClass = new FormSettingsPrivate();
        AoE2net.ComClient.TestHttpClient().ForceException = true;
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            await testClass.Awaiter.WaitAsync("FormSettings_LoadAsync");

            // Assert
            Assert.IsTrue(testClass.labelErrText.Text.Contains("Force Exception"));

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(done);

        // cleanup
        AoE2net.ComClient.TestHttpClient().ForceException = false;
    }

#pragma warning restore VSTHRD101 // Avoid unsupported async delegates

    [TestMethod]
    public void FormSettingsTestRadioButtonProfileIDSelected()
    {
        // Arrange
        var done = false;
        var expVal = string.Empty;
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.Shown += (sender, e) =>
        {
            // Assert
            Assert.IsFalse(testClass.radioButtonSteamID.Checked);
            Assert.IsTrue(testClass.radioButtonProfileID.Checked);

            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void FormSettingsTestCheckBoxAlwaysOnTop_CheckedChanged()
    {
        // Arrange
        var expVal = string.Empty;
        var testClass = new FormSettingsPrivate();
        var done = false;

        // Act
        testClass.Shown += (sender, e) =>
        {
            testClass.checkBoxAlwaysOnTop.Checked = true;

            // Assert
            Assert.IsTrue(SettingsRefs.Get<bool>("MainFormIsAlwaysOnTop"));

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void FormSettingsTestCheckBoxTransparencyWindow_CheckedChanged()
    {
        // Arrange
        var expVal = string.Empty;
        var testClass = new FormSettingsPrivate();
        var done = false;

        // Act
        testClass.Shown += async (sender, e) =>
        {
            testClass.checkBoxTransparencyWindow.Checked = false;
            testClass.checkBoxTransparencyWindow.Checked = true;
            await testClass.Awaiter.WaitAsync("CheckBoxTransparencyWindow_CheckedChanged");

            // CleanUp
            testClass.Close();
            done = true;
        };

        testClass.ShowDialog();

        // Assert
        Assert.IsTrue(SettingsRefs.Get<bool>("MainFormIsTransparency"));
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void SetChromaKeyTest()
    {
        // Arrange
        var testClass = new FormSettingsPrivate();
        var expValue = "#123456";

        // Act
        testClass.SetChromaKey(expValue);

        // Assert
        Assert.AreEqual(expValue, testClass.textBoxChromaKey.Text);
        Assert.AreEqual(ColorTranslator.FromHtml(expValue), testClass.pictureBoxChromaKey.BackColor);
        Assert.AreEqual(expValue, SettingsRefs.Get<string>("ChromaKey"));
    }

    [TestMethod]
    public void SetChromaKeyTestException()
    {
        // Arrange
        var testClass = new FormSettingsPrivate();
        var expValue = "#000000";

        // Act
        testClass.SetChromaKey("invalidColorName");

        // Assert
        Assert.AreEqual(expValue, testClass.textBoxChromaKey.Text);
        Assert.AreEqual(ColorTranslator.FromHtml(expValue), testClass.pictureBoxChromaKey.BackColor);
        Assert.AreEqual(expValue, SettingsRefs.Get<string>("ChromaKey"));
    }

    [TestMethod]
    [DynamicData(nameof(OnErrorHandlerTestData))]
    public void OnErrorHandlerTest(Exception ex, NetStatus netStatus)
    {
        // Arrange
        var testClass = new FormSettingsPrivate();

        // Act
        testClass.OnErrorHandler(this, new ComClientEventArgs(ex));

        // Assert
        Assert.AreEqual(netStatus, testClass.Controler.NetStatus);
    }
}
