using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScottPlot;
using System;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class PlotHighlightTests
    {
        [TestMethod()]
        public void UpdateHighlightTest()
        {
            // Arrange
            var formsPlot = new FormsPlot();
            var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });
            var testClass = new PlotHighlight(formsPlot, scatterPlot);

            // Act
            testClass.Update();
            testClass.Update();

            // Assert
        }

        [TestMethod()]
        public void PlotHighlightTestformsPlotNull()
        {
            // Arrange
            var formsPlot = new FormsPlot();
            var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new PlotHighlight(null, scatterPlot);
            });
        }

        [TestMethod()]
        public void PlotHighlightTestscatterPlotNull()
        {
            // Arrange
            var formsPlot = new FormsPlot();

            // Act
            // Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                _ = new PlotHighlight(formsPlot, null);
            });
        }

        [TestMethod()]
        public void UpdateHighlightTestGetIsVisible()
        {
            // Arrange
            bool expVal = true;
            var formsPlot = new FormsPlot();
            var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });
            var testClass = new PlotHighlight(formsPlot, scatterPlot);

            // Act
            var actVal = testClass.IsVisible;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void UpdateHighlightTestSetIsVisible()
        {
            // Arrange
            bool expVal = false;
            var formsPlot = new FormsPlot();
            var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });
            var testClass = new PlotHighlight(formsPlot, scatterPlot);

            // Act
            testClass.IsVisible = !testClass.IsVisible;
            var actVal = testClass.IsVisible;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}