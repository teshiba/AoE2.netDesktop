namespace AoE2NetDesktop.Utility.Tests;

using System;

using AoE2NetDesktop.Utility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LogTests
{
    [TestInitialize]
    public void TestInit()
    {
        // clean up
        Log.Level = LogLevel.Non;
        Log.Clear();
    }

    [TestMethod]
    public void NonTest()
    {
        // Arrange
        var expVal = string.Empty;
        Log.Level = LogLevel.Non;

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = Log.LevelDefault;
    }

    [TestMethod]
    public void ErrorTest()
    {
        // Arrange
        var expVal = "[ERROR] error text." + Environment.NewLine;
        Log.Level = LogLevel.Error;

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = Log.LevelDefault;
    }

    [TestMethod]
    public void DebugTest()
    {
        // Arrange
        var expVal = "[ERROR] error text." + Environment.NewLine
            + "[DEBUG] debug text." + Environment.NewLine;
        Log.Level = LogLevel.Debug;

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = Log.LevelDefault;
    }

    [TestMethod]
    public void InfoTest()
    {
        // Arrange
        var expVal = "[ERROR] error text." + Environment.NewLine
            + "[DEBUG] debug text." + Environment.NewLine
            + "[INFO] info text." + Environment.NewLine;
        Log.Level = LogLevel.Info;

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = Log.LevelDefault;
    }
}
