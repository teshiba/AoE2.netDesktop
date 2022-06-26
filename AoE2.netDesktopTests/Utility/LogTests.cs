namespace AoE2netDesktopTests.Utility;

using AoE2NetDesktop.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class LogTests
{
    [TestMethod]
    public void InfoTest()
    {
        // Arrange
        var expVal = "[INFO] debug info text.";
        Log.Level = LogLevel.Info;

        // Act
        Log.Info("debug info text.");
        var actVal = Log.LastMessage;

        // Assert
        Assert.AreEqual(expVal, actVal);

        // clean up
        Log.Level = LogLevel.Debug;

    }
}
