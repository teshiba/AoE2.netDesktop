namespace LibAoE2net.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StringIdTests
{
    [TestMethod]
    public void ToStringTest()
    {
        // Arrange
        var expVal = "1:testString";

        // Act
        var testClass = new AoE2NetDesktop.LibAoE2Net.JsonFormat.StringId {
            Id = 1,
            String = "testString",
        };
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
