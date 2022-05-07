using AoE2NetDesktop.Utility.User32;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class SystemApiStubTests
    {
        [TestMethod()]
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

        [TestMethod()]
        public void GetProcessFilePathTest()
        {
            // Arrange
            var expVal = "AoE2DE_s";

            // Act
            var testClass = new SystemApiStub(1);
            var actVal = testClass.GetProcessFilePath(expVal);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}