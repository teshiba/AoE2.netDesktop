using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class StringsExtTests
    {
        [TestMethod()]
        public void GetStringTest()
        {
            // Arrange
            var expVal = "testString21";
            List<StringId> stringIds = new List<StringId> {
                new StringId(){Id = 1, String = "testString1"},
                new StringId(){Id = 2, String = "testString21"},
                new StringId(){Id = 2, String = "testString22"},
                new StringId(){Id = 3, String = "testString3"},
            };

            // Act
            var actVal = stringIds.GetString(2);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}