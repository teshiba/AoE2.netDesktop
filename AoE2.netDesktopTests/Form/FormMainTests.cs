using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;

namespace AoE2NetDesktop.From.Tests
{
    [TestClass()]
    [TestCategory("GUI")]
    [Ignore]
    public class FormMainTests
    {
        [TestMethod()]
        public void FormMainTest()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();

            // Act
            var testClass = new FormMain(Language.en);
            string actVal = "";
            testClass.Show();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}