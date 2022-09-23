namespace AoE2NetDesktop.Utility.SysApi.Tests;

using System.Reflection;

using AoE2NetDesktop.Utility.SysApi;

using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SystemApiTests
{
    [TestMethod]
    public void GetActiveProcessTest()
    {
        // Arrange
        var expVal = "Idle";
        var user32api = new User32ApiStub {
            ProcessId = 0,
        };
        var testClass = new SystemApi(user32api);

        // Act
        var actVal = testClass.GetActiveProcess();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetActiveProcessTestWin32Exception()
    {
        // Arrange
        var expVal = string.Empty;
        var user32api = new User32ApiStub {
            ProcessId = -1,
        };
        var testClass = new SystemApi(user32api);

        // Act
        var ret = testClass.GetActiveProcess();

        // Assert
        Assert.AreEqual(expVal, ret);
    }

    [TestMethod]
    public void GetProcessFilePathTest()
    {
        // Arrange
        var expVal = Assembly.GetExecutingAssembly().Location;
        string processName = "testhost";
        var user32api = new User32ApiStub {
            ProcessId = 1,
        };
        var testClass = new SystemApi(user32api);

        // Act
        var ret = testClass.GetProcessFilePath(processName);

        // Assert
        Assert.IsTrue(expVal.Contains(ret));
    }

    [TestMethod]
    public void StartTest()
    {
        // Arrange
        var expValCmd = "cmd";
        var expValArg = "/c start ";

        var user32api = new User32ApiStub {
            ProcessId = -1,
        };
        var testClass = new SystemApi(user32api);

        // Act
        var ret = testClass.Start(string.Empty);

        // Assert
        Assert.AreEqual(expValCmd, ret.StartInfo.FileName);
        Assert.AreEqual(expValArg, ret.StartInfo.Arguments);

        // Cleanup
        ret.Kill();
    }
}
