namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.PlotEx;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StackedBarGraphDataTests
{
    [TestMethod]
    public void StackedBarGraphDataTest()
    {
        // Arrange
        var expValLower = 1;
        var expValUpper = 2;

        // Act
        var testClass = new StackedBarGraphData(expValLower, expValUpper);
        var actValLower = testClass.Lower;
        var actValUpper = testClass.Upper;

        // Assert
        Assert.AreEqual(expValUpper, actValUpper);
        Assert.AreEqual(expValLower, actValLower);
    }

    [TestMethod]
    [DataRow(0D, 0D, "L:0 U:0")]
    [DataRow(0D, null, "L:0 U:null")]
    [DataRow(123D, 456D, "L:123 U:456")]
    public void ToStringTest(double lower, double? upper, string expVal)
    {
        // Arrange

        // Act
        var testClass = new StackedBarGraphData(lower, upper);
        var actVal = testClass.ToString();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
