namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;

[TestClass]
public class MatchExtTests
{
    [TestMethod]
    public void GetOpenedTimeTest()
    {
        // Arrange
        var expVal = new DateTime(1970, 01, 01, 0, 0, 0).ToLocalTime();

        // Act
        var testClass = new Match() {
            Opened = 0,
        };
        var actVal = testClass.GetOpenedTime();

        // Assert
        Assert.AreEqual(expVal, actVal);
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
