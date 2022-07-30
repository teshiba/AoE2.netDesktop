namespace AoE2NetDesktop.Utility.SysApi;

using System;
using System.Runtime.InteropServices;

/// <summary>
/// User32 API wrapper.
/// </summary>
public class User32Api : IUser32Api
{
    /// <inheritdoc/>
    public IntPtr GetForegroundWindow()
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        return GetForegroundWindow();
    }

    /// <inheritdoc/>
    public uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId)
    {
        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        return GetWindowThreadProcessId(hWnd, out lpdwProcessId);
    }
}
