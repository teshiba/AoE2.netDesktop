using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class ComClientTests
    {
        [TestMethod()]
        public void GetStringAsyncTest()
        {
            // Arrange
            var expVal = string.Empty;

            // Act
            // Assert
            Assert.ThrowsException<AggregateException>(() =>
                {
                    var testClass = new ComClient();
                    var actVal = testClass.GetStringAsync("http://0.0.0.0").Result;
                });
        }

        [TestMethod()]
        public void GetFromJsonAsyncTestTaskCanceledException()
        {
            // Arrange
            var ComClient = new TestHttpClient();

            // Act
            // Assert
            Assert.ThrowsExceptionAsync<TaskCanceledException>(async () =>
            {
                await ComClient.GetFromJsonAsync<int>("TaskCanceledException");
            });
        }

        [TestMethod()]
        public void GetFromJsonAsyncTestHttpRequestException()
        {
            // Arrange
            var ComClient = new TestHttpClient();

            // Act
            // Assert
            Assert.ThrowsExceptionAsync<HttpRequestException>(async () =>
            {
                await ComClient.GetFromJsonAsync<int>("HttpRequestException");
            });
        }
    }
}