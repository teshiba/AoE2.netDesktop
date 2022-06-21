namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.PlotEx;
using AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

using System;
using System.Collections.Generic;
using System.Drawing;

[TestClass]
public class PlayerRatePlotTests
{
    [TestMethod]
    public void PlayerRatePlotTestGetIsVisible()
    {
        // Arrange
        var expVal = true;
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red);

        // Act
        var actVal = testClass.IsVisible;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void PlayerRatePlotTestSetIsVisible()
    {
        // Arrange
        var expVal = false;
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red);

        // Act
        testClass.IsVisible = !testClass.IsVisible;
        var actVal = testClass.IsVisible;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void PlayerRatePlotTestGetIsVisibleHighlight()
    {
        // Arrange
        var expVal = true;
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red);

        // Act
        var actVal = testClass.IsVisibleHighlight;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void PlayerRatePlotTestGetIsVisibleHighlightAfterSetIsVisible(bool isVisible)
    {
        // Arrange
        var expVal = isVisible;
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red) {
            IsVisible = isVisible,
        };

        // Act
        var actVal = testClass.IsVisibleHighlight;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void PlayerRatePlotTestSetIsVisibleHighlightIfVisibleFalse(bool isVisibleHighlight)
    {
        // Arrange
        var expVal = false;
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red) {
            IsVisible = false,
        };

        // Act
        testClass.IsVisibleHighlight = isVisibleHighlight;
        var actVal = testClass.IsVisibleHighlight;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void PlayerRatePlotTestSetIsVisibleHighlightIfVisibleTrue(bool isVisibleHighlight)
    {
        // Arrange
        var expVal = isVisibleHighlight;
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red) {
            IsVisible = true,
        };

        // Act
        testClass.IsVisibleHighlight = isVisibleHighlight;
        var actVal = testClass.IsVisibleHighlight;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void UpdateHighlightTest()
    {
        // Arrange
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red);

        // Act
        testClass.UpdateHighlight();

        // Assert
    }

    [TestMethod]
    public void PlotTest()
    {
        // Arrange
        List<PlayerRating> playerRatingHistory = new() {
            new PlayerRating() { Rating = 1, TimeStamp = 0 },
            new PlayerRating() { Rating = 1, TimeStamp = 0 },
            new PlayerRating() { Rating = 1, TimeStamp = 0 },
            new PlayerRating() { Rating = 1, TimeStamp = 0 },
        };
        var testClass = new PlayerRatePlot(new FormsPlot(), Color.Red);

        // Act
        testClass.Plot(playerRatingHistory, LeaderboardId.RM1v1);

        // Assert
    }
}
