namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Tests;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

using System;
using System.Collections.Generic;

[TestClass]
public class PlayerCountryPlotTests
{
    [TestMethod]
    public void PlayerCountryPlotTest()
    {
        // Arrange
        var plot = new FormsPlot();
        AoE2net.ComClient = new TestHttpClient();
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
