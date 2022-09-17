namespace AoE2NetDesktop.Tests
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using AoE2NetDesktop.Utility;

    using AoE2netDesktopTests.TestUtility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AsyncMethodAwaiterTests
    {
        private const string MethodName = nameof(RunCompleteAsync);
        private AsyncMethodAwaiter testClass;
        private Dictionary<string, ManualResetEvent> state;
        private Task targetTask;

        [TestInitialize]
        public void TestInit()
        {
            testClass = new AsyncMethodAwaiter();
            state = testClass.GetField<Dictionary<string, ManualResetEvent>>("state");
        }

        // ////////////////////////////////////////////////////////////////////
        // Complete
        // ////////////////////////////////////////////////////////////////////
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
        [TestMethod]
        public void CompleteTestAfterInitialize()
        {
            // Arrenge
            // Act
            RunCompleteAsync().Wait();

            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));
        }

        [TestMethod]
        public void CompleteTestAfterCompleteCall()
        {
            // Arrenge
            RunCompleteAsync().Wait();
            RunCompleteAsync().Wait();

            // Act
            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));
        }

        [TestMethod]
        public void CompleteTestAfterWaitCall()
        {
            // Arrenge
            var task = RunWaitAsync();

            // Act
            Task.WaitAll(task, RunCompleteAsync());

            // Assert
            Assert.IsFalse(state.TryGetValue(MethodName, out _));
        }

        // ////////////////////////////////////////////////////////////////////
        // WaitAsync
        // ////////////////////////////////////////////////////////////////////
        [TestMethod]
        public void WaitAsyncTestAfterInitialize()
        {
            // Arrenge
            // Act
            var task = RunWaitAsync();
            Thread.Sleep(100);

            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));

            // CleanUp
            RunCompleteAsync().Wait();
            task.Wait();
        }

        [TestMethod]
        public void WaitTestAfterCompleteCall()
        {
            // Arrenge
            RunCompleteAsync().Wait();

            // Act
            RunWaitAsync().Wait();

            // Assert
            Assert.IsFalse(state.TryGetValue(MethodName, out _));
        }

        [TestMethod]
        public void WaitTestAfterWaitCall()
        {
            // Arrenge
            var taskPrepare = RunWaitAsync();

            // Act
            var task = RunWaitAsync();
            Thread.Sleep(100);

            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));

            // CleanUp
            RunCompleteAsync().Wait();
            taskPrepare.Wait();
            task.Wait();
        }
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        private Task RunCompleteAsync()
        {
            targetTask = Task.Run(() => testClass.Complete());
            return targetTask;
        }

        private Task RunWaitAsync()
        {
            return Task.Run(() => testClass.WaitAsync(MethodName));
        }
    }
}