namespace AoE2NetDesktop.Utility.Tests;

using System;

using AoE2NetDesktop.Utility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LogTests
{
    [TestMethod]
    public void NonTest()
    {
        // Arrange
        var expVal = string.Empty;
        var currentLevel = Log.Level;
        Log.Level = LogLevel.Non;
        Log.Clear();

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = currentLevel;
    }

    [TestMethod]
    public void ErrorTest()
    {
        // Arrange
        var expVal = "[ERROR] error text." + Environment.NewLine;
        var currentLevel = Log.Level;
        Log.Level = LogLevel.Error;
        Log.Clear();

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = currentLevel;
    }

    [TestMethod]
    public void DebugTest()
    {
        // Arrange
        var expVal = "[ERROR] error text." + Environment.NewLine
            + "[DEBUG] debug text." + Environment.NewLine;
        var currentLevel = Log.Level;
        Log.Level = LogLevel.Debug;
        Log.Clear();

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = currentLevel;
    }

    [TestMethod]
    public void InfoTest()
    {
        // Arrange
        var expVal = "[ERROR] error text." + Environment.NewLine
            + "[DEBUG] debug text." + Environment.NewLine
            + "[INFO] info text." + Environment.NewLine;
        var currentLevel = Log.Level;
        Log.Level = LogLevel.Info;
        Log.Clear();

        // Act
        Log.Error("error text.");
        Log.Debug("debug text.");
        Log.Info("info text.");
        var actVal = Log.AllMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // Clean
        Log.Level = currentLevel;
    }
}
