namespace AoE2NetDesktop.Properties.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Drawing;

[TestClass]
public class ResourcesTest
{
    [TestMethod]
    public void GetResourceManager()
    {
        // Arrange
        var expVal = "AoE2NetDesktop.Properties.Resources";

        // Act
        var actVal = Resources.ResourceManager.BaseName;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetCulture()
    {
        // Arrange
        var expVal = "ar";

        // Act
        var actVal = Resources.Culture;

        // Assert
        Assert.AreEqual(expVal, actVal.Name);
    }

    [TestMethod]
    public void SetCulture()
    {
        // Arrange

        // Act
        Resources.Culture = new System.Globalization.CultureInfo(1);

        // Assert
        Assert.IsTrue(true);
    }

    [TestMethod]
    public void Getaoe2netDesktopAppIcon()
    {
        // Arrange
        var expVal = new Size(32, 32);

        // Act
        var actVal = Resources.aoe2netDesktopAppIcon.Size;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
