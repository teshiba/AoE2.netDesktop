namespace AoE2NetDesktop.CtrlForm.Tests;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;

using AoE2netDesktopTests.TestUtility;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

[TestClass]
public class CtrlMainTests
{
    [ClassInitialize]
    public static void Init(TestContext context)
    {
        if(context is null) {
            throw new ArgumentNullException(nameof(context));
        }

        StringsExt.Init();
    }

    [TestInitialize]
    public void InitTest()
    {
        AoE2net.ComClient = new TestHttpClient();
    }

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
    [DataRow(TeamType.OddColorNo, 4)]
    [DataRow(TeamType.EvenColorNo, 40)]
    public void GetAverageRateTest(TeamType teamType, int? expVal)
    {
        // Arrange
        var players = new List<Player> {
            new Player { Color = 1, Rating = 1 },
            new Player { Color = 2, Rating = 10 },
            new Player { Color = 3, Rating = 3 },
            new Player { Color = 4, Rating = 30 },
            new Player { Color = 5, Rating = 5 },
            new Player { Color = 6, Rating = 50 },
            new Player { Color = 7, Rating = 7 },
            new Player { Color = 8, Rating = 70 },
        };

        // Act
        var actVal = CtrlMain.GetAverageRate(players, teamType);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(TeamType.OddColorNo, 3)]
    [DataRow(TeamType.EvenColorNo, 30)]
    public void GetAverageRateTestIncludeRateNull(TeamType teamType, int? expVal)
    {
        // Arrange
        var players = new List<Player> {
            new Player { Color = 1, Rating = 1 },
            new Player { Color = 2, Rating = 10 },
            new Player { Color = 3, Rating = 3 },
            new Player { Color = 4, Rating = 30 },
            new Player { Color = 5, Rating = 5 },
            new Player { Color = 6, Rating = 50 },
            new Player { Color = 7, Rating = null },
            new Player { Color = 8, Rating = null },
        };

        // Act
        var actVal = CtrlMain.GetAverageRate(players, teamType);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetAverageRateTestArgumentOutOfRangeException()
    {
        // Arrange
        // Act
        // Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
        {
            _ = CtrlMain.GetAverageRate(new List<Player>(), (TeamType)(-1));
        });
    }

    [TestMethod]
    public void GetAverageRateTestPlayerNull()
    {
        // Arrange
        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            _ = CtrlMain.GetAverageRate(null, TeamType.OddColorNo);
        });
    }

    [TestMethod]
    public void GetAverageRateTestRateAllNull()
    {
        // Arrange
        var players = new List<Player> {
            new Player { Color = 1, Rating = null },
            new Player { Color = 2, Rating = null },
            new Player { Color = 3, Rating = null },
            new Player { Color = 4, Rating = null },
            new Player { Color = 5, Rating = null },
            new Player { Color = 6, Rating = null },
            new Player { Color = 7, Rating = null },
            new Player { Color = 8, Rating = null },
        };

        int? expVal = null;

        // Act
        var actVal = CtrlMain.GetAverageRate(players, TeamType.EvenColorNo);

        // Assert
        Assert.AreEqual(expVal, actVal);
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
        AoE2net.ComClient = new TestHttpClient();
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
    [DataRow(0, "invalid civ:0")]
    [DataRow(1, "Britons")]
    [DataRow(37, "Sicilians")]
    [DataRow(40, "invalid civ:40")]
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
        var player = new Player() {
            Games = games,
            Wins = wins,
        };

        // Act
        var actVal = CtrlMain.GetLossesString(player);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(20, "20")]
    [DataRow(null, "N/A")]
    public void GetWinsStringTest(int? wins, string expVal)
    {
        // Arrange
        var player = new Player() {
            Wins = wins,
        };

        // Act
        var actVal = CtrlMain.GetWinsString(player);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
