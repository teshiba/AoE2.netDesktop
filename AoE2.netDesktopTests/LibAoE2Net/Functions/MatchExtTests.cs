namespace LibAoE2net.Tests;

using System;
using System.Collections.Generic;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.SysApi;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MatchExtTests
{
    private static IEnumerable<object[]> GetTestData => new List<object[]>
    {
        // opened, finished, utcNow, expVal
        new object[] { 100L, 200L, 300L, 100L },
        new object[] { 100L, null, 300L, 200L },
        new object[] { null, null, 300L, 0L },
    };

    [TestMethod]
    public void GetOpenedTimeTest()
    {
        // Arrange
        DateTimeExt.TimeZoneInfo = TimeZoneInfo.Local;
        var expVal = new DateTime(1970, 01, 01).ToLocalTime();

        // Act
        var testClass = new Match() {
            Started = 0,
        };
        var actVal = testClass.GetOpenedTime();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DynamicData(nameof(GetTestData))]
    public void GetElapsedTimeTest(long? opened, long? finished, long utcNow, long expVal)
    {
        // Arrange
        DateTimeOffsetExt.UtcNow = () => DateTimeOffset.FromUnixTimeSeconds(utcNow);

        // Act
        var testClass = new Match() {
            Started = opened,
            Finished = finished,
        };
        var actVal = testClass.GetElapsedTime();

        // Assert
        Assert.AreEqual(expVal, actVal.TotalSeconds);
    }

    [TestMethod]
    public void GetPlayerTest()
    {
        // Arrange
        var player1 = new Player() { ProfilId = 101, };
        var player2 = new Player() { ProfilId = 102, };
        var player3 = new Player() { ProfilId = 103, };
        var player4 = new Player() { ProfilId = 104, };
        var expVal = player3;

        // Act
        var players = new List<Player>
        {
            player1,
            player2,
            player3,
            player4,
        };
        var testClass = new Match() {
            Players = players,
        };
        var actVal = testClass.GetPlayer(103);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]

    // Finished
    [DataRow(1234L, 4321L, "123", TeamType.OddColorNo, MatchResult.Victorious)]
    [DataRow(1234L, 4321L, "123", TeamType.EvenColorNo, MatchResult.Defeated)]
    [DataRow(1234L, 4321L, "-123", TeamType.OddColorNo, MatchResult.Defeated)]
    [DataRow(1234L, 4321L, "-123", TeamType.EvenColorNo, MatchResult.Victorious)]

    // Finished is null but rating change is not null
    [DataRow(1234L, null, "123", TeamType.OddColorNo, MatchResult.Victorious)]
    [DataRow(1234L, null, "123", TeamType.EvenColorNo, MatchResult.Defeated)]
    [DataRow(1234L, null, "-123", TeamType.OddColorNo, MatchResult.Defeated)]
    [DataRow(1234L, null, "-123", TeamType.EvenColorNo, MatchResult.Victorious)]

    // Finished but unknown rating change
    [DataRow(1234L, 4321L, null, TeamType.OddColorNo, MatchResult.Finished)]
    [DataRow(1234L, 4321L, null, TeamType.EvenColorNo, MatchResult.Finished)]

    // InProgress
    [DataRow(1234L, null, null, TeamType.OddColorNo, MatchResult.InProgress)]
    [DataRow(1234L, null, null, TeamType.EvenColorNo, MatchResult.InProgress)]

    // NotStarted
    [DataRow(null, null, "123", TeamType.OddColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, "123", TeamType.EvenColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, "-123", TeamType.OddColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, "-123", TeamType.EvenColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, null, TeamType.OddColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, null, TeamType.EvenColorNo, MatchResult.NotStarted)]
    public void GetMatchResultTest(long? started, long? finished, string oddPlayerRatingChange, TeamType teamType, MatchResult expVal)
    {
        // Arrange
        var testClass = new Match {
            Started = started,
            Finished = finished,
            Players = new List<Player> {
                new Player { Color = 1, RatingChange = oddPlayerRatingChange },
                new Player { Color = 2, RatingChange = null },
            },
        };

        // Act
        var actVal = testClass.GetMatchResult(teamType);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]

    // Finished
    [DataRow(1234L, 4321L, "123", TeamType.OddColorNo, MatchResult.Defeated)]
    [DataRow(1234L, 4321L, "123", TeamType.EvenColorNo, MatchResult.Victorious)]
    [DataRow(1234L, 4321L, "-123", TeamType.OddColorNo, MatchResult.Victorious)]
    [DataRow(1234L, 4321L, "-123", TeamType.EvenColorNo, MatchResult.Defeated)]

    // Finished is null but rating change is not null
    [DataRow(1234L, null, "123", TeamType.OddColorNo, MatchResult.Defeated)]
    [DataRow(1234L, null, "123", TeamType.EvenColorNo, MatchResult.Victorious)]
    [DataRow(1234L, null, "-123", TeamType.OddColorNo, MatchResult.Victorious)]
    [DataRow(1234L, null, "-123", TeamType.EvenColorNo, MatchResult.Defeated)]

    // NotStarted
    [DataRow(null, null, "123", TeamType.OddColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, "123", TeamType.EvenColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, "-123", TeamType.OddColorNo, MatchResult.NotStarted)]
    [DataRow(null, null, "-123", TeamType.EvenColorNo, MatchResult.NotStarted)]
    public void GetMatchResultTestEvenTeam(long? started, long? finished, string evenPlayerRatingChange, TeamType teamType, MatchResult expVal)
    {
        // Arrange
        var testClass = new Match {
            Started = started,
            Finished = finished,
            Players = new List<Player> {
                new Player { Color = 1, RatingChange = null },
                new Player { Color = 2, RatingChange = evenPlayerRatingChange },
            },
        };

        // Act
        var actVal = testClass.GetMatchResult(teamType);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetMatchResultTestNull()
    {
        // Arrange
        Match testClass = null;

        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
            {
                // Act
                _ = testClass.GetMatchResult(TeamType.OddColorNo);
            });
    }
}
