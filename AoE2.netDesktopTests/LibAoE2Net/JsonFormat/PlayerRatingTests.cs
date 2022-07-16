namespace AoE2NetDesktop.LibAoE2Net.JsonFormat.Tests
{
    using System;
    using System.Globalization;

    using AoE2NetDesktop.LibAoE2Net.JsonFormat;
    using AoE2NetDesktop.Utility.SysApi;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PlayerRatingTests
    {
        [TestMethod]
        public void ToStringTest()
        {
            // Arrange
            DateTimeExt.TimeZoneInfo = TimeZoneInfo.Utc;
            DateTimeExt.DateTimeFormatInfo = DateTimeFormatInfo.InvariantInfo;
            var expDrops = 1;
            var expNumLosses = 2;
            var expNumWins = 3;
            var expRating = 1234;
            var expStreak = 5;
            var expTimeStamp = 0;
            var expVal = $"R:{expRating} W:{expNumWins} L:{expNumLosses} Str:{expStreak} Drp:{expDrops} Time:01/01/1970 00:00:00";

            // Act
            var testClass = new PlayerRating() {
                Drops = expDrops,
                NumLosses = expNumLosses,
                NumWins = expNumWins,
                Rating = expRating,
                Streak = expStreak,
                TimeStamp = expTimeStamp,
            };
            var actVal = testClass.ToString();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}