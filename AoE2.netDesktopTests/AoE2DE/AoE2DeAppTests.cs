namespace AoE2NetDesktop.AoE2DE.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;

[TestClass]
public class AoE2DeAppTests
{
    [TestMethod]
    public void GetPathTest()
    {
        // Arrange
        var expVal = @"steamapps\common\AoE2DE\";

        // Act
        var actVal = AoE2DeApp.GetPath();

        // Assert
        Debug.Print($"actVal = {actVal}");
        Debug.Print($"expVal = {expVal}");
        Assert.IsTrue(actVal.Contains(expVal));
    }

    [TestMethod]
    [DataRow("Aztecs", @"steamapps\common\AoE2DE\widgetui\textures\menu\civs\aztecs.png", "../../../TestData/dummy.png")]
    [DataRow("Hindustanis", @"steamapps\common\AoE2DE\widgetui\textures\menu\civs\indians.png", "../../../TestData/dummy.png")]
    [DataRow("", @"../../../TestData/dummy.png", @"../../../TestData/dummy.png")]
    public void GetCivImageLocationTest(string civ, string expVal1, string expVal2)
    {
        // Arrange
        AoE2net.ComClient = new TestHttpClient();

        // Act
        var actVal = AoE2DeApp.GetCivImageLocation(civ);

        // Assert
        Debug.Print($"actVal = {actVal}");
        Assert.IsTrue(actVal.Contains(expVal1) | actVal.Contains(expVal2));
    }
}
