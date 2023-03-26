namespace AoE2NetDesktop.LibAoE2Net.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using AoE2NetDesktop.LibAoE2Net;
    using AoE2NetDesktop.LibAoE2Net.Functions;
    using AoE2NetDesktop.LibAoE2Net.Parameters;
    using AoE2NetDesktop.Utility;

    using AoE2NetDesktopTests.TestData;
    using AoE2NetDesktopTests.TestUtility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AoE2netHelpersTests
    {
        [TestMethod]
        [SuppressMessage("warning", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
        public void GetPlayerMatchHistoryAllAsyncTest()
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerMatchHistoryAllAsync(TestData.AvailableUserProfileId))
                .Result;

            // Assert
            Assert.AreEqual(actVal.Count, 9);
            Assert.AreEqual(actVal[0].Players[0].Won, null);
            Assert.AreEqual(actVal[1].Players[0].Won, null);
            Assert.AreEqual(actVal[2].Players[0].Won, null);
            Assert.AreEqual(actVal[3].Players[0].Won, true);
            Assert.AreEqual(actVal[4].Players[0].Won, false);
            Assert.AreEqual(actVal[5].Players[0].Won, null);
            Assert.AreEqual(actVal[6].Players[0].Won, false);
            Assert.AreEqual(actVal[7].Players[0].Won, true);
            Assert.AreEqual(actVal[8].Players[0].Won, false);
        }

        [TestMethod]
        [DataRow(IdType.Steam, TestData.AvailableUserSteamId)]
        [DataRow(IdType.Profile, TestData.AvailableUserProfileIdString)]
        [SuppressMessage("warning", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
        public void GetPlayerLastMatchAsyncTest(IdType idType, string id)
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(idType, id))
                .Result;

            // Assert
            Assert.AreEqual(TestData.AvailableUserSteamId, actVal.SteamId);
            Assert.AreEqual(TestData.AvailableUserProfileId, actVal.ProfileId);
            Assert.AreEqual(333, actVal.LastMatch.Players[2].Rating);
        }

        [TestMethod]
        [SuppressMessage("warning", "VSTHRD002:Avoid problematic synchronous waits", Justification = "Intentional sync test")]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = "Intentional sync test")]
        public void GetPlayerLastMatchAsyncTestInvalidLeaderboardId()
        {
            // Arrange
            var testHttpClient = new TestHttpClient() {
                PlayerMatchHistoryUri = "playerMatchHistoryaoe2deInvalidLeaderboardId.json",
            };
            AoE2net.ComClient = testHttpClient;

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, TestData.AvailableUserProfileIdString))
                .Result;

            // Assert
            Assert.AreEqual(null, actVal.LastMatch.Players[1].Name);

            // Cleanup
            testHttpClient.PlayerMatchHistoryUri = null;
        }

        [TestMethod]
        [SuppressMessage("warning", "VSTHRD002:Avoid problematic synchronous waits", Justification = "Intentional sync test")]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = "Intentional sync test")]
        public void GetPlayerLastMatchAsyncTestWithAIPlayer()
        {
            // Arrange
            var testHttpClient = new TestHttpClient() {
                PlayerMatchHistoryUri = "playerMatchHistoryaoe2deAIPlayer.json",
            };
            AoE2net.ComClient = testHttpClient;

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, TestData.AvailableUserProfileIdString))
                .Result;

            // Assert
            Assert.AreEqual("A.I.", actVal.LastMatch.Players[1].Name);

            // Cleanup
            testHttpClient.PlayerMatchHistoryUri = null;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        // ThrowsExceptionAsync test
        //////////////////////////////////////////////////////////////////////////////////////////

        [TestMethod]
        public async Task GetPlayerLastMatchAsyncTestSerializationExceptionAsync()
        {
            // Arrange
            _ = await Assert.ThrowsExceptionAsync<SerializationException>(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, "SerializationException"))
                .ConfigureAwait(false);
        }

        [TestMethod]
        public async Task GetPlayerLastMatchAsyncTestNullAsync()
        {
            // Arrange

            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, null));
        }

        [TestMethod]
        public async Task GetPlayerLastMatchAsyncTestHttpRequestExceptionAsync()
        {
            // Arrange
            AoE2net.ComClient.TestHttpClient().ForceHttpRequestException = true;
            AoE2net.ComClient.TestHttpClient().ForceHttpStatusCode = HttpStatusCode.NotFound;

            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<ComClientException>(() =>
                AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, TestData.AvailableUserSteamId));

            // cleanup
            AoE2net.ComClient.TestHttpClient().ForceHttpRequestException = false;
        }
    }
}