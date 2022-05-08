using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoE2NetDesktop;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Functions;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class PlayerExtTests
    {
        [TestMethod()]
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
    }
}