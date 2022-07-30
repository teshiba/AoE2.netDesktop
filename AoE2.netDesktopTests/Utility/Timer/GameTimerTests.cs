namespace AoE2NetDesktop.Utility.Timer.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using AoE2NetDesktop.CtrlForm;
    using AoE2NetDesktop.LibAoE2Net.JsonFormat;
    using AoE2NetDesktop.Tests;
    using AoE2NetDesktop.Utility;
    using AoE2NetDesktop.Utility.Timer;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GameTimerTests
    {
        private bool actDone;

        [TestMethod]
        public void GameTimerTest()
        {
            // Arrange
            var expVal = Act;
            CtrlMain.LastMatch = new Match();

            // Act
            using var testClass = new GameTimer(expVal);
            var actVal = testClass.GetField<Func<bool>>("updateFormControlFunc");

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod]
        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        public void GameTimerTestOnElapsed()
        {
            // Arrange
            var expVal = string.Empty;
            CtrlMain.LastMatch = new Match();
            actDone = false;

            // Act
            using var testClass = new GameTimer(Act);

            var task = Task.Run(() =>
            {
                testClass.Start();
                while(actDone == false) {
                }
            });

            task.Wait();

            // Assert
            Assert.IsTrue(actDone);
        }

        private bool Act()
        {
            actDone = true;
            return true;
        }
    }
}