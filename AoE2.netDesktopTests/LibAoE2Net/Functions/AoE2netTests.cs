namespace LibAoE2net.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AoE2NetDesktop.LibAoE2Net.Functions;
    using AoE2NetDesktop.LibAoE2Net.JsonFormat;
    using AoE2NetDesktop.LibAoE2Net.Parameters;

    using AoE2NetDesktopTests.TestData;
    using AoE2NetDesktopTests.TestUtility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AoE2netTests
    {
#pragma warning disable VSTHRD002 // Avoid problematic synchronous waits
#pragma warning disable VSTHRD104 // Offer async methods

        [TestMethod]
        [DataRow(LeaderboardId.RMTeam, 1)]
        public void GetPlayerRatingHistoryAsyncTestSteamId(LeaderboardId leaderBoardId, int count)
        {
            // Arrange
            var expVal = new List<PlayerRating> {
                new PlayerRating {
                    Drops = 1,
                    NumLosses = 222,
                    NumWins = 111,
                    Rating = 1234,
                    Streak = 12,
                    TimeStamp = 123456,
                },
            };

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerRatingHistoryAsync(
                    TestData.AvailableUserSteamId, leaderBoardId, count))
                .Result;

            // Assert
            for(int i = 0; i < actVal.Count; i++) {
                var rating = actVal[i];
                Assert.AreEqual(expVal[i].Drops, rating.Drops);
                Assert.AreEqual(expVal[i].NumLosses, rating.NumLosses);
                Assert.AreEqual(expVal[i].NumWins, rating.NumWins);
                Assert.AreEqual(expVal[i].Rating, rating.Rating);
                Assert.AreEqual(expVal[i].Streak, rating.Streak);
                Assert.AreEqual(expVal[i].TimeStamp, rating.TimeStamp);
            }
        }

        [TestMethod]
        [DataRow(LeaderboardId.RMTeam, 1)]
        public void GetPlayerRatingHistoryAsyncTestProfileId(LeaderboardId leaderBoardId, int count)
        {
            // Arrange
            var expVal = new List<PlayerRating> {
                new PlayerRating {
                    Drops = 1,
                    NumLosses = 222,
                    NumWins = 111,
                    Rating = 1234,
                    Streak = 12,
                    TimeStamp = 123456,
                },
            };

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerRatingHistoryAsync(
                     TestData.AvailableUserProfileId, leaderBoardId, count))
                .Result;

            // Assert
            for(int i = 0; i < actVal.Count; i++) {
                var rating = actVal[i];
                Assert.AreEqual(expVal[i].Drops, rating.Drops);
                Assert.AreEqual(expVal[i].NumLosses, rating.NumLosses);
                Assert.AreEqual(expVal[i].NumWins, rating.NumWins);
                Assert.AreEqual(expVal[i].Rating, rating.Rating);
                Assert.AreEqual(expVal[i].Streak, rating.Streak);
                Assert.AreEqual(expVal[i].TimeStamp, rating.TimeStamp);
            }
        }

        [TestMethod]
        [DataRow(Language.en)]
        public void GetStringsAsyncTest(Language language)
        {
            // Arrange

            // Act
            var actVal = Task.Run(()
                => AoE2net.GetStringsAsync(language))
                .Result;

            // Assert
            Assert.AreEqual(Language.en.ToApiString(), actVal.Language);
            Assert.AreEqual(0, actVal.Age[0].Id);
            Assert.AreEqual("Standard", actVal.Age[0].String);
            Assert.AreEqual(1, actVal.Civ[0].Id);
            Assert.AreEqual("Britons", actVal.Civ[0].String);
            Assert.AreEqual(0, actVal.GameType[0].Id);
            Assert.AreEqual("Random Map", actVal.GameType[0].String);
            Assert.AreEqual(0, actVal.Leaderboard[0].Id);
            Assert.AreEqual("Unranked", actVal.Leaderboard[0].String);
            Assert.AreEqual(0, actVal.MapSize[0].Id);
            Assert.AreEqual("Tiny (2 player)", actVal.MapSize[0].String);
            Assert.AreEqual(9, actVal.MapType[0].Id);
            Assert.AreEqual("Arabia", actVal.MapType[0].String);
            Assert.AreEqual(0, actVal.RatingType[0].Id);
            Assert.AreEqual("Unranked", actVal.RatingType[0].String);
            Assert.AreEqual(0, actVal.Resources[0].Id);
            Assert.AreEqual("Standard", actVal.Resources[0].String);
            Assert.AreEqual(0, actVal.Speed[0].Id);
            Assert.AreEqual("Slow", actVal.Speed[0].String);
            Assert.AreEqual(1, actVal.Victory[0].Id);
            Assert.AreEqual("Conquest", actVal.Victory[0].String);
            Assert.AreEqual(0, actVal.Visibility[0].Id);
            Assert.AreEqual("Normal", actVal.Visibility[0].String);
        }

        [TestMethod]
        public void GetPlayerMatchHistoryAsyncTeststeamId()
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerMatchHistoryAsync(0, 10, TestData.AvailableUserSteamId))
                .Result;

            // Assert
            Assert.AreEqual("00000000-0000-0000-0000-000000012345", actVal[0].MatchUuid);
            Assert.AreEqual("00000000-0000-0000-0000-000000000001", actVal[1].MatchUuid);
        }

        [TestMethod]
        public void GetPlayerMatchHistoryAsyncTestprofileId()
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerMatchHistoryAsync(0, 10, TestData.AvailableUserProfileId))
                .Result;

            // Assert
            Assert.AreEqual("00000000-0000-0000-0000-000000123456", actVal[0].MatchUuid);
            Assert.AreEqual("00000000-0000-0000-0000-000000123457", actVal[1].MatchUuid);
            Assert.AreEqual("00000000-0000-0000-0000-000000123458", actVal[2].MatchUuid);
        }

        [TestMethod]
        public void GetLeaderboardAsyncTestSteamId()
        {
            // Arrange
            var expVal = string.Empty;
            var expStart = 1;
            var expCount = 1;
            var expSteamIdCount = TestData.AvailableUserSteamId;
            var expLeaderBoardId = LeaderboardId.RMTeam;

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetLeaderboardAsync(
                    expLeaderBoardId, expStart, expCount, expSteamIdCount))
                .Result;

            // Assert
            Assert.AreEqual(expLeaderBoardId, actVal.LeaderBoardId);
            Assert.AreEqual(expSteamIdCount, actVal.Leaderboards[0].SteamId);
            Assert.AreEqual(expStart, actVal.Start);
            Assert.AreEqual(expCount, actVal.Count);
        }

        [TestMethod]
        public void GetLeaderboardAsyncTestProfileId()
        {
            // Arrange
            var expVal = string.Empty;
            var expStart = 1;
            var expCount = 1;
            var expProfileIdCount = TestData.AvailableUserProfileId;
            var expLeaderBoardId = LeaderboardId.RMTeam;

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetLeaderboardAsync(
                    expLeaderBoardId, expStart, expCount, expProfileIdCount))
                .Result;

            // Assert
            Assert.AreEqual(expLeaderBoardId, actVal.LeaderBoardId);
            Assert.AreEqual(expProfileIdCount, actVal.Leaderboards[0].ProfileId);
            Assert.AreEqual(expStart, actVal.Start);
            Assert.AreEqual(expCount, actVal.Count);
        }

#pragma warning restore VSTHRD104 // Offer async methods
#pragma warning restore VSTHRD002 // Avoid problematic synchronous waits

        [TestMethod]
        public void GetPlayerMatchHistoryAsyncTeststeamIdIsNull()
        {
            // Arrange

            // Act
            _ = Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                  AoE2net.GetPlayerMatchHistoryAsync(0, 10, null));

            // Assert
        }

        [TestMethod]
        public async Task GetPlayerRatingHistoryAsyncTestNullAsync()
        {
            // Assert
            _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                AoE2net.GetPlayerRatingHistoryAsync(null, LeaderboardId.RMTeam, 1));
        }

        [TestMethod]
        [DataRow("Aztecs")]
        public void GetCivImageLocationTest(string civ)
        {
            // Arrange
            AoE2net.Reset();
            var expVal = $"https://aoe2.net/assets/images/crests/25x25/aztecs.png";

            // Act
            var actVal = AoE2net.GetCivImageLocation(civ);

            // Assert
            Assert.AreEqual(expVal, actVal);

            // restore ComClient setting.
            AoE2net.ComClient = new TestHttpClient() {
                SystemApi = new SystemApiStub(1),
            };
        }

        [TestMethod]
        public void GetCivImageLocationTestNull()
        {
            // Arrange
            // Act
            var actVal = AoE2net.GetCivImageLocation(null);

            // Assert
            Assert.IsNull(actVal);
        }

        [TestMethod]
        public void OpenAoE2netTest()
        {
            // Arrange

            // Act
            var actVal = AoE2net.OpenAoE2net(TestData.AvailableUserProfileId);

            // Assert
            Assert.IsNotNull(actVal);
        }

        [TestMethod]
        public void OnErrorTest()
        {
            // Arrange
            Exception actVal = null;
            AoE2net.OnError = (ex) => { actVal = ex; };
            var errAct = AoE2net.OnError;

            // Act
            var expVal = new Exception("test");
            errAct.Invoke(expVal);

            // Assert
            Assert.AreEqual(actVal, expVal);
        }
    }
}