using Microsoft.VisualStudio.TestTools.UnitTesting;

using LibAoE2net;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class CtrlHistoryTests
    {
        [TestMethod()]
        public void CtrlHistoryTest()
        {
            // Arrange
            var expVal = 1;

            // Act
            var testClass = new CtrlHistory(expVal);
            var actVal = testClass.ProfileId;

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetRatingStringTestInvalid()
        {
            // Arrange
            var expVal = "----";
            var player = new Player()
            {
                Rating = null,
            };

            // Act
            var actVal = CtrlHistory.GetRatingString(player);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetRatingStringTestInc()
        {
            // Arrange
            var expVal = "1234+456";
            var player = new Player() { 
                Rating = 1234,
                RatingChange = "456",
            };

            // Act
            var actVal = CtrlHistory.GetRatingString(player);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void GetRatingStringTestDec()
        {
            // Arrange
            var expVal = "1234-456";
            var player = new Player()
            {
                Rating = 1234,
                RatingChange = "-456",
            };

            // Act
            var actVal = CtrlHistory.GetRatingString(player);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        [DataRow(null, "---")]
        [DataRow(true, "o")]
        [DataRow(false, "")]
        public void GetWinMarkerStringInvalid(bool? won, string expVal)
        {
            // Arrange

            // Act
            var actVal = CtrlHistory.GetWinMarkerString(won);

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void ReadPlayerMatchHistoryAsyncTest()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient();
            var expVal = 2;

            // Act
            var testClass = new CtrlHistory(TestData.AvailableUserProfileId);
            var ret = testClass.ReadPlayerMatchHistoryAsync().Result;
            var actVal = testClass.PlayerMatchHistory;

            // Assert
            Assert.IsTrue(ret);
            Assert.AreEqual(expVal, actVal.Count);
        }
    }
}