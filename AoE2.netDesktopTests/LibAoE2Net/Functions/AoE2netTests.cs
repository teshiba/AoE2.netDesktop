﻿using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using AoE2NetDesktop.Tests;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class AoE2netTests
    {
        private const int ProfileId = 0;

        [TestMethod()]
        public void GetPlayerLastMatchAsyncTest()
        {
            // Arrange
            var expPlayer = new List<Player> {
                new Player() {
                    ProfilId = 1,
                    SteamId = "00000000000000001",
                    Name = "Player1",
                    Slot = 1,
                    SlotType = 1,
                    Rating = 1111,
                    Color = 1,
                    Team = 1,
                    Civ = 11,
                    Won = false,
                },
                new Player() {
                    ProfilId = 2,
                    SteamId = null,
                    Name = "Player2",
                    Slot = 2,
                    SlotType = 1,
                    Rating = 2222,
                    Color = 2,
                    Team = 2,
                    Civ = 24,
                    Won = true,
                },
                new Player() {
                    ProfilId = 3,
                    SteamId = "00000000000000003",
                    Name = "Player3",
                    Slot = 3,
                    SlotType = 1,
                    Rating = null,
                    Color = 3,
                    Team = 1,
                    Civ = 9,
                    Won = false,
                },
                new Player() {
                    ProfilId = 4,
                    SteamId = "00000000000000004",
                    Name = "Player4",
                    Slot = 4,
                    SlotType = 1,
                    Rating = 4444,
                    Color = 4,
                    Team = 2,
                    Civ = 11,
                    Won = true,
                },
                new Player() {
                    ProfilId = 5,
                    SteamId = "00000000000000005",
                    Name = "Player5",
                    Slot = 5,
                    SlotType = 1,
                    Rating = 5555,
                    Color = 5,
                    Team = 1,
                    Civ = 24,
                    Won = false,
                },
                new Player() {
                    ProfilId = 6,
                    SteamId = "00000000000000006",
                    Name = "Player6",
                    Slot = 6,
                    SlotType = 1,
                    Rating = 6666,
                    Color = 6,
                    Team = 2,
                    Civ = 12,
                    Won = true,
                },
                new Player() {
                    ProfilId = 7,
                    SteamId = "00000000000000007",
                    Name = "Player7",
                    Slot = 7,
                    SlotType = 1,
                    Rating = 7777,
                    Color = 7,
                    Team = 1,
                    Civ = 35,
                    Won = false,
                },
                new Player() {
                    ProfilId = 8,
                    SteamId = "00000000000000008",
                    Name = "Player8",
                    Slot = 8,
                    SlotType = 1,
                    Rating = null,
                    Color = 8,
                    Team = 2,
                    Civ = 36,
                    Won = true,
                },
            };

            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerLastMatchAsync(TestInit.AvailableUserSteamId)
                ).Result;

            // Assert
            // PlayerLastMatch
            Assert.AreEqual(1, actVal.ProfileId);
            Assert.AreEqual(TestInit.AvailableUserSteamId, actVal.SteamId);
            Assert.AreEqual("Player1", actVal.Name);
            Assert.AreEqual("JP", actVal.Country);
            // LastMatch
            Assert.AreEqual("00000000", actVal.LastMatch.MatchId);
            Assert.AreEqual(null, actVal.LastMatch.LobbyId);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", actVal.LastMatch.MatchUuid);
            Assert.AreEqual("00000", actVal.LastMatch.Version);
            Assert.AreEqual("AUTOMATCH", actVal.LastMatch.Name);
            Assert.AreEqual(8, actVal.LastMatch.NumPlayers);
            Assert.AreEqual(8, actVal.LastMatch.NumSlots);
            Assert.AreEqual(null, actVal.LastMatch.AverageRating);
            Assert.AreEqual(false, actVal.LastMatch.Cheats);
            Assert.AreEqual(false, actVal.LastMatch.FullTechTree);
            Assert.AreEqual(5, actVal.LastMatch.EndingAge);
            Assert.AreEqual(null, actVal.LastMatch.Expansion);
            Assert.AreEqual(0, actVal.LastMatch.GameType);
            Assert.AreEqual(null, actVal.LastMatch.HasCustomContent);
            Assert.AreEqual(true, actVal.LastMatch.HasPassword);
            Assert.AreEqual(true, actVal.LastMatch.LockSpeed);
            Assert.AreEqual(true, actVal.LastMatch.LockTeams);
            Assert.AreEqual(4, actVal.LastMatch.MapSize);
            Assert.AreEqual(29, actVal.LastMatch.MapType);
            Assert.AreEqual(200, actVal.LastMatch.Pop);
            Assert.AreEqual(true, actVal.LastMatch.Ranked);
            Assert.AreEqual(LeaderBoardId.TeamRandomMap, actVal.LastMatch.LeaderboardId);
            Assert.AreEqual(4, actVal.LastMatch.RatingType);
            Assert.AreEqual(1, actVal.LastMatch.Resources);
            Assert.AreEqual(null, actVal.LastMatch.Rms);
            Assert.AreEqual(null, actVal.LastMatch.Scenario);
            Assert.AreEqual("testServer", actVal.LastMatch.Server);
            Assert.AreEqual(false, actVal.LastMatch.SharedExploration);
            Assert.AreEqual(2, actVal.LastMatch.Speed);
            Assert.AreEqual(2, actVal.LastMatch.StartingAge);
            Assert.AreEqual(true, actVal.LastMatch.TeamTogether);
            Assert.AreEqual(true, actVal.LastMatch.TeamPositions);
            Assert.AreEqual(0, actVal.LastMatch.TreatyLength);
            Assert.AreEqual(false, actVal.LastMatch.Turbo);
            Assert.AreEqual(1, actVal.LastMatch.Victory);
            Assert.AreEqual(0, actVal.LastMatch.VictoryTime);
            Assert.AreEqual(0, actVal.LastMatch.Visibility);
            Assert.AreEqual(1612182081, actVal.LastMatch.Opened);
            Assert.AreEqual(1612182081, actVal.LastMatch.Started);
            Assert.AreEqual(1643808142, actVal.LastMatch.Finished);
            // Players
            for (var i = 0; i < actVal.LastMatch.Players.Count; i++) {
                var player = actVal.LastMatch.Players[i];
                Assert.AreEqual(expPlayer[i].ProfilId, player.ProfilId);
                Assert.AreEqual(expPlayer[i].SteamId, player.SteamId);
                Assert.AreEqual(expPlayer[i].Name, player.Name);
                Assert.AreEqual(expPlayer[i].Clan, player.Clan);
                Assert.AreEqual(expPlayer[i].Country, player.Country);
                Assert.AreEqual(expPlayer[i].Slot, player.Slot);
                Assert.AreEqual(expPlayer[i].SlotType, player.SlotType);
                Assert.AreEqual(expPlayer[i].Rating, player.Rating);
                Assert.AreEqual(expPlayer[i].RatingChange, player.RatingChange);
                Assert.AreEqual(expPlayer[i].Games, player.Games);
                Assert.AreEqual(expPlayer[i].Wins, player.Wins);
                Assert.AreEqual(expPlayer[i].Streak, player.Streak);
                Assert.AreEqual(expPlayer[i].Drops, player.Drops);
                Assert.AreEqual(expPlayer[i].Color, player.Color);
                Assert.AreEqual(expPlayer[i].Team, player.Team);
                Assert.AreEqual(expPlayer[i].Civ, player.Civ);
                Assert.AreEqual(expPlayer[i].Won, player.Won);
            }
        }

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestNullAsync()
        {
            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                AoE2net.GetPlayerLastMatchAsync(null)
            );
        }

        [TestMethod()]
        [DataRow(LeaderBoardId.TeamRandomMap, 1)]
        public void GetPlayerRatingHistoryAsyncTestSteamId(LeaderBoardId leaderBoardId, int count)
        {
            // Arrange
            var expVal = new List<PlayerRating>{
                new PlayerRating {
                Drops = 0,
                NumLosses = 100,
                NumWins = 100,
                Rating = 1111,
                Streak = 0,
                TimeStamp = 1643808142,
                },
            };

            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerRatingHistoryAsync(
                    TestInit.AvailableUserSteamId, leaderBoardId, count)
                ).Result;

            // Assert
            for (int i = 0; i < actVal.Count; i++) {
                var rating = actVal[i];
                Assert.AreEqual(expVal[i].Drops, rating.Drops);
                Assert.AreEqual(expVal[i].NumLosses, rating.NumLosses);
                Assert.AreEqual(expVal[i].NumWins, rating.NumWins);
                Assert.AreEqual(expVal[i].Rating, rating.Rating);
                Assert.AreEqual(expVal[i].Streak, rating.Streak);
                Assert.AreEqual(expVal[i].TimeStamp, rating.TimeStamp);
            }
        }

        [TestMethod()]
        [DataRow(LeaderBoardId.TeamRandomMap, 1)]
        public void GetPlayerRatingHistoryAsyncTestProfileId(LeaderBoardId leaderBoardId, int count)
        {
            // Arrange
            var expVal = new List<PlayerRating>{
                new PlayerRating {
                Drops = 0,
                NumLosses = 100,
                NumWins = 100,
                Rating = 9999,
                Streak = 0,
                TimeStamp = 1643808142,
                },
            };

            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = Task.Run(
                () => AoE2net.GetPlayerRatingHistoryAsync(
                    ProfileId, leaderBoardId, count)
                ).Result;

            // Assert
            for (int i = 0; i < actVal.Count; i++) {
                var rating = actVal[i];
                Assert.AreEqual(expVal[i].Drops, rating.Drops);
                Assert.AreEqual(expVal[i].NumLosses, rating.NumLosses);
                Assert.AreEqual(expVal[i].NumWins, rating.NumWins);
                Assert.AreEqual(expVal[i].Rating, rating.Rating);
                Assert.AreEqual(expVal[i].Streak, rating.Streak);
                Assert.AreEqual(expVal[i].TimeStamp, rating.TimeStamp);
            }
        }

        [TestMethod()]
        public async Task GetPlayerRatingHistoryAsyncTestNullAsync()
        {
            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                AoE2net.GetPlayerRatingHistoryAsync(null, LeaderBoardId.TeamRandomMap, 1)
            );
        }

        [TestMethod()]
        [DataRow(Language.en)]
        public void GetStringsAsyncTest(Language language)
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = Task.Run(() 
                => AoE2net.GetStringsAsync(language)
                ).Result;


            // Assert
            Assert.AreEqual(Language.en.ToApiString(), actVal.Language);
            Assert.AreEqual(0, actVal.Age[0].Id);
            Assert.AreEqual("Standard", actVal.Age[0].String);
            Assert.AreEqual(0, actVal.Civ[0].Id);
            Assert.AreEqual("Aztecs", actVal.Civ[0].String);
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

        [TestMethod()]
        [DataRow("Aztecs")]
        public void GetCivImageLocationTest(string civ)
        {
            // Arrange
            var expVal = $"https://aoe2.net/assets/images/crests/25x25/aztecs.png";
            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = AoE2net.GetCivImageLocation(civ);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetCivImageLocationTestNull()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();

            // Act
            var actVal = AoE2net.GetCivImageLocation(null);

            // Assert
            Assert.IsNull(actVal);
        }
    }
}