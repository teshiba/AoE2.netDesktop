namespace AoE2NetDesktop.Form.Tests;

using System;
using System.Collections.Generic;
using System.Drawing;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Utility.SysApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

[TestClass]
public class PlayerRateFormsPlotTests
{
    private const int IndexRM1v1 = 0;
    private const int IndexRMTeam = 1;
    private const int IndexEW1v1 = 2;
    private const int IndexEWTeam = 3;
    private const int IndexUnranked = 4;
    private const int IndexDM1v1 = 5;
    private const int IndexDMTeam = 6;

    private readonly List<LeaderboardView> leaderboardViews = new() {
        new(IndexRM1v1, "1v1 RM", LeaderboardId.RM1v1, Color.Blue),
        new(IndexRMTeam, "Team RM", LeaderboardId.RMTeam, Color.Indigo),
        new(IndexDM1v1, "1v1 DM", LeaderboardId.DM1v1, Color.DarkGreen),
        new(IndexDMTeam, "Team DM", LeaderboardId.DMTeam, Color.SeaGreen),
        new(IndexEW1v1, "1v1 EW", LeaderboardId.EW1v1, Color.Red),
        new(IndexEWTeam, "Team EW", LeaderboardId.EWTeam, Color.OrangeRed),
        new(IndexUnranked, "Unranked", LeaderboardId.Unranked, Color.SlateGray),
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
            _ = new PlayerRateFormsPlot(null, leaderboardViews, 16);
        });
    }

    [TestMethod]
    public void PlotTest()
    {
        // Arrange
        var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardViews, 16);
        var datetimeMaxX = DateTimeExt.FromUnixTimeSeconds(0).ToOADate();
        var datetimeMinX = DateTimeExt.FromUnixTimeSeconds(0).ToOADate();
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
        var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardViews, 16);
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
        var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardViews, 16);

        // Act
        testClass.UpdateHighlight();

        // Assert
    }
}
