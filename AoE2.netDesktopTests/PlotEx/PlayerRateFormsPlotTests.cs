using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using ScottPlot;
using LibAoE2net;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class PlayerRateFormsPlotTests
    {
        private const int profileId = TestData.AvailableUserProfileId;
        private const int profileIdp1 = TestData.AvailableUserProfileId + 1;
        private const int profileIdp2 = TestData.AvailableUserProfileId + 2;
        private readonly PlayerMatchHistory matches = new() {
            new Match() {
                LeaderboardId = LeaderboardId.RM1v1,
                Players = new List<Player>{
                        new Player { Name ="me", ProfilId =  profileId,   Color = 1 },
                        new Player { Name ="p1", ProfilId =  profileIdp1, Color = 2 },
                    },
            },
            new Match() {
                LeaderboardId = LeaderboardId.RMTeam,
                Players = new List<Player>{
                        new Player { Name ="me",  ProfilId =  profileId,   Color = 3 },
                        new Player { Name ="p2",  ProfilId =  profileIdp2, Color = 2 },
                        new Player { Name ="p1",  ProfilId =  profileIdp1, Color = 1 },
                    },
            },
        };

        [TestMethod()]
        public void PlayerRateFormsPlotTest()
        {
            // Arrange
            var expVal = string.Empty;

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new PlayerRateFormsPlot(null);
            });
        }

        [TestMethod()]
        public void PlotTest()
        {
            // Arrange
            var testClass = new PlayerRateFormsPlot(new FormsPlot());

            // Act
            testClass.Plot(matches, profileId);

            // Assert
        }

        [TestMethod()]
        public void UpdateHighlightTest()
        {
            // Arrange
            var testClass = new PlayerRateFormsPlot(new FormsPlot());

            // Act
            testClass.UpdateHighlight();

            // Assert
        }
    }
}