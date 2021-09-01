using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using System.Windows.Forms;
using System.Reflection;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.From.Tests
{
    [TestClass()]
    public class FormMainTests
    {
        [TestMethod()]
        [TestCategory("GUI")]
        [Ignore]
        public void FormMainTestGUI()
        {
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            //testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormMainTest()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            var settings = Assembly.GetAssembly(testClass.GetType()).GetType("AoE2NetDesktop.Settings");
            var settingsDefault = settings.GetProperty("Default").GetValue(settings);
            settingsDefault.GetType().GetProperty("SteamId").SetValue(settingsDefault, TestInit.AvailableUserSteamId);
            var buttonUpdate = testClass.GetControl<Button>("buttonUpdate");
            var textBoxSettingSteamId = testClass.GetControl<TextBox>("textBoxSettingSteamId");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("StartVerify");
                textBoxSettingSteamId.Text = TestInit.AvailableUserSteamId;
                await testClass.Awaiter.WaitAsync("LabelNameP1_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP2_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP3_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP4_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP5_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP6_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP7_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP8_Paint");
                buttonUpdate.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonUpdate_Click");
                await testClass.Awaiter.WaitAsync("LabelNameP1_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP2_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP3_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP4_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP5_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP6_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP7_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP8_Paint");

                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
        }

        [TestMethod()]
        public void FormMainTestExceptionFormMain_Load()
        {
            // Arrange
            var expVal = string.Empty;
            var httpClient = new TestHttpClient() {
                ForceHttpRequestException = true,
            };
            AoE2net.ComClient = httpClient;
            var testClass = new FormMain(Language.en);
            var labelErrText = testClass.GetControl<Label>("labelErrText");
            var buttonUpdate = testClass.GetControl<Button>("buttonUpdate");
            var textBoxSettingSteamId = testClass.GetControl<TextBox>("textBoxSettingSteamId");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");

                // Assert
                Assert.IsTrue(labelErrText.Text.Contains("Forced HttpRequestException"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormMainTestExceptionButtonUpdate_Click()
        {
            // Arrange
            var expVal = string.Empty;
            var httpClient = new TestHttpClient();
            AoE2net.ComClient = httpClient;
            var testClass = new FormMain(Language.en);
            var labelErrText = testClass.GetControl<Label>("labelErrText");
            var buttonUpdate = testClass.GetControl<Button>("buttonUpdate");
            var textBoxSettingSteamId = testClass.GetControl<TextBox>("textBoxSettingSteamId");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("StartVerify");
                textBoxSettingSteamId.Text = TestInit.AvailableUserSteamId;
                httpClient.ForceHttpRequestException = true;
                buttonUpdate.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonUpdate_Click");

                // Assert
                Assert.IsTrue(labelErrText.Text.Contains("Forced HttpRequestException"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormMainTestSetInvalidSteamId()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);

            var settings = Assembly.GetAssembly(testClass.GetType()).GetType("AoE2NetDesktop.Settings");
            var settingsDefault = settings.GetProperty("Default").GetValue(settings);
            settingsDefault.GetType().GetProperty("SteamId").SetValue(settingsDefault, "0");

            var labelSettingsName = testClass.GetControl<Label>("labelSettingsName");
            var labelSettingsCountry = testClass.GetControl<Label>("labelSettingsCountry");
            var Controler = testClass.GetProperty<CtrlMain>("Controler");
            var InvalidSteamIdString = Controler.GetField<string>("InvalidSteamIdString");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("StartVerify");

                // Assert
                Assert.AreEqual($"   Name: {InvalidSteamIdString}", labelSettingsName.Text);
                Assert.AreEqual($"Country: {InvalidSteamIdString}", labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestCheckBoxAlwaysOnTop_CheckedChanged()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            var CheckBoxAlwaysOnTop = testClass.GetControl<CheckBox>("checkBoxAlwaysOnTop");

            // Act
            testClass.Shown += (sender, e) =>
            {
                CheckBoxAlwaysOnTop.Checked = true;

                // Assert
                Assert.IsTrue(testClass.TopMost);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestGetInvalidPlayerColor()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient() {
                PlayerLastMatchUri = "playerLastMatchInvalidPlayerColor.json",
            };

            var testClass = new FormMain(Language.en);
            var settings = Assembly.GetAssembly(testClass.GetType()).GetType("AoE2NetDesktop.Settings");
            var settingsDefault = settings.GetProperty("Default").GetValue(settings);
            settingsDefault.GetType().GetProperty("SteamId").SetValue(settingsDefault, TestInit.AvailableUserSteamId);
            var labelErrText = testClass.GetControl<Label>("labelErrText");
            var buttonUpdate = testClass.GetControl<Button>("buttonUpdate");

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("StartVerify");
                buttonUpdate.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonUpdate_Click");

                // Assert
                Assert.AreEqual($"invalid player.Color[{null}]", labelErrText.Text);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

    }
}