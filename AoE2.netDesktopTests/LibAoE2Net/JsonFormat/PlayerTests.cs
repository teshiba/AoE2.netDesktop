﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            // Arrange
            var expVal = "[2]testName(R:1234) ID:1234567890 54321";

            // Act
            var testClass = new Player {
                Color = 2,
                Name = "testName",
                Rating = 1234,
                SteamId = "1234567890",
                ProfilId = 54321,
            };
            var actVal = testClass.ToString();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}