namespace AoE2NetDesktop.Form.Tests;
using System;
using System.Collections.Generic;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;

using AoE2NetDesktopTests.TestData;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

[TestClass]
public class PlayerCountryPlotTests
{
    [TestMethod]
    public void PlayerCountryPlotTest()
    {
        // Arrange
        var plot = new FormsPlot();
        var leaderBoardId = LeaderboardId.RM1v1;
        int profileId = TestData.AvailableUserProfileId;
        var playerMatchHistory = new PlayerMatchHistory {
            new Match {
                LeaderboardId = leaderBoardId,
                Players = new List<Player> {
                    new Player {
                        Country = "JP",
                        ProfilId = profileId,
                    },
                },
            },
        };
        var testClass = new PlayerCountryPlot(plot, 16);

        // Act
        testClass.Plot(playerMatchHistory, profileId);

        // Assert
    }

    [TestMethod]
    public void PlayerCountryPlotTestNoData()
    {
        // Arrange
        var playerMatchHistory = new PlayerMatchHistory();
        int profileId = TestData.AvailableUserProfileId;
        var plot = new FormsPlot();
        var testClass = new PlayerCountryPlot(plot, 16);

        // Act
        testClass.Plot(playerMatchHistory, profileId);

        // Assert
    }

    [TestMethod]
    public void PlayerCountryPlotTestNullArg()
    {
        // Arrange
        var plot = new FormsPlot();
        var testClass = new PlayerCountryPlot(plot, 16);

        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            testClass.Plot(null, 0);
        });
    }
}
