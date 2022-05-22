namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Drawing;

[TestClass]
public class PlayerExtTests
{
    private static IEnumerable<object[]> GetColorTestData => new List<object[]>
    {
        new object[] { 1, Color.Blue },
        new object[] { 2, Color.Red },
        new object[] { 3, Color.Green },
        new object[] { 4, Color.Yellow },
        new object[] { 5, Color.Aqua },
        new object[] { 6, Color.Magenta },
        new object[] { 7, Color.Gray },
        new object[] { 8, Color.Orange },
        new object[] { 9, Color.Transparent },
    };

    [TestMethod]
    [DataRow(1, 2, Diplomacy.Enemy)]
    [DataRow(1, 3, Diplomacy.Ally)]
    [DataRow(null, 2, Diplomacy.Neutral)]
    [DataRow(1, null, Diplomacy.Neutral)]
    [DataRow(null, null, Diplomacy.Neutral)]
    public void CheckDiplomacyTest(int? p1Color, int? p2Color, Diplomacy expDiplomacy)
    {
        // Arrange
        var player1 = new Player() { Color = p1Color };
        var player2 = new Player() { Color = p2Color };

        // Act
        var actVal = player1.CheckDiplomacy(player2);

        // Assert
        Assert.AreEqual(expDiplomacy, actVal);
    }

    [TestMethod]
    [DataRow(1, "1")]
    [DataRow(null, "-")]
    public void GetColorStringTest(int? color, string expVal)
    {
        // Arrange
        var player = new Player {
            Color = color,
        };

        // Act
        var actVal = player.GetColorString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DynamicData(nameof(GetColorTestData))]
    public void GetColorTest(int playerNo, Color expVal)
    {
        // Arrange
        var player = new Player {
            Color = playerNo,
        };

        // Act
        var actVal = player.GetColor();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
