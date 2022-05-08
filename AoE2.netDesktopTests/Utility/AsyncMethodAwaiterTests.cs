using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using AoE2NetDesktop.Utility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoE2NetDesktop.Tests
{
    [TestClass]
    public class AsyncMethodAwaiterTests
    {
        private const string MethodName = nameof(RunComplete);
        private AsyncMethodAwaiter testClass;
        private Dictionary<string, ManualResetEvent> state;
        private Task targetTask;

        private Task RunComplete()
        {
            targetTask = Task.Run(() => testClass.Complete());
            return targetTask;
        }

        private Task RunWait()
        {
            return Task.Run(() => testClass.WaitAsync(MethodName));
        }

        [TestInitialize]
        public void TestInit()
        {
            testClass = new AsyncMethodAwaiter();
            state = testClass.GetField<Dictionary<string, ManualResetEvent>>("state");
        }

        // ////////////////////////////////////////////////////////////////////
        // Complete
        // ////////////////////////////////////////////////////////////////////
        [TestMethod]
        public void CompleteTestAfterInitialize()
        {
            // Arrenge
            // Act
            RunComplete().Wait();

            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));
        }

        [TestMethod]
        public void CompleteTestAfterCompleteCall()
        {
            // Arrenge
            RunComplete().Wait();
            RunComplete().Wait();

            // Act
            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));
        }

        [TestMethod]
        public void CompleteTestAfterWaitCall()
        {
            // Arrenge
            var task = RunWait();

            // Act
            Task.WaitAll(task, RunComplete());

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
            var task = RunWait();
            Thread.Sleep(100);

            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));

            // CleanUp
            RunComplete().Wait();
            task.Wait();
        }

        [TestMethod]
        public void WaitTestAfterCompleteCall()
        {
            // Arrenge
            RunComplete().Wait();

            // Act
            RunWait().Wait();

            // Assert
            Assert.IsFalse(state.TryGetValue(MethodName, out _));
        }

        [TestMethod]
        public void WaitTestAfterWaitCall()
        {
            // Arrenge
            var taskPrepare = RunWait();

            // Act
            var task = RunWait();
            Thread.Sleep(100);
 
            // Assert
            Assert.IsTrue(state.TryGetValue(MethodName, out _));

            // CleanUp
            RunComplete().Wait();
            taskPrepare.Wait();
            task.Wait();
        }
    }
}