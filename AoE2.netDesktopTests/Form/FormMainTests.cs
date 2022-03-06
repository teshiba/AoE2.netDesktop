using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;
using System.Drawing;

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
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "ProfileId", 1);
            var expVal = string.Empty;
            var done = false;

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");
                testClass.updateToolStripMenuItem.PerformClick();
                await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");
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
        public void FormMainTestException_UpdateToolStripMenuItem_ClickAsync()
        {
            // Arrange
            var expVal = string.Empty;
            var testClass = new FormMainPrivate();
            TestUtilityExt.SetSettings(testClass, "AoE2NetDesktop", "SelectedIdType", IdType.Steam);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormMain_Load");

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

                testClass.updateToolStripMenuItem.PerformClick();
                await testClass.Awaiter.WaitAsync("UpdateToolStripMenuItem_ClickAsync");

                // Assert
                Assert.IsTrue(testClass.labelErrText.Text.Contains($"invalid player.Color[{null}]"));

                // CleanUp
                testClass.Close();
            };

            testClass.ShowDialog();

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
                testClass.FormMainOnKeyDown(Keys.F5);
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
                testClass.FormMainOnKeyDown(Keys.F4);
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
    }
}