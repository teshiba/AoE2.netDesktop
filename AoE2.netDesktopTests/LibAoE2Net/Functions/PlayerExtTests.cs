namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Drawing;

[TestClass]
public class PlayerExtTests
{
    private static IEnumerable<object[]> GetColorTestData => new List<object[]>
    {
        new object[] { 1, Color.Blue },
        new object[] { 2, Color.Red },
        new object[] { 3, Color.Green },
        new object[] { 4, Color.Yellow },
        new object[] { 5, Color.Aqua },
        new object[] { 6, Color.Magenta },
        new object[] { 7, Color.Gray },
        new object[] { 8, Color.Orange },
        new object[] { 9, Color.Transparent },
    };

    [TestMethod]
    [DataRow(1, 2, Diplomacy.Enemy)]
    [DataRow(1, 3, Diplomacy.Ally)]
    [DataRow(null, 2, Diplomacy.Neutral)]
    [DataRow(1, null, Diplomacy.Neutral)]
    [DataRow(null, null, Diplomacy.Neutral)]
    public void CheckDiplomacyTest(int? p1Color, int? p2Color, Diplomacy expDiplomacy)
    {
        // Arrange
        var player1 = new Player() { Color = p1Color };
        var player2 = new Player() { Color = p2Color };

        // Act
        var actVal = player1.CheckDiplomacy(player2);

        // Assert
        Assert.AreEqual(expDiplomacy, actVal);
    }

    [TestMethod]
    [DataRow(1, "1")]
    [DataRow(null, "-")]
    public void GetColorStringTest(int? color, string expVal)
    {
        // Arrange
        var player = new Player {
            Color = color,
        };

        // Act
        var actVal = player.GetColorString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DynamicData(nameof(GetColorTestData))]
    public void GetColorTest(int playerNo, Color expVal)
    {
        // Arrange
        var player = new Player {
            Color = playerNo,
        };

        // Act
        var actVal = player.GetColor();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetRatingStringTestInvalid()
    {
        // Arrange
        var expVal = "----";
        var player = new Player() {
            Rating = null,
        };

        // Act
        var actVal = player.GetRatingString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetRatingStringTestInc()
    {
        // Arrange
        var expVal = "1234+456";
        var player = new Player() {
            Rating = 1234,
            RatingChange = "456",
        };

        // Act
        var actVal = player.GetRatingString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetRatingStringTestDec()
    {
        // Arrange
        var expVal = "1234-456";
        var player = new Player() {
            Rating = 1234,
            RatingChange = "-456",
        };

        // Act
        var actVal = player.GetRatingString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(null, "---")]
    [DataRow(true, "o")]
    [DataRow(false, "")]
    public void GetWinMarkerStringInvalid(bool? won, string expVal)
    {
        // Arrange
        var player = new Player() {
            Won = won,
        };

        // Act
        var actVal = player.GetWinMarkerString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(TeamType.OddColorNo, 4)]
    [DataRow(TeamType.EvenColorNo, 40)]
    public void GetAverageRateTest(TeamType teamType, int? expVal)
    {
        // Arrange
        var players = new List<Player> {
            new Player { Color = 1, Rating = 1 },
            new Player { Color = 2, Rating = 10 },
            new Player { Color = 3, Rating = 3 },
            new Player { Color = 4, Rating = 30 },
            new Player { Color = 5, Rating = 5 },
            new Player { Color = 6, Rating = 50 },
            new Player { Color = 7, Rating = 7 },
            new Player { Color = 8, Rating = 70 },
        };

        // Act
        var actVal = players.GetAverageRate(teamType);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(TeamType.OddColorNo, 3)]
    [DataRow(TeamType.EvenColorNo, 30)]
    public void GetAverageRateTestIncludeRateNull(TeamType teamType, int? expVal)
    {
        // Arrange
        var players = new List<Player> {
            new Player { Color = 1, Rating = 1 },
            new Player { Color = 2, Rating = 10 },
            new Player { Color = 3, Rating = 3 },
            new Player { Color = 4, Rating = 30 },
            new Player { Color = 5, Rating = 5 },
            new Player { Color = 6, Rating = 50 },
            new Player { Color = 7, Rating = null },
            new Player { Color = 8, Rating = null },
        };

        // Act
        var actVal = players.GetAverageRate(teamType);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetAverageRateTestArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = new List<Player>().GetAverageRate((TeamType)(-1));
        });
    }

    [TestMethod]
    public void GetAverageRateTestPlayerNull()
    {
        // Arrange
        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            List<Player> list = null;
            _ = list.GetAverageRate(TeamType.OddColorNo);
        });
    }

    [TestMethod]
    public void GetAverageRateTestRateAllNull()
    {
        // Arrange
        var players = new List<Player> {
            new Player { Color = 1, Rating = null },
            new Player { Color = 2, Rating = null },
            new Player { Color = 3, Rating = null },
            new Player { Color = 4, Rating = null },
            new Player { Color = 5, Rating = null },
            new Player { Color = 6, Rating = null },
            new Player { Color = 7, Rating = null },
            new Player { Color = 8, Rating = null },
        };

        int? expVal = null;

        // Act
        var actVal = players.GetAverageRate(TeamType.EvenColorNo);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
