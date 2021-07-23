using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoE2NetDesktop.Tests
{
    [TestClass()]
    public static class TestInit
    {
        public const string AvailableUserSteamId = "00000000000000001";
        public const int AvailableUserProfileId = 1;

        [AssemblyInitialize]
        public static void AssemblyIntiialize(TestContext testContext)
        {
            _ = testContext;
        }

    }
}