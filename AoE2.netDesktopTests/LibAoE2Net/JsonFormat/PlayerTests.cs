namespace LibAoE2net.Tests;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PlayerTests
{
    [TestMethod]
    public void ToStringTestRatingChange()
    {
        // Arrange
        var expVal = "[2]testName(R:1234+10) ID:54321";

        // Act
        var testClass = new Player {
            Color = 2,
            Name = "testName",
            Rating = 1234,
            ProfilId = 54321,
            RatingChange = "10",
        };
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void ToStringTestRatingChangeNegative()
    {
        // Arrange
        var expVal = "[2]testName(R:1234-10) ID:54321";

        // Act
        var testClass = new Player {
            Color = 2,
            Name = "testName",
            Rating = 1234,
            ProfilId = 54321,
            RatingChange = "-10",
        };
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void ToStringTestRatingChangeNull()
    {
        // Arrange
        var expVal = "[2]testName(R:1234) ID:54321";

        // Act
        var testClass = new Player {
            Color = 2,
            Name = "testName",
            Rating = 1234,
            ProfilId = 54321,
            RatingChange = null,
        };
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
