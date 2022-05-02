using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class LastMatchLoaderTests
    {
        [TestMethod()]
        public void StartTestAsync()
        {
            // Arrange
            var expVal = string.Empty;
            var cancellationToken = new CancellationTokenSource();
            var cancelToken = cancellationToken.Token;
            int intervalSec = 1;

            void action(object sender, ElapsedEventArgs ev)
            {
                cancellationToken.Cancel();
            }

            var testClass = new LastMatchLoader(action, intervalSec);

            // Act
            testClass.Start();

            while(!cancellationToken.IsCancellationRequested) {
                Task.Delay(500).Wait();
            }

            testClass.Stop();

            // Assert
            Assert.IsTrue(cancelToken.IsCancellationRequested);
        }

        [TestMethod()]
        public void StopTest()
        {
            // Arrange
            var expVal = string.Empty;
            var cancellationToken = new CancellationTokenSource();
            var cancelToken = cancellationToken.Token;
            int intervalSec = 1;

            void action(object sender, ElapsedEventArgs ev)
            {
                cancellationToken.Cancel();
            }

            var testClass = new LastMatchLoader(action, intervalSec);

            // Act
            testClass.Start();
            testClass.Stop();
            Task.Delay(2000).Wait();

            // Assert
            Assert.IsFalse(cancelToken.IsCancellationRequested);
        }
    }
}