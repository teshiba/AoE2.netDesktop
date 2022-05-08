namespace AoE2NetDesktop.Utility.User32;

/// <summary>
/// System API Interface.
/// </summary>
public interface ISystemApi
{
    /// <summary>
    /// Get active process name.
    /// </summary>
    /// <returns>active process name.</returns>
    string GetActiveProcess();

    /// <summary>
    /// Get file name of the process.
    /// </summary>
    /// <param name="processName">Process name.</param>
    /// <returns>file full name path.</returns>
    string GetProcessFilePath(string processName);
}
