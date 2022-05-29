namespace AoE2NetDesktop.PlotEx.Tests;

using AoE2NetDesktop.PlotEx;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

[TestClass]
public class BarPlotExTests
{
    [TestMethod]
    public void BarPlotExTest()
    {
        // Arrange
        var expVal = Orientation.Horizontal;

        // Act
        var testClass = new BarPlotEx(new FormsPlot());
        var actVal = testClass.Orientation;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
