namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.Utility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

[TestClass]
public class LastMatchLoaderTests
{
    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    public void StartTest()
    {
        // Arrange
        var expVal = string.Empty;
        var cancellationToken = new CancellationTokenSource();
        var cancelToken = cancellationToken.Token;
        int intervalSec = 1;

        void Action(object sender, ElapsedEventArgs ev)
        {
            cancellationToken.Cancel();
        }

        var testClass = new LastMatchLoader(Action, intervalSec);

        // Act
        testClass.Start();

        while(!cancellationToken.IsCancellationRequested) {
            Task.Delay(500).Wait();
        }

        testClass.Stop();

        // Assert
        Assert.IsTrue(cancelToken.IsCancellationRequested);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    public void StopTest()
    {
        // Arrange
        var expVal = string.Empty;
        var cancellationToken = new CancellationTokenSource();
        var cancelToken = cancellationToken.Token;
        int intervalSec = 1;

        void Action(object sender, ElapsedEventArgs ev)
        {
            cancellationToken.Cancel();
        }

        var testClass = new LastMatchLoader(Action, intervalSec);

        // Act
        testClass.Start();
        testClass.Stop();
        Task.Delay(2000).Wait();

        // Assert
        Assert.IsFalse(cancelToken.IsCancellationRequested);
    }
}
