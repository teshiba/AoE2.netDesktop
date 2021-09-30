using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.Form.Tests
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
            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormMainTest()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);
            var buttonUpdate = testClass.GetControl<Button>("buttonUpdate");
            var buttonSetId = testClass.GetControl<Button>("buttonSetId");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");
                await testClass.Awaiter.WaitAsync("LabelNameP1_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP2_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP3_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP4_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP5_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP6_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP7_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP8_Paint");
                tabControlMain.SelectedIndex = 0;
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

                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }


        [TestMethod()]
        public void FormMainTestRadioButtonProfileIDSelected()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);
            var radioButtonSteamID = testClass.GetControl<RadioButton>("radioButtonSteamID");
            var radioButtonProfileID = testClass.GetControl<RadioButton>("radioButtonProfileID");

            // Act
            testClass.Shown += (sender, e) =>
            {

                // Assert
                Assert.IsFalse(radioButtonSteamID.Checked);
                Assert.IsTrue(radioButtonProfileID.Checked);

                testClass.Close();
            };

            testClass.ShowDialog();

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
            var buttonSetId = testClass.GetControl<Button>("buttonSetId");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            var textBoxSettingSteamId = testClass.GetControl<TextBox>("textBoxSettingSteamId");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");
                httpClient.ForceHttpRequestException = true;

                tabControlMain.SelectedIndex = 0;
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
            var labelSettingsName = testClass.GetControl<Label>("labelSettingsName");
            var labelSettingsCountry = testClass.GetControl<Label>("labelSettingsCountry");
            var Controler = testClass.GetProperty<CtrlMain>("Controler");
            var InvalidSteamIdString = Controler.GetField<string>("InvalidSteamIdString");
            var buttonSetId = testClass.GetControl<Button>("buttonSetId");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", "0");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

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
            var labelErrText = testClass.GetControl<Label>("labelErrText");
            var buttonUpdate = testClass.GetControl<Button>("buttonUpdate");
            var buttonSetId = testClass.GetControl<Button>("buttonSetId");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                tabControlMain.SelectedIndex = 0;
                buttonUpdate.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonUpdate_Click");

                // Assert
                Assert.IsTrue(labelErrText.Text.Contains($"invalid player.Color[{null}]"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestButtonSetId_ClickAsyncProfileId()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            var buttonSetId = testClass.GetControl<Button>("buttonSetId");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            var labelSettingsName = testClass.GetControl<Label>("labelSettingsName");
            var labelSettingsCountry = testClass.GetControl<Label>("labelSettingsCountry");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual("   Name: Player1", labelSettingsName.Text);
                Assert.AreEqual("Country: JP", labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestButtonViewHistory_Click()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            var buttonViewHistory = testClass.GetControl<Button>("buttonViewHistory");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                buttonViewHistory.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonViewHistory_Click");
                done = true;

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormMainTestCheckBoxHideTitle_CheckedChanged()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormMain(Language.en);
            var checkBoxHideTitle = testClass.GetControl<CheckBox>("checkBoxHideTitle");
            var tabControlMain = testClass.GetControl<TabControl>("tabControlMain");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                tabControlMain.SelectedIndex = 1;
                checkBoxHideTitle.Checked = true;
                checkBoxHideTitle.Checked = false;
                done = true;

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }
    }
}