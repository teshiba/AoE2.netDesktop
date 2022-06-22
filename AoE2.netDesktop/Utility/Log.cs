namespace AoE2NetDesktop.Utility;

using System.Diagnostics;

/// <summary>
/// Debug log class.
/// </summary>
public static class Log
{
    /// <summary>
    /// Gets or Sets log level.
    /// </summary>
    public static LogLevel Level { get; set; } = LogLevel.Debug;

    /// <summary>
    /// Gets last printed message.
    /// </summary>
    public static string LastMessage { get; private set; }

    /// <summary>
    /// Print Debug.
    /// </summary>
    /// <param name="debugString">output strings.</param>
    public static void Info(string debugString)
    {
        LastMessage = $"[INFO] {debugString}";

        if(Level == LogLevel.Info) {
            Debug.Print(LastMessage);
        }
    }
}
