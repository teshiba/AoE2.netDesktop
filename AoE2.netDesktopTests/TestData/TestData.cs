namespace AoE2NetDesktopTests.TestData;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.Utility;

using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics.CodeAnalysis;

[TestClass]
public static class TestData
{
    public const string Path = @"../../../TestData";

    public const string AvailableUserSteamId = "00000000000000001";
    public const string AvailableUserProfileIdString = "1";
    public const string NotFoundUserProfileIdString = "-1";
    public const int AvailableUserProfileId = 1;
    public const int UnavailableUserProfileId = -1;
    public const int AvailableUserProfileIdWithoutHistory = 101;
    public const int AvailableUserProfileIdWithoutSteamId = 100;
    public const string AvailableUserProfileIdWithoutSteamIdString = "100";
    public const string DdsFile = $"{Path}/testImage.dds";
    public const string DdsNonExsistFile = $"{Path}/DdsNonExsistFile.dds";
    public const string DdsNonExsistDir = $"{Path}/DdsNonExsistDir/DdsNonExsistFile.dds";
    public const string DdsFileUnexpectedDwFlags = $"{Path}/unexpectedDwFlags.dds";
    public const string DdsFileUnexpectedMagic = $"{Path}/unexpectedMagic.dds";

    [AssemblyInitialize]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    public static void AssemblyIntiialize(TestContext testContext)
    {
        _ = testContext;
        TestUtilityExt.AssemblyName = "AoE2NetDesktop";
        AoE2net.ComClient = new TestHttpClient() {
            SystemApi = new SystemApiStub(1),
        };
        StringsExt.InitAsync().Wait();
    }
}
