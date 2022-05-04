namespace AoE2NetDesktop
{
    using System;

    /// <summary>
    /// User32 API wrapper interface.
    /// </summary>
    public interface IUser32Api
    {
        /// <summary>
        /// GetForegroundWindow.
        /// </summary>
        /// <returns>IntPtr.</returns>
        IntPtr GetForegroundWindow();

        /// <summary>
        /// GetWindowThreadProcessId.
        /// </summary>
        /// <param name="hWnd">hWnd.</param>
        /// <param name="lpdwProcessId">lpdwProcessId.</param>
        /// <returns>Window thread process ID.</returns>
        uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);
    }
}