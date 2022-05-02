﻿using AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public partial class FormSettingsTests
    {
        [TestMethod()]
        public void FormSettingsTestPictureBoxChromaKey_Click()
        {
            // Arrange
            var expVal = Color.FromArgb(255, 255, 0, 0);
            var done = false;
            var testClass = new FormSettingsPrivate {
            };

            testClass.ColorDialog = new ColorDialogEx {
                Color = expVal,
                Opening = () => false,
            };

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.PictureBoxChromaKey_Click(e);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.AreEqual(expVal, ColorTranslator.FromHtml(testClass.Controler.PropertySetting.ChromaKey));
            Assert.AreEqual(expVal, testClass.pictureBoxChromaKey.BackColor);
            Assert.AreEqual(expVal, ColorTranslator.FromHtml(testClass.textBoxChromaKey.Text));
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestCheckBoxDrawQuality_CheckedChanged()
        {
            // Arrange
            var done = false;
            var testClass = new FormSettingsPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.checkBoxDrawQuality.Checked = true;

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(testClass.Controler.PropertySetting.DrawHighQuality);
            Assert.IsTrue(TestUtilityExt.GetSettings<bool>(testClass, "DrawHighQuality"));
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestCheckBoxAutoReloadLastMatch_CheckedChanged()
        {
            // Arrange
            var done = false;
            var testClass = new FormSettingsPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.checkBoxAutoReloadLastMatch.Checked = true;

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(testClass.Controler.PropertySetting.IsAutoReloadLastMatch);
            Assert.IsTrue(TestUtilityExt.GetSettings<bool>(testClass, "IsAutoReloadLastMatch"));
            Assert.IsTrue(done);
        }

        [TestMethod()]
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
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
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

        [TestMethod()]
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
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.httpClient.ForceHttpRequestException = true;
                testClass.upDownOpacity.Value = expVal;
                // Assert
                Assert.AreEqual(expVal, testClass.Controler.PropertySetting.Opacity * 100);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        [DataRow(9)]
        [DataRow(101)]
        public void FormSettingsTestUpDownOpacity_ValueChangedOutOfRange(int expVal)
        {
            // Arrange
            var done = false;
            var testClass = new FormSettingsPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
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
        }

        [TestMethod()]
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

        [TestMethod()]
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
                await testClass.Awaiter.WaitAsync("FormSettings_Load");

                // Assert
                Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Server Error"));

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestExceptionFormSettings_LoadNetStatusTimeout()
        {
            // Arrange
            var expVal = string.Empty;
            var done = false;
            var testClass = new FormSettingsPrivate();
            testClass.httpClient.ForceTaskCanceledException = true;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");

                // Assert
                Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Timeout"));

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestSetInvalidSteamId()
        {
            // Arrange
            var expVal = string.Empty;
            var done = false;
            var testClass = new FormSettingsPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
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

        [TestMethod()]
        public void FormSettingsTestSameLastMatchAndMatchHistory0()
        {
            // Arrange
            var expVal = string.Empty;
            var done = false;
            var testClass = new FormSettingsPrivate();
            TestUtilityExt.SetSettings(testClass, "ProfileId", 100);
            TestUtilityExt.SetSettings(testClass, "SelectedIdType", IdType.Profile);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual($"   Name: Player1", testClass.labelSettingsName.Text);
                Assert.AreEqual($"Country: Japan", testClass.labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestNonMatchHistory()
        {
            // Arrange
            var expVal = string.Empty;
            var done = false;
            var testClass = new FormSettingsPrivate();
            TestUtilityExt.SetSettings(testClass, "ProfileId", TestData.AvailableUserProfileIdWithoutHistory);
            TestUtilityExt.SetSettings(testClass, "SelectedIdType", IdType.Profile);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual($"   Name: Player1", testClass.labelSettingsName.Text);
                Assert.AreEqual($"Country: Japan", testClass.labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
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
                Assert.IsTrue(testClass.Controler.PropertySetting.IsAlwaysOnTop);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        [DataRow(IdType.Steam)]
        [DataRow(IdType.Profile)]
        public void FormSettingsTestButtonSetId_ClickAsync(IdType idType)
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var done = false;

            TestUtilityExt.SetSettings(testClass, "SelectedIdType", idType);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual("   Name: Player1", testClass.labelSettingsName.Text);
                Assert.AreEqual("Country: Japan", testClass.labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestButtonSetId_ClickAsyncCatchException()
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var done = false;


            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.httpClient.ForceTaskCanceledException = true;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Timeout"));

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestCheckBoxHideTitle_CheckedChanged()
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
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

        [TestMethod()]
        public void SetChromaKeyTest()
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var expValue = "#123456";

            // Act
            testClass.SetChromaKey(expValue);

            // Assert
            Assert.AreEqual(expValue, testClass.Controler.PropertySetting.ChromaKey);
            Assert.AreEqual(expValue, testClass.textBoxChromaKey.Text);
            Assert.AreEqual(ColorTranslator.FromHtml(expValue), testClass.pictureBoxChromaKey.BackColor);
            Assert.AreEqual(expValue, TestUtilityExt.GetSettings<string>(testClass, "ChromaKey"));
        }

        [TestMethod()]
        public void SetChromaKeyTestException()
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var expValue = "#000000";

            // Act
            testClass.SetChromaKey("invalidColorName");

            // Assert
            Assert.AreEqual(expValue, testClass.Controler.PropertySetting.ChromaKey);
            Assert.AreEqual(expValue, testClass.textBoxChromaKey.Text);
            Assert.AreEqual(ColorTranslator.FromHtml(expValue), testClass.pictureBoxChromaKey.BackColor);
            Assert.AreEqual(expValue, TestUtilityExt.GetSettings<string>(testClass, "ChromaKey"));
        }

        [TestMethod()]
        [DataRow(IdType.Steam, TestData.AvailableUserSteamId)]
        [DataRow(IdType.Profile, TestData.AvailableUserProfileIdString)]
        [DataRow(IdType.Profile, TestData.AvailableUserProfileIdWithoutSteamIdString)]
        public void ReloadProfileAsyncTest(IdType idtype, string idText)
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.ReloadProfileAsync(idtype, idText);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void ReloadProfileAsyncTestIdTypeNotSelected()
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormSettings_Load");
                testClass.ReloadProfileAsync(IdType.NotSelected, TestData.AvailableUserProfileIdWithoutSteamIdString);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        private static IEnumerable<object[]> OnErrorHandlerTestData => new List<object[]>
        {
            new object[] { new HttpRequestException("404"), "Invalid ID" , Color.Red },
            new object[] { new HttpRequestException(""), "Server Error", Color.Olive },
            new object[] { new TaskCanceledException(), "Timeout", Color.Purple },
        };

        [TestMethod()]
        [DynamicData(nameof(OnErrorHandlerTestData))]
        public void OnErrorHandlerTest(Exception ex, string expValueText, Color expValueForeColor)
        {
            // Arrange
            var testClass = new FormSettingsPrivate();

            // Act
            testClass.OnErrorHandler(ex);

            // Assert
            Assert.AreEqual(expValueText, testClass.labelAoE2NetStatus.Text);
            Assert.AreEqual(expValueForeColor, testClass.labelAoE2NetStatus.ForeColor);
        }
    }
}