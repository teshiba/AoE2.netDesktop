using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;
using System;
using System.Drawing;
using ScottPlot.Plottable;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public partial class FormMainTests
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
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");
                await testClass.Awaiter.WaitAsync("LabelNameP1_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP2_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP3_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP4_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP5_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP6_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP7_Paint");
                await testClass.Awaiter.WaitAsync("LabelNameP8_Paint");
                testClass.tabControlMain.SelectedIndex = 0;
                testClass.buttonUpdate.PerformClick();
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
            var testClass = new FormMainPrivate();

            // Act
            testClass.Shown += (sender, e) =>
            {
                // Assert
                Assert.IsFalse(testClass.radioButtonSteamID.Checked);
                Assert.IsTrue(testClass.radioButtonProfileID.Checked);

                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestExceptionFormMain_LoadHttpRequestException()
        {
            // Arrange
            var expVal = string.Empty;
            var testClass = new FormMainPrivate();
            testClass.httpClient.ForceHttpRequestException = true;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");

                // Assert
                Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));
                Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Server Error"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();
        }

        [TestMethod()]
        public void FormMainTestExceptionFormMain_LoadHTaskCanceledException()
        {
            // Arrange
            var expVal = string.Empty;
            var testClass = new FormMainPrivate();
            testClass.httpClient.ForceTaskCanceledException = true;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");

                // Assert
                Assert.IsTrue(testClass.labelErrText.Text.Contains("One or more errors occurred. (A task was canceled.)"));
                Assert.IsTrue(testClass.labelAoE2NetStatus.Text.Contains("Timeout"));

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
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");

                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");
                testClass.httpClient.ForceHttpRequestException = true;

                testClass.tabControlMain.SelectedIndex = 0;
                testClass.buttonUpdate.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonUpdate_Click");

                // Assert
                Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));

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
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SteamId", "0");
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual($"   Name: {testClass.InvalidSteamIdString}", testClass.labelSettingsName.Text);
                Assert.AreEqual($"Country: N/A", testClass.labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestSameLastMatchAndMatchHistory0()
        {
            // Arrange
            var expVal = string.Empty;
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", 100);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual($"   Name: Player1", testClass.labelSettingsName.Text);
                Assert.AreEqual($"Country: Japan", testClass.labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestNonMatchHistory()
        {
            // Arrange
            var expVal = string.Empty;
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", 101);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual($"   Name: Player1", testClass.labelSettingsName.Text);
                Assert.AreEqual($"Country: Japan", testClass.labelSettingsCountry.Text);

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
            var testClass = new FormMainPrivate();

            // Act
            testClass.Shown += (sender, e) =>
            {
                testClass.checkBoxAlwaysOnTop.Checked = true;

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
            var testClass = new FormMainPrivate();
            testClass.httpClient.PlayerLastMatchUri = "playerLastMatchInvalidPlayerColor.json";
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");

                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                testClass.tabControlMain.SelectedIndex = 0;
                testClass.buttonUpdate.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonUpdate_Click");

                // Assert
                Assert.IsTrue(testClass.labelErrText.Text.Contains($"invalid player.Color[{null}]"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestButtonSetId_ClickAsyncProfileId()
        {
            // Arrange
            var testClass = new FormMainPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonSetId.PerformClick();
                await testClass.Awaiter.WaitAsync("ButtonSetId_ClickAsync");

                // Assert
                Assert.AreEqual("   Name: Player1", testClass.labelSettingsName.Text);
                Assert.AreEqual("Country: Japan", testClass.labelSettingsCountry.Text);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

        }

        [TestMethod()]
        public void FormMainTestButtonViewHistory_Click()
        {
            // Arrange
            var testClass = new FormMainPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.buttonViewHistory.PerformClick();
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
            var testClass = new FormMainPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.tabControlMain.SelectedIndex = 1;
                testClass.checkBoxHideTitle.Checked = true;
                testClass.checkBoxHideTitle.Checked = false;
                done = true;

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormMainTestTabControlMain_KeyDownF5()
        {
            // Arrange
            var testClass = new FormMainPrivate();
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.httpClient.ForceHttpRequestException = true;
                testClass.tabControlMain.SelectedIndex = 0;
                testClass.TabControlMainOnKeyDown(Keys.F5);
                done = true;

                // Assert
                Assert.IsTrue(testClass.labelErrText.Text.Contains("Forced HttpRequestException"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormMainTestTabControlMain_KeyDownOtherKey()
        {
            // Arrange
            var done = false;
            var testClass = new FormMainPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.httpClient.ForceHttpRequestException = true;
                testClass.tabControlMain.SelectedIndex = 0;
                testClass.TabControlMainOnKeyDown(Keys.F4);
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

        [TestMethod()]
        [DataRow(10)]
        [DataRow(100)]
        public void FormMainTestUpDownOpacity_ValueChanged(int expVal)
        {
            // Arrange
            var done = false;
            var testClass = new FormMainPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.httpClient.ForceHttpRequestException = true;
                testClass.upDownOpacity.Value = expVal;
                done = true;

                // Assert
                Assert.AreEqual(expVal, testClass.Opacity * 100);

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        [DataRow(9)]
        [DataRow(101)]
        public void FormMainTestUpDownOpacity_ValueChangedOutOfRange(int expVal)
        {
            // Arrange
            var done = false;
            var testClass = new FormMainPrivate();

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.httpClient.ForceHttpRequestException = true;
                done = true;

                // Assert

                Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                {
                    testClass.upDownOpacity.Value = expVal;
                });

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
        }

        [TestMethod()]
        public void FormMainTestFormMain_MouseDown()
        {
            // Arrange
            var done = false;
            var point = new Point(10, 20);
            var testClass = new FormMainPrivate {
                mouseDownPoint = point,
            };

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.FormMainOnMouseDown(new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0));
                done = true;

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

            // Assert
            Assert.IsTrue(done);
            Assert.AreEqual(testClass.mouseDownPoint, point);
        }

        [TestMethod()]
        public void FormMainTestFormMain_MouseMove()
        {
            // Arrange
            var expTop = 0;
            var expLeft = 0;
            var done = false;
            var point = new Point(10, 20);
            var testClass = new FormMainPrivate {
                mouseDownPoint = point,
            };

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                expTop = testClass.Top + point.Y;
                expLeft = testClass.Left + point.X;
                testClass.FormMainOnMouseMove(new MouseEventArgs(MouseButtons.Left, 0, point.X, point.Y, 0));
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

        [TestMethod()]
        public void FormMainTestPictureBoxChromaKey_Click()
        {
            // Arrange
            var expTop = 0;
            var expLeft = 0;
            var done = false;
            var testClass = new FormMainPrivate {
            };

            testClass.ColorDialog = new ColorDialogEx {
                Color = Color.Red,
                Opening = () => false,
            };

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.PictureBoxChromaKeyOnClick(e);
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
    }
}