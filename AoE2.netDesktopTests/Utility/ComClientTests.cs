namespace LibAoE2net.Tests;

using AoE2NetDesktop.Utility;

using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

[TestClass]
public class ComClientTests
{
    [TestMethod]
    public async Task GetStringAsyncTestAsync()
    {
        // Arrange
        var testClass = new ComClient();

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
            testClass.GetStringAsync(string.Empty));
    }

    [TestMethod]
    public async Task GetFromJsonAsyncTestTaskCanceledExceptionAsync()
    {
        // Arrange
        var testClass = new TestHttpClient() {
            ForceTaskCanceledException = true,
        };

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(() =>
            testClass.GetFromJsonAsync<int>("TaskCanceledException"));
    }

    [TestMethod]
    public async Task GetFromJsonAsyncTestHttpRequestExceptionAsync()
    {
        // Arrange
        var testClass = new TestHttpClient() {
            ForceHttpRequestException = true,
        };

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<HttpRequestException>(() =>
            testClass.GetFromJsonAsync<int>("HttpRequestException"));
    }

    [TestMethod]
    public void OpenBrowserTest()
    {
        // Arrange
        var testClass = new TestHttpClient() {
            SystemApi = new SystemApiStub(1),
        };

        // Act
        testClass.OpenBrowser("https://aoe2.net/#api");

        // Assert
    }

    [TestMethod]
    public void OpenBrowserTestException()
    {
        // Arrange
        var testClass = new TestHttpClient() {
            SystemApi = new SystemApiStub(1) {
                ForceException = true,
            },
        };

        // Act
        // Assert
        Assert.ThrowsException<Exception>(() =>
        {
            testClass.OpenBrowser("https://aoe2.net/#api");
        });
    }

    [TestMethod]
    public void OpenBrowserTestWin32Exception()
    {
        // Arrange
        var testClass = new TestHttpClient() {
            SystemApi = new SystemApiStub(1) {
                ForceWin32Exception = true,
            },
        };

        // Act
        // Assert
        Assert.ThrowsException<Win32Exception>(() =>
        {
            testClass.OpenBrowser("https://aoe2.net/#api");
        });
    }
}
