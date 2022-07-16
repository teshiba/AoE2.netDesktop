namespace AoE2NetDesktop.Utility.SysApi.Tests
{
    using System;
    using System.Collections.Generic;
    using AoE2NetDesktop.Utility.SysApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DateTimeExtTests
    {
        private static IEnumerable<object[]> GetTestData => new List<object[]>
        {
            new object[] { TimeZoneInfo.Local.ToSerializedString() },
            new object[] { TimeZoneInfo.Utc.ToSerializedString() },
        };

        [TestMethod]
        [DynamicData(nameof(GetTestData))]
        public void SpZoneDateTimeTest(string timeZoneInfo)
        {
            // Arrange
            DateTimeExt.TimeZoneInfo = TimeZoneInfo.FromSerializedString(timeZoneInfo);
            var expVal = new DateTime(1980, 1, 1);
            var dateTimeOffset = new DateTimeOffset(expVal, DateTimeExt.TimeZoneInfo.BaseUtcOffset);

            // Act
            var actVal = DateTimeExt.SpZoneDateTime(dateTimeOffset);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod]
        [DataRow(1970, 1, 1, 0, 0, 0, null)]
        [DataRow(1969, 12, 31, 23, 59, 59, -1L)]
        [DataRow(1970, 1, 1, 0, 0, 0, 0L)]
        [DataRow(1970, 1, 1, 1, 0, 0, 3600L)]
        public void FromUnixTimeSecondsTest(int year, int month, int day, int hour, int minute, int second, long? expSecond)
        {
            // Arrange
            DateTimeExt.TimeZoneInfo = TimeZoneInfo.Local;
            var expVal = new DateTime(year, month, day, hour, minute, second).ToLocalTime();

            // Act
            var actVal = DateTimeExt.FromUnixTimeSeconds(expSecond);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}