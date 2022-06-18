namespace AoE2NetDesktop.AoE2DE.Tests;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.Tests;

using AoE2netDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Drawing;

[TestClass]
public class UnitImagesTests
{
    [TestMethod]
    public void GetFileNameTest()
    {
        // Arrange
        var files = typeof(UnitImages).GetField<Dictionary<string, string>>("FileNames");
        foreach(var item in files) {
            // Act
            var actVal = UnitImages.GetFileName(item.Key);

            // Assert
            Assert.IsFalse(actVal.Contains("265_50730.DDS"));
        }
    }

    [TestMethod]
    public void LoadTest()
    {
        // Arrange
        AoE2DeApp.SystemApi = new SystemApiStub(1);
        var expVal = 1;

        // Act
        var actVal = UnitImages.Load("invalidCiv", Color.Blue);

        // Assert
        Assert.AreEqual(expVal, actVal.Width);
        Assert.AreEqual(expVal, actVal.Height);
    }
}
