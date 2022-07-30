namespace AoE2NetDesktop.CtrlForm.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility;

using LibAoE2net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

[TestClass]
public class CtrlSettingsTests
{
    [ClassInitialize]
    public static void Init(TestContext context)
    {
        if(context is null) {
            throw new ArgumentNullException(nameof(context));
        }

        StringsExt.Init();
    }

    [TestInitialize]
    public void InitTest()
    {
        AoE2net.ComClient = new TestHttpClient();
    }

    [TestMethod]
    public void CtrlSettingsTest()
    {
        // Arrange
        // Act
        var testClass = new CtrlSettings();

        // Assert
        Assert.AreEqual("N/A", testClass.UserCountry);
        Assert.AreEqual("-- Invalid ID --", testClass.UserName);
    }

    [TestMethod]
    [DataRow(IdType.Steam, TestData.AvailableUserSteamId)]
    [DataRow(IdType.Profile, TestData.AvailableUserProfileIdString)]
    [DataRow(IdType.Profile, TestData.AvailableUserProfileIdWithoutSteamIdString)]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReloadProfileAsyncTest(IdType idType, string id)
    {
        // Arrange
        var notExpValUserCountry = "N/A";
        var notExpValUserName = "-- Invalid ID --";

        // Act
        var testClass = new CtrlSettings();
        var actVal = Task.Run(
            () => testClass.ReloadProfileAsync(idType, id))
            .Result;

        // Assert
        Assert.IsTrue(actVal);
        Assert.AreNotEqual(notExpValUserCountry, testClass.UserCountry);
        Assert.AreNotEqual(notExpValUserName, testClass.UserName);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReloadProfileAsyncTestException()
    {
        // Arrange
        var expValUserCountry = "N/A";
        var expValUserName = "-- Invalid ID --";

        // Act
        var testClass = new CtrlSettings() {
            SelectedIdType = IdType.Profile,
        };
        var actVal = Task.Run(
            async () =>
            {
                // The following code can read the player data.
                _ = await testClass.ReloadProfileAsync(IdType.Profile, TestData.AvailableUserProfileIdString);

                // The following code cannot read the player data, so write null to playerLastmatch..
                return await testClass.ReloadProfileAsync(IdType.Profile, TestData.NotFoundUserProfileIdString);
            })
            .Result;

        // Assert
        Assert.AreEqual(expValUserCountry, testClass.UserCountry);
        Assert.AreEqual(expValUserName, testClass.UserName);
    }

    [TestMethod]
    public void ReloadProfileAsyncTestInvalidArg()
    {
        // Arrange
        var expValUserCountry = "N/A";
        var expValUserName = "-- Invalid ID --";
        var testClass = new CtrlSettings();

        // Assert
        _ = Assert.ThrowsExceptionAsync<Exception>(() =>

              // Act
              testClass.ReloadProfileAsync(IdType.NotSelected, TestData.AvailableUserSteamId));

        Assert.AreEqual(expValUserCountry, testClass.UserCountry);
        Assert.AreEqual(expValUserName, testClass.UserName);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReloadProfileAsyncTestAvailableUserProfileIdWithoutSteamIdString()
    {
        // Arrange
        var notExpValUserCountry = "N/A";
        var notExpValUserName = "-- Invalid ID --";

        // Act
        var testClass = new CtrlSettings();
        var actVal = Task.Run(
            () => testClass.ReloadProfileAsync(IdType.Profile, TestData.AvailableUserProfileIdWithoutSteamIdString))
            .Result;

        // Assert
        Assert.IsTrue(actVal);
        Assert.AreNotEqual(notExpValUserCountry, testClass.UserCountry);
        Assert.AreNotEqual(notExpValUserName, testClass.UserName);
        Assert.AreEqual(TestData.AvailableUserProfileIdWithoutSteamId, testClass.ProfileId);
        Assert.IsNull(testClass.SteamId);
    }

    [TestMethod]
    public void ShowMyHistoryTest()
    {
        // Arrange
        var testClass = new CtrlSettings();

        // Act
        testClass.ShowMyHistory();

        // Assert
        Assert.IsNotNull(testClass.FormMyHistory);
    }

    [TestMethod]
    public void ReadProfileAsyncTestIdTypeNotSelected()
    {
        // Arrange
        TestUtilityExt.SetSettings("SteamId", TestData.AvailableUserSteamId);
        TestUtilityExt.SetSettings("ProfileId", TestData.AvailableUserProfileId);
        TestUtilityExt.SetSettings("SelectedIdType", IdType.NotSelected);
        var testClass = new CtrlSettings();

        // Assert
        _ = Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
              testClass.ReadProfileAsync());
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReadProfileAsyncTestIdTypeSteamId()
    {
        // Arrange
        var notExpValUserCountry = "N/A";
        var notExpValUserName = "-- Invalid ID --";

        // Act
        TestUtilityExt.SetSettings("SteamId", TestData.AvailableUserSteamId);
        TestUtilityExt.SetSettings("ProfileId", TestData.AvailableUserProfileId);
        TestUtilityExt.SetSettings("SelectedIdType", IdType.Steam);
        var testClass = new CtrlSettings();
        var actVal = Task.Run(
            () => testClass.ReadProfileAsync())
            .Result;

        // Assert
        Assert.IsTrue(actVal);
        Assert.AreNotEqual(notExpValUserCountry, testClass.UserCountry);
        Assert.AreNotEqual(notExpValUserName, testClass.UserName);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncTest)]
    public void ReadProfileAsyncTestIdTypeProfileId()
    {
        // Arrange
        var notExpValUserCountry = "N/A";
        var notExpValUserName = "-- Invalid ID --";

        // Act
        TestUtilityExt.SetSettings("SteamId", TestData.AvailableUserSteamId);
        TestUtilityExt.SetSettings("ProfileId", TestData.AvailableUserProfileId);
        TestUtilityExt.SetSettings("SelectedIdType", IdType.Profile);
        var testClass = new CtrlSettings();
        var actVal = Task.Run(
            () => testClass.ReadProfileAsync())
            .Result;

        // Assert
        Assert.IsTrue(actVal);
        Assert.AreNotEqual(notExpValUserCountry, testClass.UserCountry);
        Assert.AreNotEqual(notExpValUserName, testClass.UserName);
    }
}
