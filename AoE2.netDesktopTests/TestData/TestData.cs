namespace AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public static class TestData
{
    public const string Path = @"../../../TestData";

    public const string AvailableUserSteamId = "00000000000000001";
    public const string AvailableUserProfileIdString = "1";
    public const int AvailableUserProfileId = 1;
    public const int AvailableUserProfileIdWithoutHistory = 101;
    public const int AvailableUserProfileIdWithoutSteamId = 100;
    public const string AvailableUserProfileIdWithoutSteamIdString = "100";
    public const string DdsFile = $"{Path}/testImage.dds";
    public const string DdsFileUnexpectedDwFlags = $"{Path}/unexpectedDwFlags.dds";
    public const string DdsFileUnexpectedMagic = $"{Path}/unexpectedMagic.dds";

    [AssemblyInitialize]
    public static void AssemblyIntiialize(TestContext testContext)
    {
        _ = testContext;
        TestUtilityExt.AssemblyName = "AoE2NetDesktop";
    }
}
