namespace AoE2NetDesktop.Tests;

using AoE2NetDesktop.Utility.User32;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

[TestClass]
public class User32ApiTests
{
    [TestMethod]
    public void GetForegroundWindowTest()
    {
        // Arrange
        var expNotVal = "Idle";
        var testClass = new User32Api();

        // Act
        var actVal = testClass.GetForegroundWindow();
        _ = GetWindowThreadProcessId(actVal, out int processid);
        var processName = Process.GetProcessById(processid).ProcessName;

        // Assert
        Assert.AreNotEqual(expNotVal, processName);
    }

    [TestMethod]
    public void GetWindowThreadProcessIdTest()
    {
        // Arrange
        var expNotVal = "Idle";
        var testClass = new User32Api();
        var foregroundWindow = testClass.GetForegroundWindow();

        // Act
        var threadId = testClass.GetWindowThreadProcessId(foregroundWindow, out int lpdwProcessId);
        var processName = Process.GetProcessById(lpdwProcessId).ProcessName;

        // Assert
        Assert.AreNotEqual(0, threadId);
        Assert.AreNotEqual(expNotVal, processName);
    }

    [DllImport("user32.dll")]
    private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
}
