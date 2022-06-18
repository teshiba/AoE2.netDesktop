namespace AoE2NetDesktop.Utility;

using System.Diagnostics;

/// <summary>
/// Debug log class.
/// </summary>
public static class Log
{
    /// <summary>
    /// Print Debug.
    /// </summary>
    /// <param name="debugString">output strings.</param>
    [Conditional("DEBUG_INFO")]
    public static void Info(string debugString)
    {
        Debug.Print($"[INFO] {debugString}");
    }
}
