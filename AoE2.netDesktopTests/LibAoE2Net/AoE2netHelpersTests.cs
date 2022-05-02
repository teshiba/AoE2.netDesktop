using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Threading.Tasks;
using AoE2NetDesktop.Tests;
using LibAoE2net;
using System.Net.Http;
using System.Runtime.Serialization;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class AoE2netHelpersTests
    {
        [ClassInitialize]
        public static void Init(TestContext context)
        {
            if (context is null) {
                throw new ArgumentNullException(nameof(context));
            }

            StringsExt.InitAsync();
        }

        [TestInitialize]
        public void InitTest()
        {
            AoE2net.ComClient = new TestHttpClient();
        }

        [TestMethod()]
        [DataRow(IdType.Steam, TestData.AvailableUserSteamId)]
        [DataRow(IdType.Profile, TestData.AvailableUserProfileIdString)]
        public void GetPlayerLastMatchAsyncTest(IdType idType, string id)
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(idType, id)
                ).Result;

            // Assert
            Assert.AreEqual(TestData.AvailableUserSteamId, actVal.SteamId);
            Assert.AreEqual(TestData.AvailableUserProfileId, actVal.ProfileId);
            Assert.AreEqual(3333, actVal.LastMatch.Players[2].Rating);
        }

        [TestMethod()]
        public void GetPlayerLastMatchAsyncTestInvalidArg()
        {
            // Arrange

            // Act
            var actVal = Task.Run(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(IdType.NotSelected, TestData.AvailableUserSteamId)
                ).Result;

            // Assert
            Assert.IsNull(actVal.ProfileId);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// ThrowsExceptionAsync test
        //////////////////////////////////////////////////////////////////////////////////////////

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestSerializationException()
        {
            // Arrange

            _ = await Assert.ThrowsExceptionAsync<SerializationException>(
                () => AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, "SerializationException")
                ).ConfigureAwait(false);
        }

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestNullAsync()
        {
            // Arrange

            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() =>
                AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, null)
            );
        }

        [TestMethod()]
        public async Task GetPlayerLastMatchAsyncTestHttpRequestExceptionAsync()
        {
            // Arrange
            AoE2net.ComClient = new TestHttpClient() {
                ForceHttpRequestException = true,
            };
            // Act
            // Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() =>
                AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Steam, TestData.AvailableUserSteamId)
            );
        }
    }
}