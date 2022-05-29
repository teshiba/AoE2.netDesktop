namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.PlotEx;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

using System;

[TestClass]
public class PlotHighlightTests
{
    [TestMethod]
    public void UpdateTestDataIsNaN()
    {
        // Arrange
        var formsPlot = new FormsPlot();
        var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 10 }, new double[] { 20 });
        var testClass = new PlotHighlight(formsPlot, scatterPlot, string.Empty);

        // Act
        var ret = testClass.Update();

        // Assert
        Assert.AreEqual((0, 0), ret);
    }

    [TestMethod]
    public void UpdateTest()
    {
        // Arrange
        var formsPlot = new FormsPlot();
        var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 10 }, new double[] { 20 });
        var testClass = new PlotHighlight(formsPlot, scatterPlot, string.Empty);
        formsPlot.Render();

        // Act
        testClass.Update();
        var ret = testClass.Update();

        // Assert
        Assert.AreEqual((10, 20), ret);
    }

    [TestMethod]
    public void PlotHighlightTestformsPlotNull()
    {
        // Arrange
        var formsPlot = new FormsPlot();
        var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });

        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            _ = new PlotHighlight(null, scatterPlot, string.Empty);
        });
    }

    [TestMethod]
    public void PlotHighlightTestscatterPlotNull()
    {
        // Arrange
        var formsPlot = new FormsPlot();

        // Act
        // Assert
        Assert.ThrowsException<ArgumentNullException>(() =>
        {
            _ = new PlotHighlight(formsPlot, null, string.Empty);
        });
    }

    [TestMethod]
    public void PlotHighlightTestGetIsVisible()
    {
        // Arrange
        bool expVal = true;
        var formsPlot = new FormsPlot();
        var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });
        var testClass = new PlotHighlight(formsPlot, scatterPlot, string.Empty);

        // Act
        var actVal = testClass.IsVisible;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void PlotHighlightTestSetIsVisible()
    {
        // Arrange
        bool expVal = false;
        var formsPlot = new FormsPlot();
        var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });
        var testClass = new PlotHighlight(formsPlot, scatterPlot, string.Empty);

        // Act
        testClass.IsVisible = !testClass.IsVisible;
        var actVal = testClass.IsVisible;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
