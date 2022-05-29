namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

[TestClass]
public class MatchTests
{
    [TestMethod]
    public void ToStringTest()
    {
        // Arrange
        var date = DateTime.Now.ToLocalTime();
        var expVal = $"{date} 2 Players Map:Arabia";
        var dateTimeSec = new DateTimeOffset(date).ToUnixTimeSeconds();
        var testClass = new Match();
        testClass.Players.Add(new Player());
        testClass.Players.Add(new Player());
        testClass.Opened = dateTimeSec;
        testClass.MapType = 9;

        // Act
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
