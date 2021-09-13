using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibAoE2net;
using AoE2NetDesktop.Tests;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.ComponentModel;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class CtrlHistoryTests
    {
        [TestMethod()]
        public void CtrlHistoryTest()
        {
            // Arrange
            var expVal = 1;

            // Act
            var testClass = new CtrlHistory(expVal);
            var actVal = testClass.ProfileId;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetRatingStringTestInvalid()
        {
            // Arrange
            var expVal = "----";
            var player = new Player()
            {
                Rating = null,
            };

            // Act
            var actVal = CtrlHistory.GetRatingString(player);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetRatingStringTestInc()
        {
            // Arrange
            var expVal = "1234+456";
            var player = new Player()
            {
                Rating = 1234,
                RatingChange = "456",
            };

            // Act
            var actVal = CtrlHistory.GetRatingString(player);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetRatingStringTestDec()
        {
            // Arrange
            var expVal = "1234-456";
            var player = new Player()
            {
                Rating = 1234,
                RatingChange = "-456",
            };

            // Act
            var actVal = CtrlHistory.GetRatingString(player);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow(null, "---")]
        [DataRow(true, "o")]
        [DataRow(false, "")]
        public void GetWinMarkerStringInvalid(bool? won, string expVal)
        {
            // Arrange

            // Act
            var actVal = CtrlHistory.GetWinMarkerString(won);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void ReadPlayerMatchHistoryAsyncTest()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var expVal = 3;

            // Act
            var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
            var ret = Task.Run(
                () => testClass.ReadPlayerMatchHistoryAsync()
                ).Result;

            var actVal = testClass.PlayerMatchHistory;

            // Assert
            Assert.IsTrue(ret);
            Assert.AreEqual(expVal, actVal.Count);
        }

        [TestMethod()]
        public void ReadLeaderBoardAsyncTest()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var expVal = 4;

            // Act
            var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
            var actVal = Task.Run(
                () => testClass.ReadLeaderBoardAsync()
                ).Result;

            // Assert
            Assert.AreEqual(expVal, actVal.Count);
        }

        [TestMethod()]
        public void CreateListViewItemTest()
        {
            // Arrange
            var leaderboardName = "test leaderboard";
            var leaderboard = new Leaderboard
            {
                Rank = 1000,
                Rating = 1100,
                HighestRating = 1200,
                Games = 101,
                Wins = 61,
                Losses = 40,
                Drops = 5,
                Streak = 10,
                HighestStreak = 20,
                LowestStreak = 3
            };

            // Act
            var testClass = CtrlHistory.CreateListViewLeaderboard(leaderboardName, leaderboard);

            // Assert
            Assert.AreEqual(leaderboardName, testClass.SubItems[0].Text);
            Assert.AreEqual("1000", testClass.SubItems[1].Text); // Rank
            Assert.AreEqual("1100", testClass.SubItems[2].Text); // Rating
            Assert.AreEqual("1200", testClass.SubItems[3].Text); // HighestRating
            Assert.AreEqual("101", testClass.SubItems[4].Text); // Games
            Assert.AreEqual("60.4%", testClass.SubItems[5].Text); // Win rate
            Assert.AreEqual("61", testClass.SubItems[6].Text); // Wins
            Assert.AreEqual("40", testClass.SubItems[7].Text); // Losses
            Assert.AreEqual("5", testClass.SubItems[8].Text); // Drops
            Assert.AreEqual("10", testClass.SubItems[9].Text); // Streak
            Assert.AreEqual("20", testClass.SubItems[10].Text); // HighestStreak
            Assert.AreEqual("3", testClass.SubItems[11].Text); // LowestStreak
        }

        [TestMethod()]
        public void CreateListViewItemTestEmptyLeaderboard()
        {
            // Arrange
            var leaderboardName = "test leaderboard";
            var leaderboard = new Leaderboard();

            // Act
            var testClass = CtrlHistory.CreateListViewLeaderboard(leaderboardName, leaderboard);

            // Assert
            Assert.AreEqual(leaderboardName, testClass.SubItems[0].Text);
            Assert.AreEqual("-", testClass.SubItems[1].Text); // Rank
            Assert.AreEqual("-", testClass.SubItems[2].Text); // Rating
            Assert.AreEqual("-", testClass.SubItems[3].Text); // HighestRating
            Assert.AreEqual("0", testClass.SubItems[4].Text); // Games
            Assert.AreEqual("00.0%", testClass.SubItems[5].Text); // Win rate
            Assert.AreEqual("0", testClass.SubItems[6].Text); // Wins
            Assert.AreEqual("0", testClass.SubItems[7].Text); // Losses
            Assert.AreEqual("0", testClass.SubItems[8].Text); // Drops
            Assert.AreEqual("0", testClass.SubItems[9].Text); // Streak
            Assert.AreEqual("0", testClass.SubItems[10].Text); // HighestStreak
            Assert.AreEqual("0", testClass.SubItems[11].Text); // LowestStreak
        }

        [TestMethod()]
        public void CreateMatchedPlayersInfoTest()
        {
            // Arrange
            int profileId = TestData.AvailableUserProfileId;
            var matches = new PlayerMatchHistory {
                new Match(){
                    LeaderboardId = LeaderBoardId.OneVOneRandomMap,
                    Opened = 1,
                    Players = new List<Player>{
                        new Player(){ Name ="me", ProfilId =  profileId,     Color = 1},
                        new Player(){ Name ="p1", ProfilId =  profileId + 1, Color = 2},
                    },
                },
                new Match(){
                    LeaderboardId = LeaderBoardId.TeamRandomMap,
                    Opened = 2,
                    Players = new List<Player>{
                        new Player(){ Name ="me",  ProfilId =  profileId,     Color = 3},
                        new Player(){ Name ="p2",  ProfilId =  profileId + 2, Color = 2},
                        new Player(){ Name ="p1",  ProfilId =  profileId + 1, Color = 1},
                    },
                },
            };

            // Act
            var testClass = new CtrlHistory(profileId);
            var actVal = testClass.CreateMatchedPlayersInfo(matches);

            // Assert
            Assert.AreEqual(profileId + 1, actVal["p1"].ProfileId);
            Assert.AreEqual(1, actVal["p1"].Games1v1);
            Assert.AreEqual(1, actVal["p1"].GamesAlly);
            Assert.AreEqual(0, actVal["p1"].GamesEnemy);
            Assert.AreEqual(1, actVal["p1"].GamesTeam);
            Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(2).LocalDateTime, actVal["p1"].LastDate);
            Assert.AreEqual(profileId + 2, actVal["p2"].ProfileId);
            Assert.AreEqual(0, actVal["p2"].Games1v1);
            Assert.AreEqual(0, actVal["p2"].GamesAlly);
            Assert.AreEqual(1, actVal["p2"].GamesEnemy);
            Assert.AreEqual(1, actVal["p2"].GamesTeam);
            Assert.AreEqual(DateTimeOffset.FromUnixTimeSeconds(2).LocalDateTime, actVal["p2"].LastDate);
        }

        [TestMethod()]
        public void CreateListViewHistoryTest()
        {
            // Arrange
            int profileId = TestData.AvailableUserProfileId;
            var matches = new PlayerMatchHistory {
                new Match(){
                    LeaderboardId = LeaderBoardId.OneVOneRandomMap,
                    Players = new List<Player>{
                        new Player { Name ="me", ProfilId =  profileId,     Color = 1 },
                        new Player { Name ="p1", ProfilId =  profileId + 1, Color = 2 },
                    },
                },
                new Match(){
                    LeaderboardId = LeaderBoardId.TeamRandomMap,
                    Players = new List<Player>{
                        new Player { Name ="me",  ProfilId =  profileId,     Color = 3 },
                        new Player { Name ="p2",  ProfilId =  profileId + 2, Color = 2 },
                        new Player { Name ="p1",  ProfilId =  profileId + 1, Color = 1 },
                    },
                },
            };

            // Act
            var testClass = new CtrlHistory(profileId, matches);
            var actVal = testClass.CreateListViewHistory();

            // Assert
            Assert.AreEqual("1", actVal[LeaderBoardId.OneVOneRandomMap][0].SubItems[4].Text); //Color
            Assert.AreEqual("3", actVal[LeaderBoardId.TeamRandomMap][0].SubItems[4].Text); //Color
        }

        [TestMethod()]
        public void OpenProfileTest()
        {
            // Arrange
            var expVal = "Start https://aoe2.net/#profile-1";
            var testHttpClient = new TestHttpClient();
            AoE2net.ComClient = testHttpClient;
            var playerName = "player1";
            var profileId = TestData.AvailableUserProfileId;
            var testClass = new CtrlHistory(profileId);
            testClass.MatchedPlayerInfos.Add(playerName, new PlayerInfo() { ProfileId = profileId });

            // Act
            testClass.OpenProfile(playerName);

            // Assert
            Assert.AreEqual(expVal, testHttpClient.LastRequest);
        }

        [TestMethod()]
        public void OpenProfileTestWin32Exception()
        {
            // Arrange
            var testHttpClient = new TestHttpClient
            {
                ForceWin32Exception = true,
            };
            AoE2net.ComClient = testHttpClient;
            var playerName = "player1";
            var profileId = TestData.AvailableUserProfileId;
            var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
            testClass.MatchedPlayerInfos.Add(playerName, new PlayerInfo() { ProfileId = profileId });

            // Act
            testClass.OpenProfile("player1");

            // Assert
            Assert.IsNull(testHttpClient.LastRequest);
        }

        [TestMethod()]
        public void OpenProfileTestException()
        {
            // Arrange
            var testHttpClient = new TestHttpClient
            {
                ForceException = true,
            };
            AoE2net.ComClient = testHttpClient;
            var playerName = "player1";
            var profileId = TestData.AvailableUserProfileId;
            var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
            testClass.MatchedPlayerInfos.Add(playerName, new PlayerInfo() { ProfileId = profileId });

            // Act
            testClass.OpenProfile("player1");

            // Assert
            Assert.IsNull(testHttpClient.LastRequest);
        }


        [TestMethod()]
        public void OpenProfileTestUnavailablePlayer()
        {
            // Arrange
            var testHttpClient = new TestHttpClient();
            AoE2net.ComClient = testHttpClient;
            var playerName = "UnavailablePlayer";
            var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
            testClass.MatchedPlayerInfos.Add("player1", new PlayerInfo() { ProfileId = 1 });

            // Act
            testClass.OpenProfile(playerName);

            // Assert
            Assert.IsNull(testHttpClient.LastRequest);
        }
    }
}