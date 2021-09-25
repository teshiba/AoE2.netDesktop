using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using System.Collections.Generic;
using ScottPlot;
using AoE2NetDesktop.Tests;
using System;

#if false
namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class DataPlotTests
    {
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
        public void DataPlotTest()
        {
            // Arrange
            int expProfileId = 0;
            var expPlayerMatchHistory = new PlayerMatchHistory();

            // Act
            var testClass = new DataPlot(expPlayerMatchHistory, expProfileId);
            var actPlayerMatchHistory = testClass.GetField<PlayerMatchHistory>("playerMatchHistory");
            var actProfileId = testClass.GetField<int>("profileId");

            // Assert
            Assert.AreEqual(actPlayerMatchHistory, expPlayerMatchHistory);
            Assert.AreEqual(actProfileId, expProfileId);
        }

        [TestMethod()]
        public void PlotWinRateTestCivilization()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var leaderBoardId = LeaderBoardId.OneVOneRandomMap;
            int profileId = TestData.AvailableUserProfileId;

            var playerMatchHistory = new PlayerMatchHistory {
                new Match {
                    LeaderboardId = leaderBoardId,
                    Players = new List<Player> {
                        new Player {
                            Civ = 1,
                            ProfilId = profileId,
                            Won = true,
                        }
                    },
                }
            };

            var testClass = new DataPlot(playerMatchHistory, profileId);
            var plot = new Plot();

            // Act
            testClass.PlotWinRate(leaderBoardId, DataSource.Civilization, plot);

            // Assert
        }


        [TestMethod()]
        public void PlotWinRateCivilizationTestNullArg()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var leaderBoardId = LeaderBoardId.OneVOneRandomMap;
            int profileId = TestData.AvailableUserProfileId;
            var playerMatchHistory = new PlayerMatchHistory();
            var testClass = new DataPlot(playerMatchHistory, profileId);

            // Act
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                testClass.PlotWinRate(leaderBoardId, DataSource.Civilization, null);
            });

            // Assert
        }

        [TestMethod()]
        public void PlotWinRateMapTest()
        {
            // Arrange
            int profileId = 0;
            var playerMatchHistory = new PlayerMatchHistory();
            var testClass = new DataPlot(playerMatchHistory, profileId);
            var plot = new Plot();

            // Act
            testClass.PlotWinRate(LeaderBoardId.OneVOneRandomMap, DataSource.Map, plot);

            // Assert
        }

        [TestMethod()]
        public void PlotWinRateMapTestNullArg()
        {
            // Arrange
            var testClass = new DataPlot(new PlayerMatchHistory(), 0);

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                testClass.PlotWinRate(LeaderBoardId.OneVOneRandomMap, DataSource.Map, null);
            });

        }

        [TestMethod()]
        public void PlotRateTest()
        {
            // Arrange
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


            var testClass = new DataPlot(playerMatchHistory, 1);
            var plot = new Plot();

            // Act
            testClass.PlotRate(LeaderBoardId.OneVOneRandomMap, plot);

            // Assert
        }

        private static Match CreateMatch(int rate, DateTime datetime)
        {
            return new Match {
                Opened = new DateTimeOffset(datetime).ToUnixTimeSeconds(),
                LeaderboardId = LeaderBoardId.OneVOneRandomMap,
                Players = new List<Player> {
                        new Player {
                            Civ = 1,
                            ProfilId = 1,
                            Won = true,
                            Rating = rate,
                        }
                    },
            };
        }

        [TestMethod()]
        public void PlotRateTestNullArg()
        {
            // Arrange
            var testClass = new DataPlot(new PlayerMatchHistory(), 0);

            // Act

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                testClass.PlotRate(LeaderBoardId.OneVOneRandomMap, null);
            });

            // Assert
        }
    }
}
#endif
