using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibAoE2net;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class FormHistoryTests
    {
        [TestMethod()]
        public void FormHistoryTest()
        {
            // Arrange
            var expVal = string.Empty;
            AoE2net.ComClient = new TestHttpClient();
            var testClass = new FormHistory(TestData.AvailableUserProfileId);
            //_ = CtrlHistory.InitAsync(Language.en);

            // Act
            testClass.Shown += async (sender, e) =>
            {
                await testClass.Awaiter.WaitAsync("FormHistory_ShownAsync");
                testClass.Close();
            };

            testClass.ShowDialog();
        }
    }
}