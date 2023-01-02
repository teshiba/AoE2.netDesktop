namespace AoE2NetDesktopTests.TestUtility;

using System;

using AoE2NetDesktop.Utility.SysApi;

/// <summary>
/// User32 Stub API.
/// </summary>
public class User32ApiStub : IUser32Api
{
    /// <summary>
    /// Gets or sets process ID.
    /// </summary>
    public int ProcessId { get; set; } = 1;

    /// <summary>
    /// Gets or sets thread ID.
    /// </summary>
    public uint ThreadId { get; set; } = 1;

    /// <inheritdoc/>
    public IntPtr GetForegroundWindow()
        => default;

    /// <inheritdoc/>
    public uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId)
    {
        lpdwProcessId = ProcessId;
        return ThreadId;
    }
}
