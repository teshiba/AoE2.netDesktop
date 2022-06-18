namespace AoE2NetDesktop.Form.Tests;

using AoE2netDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

[TestClass]
public class User32ApiStubTests
{
    [TestMethod]
    public void GetForegroundWindowTest()
    {
        // Arrange
        IntPtr expVal = default;

        // Act
        var testClass = new User32ApiStub();
        var actVal = testClass.GetForegroundWindow();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetWindowThreadProcessIdTest()
    {
        // Arrange
        uint expThreadVal = 67890;
        int expProcessId = 12345;

        // Act
        var testClass = new User32ApiStub {
            ThreadId = expThreadVal,
            ProcessId = expProcessId,
        };

        var actThreadVal = testClass.GetWindowThreadProcessId(default, out int actProcessId);

        // Assert
        Assert.AreEqual(expThreadVal, actThreadVal);
        Assert.AreEqual(expProcessId, actProcessId);
    }
}
