namespace AoE2NetDesktop.AoE2DE.Tests;

using System.Collections.Generic;
using System.Drawing;

using AoE2NetDesktop.AoE2DE;

using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        AoE2DeApp.SystemApi = new SystemApiStub(1) {
            AoE2deAppStatus = AppStatus.NotInstalled,
        };
        var expVal = 1;

        // Act
        var actVal = UnitImages.Load("invalidCiv", Color.Blue);

        // Assert
        Assert.AreEqual(expVal, actVal.Width);
        Assert.AreEqual(expVal, actVal.Height);
    }
}
