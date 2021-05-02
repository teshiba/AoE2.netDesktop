using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibAoE2net.Tests
{
    [TestClass()]
    public class StringIdTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            // Arrange
            var expVal = "1:testString";

            // Act
            var testClass = new StringId {
                Id = 1,
                String = "testString",
            };
            var actVal = testClass.ToString();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}