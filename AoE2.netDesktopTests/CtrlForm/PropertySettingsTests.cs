namespace AoE2NetDesktop.CtrlForm.Tests;

using System.ComponentModel;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PropertySettingsTests
{
    [TestMethod]
    public void ChromaKeyTest()
    {
        // Arrange
        var expVal = "#123456";
        var chromaKey = ColorTranslator.FromHtml(expVal);

        // Act
        var testClass = new PropertySettings();
        testClass.PropertyChanged += OnChangeProperty;
        testClass.ChromaKey = $"#{chromaKey.R:X02}{chromaKey.G:X02}{chromaKey.B:X02}";

        // Assert
        Assert.AreEqual(expVal, testClass.ChromaKey);
    }

    [TestMethod]
    public void IsHideTitleTest()
    {
        // Arrange
        var expVal = true;

        // Act
        var testClass = new PropertySettings();
        testClass.PropertyChanged += OnChangeProperty;
        testClass.IsHideTitle = expVal;

        // Assert
        Assert.AreEqual(expVal, testClass.IsHideTitle);
    }

    [TestMethod]
    public void IsAlwaysOnTopTest()
    {
        // Arrange
        var expVal = true;

        // Act
        var testClass = new PropertySettings();
        testClass.PropertyChanged += OnChangeProperty;
        testClass.IsAlwaysOnTop = expVal;

        // Assert
        Assert.AreEqual(expVal, testClass.IsAlwaysOnTop);
    }

    [TestMethod]
    public void IsTransparencyTest()
    {
        // Arrange
        var expVal = true;

        // Act
        var testClass = new PropertySettings();
        testClass.PropertyChanged += OnChangeProperty;
        testClass.IsTransparency = expVal;

        // Assert
        Assert.AreEqual(expVal, testClass.IsTransparency);
    }

    [TestMethod]
    public void DrawHighQualityTest()
    {
        // Arrange
        var expVal = true;

        // Act
        var testClass = new PropertySettings();
        testClass.PropertyChanged += OnChangeProperty;
        testClass.DrawHighQuality = expVal;

        // Assert
        Assert.AreEqual(expVal, testClass.DrawHighQuality);
    }

    private void OnChangeProperty(object sender, PropertyChangedEventArgs e)
    {
    }
}
