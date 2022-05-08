using AoE2NetDesktop.Utility.User32;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class SystemApiTests
    {
        [TestMethod()]
        public void GetActiveProcessTest()
        {
            // Arrange
            var expVal = "Idle";
            var user32api = new User32ApiStub {
                ProcessId = 0
            };
            var testClass = new SystemApi(user32api);

            // Act
            var actVal = testClass.GetActiveProcess();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetActiveProcessTestWin32Exception()
        {
            // Arrange
            var expVal = string.Empty;
            var user32api = new User32ApiStub {
                ProcessId = -1
            };
            var testClass = new SystemApi(user32api);
            // Act
            var ret = testClass.GetActiveProcess();

            // Assert
            Assert.AreEqual(expVal, ret);
        }
    }
}
