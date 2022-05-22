namespace AoE2NetDesktop.AoE2DE.Tests;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

[TestClass]
public class UnitImagesTests
{
    [TestMethod]
    public void GetFileNameTest()
    {
        // Arrange
        var files = typeof(UnitImages).GetField<Dictionary<string, string>>("FileNames");
        foreach (var item in files) {
            // Act
            var actVal = File.Exists(UnitImages.GetFileName(item.Key));

            // Assert
            Assert.IsTrue(actVal);
        }
    }

    [TestMethod]
    public void LoadTest()
    {
        // Arrange
        var expVal = 256;

        // Act
        var actVal = UnitImages.Load("Britons", Color.Blue);

        // Assert
        Assert.AreEqual(expVal, actVal.Width);
        Assert.AreEqual(expVal, actVal.Height);
    }
}
