namespace AoE2NetDesktop.Form.Tests;

using AoE2netDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SystemApiStubTests
{
    [TestMethod]
    public void GetActiveProcessTest()
    {
        // Arrange
        var expVal = "AoE2DE_s";

        // Act
        var testClass = new SystemApiStub(1);
        var actVal = testClass.GetActiveProcess();

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetProcessFilePathTest()
    {
        // Arrange
        var expVal = @"c:\AoE2DE_s\is\not\installed\at\steamapps\common\AoE2DE\";

        // Act
        var testClass = new SystemApiStub(1);
        var actVal = testClass.GetProcessFilePath("AoE2DE_s");

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
