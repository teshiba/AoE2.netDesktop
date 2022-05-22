namespace AoE2NetDesktop.Utility;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Justification param of <see cref="SuppressMessageAttribute"/> class.
/// </summary>
public static class SuppressReason
{
    /// <summary>
    /// For GUI test.
    /// </summary>
    public const string GuiEvent = "For GUI event";

    /// <summary>
    /// For GUI test.
    /// </summary>
    public const string GuiTest = "For GUI test";

    /// <summary>
    /// For intentional sync test.
    /// </summary>
    public const string IntentionalSyncTest = "For intentional sync test";

    /// <summary>
    /// For private invoke test.
    /// </summary>
    public const string PrivateInvokeTest = "For private invoke test";

    /// <summary>
    /// For intentional sync test.
    /// </summary>
    public const string IntentionalSyncWait = "For intentional sync wait";
}
