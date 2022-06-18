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
    private const int ProfileId = TestData.AvailableUserProfileId;
    private const int ProfileIdp1 = TestData.AvailableUserProfileId + 1;
    private const int ProfileIdp2 = TestData.AvailableUserProfileId + 2;
    private readonly PlayerMatchHistory matchesWithoutRate = new() {
        new Match() {
            LeaderboardId = LeaderboardId.RM1v1,
            Players = new List<Player> {
                    new Player { Name = "me", ProfilId = ProfileId,   Color = 1 },
                    new Player { Name = "p1", ProfilId = ProfileIdp1, Color = 2 },
                },
        },
        new Match() {
            LeaderboardId = LeaderboardId.RMTeam,
            Players = new List<Player> {
                    new Player { Name = "me",  ProfilId = ProfileId,   Color = 3 },
                    new Player { Name = "p2",  ProfilId = ProfileIdp2, Color = 2 },
                    new Player { Name = "p1",  ProfilId = ProfileIdp1, Color = 1 },
                },
        },
    };

    private readonly PlayerMatchHistory matchesWithRate = new() {
        new Match() {
            LeaderboardId = LeaderboardId.RM1v1,
            Started = 1,
            Players = new List<Player> {
                    new Player { Name = "me", ProfilId = ProfileId,   Color = 1, Rating = 110 },
                    new Player { Name = "p1", ProfilId = ProfileIdp1, Color = 2, Rating = 120 },
                },
        },
        new Match() {
            LeaderboardId = LeaderboardId.RM1v1,
            Started = 2,
            Players = new List<Player> {
                    new Player { Name = "me",  ProfilId = ProfileId,   Color = 3, Rating = 130 },
                    new Player { Name = "p2",  ProfilId = ProfileIdp2, Color = 2, Rating = 140 },
                    new Player { Name = "p1",  ProfilId = ProfileIdp1, Color = 1, Rating = 150 },
                },
        },
    };

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

        // Act
        testClass.Plot(matchesWithRate, ProfileId);

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

        // Act
        testClass.Plot(matchesWithoutRate, ProfileId);

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
