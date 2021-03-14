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
            var testClass = new ComClient {
                BaseAddress = new Uri("https://aoe2.net/")
            };

            // Act
            var response = testClass.GetStringAsync("#api").Result;

            // Assert
            Assert.IsNotNull(response);

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