using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System;
using System.Threading.Tasks;
using System.Reflection;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.User32;

namespace AoE2NetDesktop.Form.Tests
{
    public partial class FormMainTests
    {
        [TestMethod()]
        public void OnChangePropertyTestException()
        {
            // Arrange
            var testClass = new FormMainPrivate();
            var propertySettings = new PropertySettings();
            var e = new PropertyChangedEventArgs("");

            // Assert
            var ex = Assert.ThrowsException<TargetInvocationException>(() =>
            {
                // Act
                testClass.OnChangeProperty(propertySettings, e);
            });

            Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.InnerException.GetType());
        }

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
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
            if (value) {
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

        [TestMethod()]
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

        [TestMethod()]
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

        [TestMethod()]
        public void OnTimerAsyncTestAsyncIsAoE2deActive()
        {
            // Arrange
            CtrlMain.IntervalSec = 1;
            CtrlMain.SystemApi = new SystemApiStub(1);
            var testClass = new FormMainPrivate();
            testClass.labelAoE2DEActive.Text = string.Empty;

            // Act
            testClass.LastMatchLoader.Start();

            while (testClass.LastMatchLoader.Enabled) {
                Task.Delay(500);
            }
            testClass.Awaiter.WaitAsync("OnTimerAsync").ConfigureAwait(false);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void OnTimerAsyncTestAsyncIsNotAoE2deActive()
        {
            // Arrange
            CtrlMain.IntervalSec = 1;
            CtrlMain.SystemApi = new SystemApiStub(0);
            var testClass = new FormMainPrivate();
            testClass.labelAoE2DEActive.Text = string.Empty;

            // Act
            testClass.LastMatchLoader.Start();

            while (testClass.LastMatchLoader.Enabled) {
                Task.Delay(500);
            }
            testClass.Awaiter.WaitAsync("OnTimerAsync").ConfigureAwait(false);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void SetLastMatchDataAsyncTestSetLastHistory()
        {
            // Arrange
            var testClass = new FormMainPrivate();
            Match ret = null;
            
            // Act
            Task.Run( async () => {
                ret = await testClass.SetLastMatchDataAsync(TestData.AvailableUserProfileId).ConfigureAwait(false);
            }).Wait();

            // Assert
            Assert.AreEqual("00000002", ret.MatchId);
        }

        [TestMethod()]
        public void SetLastMatchDataAsyncTestSetLastMatch()
        {
            // Arrange
            var testClass = new FormMainPrivate();
            Match ret = null;

            // Act
            Task.Run(async () => {
                ret = await testClass.SetLastMatchDataAsync(TestData.AvailableUserProfileIdWithoutHistory).ConfigureAwait(false);
            }).Wait();

            // Assert
            Assert.AreEqual("00000003", ret.MatchId);
        }

        [TestMethod()]
        public void SetLastMatchDataAsyncTestSameGameID()
        {
            // Arrange
            var testClass = new FormMainPrivate();
            testClass.labelGameId.Text = $"GameID: 00000002";

            Match ret = null;

            // Act
            Task.Run(async () => {
                ret = await testClass.SetLastMatchDataAsync(TestData.AvailableUserProfileId).ConfigureAwait(false);
            }).Wait();

            // Assert
            Assert.AreEqual("00000002", ret.MatchId);
        }
    }
}