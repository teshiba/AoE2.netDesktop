namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.Utility.SysApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

[TestClass]
public class MatchTests
{
    [TestMethod]
    public void ToStringTest()
    {
        // Arrange
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Local;
        var date = new DateTime(1970, 1, 1);
        var expVal = $"{date} 2 Players Map:Arabia";
        var dateTimeSec = (date - DateTimeExt.TimeZoneInfo.BaseUtcOffset).ToUnixTimeSeconds();
        var testClass = new Match();
        testClass.Players.Add(new Player());
        testClass.Players.Add(new Player());
        testClass.Started = dateTimeSec;
        testClass.MapType = 9;

        // Act
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
