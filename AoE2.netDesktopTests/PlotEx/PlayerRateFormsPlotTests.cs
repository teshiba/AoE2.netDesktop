namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

using System;
using System.Collections.Generic;
using System.Drawing;

[TestClass]
public class PlayerRateFormsPlotTests
{
    private readonly Dictionary<LeaderboardId, Color> leaderboardColor = new() {
        { LeaderboardId.RM1v1, Color.Blue },
        { LeaderboardId.RMTeam, Color.Indigo },
        { LeaderboardId.DM1v1, Color.DarkGreen },
        { LeaderboardId.DMTeam, Color.SeaGreen },
        { LeaderboardId.EW1v1, Color.Red },
        { LeaderboardId.EWTeam, Color.OrangeRed },
        { LeaderboardId.Unranked, Color.SlateGray },
    };

    [TestMethod]
    public void PlayerRateFormsPlotTest()
    {
        // Arrange
        var expVal = string.Empty;

        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            _ = new PlayerRateFormsPlot(null, leaderboardColor, 16);
        });
    }

    [TestMethod]
    public void PlotTest()
    {
        // Arrange
        var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardColor, 16);
        var datetimeMaxX = DateTimeOffset.FromUnixTimeSeconds(0).LocalDateTime.ToOADate();
        var datetimeMinX = DateTimeOffset.FromUnixTimeSeconds(0).LocalDateTime.ToOADate();
        var rateMaxY = 130;
        var rateMinY = 110;

        var playerRatings = new List<PlayerRating>() {
            new PlayerRating() { Rating = rateMaxY, TimeStamp = 0 },
            new PlayerRating() { Rating = rateMinY, TimeStamp = 0 },
        };

        var playerRatingHistory = new PlayerRatingHistories {
            [LeaderboardId.RM1v1] = playerRatings,
            [LeaderboardId.RMTeam] = playerRatings,
            [LeaderboardId.EW1v1] = playerRatings,
            [LeaderboardId.EWTeam] = playerRatings,
            [LeaderboardId.DM1v1] = playerRatings,
            [LeaderboardId.DMTeam] = playerRatings,
            [LeaderboardId.Unranked] = playerRatings,
        };

        // Act
        testClass.Plot(playerRatingHistory);

        // Assert
        Assert.AreEqual(datetimeMaxX, (double)testClass.Plots[LeaderboardId.RM1v1].MaxX);
        Assert.AreEqual(datetimeMinX, (double)testClass.Plots[LeaderboardId.RM1v1].MinX);
        Assert.AreEqual(rateMaxY, testClass.Plots[LeaderboardId.RM1v1].MaxY);
        Assert.AreEqual(rateMinY, testClass.Plots[LeaderboardId.RM1v1].MinY);
    }

    [TestMethod]
    public void PlotTestWithoutRate()
    {
        // Arrange
        var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardColor, 16);
        var playerRatings = new List<PlayerRating>() {
            new PlayerRating() { Rating = null, TimeStamp = null },
            new PlayerRating() { Rating = null, TimeStamp = null },
        };

        var playerRatingHistory = new PlayerRatingHistories {
            [LeaderboardId.RM1v1] = playerRatings,
            [LeaderboardId.RMTeam] = playerRatings,
            [LeaderboardId.EW1v1] = playerRatings,
            [LeaderboardId.EWTeam] = playerRatings,
            [LeaderboardId.DM1v1] = playerRatings,
            [LeaderboardId.DMTeam] = playerRatings,
            [LeaderboardId.Unranked] = playerRatings,
        };

        // Act
        testClass.Plot(playerRatingHistory);

        // Assert
        Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MaxX);
        Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MaxY);
        Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MinX);
        Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MinY);
    }

    [TestMethod]
    public void UpdateHighlightTest()
    {
        // Arrange
        var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardColor, 16);

        // Act
        testClass.UpdateHighlight();

        // Assert
    }
}
