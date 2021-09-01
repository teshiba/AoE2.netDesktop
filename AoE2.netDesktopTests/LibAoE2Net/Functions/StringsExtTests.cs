using LibAoE2net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class StringsExtTests
    {
        [TestMethod()]
        [DataRow(-1, null)]
        [DataRow(0, null)]
        [DataRow(1, "testString1")]
        [DataRow(2, "testString21")]
        [DataRow(3, "testString3")]
        [DataRow(4, null)]
        public void GetStringTest(int id, string expVal)
        {
            // Arrange
            List<StringId> stringIds = new List<StringId> {
                new StringId(){Id = 1, String = "testString1"},
                new StringId(){Id = 2, String = "testString21"},
                new StringId(){Id = 2, String = "testString22"},
                new StringId(){Id = 3, String = "testString3"},
            };

            // Act
            var actVal = stringIds.GetString(id);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetOpenedTimeTest()
        {
            // Arrange
            var expVal = DateTime.Now.ToLocalTime();
            var dateTimeSec = new DateTimeOffset(expVal).ToUnixTimeSeconds();

            // Act
            var testClass = new Match() {
                Opened = dateTimeSec,
            };

            var actVal = testClass.GetOpenedTime();

            // Assert
            Assert.AreEqual(expVal.ToString(), actVal.ToString());
        }
    }
}