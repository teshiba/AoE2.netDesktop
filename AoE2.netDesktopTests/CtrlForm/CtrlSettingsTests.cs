namespace AoE2NetDesktop.CtrlForm.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;

using AoE2NetDesktopTests.TestData;
using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CtrlSettingsTests
{
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
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncWait)]
    [SuppressMessage("Usage", "VSTHRD104:Offer async methods", Justification = SuppressReason.IntentionalSyncWait)]
    public void ReloadProfileAsyncTestComClientException()
    {
        // Arrange
        var expValUserCountry = "N/A";
        var expValUserName = "-- Invalid ID --";

        // Act
        var testClass = new CtrlSettings() {
            SelectedIdType = IdType.Profile,
        };

        // The following code can read the player data.
        testClass.ReloadProfileAsync(IdType.Profile, TestData.AvailableUserProfileIdString).Wait();

        // Assert
        var actException = Assert.ThrowsExceptionAsync<ComClientException>(() =>
            testClass.ReloadProfileAsync(IdType.Profile, TestData.NotFoundUserProfileIdString)).Result;

        Assert.AreEqual(actException.Status, NetStatus.InvalidRequest);
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
              testClass.ReloadProfileAsync((IdType)(-1), TestData.AvailableUserSteamId));

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
        var formMain = new FormMain(Language.en);

        // Act
        testClass.ShowMyHistory(formMain);

        // Assert
        Assert.IsNotNull(testClass.FormMyHistory);
    }

    [TestMethod]
    public void ReadProfileAsyncTestIdTypeNotSelected()
    {
        SettingsRefs.Set("SteamId", TestData.AvailableUserSteamId);
        SettingsRefs.Set("ProfileId", TestData.AvailableUserProfileId);
        SettingsRefs.Set("SelectedIdType", (IdType)(-1));
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
        SettingsRefs.Set("SteamId", TestData.AvailableUserSteamId);
        SettingsRefs.Set("ProfileId", TestData.AvailableUserProfileId);
        SettingsRefs.Set("SelectedIdType", IdType.Steam);
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
        SettingsRefs.Set("SteamId", TestData.AvailableUserSteamId);
        SettingsRefs.Set("ProfileId", TestData.AvailableUserProfileId);
        SettingsRefs.Set("SelectedIdType", IdType.Profile);
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
