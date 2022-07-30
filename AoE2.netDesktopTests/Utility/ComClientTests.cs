namespace LibAoE2net.Tests;

using AoE2NetDesktop.Utility;

using AoE2netDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

[TestClass]
public class ComClientTests
{
    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetStringAsyncTest()
    {
        // Arrange
        var testClass = new ComClient {
            BaseAddress = new Uri("https://aoe2.net/"),
        };

        // Act
        var actVal = Task.Run(
            () => testClass.GetStringAsync("#api"))
            .Result;

        // Assert
        Assert.IsNotNull(actVal);
    }

    [TestMethod]
    public async Task GetFromJsonAsyncTestTaskCanceledExceptionAsync()
    {
        // Arrange
        var comClient = new TestHttpClient() {
            ForceTaskCanceledException = true,
        };

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<TaskCanceledException>(() =>
            comClient.GetFromJsonAsync<int>("TaskCanceledException"));
    }

    [TestMethod]
    public async Task GetFromJsonAsyncTestHttpRequestExceptionAsync()
    {
        // Arrange
        var comClient = new TestHttpClient() {
            ForceHttpRequestException = true,
        };

        // Act
        // Assert
        await Assert.ThrowsExceptionAsync<HttpRequestException>(() =>
            comClient.GetFromJsonAsync<int>("HttpRequestException"));
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
