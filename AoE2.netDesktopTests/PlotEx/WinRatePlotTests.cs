using AoE2NetDesktop.Tests;

using LibAoE2net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScottPlot;
using System;
using System.Collections.Generic;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class WinRatePlotTests
    {
        private const int profileId = TestData.AvailableUserProfileId;
        private const LeaderboardId leaderboardId = LeaderboardId.RM1v1;
        private readonly PlayerMatchHistory playerMatchHistory = new() {
            new Match() {
                LeaderboardId = leaderboardId,
                MapType = 9,
                Players = new List<Player>() {
                    new Player(){
                        ProfilId = profileId,
                        Civ = 2,
                        Won = true,
                    }
                }
            },
            new Match() {
                LeaderboardId = leaderboardId,
                MapType = 9,
                Players = new List<Player>() {
                    new Player(){
                        ProfilId = profileId,
                        Civ = 2,
                        Won = false,
                    }
                }
            },
            new Match() {
                LeaderboardId = leaderboardId,
                MapType = 9,
                Players = new List<Player>() {
                    new Player(){
                        ProfilId = profileId,
                        Civ = 2,
                        Won = false,
                    }
                }
            },
            new Match() {
                LeaderboardId = leaderboardId,
                MapType = 10,
                Players = new List<Player>() {
                    new Player(){
                        ProfilId = profileId,
                        Civ = 4,
                        Won = null,
                    }
                }
            }
        };

        [ClassInitialize]
        public static void Init(TestContext context)
        {
            if (context is null) {
                throw new ArgumentNullException(nameof(context));
            }

            AoE2net.ComClient = new TestHttpClient();
            StringsExt.InitAsync();
        }

        [TestMethod()]
        public void PlotTestMap()
        {
            // Arrange

            // Act
            var testClass = new WinRatePlot(new FormsPlot(), 16);
            testClass.Plot(playerMatchHistory, profileId, leaderboardId, DataSource.Map);

            // Assert
            Assert.AreEqual(1, testClass.Values["Arabia"].Lower);
            Assert.AreEqual(2, testClass.Values["Arabia"].Upper);
        }

        [TestMethod()]
        public void PlotTestCivilization()
        {
            // Arrange

            // Act
            var testClass = new WinRatePlot(new FormsPlot(), 16);
            testClass.Plot(playerMatchHistory, profileId, leaderboardId, DataSource.Civilization);

            // Assert
            Assert.AreEqual(1, testClass.Values["Franks"].Lower);
            Assert.AreEqual(2, testClass.Values["Franks"].Upper);
        }

        [TestMethod()]
        public void PlotTestMapPlayerMatchHistoryNull()
        {
            // Arrange

            // Act
            var testClass = new WinRatePlot(new FormsPlot(), 16);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                testClass.Plot(null, profileId, leaderboardId, DataSource.Map);
            });
        }
    }
}