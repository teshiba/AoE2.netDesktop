using AoE2NetDesktop.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

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
                testClass.PictureBoxChromaKeyOnClick(e);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();

            // Assert
            Assert.AreEqual(expVal, ColorTranslator.FromHtml(testClass.Controler.ChromaKey));
            Assert.AreEqual(expVal, testClass.pictureBoxChromaKey.BackColor);
            Assert.AreEqual(expVal, ColorTranslator.FromHtml(testClass.textBoxChromaKey.Text));
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
                Assert.AreEqual(expVal, testClass.Controler.Opacity * 100);

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
                Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));
                Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Server Error"));

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestExceptionFormSettings_LoadHTaskCanceledException()
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
                Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced TaskCanceledException"));
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
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", 100);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);

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
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", 101);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);

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
                Assert.IsTrue(testClass.Controler.IsAlwaysOnTop);

                // CleanUp
                testClass.Close();
                done = true;
            };

            testClass.ShowDialog();
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormSettingsTestButtonSetId_ClickAsyncProfileId()
        {
            // Arrange
            var testClass = new FormSettingsPrivate();
            var done = false;

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
    }
}