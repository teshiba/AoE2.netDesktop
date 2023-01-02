namespace AoE2NetDesktop.Properties.Tests;

using System.Drawing;
using System.Globalization;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ResourcesTest
{
    [TestMethod]
    public void GetResourceManagerTest()
    {
        // Arrange
        var expVal = "AoE2NetDesktop.Properties.Resources";

        // Act
        var actVal = Resources.ResourceManager.BaseName;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void SetAndGetCultureTest()
    {
        // Arrange
        var expVal = "ar";

        // Act
        Resources.Culture = new CultureInfo(1);
        var actVal = Resources.Culture;

        // Assert
        Assert.AreEqual(expVal, actVal.Name);
    }

    [TestMethod]
    public void Getaoe2netDesktopAppIconTest()
    {
        // Arrange
        var expVal = new Size(32, 32);

        // Act
        var actVal = Resources.aoe2netDesktopAppIcon.Size;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
