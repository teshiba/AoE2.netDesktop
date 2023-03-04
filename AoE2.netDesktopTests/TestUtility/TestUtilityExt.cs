namespace AoE2NetDesktopTests.TestUtility;

using AoE2NetDesktop.Utility;

public static class TestUtilityExt
{
    public static SystemApiStub SystemApiStub(this ComClient comClient)
        => (SystemApiStub)((TestHttpClient)comClient).SystemApi;

    public static TestHttpClient TestHttpClient(this ComClient comClient)
        => (TestHttpClient)comClient;
}
