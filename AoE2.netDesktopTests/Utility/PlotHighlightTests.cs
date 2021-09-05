using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScottPlot;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class PlotHighlightTests
    {
        [TestMethod()]
        public void PlotHighlightTest()
        {
            // Arrange

            // Act
            var formsPlot = new FormsPlot();
            var scatterPlot = formsPlot.Plot.AddScatter(new double[] { 0 }, new double[] { 0 });
            var testClass = new PlotHighlight(formsPlot, scatterPlot);
            testClass.UpdateHighlight();

            // Assert
        }
    }
}