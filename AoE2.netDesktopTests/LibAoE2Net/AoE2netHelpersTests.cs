namespace AoE2NetDesktop.Form.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    using AoE2NetDesktop.LibAoE2Net;
    using AoE2NetDesktop.LibAoE2Net.Functions;
    using AoE2NetDesktop.LibAoE2Net.Parameters;
    using AoE2NetDesktop.Tests;
    using AoE2NetDesktop.Utility;

    using AoE2netDesktopTests.TestUtility;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class AoE2netHelpersTests
    {
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
            Assert.AreEqual(3333, actVal.LastMatch.Players[2].Rating);
        }

        [TestMethod]
        [SuppressMessage("warning", "VSTHRD002:Avoid problematic synchronous waits", Justification = "Intentional sync test")]
        [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = "Intentional sync test")]
        public void GetPlayerLastMatchAsyncTestInvalidArg()
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(IdType.NotSelected, TestData.AvailableUserSteamId))
                .Result;

            // Assert
            Assert.IsNull(actVal.ProfileId);
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

            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() =>
                AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, TestData.AvailableUserSteamId));

            // cleanup
            AoE2net.ComClient.TestHttpClient().ForceHttpRequestException = false;
        }
    }
}