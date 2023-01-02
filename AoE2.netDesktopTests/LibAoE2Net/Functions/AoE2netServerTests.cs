namespace LibAoE2net.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;

    using AoE2NetDesktop.Utility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    [TestCategory("ServerTest")]
    [Ignore]
    public class AoE2netServerTests
    {
        private const int HttpTimeoutSec = 20;
        private static readonly Uri BaseAddress = new($"https://aoe2.net/");
        private static readonly Uri ApiAddress = new($"{BaseAddress}api/");
        private static readonly Uri CivImageAddress = new($"{BaseAddress}/assets/images/crests/25x25/");
        private static readonly Uri ProfileIdAddress = new($"{BaseAddress}#aoe2de-profile-");

        [TestMethod]
        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
        public void BaseAddressTest()
        {
            // Arrange
            var testClass = new HttpClient {
                BaseAddress = BaseAddress,
                Timeout = TimeSpan.FromSeconds(HttpTimeoutSec),
            };
            var notExpVal = string.Empty;
            var actVal = string.Empty;
            bool done;

            try {
                // Act
                actVal = testClass.GetStringAsync(string.Empty).Result;
                done = true;
            } catch(Exception) {
                done = false;
            }

            // Assert
            Assert.AreNotEqual(actVal, notExpVal);
            Assert.IsTrue(done);
        }

        [TestMethod]
        [DataRow("strings")]
        [DataRow("leaderboard?game=aoe2de&leaderboard_id=1&start=1&count=1")]
        [DataRow("player/ratinghistory?game=aoe2de&leaderboard_id=3&profile_id=1")]
        [DataRow("player/matches?game=aoe2de&profile_id=1")]
        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
        public void ApiAddressTest(string endpoint)
        {
            // Arrange
            var testClass = new HttpClient {
                BaseAddress = ApiAddress,
                Timeout = TimeSpan.FromSeconds(HttpTimeoutSec),
            };
            var notExpVal = string.Empty;
            var actVal = string.Empty;
            bool done;

            try {
                // Act
                actVal = testClass.GetStringAsync(endpoint).Result;
                done = true;
            } catch(Exception) {
                done = false;
            }

            // Assert
            Assert.AreNotEqual(actVal, notExpVal);
            Assert.IsTrue(done);
        }

        [TestMethod]
        [DataRow("berbers.png")]
        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
        public void CivImageAddressTest(string civName)
        {
            // Arrange
            var testClass = new HttpClient {
                BaseAddress = CivImageAddress,
                Timeout = TimeSpan.FromSeconds(HttpTimeoutSec),
            };
            var notExpVal = string.Empty;
            var actVal = string.Empty;
            bool done;

            try {
                // Act
                actVal = testClass.GetStringAsync(civName).Result;
                done = true;
            } catch(Exception) {
                done = false;
            }

            // Assert
            Assert.AreNotEqual(actVal, notExpVal);
            Assert.IsTrue(done);
        }

        [TestMethod]
        [DataRow("1")]
        [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
        public void ProfileIdAddressTest(string profileId)
        {
            // Arrange
            var testClass = new HttpClient {
                BaseAddress = new Uri($"{ProfileIdAddress.AbsoluteUri}{profileId}"),
                Timeout = TimeSpan.FromSeconds(HttpTimeoutSec),
            };
            var notExpVal = string.Empty;
            var actVal = string.Empty;
            bool done;

            try {
                // Act
                actVal = testClass.GetStringAsync(string.Empty).Result;
                done = true;
            } catch(Exception) {
                done = false;
            }

            // Assert
            Assert.AreNotEqual(actVal, notExpVal);
            Assert.IsTrue(done);
        }
    }
}