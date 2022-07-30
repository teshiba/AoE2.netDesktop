﻿namespace AoE2NetDesktop.CtrlForm.Tests;
using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.SysApi;

using AoE2netDesktopTests.TestUtility;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;

[TestClass]
public class CtrlHistoryTests
{
    private const int IndexRM1v1 = 0;
    private const int IndexRMTeam = 1;
    private const int IndexEW1v1 = 2;
    private const int IndexEWTeam = 3;
    private const int IndexUnranked = 4;
    private const int IndexDM1v1 = 5;
    private const int IndexDMTeam = 6;

    private const int ProfileId = TestData.AvailableUserProfileId;
    private const int ProfileIdp1 = TestData.AvailableUserProfileId + 1;
    private const int ProfileIdp2 = TestData.AvailableUserProfileId + 2;

    private readonly PlayerMatchHistory matches = new() {
        new Match() {
            LeaderboardId = LeaderboardId.RM1v1,
            Opened = 1,
            Players = new List<Player> {
                    new Player { Name = "me", ProfilId = ProfileId,   Color = 1 },
                    new Player { Name = "p1", ProfilId = ProfileIdp1, Color = 2 },
                },
        },
        new Match() {
            LeaderboardId = LeaderboardId.RMTeam,
            Opened = 2,
            Players = new List<Player> {
                    new Player { Name = "me",  ProfilId = ProfileId,   Color = 3 },
                    new Player { Name = "p2",  ProfilId = ProfileIdp2, Color = 2 },
                    new Player { Name = "p1",  ProfilId = ProfileIdp1, Color = 1 },
                },
        },
    };

    private readonly List<LeaderboardView> leaderboardViews = new() {
        new(IndexRM1v1, "1v1 RM", LeaderboardId.RM1v1, Color.Blue),
        new(IndexRMTeam, "Team RM", LeaderboardId.RMTeam, Color.Indigo),
        new(IndexDM1v1, "1v1 DM", LeaderboardId.DM1v1, Color.DarkGreen),
        new(IndexDMTeam, "Team DM", LeaderboardId.DMTeam, Color.SeaGreen),
        new(IndexEW1v1, "1v1 EW", LeaderboardId.EW1v1, Color.Red),
        new(IndexEWTeam, "Team EW", LeaderboardId.EWTeam, Color.OrangeRed),
        new(IndexUnranked, "Unranked", LeaderboardId.Unranked, Color.SlateGray),
    };

    [TestInitialize]
    public void InitTest()
    {
        AoE2net.ComClient = new TestHttpClient();
    }

    [TestMethod]
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

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReadPlayerMatchHistoryAsyncTest()
    {
        // Arrange
        var expVal = 3;

        // Act
        var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
        var ret = Task.Run(
            () => testClass.ReadPlayerMatchHistoryAsync())
            .Result;

        var actVal = testClass.PlayerMatchHistory;

        // Assert
        Assert.IsTrue(ret);
        Assert.AreEqual(expVal, actVal.Count);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReadLeaderBoardAsyncTest()
    {
        // Arrange
        var expVal = Enum.GetNames(typeof(LeaderboardId)).Length - 1;

        // Act
        var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
        var actVal = Task.Run(
            () => testClass.ReadLeaderBoardAsync())
            .Result;

        // Assert
        Assert.AreEqual(expVal, actVal.Count);
    }

    [TestMethod]
    public void CreateListViewItemTest()
    {
        // Arrange
        var leaderboardName = "1v1 RM";
        var leaderboards = new Dictionary<LeaderboardId, Leaderboard> {
            {
                LeaderboardId.RM1v1,
                new Leaderboard {
                    Rank = 1000,
                    Rating = 1100,
                    HighestRating = 1200,
                    Games = 101,
                    Wins = 61,
                    Losses = 40,
                    Drops = 5,
                    Streak = 10,
                    HighestStreak = 20,
                    LowestStreak = 3,
                }
            },
        };

        // Act
        var testClass = CtrlHistory.CreateListViewItem(leaderboards[LeaderboardId.RM1v1], leaderboardViews[0]);

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

    [TestMethod]
    public void CreateListViewItemTestEmptyLeaderboard()
    {
        // Arrange
        var leaderboardName = "1v1 RM";
        var leaderboards = new Dictionary<LeaderboardId, Leaderboard> {
            { LeaderboardId.RM1v1, new Leaderboard() },
        };

        // Act
        var testClass = CtrlHistory.CreateListViewItem(leaderboards[LeaderboardId.RM1v1], leaderboardViews[0]);

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

    [TestMethod]
    public void CreateMatchedPlayersInfoTest()
    {
        // Arrange
        matches[0].Players[1].Name = "p1";
        matches[1].Players[2].Name = "p1";

        // Act
        var testClass = new CtrlHistory(ProfileId);
        var actVal = testClass.CreateMatchedPlayersInfo(matches);

        // Assert
        Assert.AreEqual(ProfileId + 1, actVal["p1"].ProfileId);
        Assert.AreEqual(1, actVal["p1"].Games1v1);
        Assert.AreEqual(1, actVal["p1"].GamesAlly);
        Assert.AreEqual(0, actVal["p1"].GamesEnemy);
        Assert.AreEqual(1, actVal["p1"].GamesTeam);
        Assert.AreEqual(DateTimeExt.FromUnixTimeSeconds(2), actVal["p1"].LastDate);
        Assert.AreEqual(ProfileId + 2, actVal["p2"].ProfileId);
        Assert.AreEqual(0, actVal["p2"].Games1v1);
        Assert.AreEqual(0, actVal["p2"].GamesAlly);
        Assert.AreEqual(1, actVal["p2"].GamesEnemy);
        Assert.AreEqual(1, actVal["p2"].GamesTeam);
        Assert.AreEqual(DateTimeExt.FromUnixTimeSeconds(2), actVal["p2"].LastDate);
    }

    [TestMethod]
    public void CreateMatchedPlayersInfoTestNameNull()
    {
        // Arrange
        matches[0].Players[1].Name = null;
        matches[1].Players[2].Name = null;
        var expName = $"<Name null: ID: {ProfileIdp1} >";

        // Act
        var testClass = new CtrlHistory(ProfileId);
        var actVal = testClass.CreateMatchedPlayersInfo(matches);

        // Assert
        Assert.AreEqual(ProfileId + 1, actVal[expName].ProfileId);
        Assert.AreEqual(1, actVal[expName].Games1v1);
        Assert.AreEqual(1, actVal[expName].GamesAlly);
        Assert.AreEqual(0, actVal[expName].GamesEnemy);
        Assert.AreEqual(1, actVal[expName].GamesTeam);
        Assert.AreEqual(DateTimeExt.FromUnixTimeSeconds(2), actVal[expName].LastDate);
        Assert.AreEqual(ProfileId + 2, actVal["p2"].ProfileId);
        Assert.AreEqual(0, actVal["p2"].Games1v1);
        Assert.AreEqual(0, actVal["p2"].GamesAlly);
        Assert.AreEqual(1, actVal["p2"].GamesEnemy);
        Assert.AreEqual(1, actVal["p2"].GamesTeam);
        Assert.AreEqual(DateTimeExt.FromUnixTimeSeconds(2), actVal["p2"].LastDate);
    }

    [TestMethod]
    public void CreateListViewHistoryTest()
    {
        // Arrange
        matches[0].Players[1].Name = "p1";
        matches[1].Players[2].Name = "p1";

        // Act
        var testClass = new CtrlHistory(ProfileId, matches);
        var actVal = testClass.CreateListViewHistory();

        // Assert
        Assert.AreEqual("1", actVal[LeaderboardId.RM1v1][0].SubItems[4].Text);  // Color
        Assert.AreEqual("3", actVal[LeaderboardId.RMTeam][0].SubItems[4].Text); // Color
    }

    [TestMethod]
    public void OpenProfileTest()
    {
        // Arrange
        var expVal = "/c start https://aoe2.net/#profile-1";
        var testHttpClient = new TestHttpClient() {
            SystemApi = new SystemApiStub(1),
        };
        AoE2net.ComClient = testHttpClient;
        var playerName = "player1";
        var profileId = TestData.AvailableUserProfileId;
        var testClass = new CtrlHistory(profileId);
        testClass.MatchedPlayerInfos.Add(playerName, new PlayerInfo() { ProfileId = profileId });

        // Act
        var actVal = testClass.OpenProfile(playerName);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void OpenProfileTestWin32Exception()
    {
        // Arrange
        var testHttpClient = new TestHttpClient {
            SystemApi = new SystemApiStub(1) {
                ForceWin32Exception = true,
            },
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

    [TestMethod]
    public void OpenProfileTestException()
    {
        // Arrange
        var testHttpClient = new TestHttpClient {
            SystemApi = new SystemApiStub(1) {
                ForceException = true,
            },
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

    [TestMethod]
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

    [TestMethod]
    [DataRow("1v1 Random Map", LeaderboardId.RM1v1)]
    [DataRow("Team Random Map", LeaderboardId.RMTeam)]
    [DataRow("1v1 Empire Wars", LeaderboardId.EW1v1)]
    [DataRow("Team Empire Wars", LeaderboardId.EWTeam)]
    [DataRow("Unranked", LeaderboardId.Unranked)]
    [DataRow("1v1 Death Match", LeaderboardId.DM1v1)]
    [DataRow("Team Death Match", LeaderboardId.DMTeam)]
    [DataRow("----", LeaderboardId.Undefined)]
    [DataRow(null, LeaderboardId.Undefined)]
    public void GetLeaderboardIdTest(string leaderboardString, LeaderboardId expVal)
    {
        // Arrange
        // Act
        var actVal = CtrlHistory.GetLeaderboardId(leaderboardString);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD101:Avoid unsupported async delegates", Justification = SuppressReason.GuiEvent)]
    public void OpenHistoryTest()
    {
        // Arrange
        var playerName = "AvailablePlayerName";
        var done = false;
        var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
        var playerInfo = new PlayerInfo {
            ProfileId = TestData.AvailableUserProfileId,
        };

        testClass.MatchedPlayerInfos.Add(playerName, playerInfo);
        var actVal = testClass.GenerateFormHistory(playerName);

        actVal.Shown += async (sender, e) =>
        {
            await actVal.Awaiter.WaitAsync("FormHistory_ShownAsync");

            // Assert
            Assert.AreEqual($"{playerName}'s history - AoE2.net Desktop", actVal.Text);

            actVal.Close();
            done = true;
        };

        // Act
        actVal.ShowDialog();
        Assert.IsTrue(done);
    }

    [TestMethod]
    public void GenerateFormHistoryTestUnavailablePlayerName()
    {
        // Arrange
        var playerName = "AvailablePlayerName";
        var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
        var playerInfo = new PlayerInfo {
            ProfileId = TestData.AvailableUserProfileId,
        };

        testClass.MatchedPlayerInfos.Add(playerName, playerInfo);
        var actVal = testClass.GenerateFormHistory("UnavailablePlayerName");

        // Act
        Assert.IsNull(actVal);
    }

    [TestMethod]
    public void GenerateFormHistoryTestprofileIdNull()
    {
        // Arrange
        var playerName = "AvailablePlayerName";
        var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
        var playerInfo = new PlayerInfo {
            ProfileId = null,
        };

        testClass.MatchedPlayerInfos.Add(playerName, playerInfo);
        var actVal = testClass.GenerateFormHistory(playerName);

        // Act
        Assert.IsNull(actVal);
    }

    [TestMethod]
    public void ShowHistoryTest()
    {
        // Arrange
        var player = new Player() {
            ProfilId = TestData.AvailableUserProfileId,
        };

        // Act
        var ret = CtrlHistory.GenerateFormHistory(player.Name, player.ProfilId);

        // Assert
        Assert.IsNotNull(ret);
    }

    [TestMethod]
    public void ShowHistoryTestInvalidProfileId()
    {
        // Arrange
        var player = new Player() {
            ProfilId = null,
        };

        // Act
        var ret = CtrlHistory.GenerateFormHistory(player.Name, player.ProfilId);

        // Assert
        Assert.IsNull(ret);
    }
}
