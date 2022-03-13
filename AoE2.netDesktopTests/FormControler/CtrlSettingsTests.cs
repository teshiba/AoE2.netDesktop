﻿using AoE2NetDesktop.Form;
using AoE2NetDesktop.Tests;
using LibAoE2net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScottPlot;

using System;
using System.Threading.Tasks;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class CtrlSettingsTests
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
        public void CtrlSettingsTest()
        {
            // Arrange
            // Act
            var testClass = new CtrlSettings();

            // Assert
            Assert.AreEqual("N/A", testClass.UserCountry);
            Assert.AreEqual("-- Invalid ID --", testClass.UserName);
        }

        [TestMethod()]
        [DataRow(IdType.Steam, TestData.AvailableUserSteamId)]
        [DataRow(IdType.Profile, TestData.AvailableUserProfileIdString)]
        [DataRow(IdType.Profile, TestData.AvailableUserProfileIdWithoutSteamIdString)]
        public void ReloadProfileAsyncTest(IdType idType, string id)
        {
            // Arrange
            var notExpValUserCountry = "N/A";
            var notExpValUserName = "-- Invalid ID --";

            // Act
            var testClass = new CtrlSettings();
            var actVal = Task.Run(
                () => testClass.ReloadProfileAsync(idType, id)
                ).Result;

            // Assert
            Assert.IsTrue(actVal);
            Assert.AreNotEqual(notExpValUserCountry, testClass.UserCountry);
            Assert.AreNotEqual(notExpValUserName, testClass.UserName);
        }

        [TestMethod()]
        public void ReloadProfileAsyncTestException()
        {
            // Arrange
            var expValUserCountry = "N/A";
            var expValUserName = "-- Invalid ID --";

            // Act
            var testClass = new CtrlSettings() {
                SelectedIdType = IdType.Profile,
            };
            try {
                var actVal = Task.Run(
                    async () =>
                    {
                        // The following code can read the player data.
                        _ = await testClass.ReloadProfileAsync(IdType.Profile, TestData.AvailableUserProfileIdString);
                        // The following code cannot read the player data, so write null to playerLastmatch..
                        return await testClass.ReloadProfileAsync(IdType.Profile, "-1");
                    }
                    ).Result;
            } catch (Exception) {
            }

            // Assert
            Assert.AreEqual(expValUserCountry, testClass.UserCountry);
            Assert.AreEqual(expValUserName, testClass.UserName);
        }

        [TestMethod()]
        public void ReloadProfileAsyncTestInvalidArg()
        {
            // Arrange
            var expValUserCountry = "N/A";
            var expValUserName = "-- Invalid ID --";

            // Act
            var testClass = new CtrlSettings();
            var actVal = Task.Run(
                () => testClass.ReloadProfileAsync(IdType.NotSelected, TestData.AvailableUserSteamId)
                ).Result;

            // Assert
            Assert.IsTrue(actVal);
            Assert.AreEqual(expValUserCountry, testClass.UserCountry);
            Assert.AreEqual(expValUserName, testClass.UserName);
        }

        [TestMethod()]
        public void ReloadProfileAsyncTestAvailableUserProfileIdWithoutSteamIdString()
        {
            // Arrange
            var notExpValUserCountry = "N/A";
            var notExpValUserName = "-- Invalid ID --";


            // Act
            var testClass = new CtrlSettings();
            var actVal = Task.Run(
                () => testClass.ReloadProfileAsync(IdType.Profile, TestData.AvailableUserProfileIdWithoutSteamIdString)
                ).Result;

            // Assert
            Assert.IsTrue(actVal);
            Assert.AreNotEqual(notExpValUserCountry, testClass.UserCountry);
            Assert.AreNotEqual(notExpValUserName, testClass.UserName);
            Assert.AreEqual(TestData.AvailableUserProfileIdWithoutSteamId, testClass.ProfileId);
            Assert.IsNull(testClass.SteamId);
        }

        [TestMethod()]
        public void ShowMyHistoryTest()
        {
            // Arrange
            var testClass = new CtrlSettings();

            // Act
            testClass.ShowMyHistory();

            // Assert
            Assert.IsNotNull(testClass.FormHistory);
        }
    }
}