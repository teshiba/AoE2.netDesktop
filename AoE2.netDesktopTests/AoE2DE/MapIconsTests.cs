namespace AoE2NetDesktop.AoE2DE.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MapIconsTests
{
    [TestMethod]
    [DataRow(null, "cm_generic.DDS")]
    [DataRow(9, "rm_arabia.DDS")]
    public void GetFileNameTest(int? mapId, string mapName)
    {
        // Arrange

        // Act
        var actVal = MapIcons.GetFileName(mapId);

        // Assert
        Assert.IsTrue(actVal.Contains(mapName));
    }
}
