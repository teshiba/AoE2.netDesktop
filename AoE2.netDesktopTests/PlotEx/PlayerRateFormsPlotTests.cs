using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using ScottPlot;
using LibAoE2net;
using AoE2NetDesktop.Tests;
using System.Drawing;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class PlayerRateFormsPlotTests
    {
        private const int profileId = TestData.AvailableUserProfileId;
        private const int profileIdp1 = TestData.AvailableUserProfileId + 1;
        private const int profileIdp2 = TestData.AvailableUserProfileId + 2;
        private readonly PlayerMatchHistory matchesWithoutRate = new() {
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

        private readonly PlayerMatchHistory matchesWithRate = new() {
            new Match() {
                LeaderboardId = LeaderboardId.RM1v1,
                Started = 1,
                Players = new List<Player>{
                        new Player { Name ="me", ProfilId =  profileId,   Color = 1 , Rating = 110},
                        new Player { Name ="p1", ProfilId =  profileIdp1, Color = 2 , Rating = 120},
                    },
            },
            new Match() {
                LeaderboardId = LeaderboardId.RM1v1,
                Started = 2,
                Players = new List<Player>{
                        new Player { Name ="me",  ProfilId =  profileId,   Color = 3 , Rating = 130},
                        new Player { Name ="p2",  ProfilId =  profileIdp2, Color = 2 , Rating = 140},
                        new Player { Name ="p1",  ProfilId =  profileIdp1, Color = 1 , Rating = 150},
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

        [TestMethod()]
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

        [TestMethod()]
        public void PlotTest()
        {
            // Arrange
            var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardColor, 16);
            var datetimeMaxX = new DateTime(1970, 01, 01, 9, 0, 0);
            var datetimeMinX = new DateTime(1970, 01, 01, 9, 0, 0);
            var rateMaxY = 130;
            var rateMinY = 110;

            // Act
            testClass.Plot(matchesWithRate, profileId);

            // Assert
            Assert.AreEqual(datetimeMaxX, DateTime.FromOADate((double)testClass.Plots[LeaderboardId.RM1v1].MaxX));
            Assert.AreEqual(datetimeMinX, DateTime.FromOADate((double)testClass.Plots[LeaderboardId.RM1v1].MinX));
            Assert.AreEqual(rateMaxY, testClass.Plots[LeaderboardId.RM1v1].MaxY);
            Assert.AreEqual(rateMinY, testClass.Plots[LeaderboardId.RM1v1].MinY);
        }

        [TestMethod()]
        public void PlotTestWithoutRate()
        {
            // Arrange
            var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardColor, 16);

            // Act
            testClass.Plot(matchesWithoutRate, profileId);

            // Assert
            Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MaxX);
            Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MaxY);
            Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MinX);
            Assert.IsNull(testClass.Plots[LeaderboardId.RM1v1].MinY);
        }

        [TestMethod()]
        public void UpdateHighlightTest()
        {
            // Arrange
            var testClass = new PlayerRateFormsPlot(new FormsPlot(), leaderboardColor, 16);

            // Act
            testClass.UpdateHighlight();

            // Assert
        }
    }
}