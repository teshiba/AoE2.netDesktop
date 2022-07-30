namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.Utility.SysApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;

[TestClass]
public class MatchExtTests
{
    private static IEnumerable<object[]> GetTestData => new List<object[]>
    {
        // opened, finished, utcNow, expVal
        new object[] { 100L, 200L, 300L, 100L },
        new object[] { 100L, null, 300L, 200L },
        new object[] { null, null, 300L, 0L },
    };

    [TestMethod]
    public void GetOpenedTimeTest()
    {
        // Arrange
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Local;
        var expVal = new DateTime(1970, 01, 01).ToLocalTime();

        // Act
        var testClass = new Match() {
            Opened = 0,
        };
        var actVal = testClass.GetOpenedTime();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData))]
    public void GetElapsedTimeTest(long? opened, long? finished, long utcNow, long expVal)
    {
        // Arrange
        DateTimeOffsetExt.UtcNow = () => DateTimeOffset.FromUnixTimeSeconds(utcNow);

        // Act
        var testClass = new Match() {
            Opened = opened,
            Finished = finished,
        };
        var actVal = testClass.GetElapsedTime();

        // Assert
        Assert.AreEqual(expVal, actVal.TotalSeconds);
    }

    [TestMethod]
    public void GetPlayerTest()
    {
        // Arrange
        var player1 = new Player() { ProfilId = 101, };
        var player2 = new Player() { ProfilId = 102, };
        var player3 = new Player() { ProfilId = 103, };
        var player4 = new Player() { ProfilId = 104, };
        var expVal = player3;

        // Act
        var players = new List<Player>
        {
            player1,
            player2,
            player3,
            player4,
        };
        var testClass = new Match() {
            Players = players,
        };
        var actVal = testClass.GetPlayer(103);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
