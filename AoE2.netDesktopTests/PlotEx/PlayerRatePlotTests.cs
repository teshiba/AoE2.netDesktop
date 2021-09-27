using AoE2NetDesktop.Tests;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScottPlot;

using System;
using System.Collections.Generic;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class PlayerRatePlotTests
    {
        private static Match CreateMatch(int rate, DateTime datetime)
        {
            return new Match {
                Opened = new DateTimeOffset(datetime).ToUnixTimeSeconds(),
                LeaderboardId = LeaderboardId.RM1v1,
                Players = new List<Player> {
                        new Player {
                            Civ = 1,
                            ProfilId = TestData.AvailableUserProfileId,
                            Won = true,
                            Rating = rate,
                        }
                    },
            };
        }

        [TestMethod()]
        public void PlayerRatePlotTestGetIsVisible()
        {
            // Arrange
            var expVal = true;
            var testClass = new PlayerRatePlot(new FormsPlot());

            // Act
            var actVal = testClass.IsVisible;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void PlayerRatePlotTestSetIsVisible()
        {
            // Arrange
            var expVal = false;
            var testClass = new PlayerRatePlot(new FormsPlot());

            // Act
            testClass.IsVisible = !testClass.IsVisible;
            var actVal = testClass.IsVisible;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void UpdateHighlightTest()
        {
            // Arrange
            var testClass = new PlayerRatePlot(new FormsPlot());

            // Act
            testClass.UpdateHighlight();

            // Assert
        }

        [TestMethod()]
        public void PlotTest()
        {
            // Arrange
            int profileId = TestData.AvailableUserProfileId;
            var datetime = new DateTime(1970, 01, 01, 0, 0, 0);
            var playerMatchHistory = new PlayerMatchHistory {
                CreateMatch(1001, datetime),
                CreateMatch(1002, datetime),
                CreateMatch(1000, datetime),
                CreateMatch(1001, datetime + new TimeSpan(1, 0, 0, 0)),
                CreateMatch(1001, datetime + new TimeSpan(2, 0, 0, 0)),
                CreateMatch(1001, datetime + new TimeSpan(3, 0, 0, 0)),
                CreateMatch(1001, datetime + new TimeSpan(4, 0, 0, 0)),
                CreateMatch(1001, datetime + new TimeSpan(5, 0, 0, 0)),
                CreateMatch(1001, datetime + new TimeSpan(6, 0, 0, 0)),
                CreateMatch(1001, datetime + new TimeSpan(7, 0, 0, 0)),
            };
            var testClass = new PlayerRatePlot(new FormsPlot());

            // Act
            testClass.Plot(playerMatchHistory, profileId, LeaderboardId.RM1v1);

            // Assert
        }
    }
}