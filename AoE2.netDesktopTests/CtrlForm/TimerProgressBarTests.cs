namespace AoE2NetDesktop.CtrlForm.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using AoE2NetDesktop.CtrlForm;
    using AoE2NetDesktop.Utility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TimerProgressBarTests
    {
        private readonly ProgressBar progressBar = new();

        [TestMethod]
        public void TimerProgressBarTestNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new TimerProgressBar(null);
            });
        }

        [TestMethod]
        public void StartTestTrue()
        {
            // Arrange

            // Act
            var testClass = new TimerProgressBar(progressBar);
            var actVal = testClass.Start();

            // Assert
            Assert.IsTrue(actVal);
            Assert.IsTrue(testClass.Started);
        }

        [TestMethod]
        public void StartTestFalse()
        {
            // Arrange

            // Act
            var testClass = new TimerProgressBar(progressBar);
            _ = testClass.Start();
            var actVal = testClass.Start();

            // Assert
            Assert.IsFalse(actVal);
            Assert.IsTrue(testClass.Started);
        }

        [TestMethod]
        public void StopTest()
        {
            // Arrange

            // Act
            var testClass = new TimerProgressBar(progressBar);
            _ = testClass.Start();
            testClass.Stop();
            var actVal = testClass.Started;

            // Assert
            Assert.IsFalse(actVal);
        }

        [TestMethod]
        public void RestartTest()
        {
            // Arrange

            // Act
            var testClass = new TimerProgressBar(progressBar);
            testClass.Restart();

            // Assert
            Assert.IsTrue(testClass.Started);
        }

        [TestMethod]
        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.IntentionalSyncTest)]
        public void TimerTickTest()
        {
            // Arrange
            progressBar.Value = 0;
            progressBar.Maximum = 1;
            var form = new Form();

            // Act
            var testClass = new TimerProgressBar(progressBar);
            form.Controls.Add(progressBar);

            // Arrange
            var done = false;

            // Act
            form.Shown += async (sender, e) =>
            {
                // Assert
                testClass.Start();

                await Task.Run(() =>
                {
                    while(testClass.Value < progressBar.Maximum) {
                        Task.Delay(1000).Wait();
                    }
                });

                form.Close();
                done = true;
            };

            form.ShowDialog();

            // Assert
            Assert.IsTrue(testClass.Started);
            Assert.IsTrue(done);
        }
    }
}