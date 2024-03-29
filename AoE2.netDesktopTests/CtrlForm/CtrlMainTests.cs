﻿namespace AoE2NetDesktop.CtrlForm.Tests;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.SysApi;

using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CtrlMainTests
{
    private static IEnumerable<object[]> GetTestData => new List<object[]>
    {
                  // opened, finished, utcNow, expVal
        new object[] { 60L,  120L,     360L,   "0:01:00 (0:01:42 in game)" },
        new object[] { 60L,  null,     360L,   "0:05:00 (0:08:30 in game)" },
        new object[] { null, null,     360L,   "0:00:00 (0:00:00 in game)" },
    };

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void InitAsyncTest()
    {
        // Arrange
        CtrlMain.SystemApi = new SystemApiStub(1);
        var testClass = new CtrlMain();

        // Act
        var actVal = Task.Run(
            () => CtrlMain.InitAsync(Language.en))
            .Result;

        // Assert
        Assert.IsTrue(actVal);
    }

    [TestMethod]
    [DataRow(true, true, false)]
    [DataRow(false, true, true)]
    [DataRow(null, true, false)]
    public void GetFontStyleTest(bool? won, bool isBold, bool isStrikeout)
    {
        // Arrange
        var player = new Player {
            Won = won,
        };
        var prototype = new Font(new Label().Font, FontStyle.Regular);

        // Act
        var expVal = CtrlMain.GetFontStyle(player, prototype);

        // Assert
        Assert.AreEqual(isBold, expVal.Bold);
        Assert.AreEqual(isStrikeout, expVal.Strikeout);
    }

    [TestMethod]
    [DataRow(1, "1")]
    [DataRow(null, " N/A")]
    public void GetRateStringTest(int? rate, string expVal)
    {
        // Arrange
        // Act
        var actVal = CtrlMain.GetRateString(rate);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow("testName", "testName")]
    [DataRow(null, "-- AI --")]
    public void GetPlayerNameStringTest(string name, string expVal)
    {
        // Arrange
        // Act
        var actVal = CtrlMain.GetPlayerNameString(name);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(9, "Arabia")]
    [DataRow(0, "Unknown(Map No.0)")]
    [DataRow(null, "Unknown(Map No.)")]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetMapNameTest(int? mapType, string expVal)
    {
        // Arrange
        CtrlMain.SystemApi = new SystemApiStub(1);
        var testClass = new CtrlMain();
        var match = new Match() {
            MapType = mapType,
        };

        // Act
        _ = Task.Run(
            () => CtrlMain.InitAsync(Language.en))
            .Result;

        var actVal = match.GetMapName();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetMapNameTestEnableRMS()
    {
        // Arrange
        AoE2net.ComClient = new TestHttpClient();
        CtrlMain.SystemApi = new SystemApiStub(1);
        var testClass = new CtrlMain();
        string expVal = "RandomMapScript";
        var match = new Match() {
            MapType = 59,
            Rms = expVal,
        };

        // Act
        _ = Task.Run(
            () => CtrlMain.InitAsync(Language.en))
            .Result;

        var actVal = match.GetMapName();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(0, "invalid civ:0")]
    [DataRow(1, "Britons")]
    [DataRow(40, "Dravidians")]
    [DataRow(41, "Bengalis")]
    [DataRow(42, "Gurjaras")]
    [DataRow(43, "invalid civ:43")]
    [DataRow(null, "invalid civ:")]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetCivEnNameTest(int? civ, string expVal)
    {
        // Arrange
        var player = new Player() {
            Civ = civ,
        };
        CtrlMain.SystemApi = new SystemApiStub(1);
        var testClass = new CtrlMain();

        // Act
        _ = Task.Run(
            () => CtrlMain.InitAsync(Language.en))
            .Result;

        var actVal = player.GetCivEnName();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(100, 20, "80")]
    [DataRow(null, 20, "N/A")]
    [DataRow(20, null, "N/A")]
    [DataRow(null, null, "N/A")]
    public void GetLossesStringTest(int? games, int? wins, string expVal)
    {
        // Arrange
        var leaderboardContainer = new LeaderboardContainer() {
            Leaderboards = new List<Leaderboard>() {
                new Leaderboard() {
                    Games = games,
                    Wins = wins,
                },
            },
        };

        // Act
        var actVal = CtrlMain.GetLossesString(leaderboardContainer);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetLossesStringTestCount0()
    {
        // Arrange
        string expVal = "N/A";
        var leaderboardContainer = new LeaderboardContainer() {
            Leaderboards = new List<Leaderboard>(),
        };

        // Act
        var actVal = CtrlMain.GetLossesString(leaderboardContainer);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(20, "20")]
    [DataRow(null, "N/A")]
    public void GetWinsStringTest(int? wins, string expVal)
    {
        // Arrange
        var leaderboardContainer = new LeaderboardContainer() {
            Leaderboards = new List<Leaderboard>() {
                new Leaderboard() {
                    Wins = wins,
                },
            },
        };

        // Act
        var actVal = CtrlMain.GetWinsString(leaderboardContainer);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetWinsStringTestCount0()
    {
        // Arrange
        string expVal = "N/A";
        var leaderboardContainer = new LeaderboardContainer() {
            Leaderboards = new List<Leaderboard>(),
        };

        // Act
        var actVal = CtrlMain.GetWinsString(leaderboardContainer);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetOpenedTimeTest()
    {
        // Arrange
        var expVal = "01/01/1970 00:00:00 UTC";
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Utc;
        DateTimeExt.DateTimeFormatInfo = DateTimeFormatInfo.InvariantInfo;

        CtrlMain.DisplayedMatch = new Match() {
            Started = 0,
        };

        // Act
        var actVal = CtrlMain.GetOpenedTimeString(CtrlMain.DisplayedMatch);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetOpenedTimeTestLastMatchNull()
    {
        // Arrange
        var expVal = DateTimeExt.InvalidTime;
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Utc;
        CtrlMain.DisplayedMatch = null;

        // Act
        var actVal = CtrlMain.GetOpenedTimeString(CtrlMain.DisplayedMatch);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData))]
    public void GetElapsedTimeTest(long? opened, long? finished, long utcNow, string expVal)
    {
        // Arrange
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Local;
        DateTimeOffsetExt.UtcNow = () => DateTimeOffset.FromUnixTimeSeconds(utcNow);
        var testClass = new Match() {
            Started = opened,
            Finished = finished,
        };
        CtrlMain.DisplayedMatch = testClass;

        // Act
        var actVal = CtrlMain.GetElapsedTimeString(CtrlMain.DisplayedMatch);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(MatchResult.Defeated)]
    [DataRow(MatchResult.InProgress)]
    [DataRow(MatchResult.Unknown)]
    [DataRow(MatchResult.Victorious)]
    [DataRow(MatchResult.NotStarted)]
    public void GetBorderedStyleTest(MatchResult matchResult)
    {
        // Act
        var actVal = CtrlMain.GetBorderedStyle(matchResult);

        // Assert
        Assert.IsNotNull(actVal);
    }

    [TestMethod]
    public void GetBorderedStyleTestOutOfRange()
    {
        // Act
        var actVal = CtrlMain.GetBorderedStyle((MatchResult)(-1));

        // Assert
        Assert.IsNull(actVal);
    }

    [TestMethod]
    [DataRow(0, "Last match")]
    [DataRow(null, "Last match")]
    [DataRow(1, "1 match ago")]
    [DataRow(-1, "-1 match ago")]
    public void GetMatchNoStringTest(int? matchNo, string expVal)
    {
        // Arrange
        // Act
        var actVal = CtrlMain.GetMatchNoString(matchNo);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
