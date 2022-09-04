namespace AoE2NetDesktop.Utility;

using System;
using System.Collections.Generic;

/// <summary>
/// Debug log class.
/// </summary>
public static class Log
{
    private static readonly List<string> History = new();
    private static LogLevel level = LogLevel.Debug;

    /// <summary>
    /// Gets or Sets log level.
    /// </summary>
    public static LogLevel Level
    {
        get => level;
        set
        {
            System.Diagnostics.Debug.Print($"Set log level {level}");
            level = value;
        }
    }

    /// <summary>
    /// Gets last printed message.
    /// </summary>
    public static string LastMessage { get; private set; }

    /// <summary>
    /// Gets all log messages.
    /// </summary>
    public static string AllMessage
    {
        get
        {
            var ret = string.Empty;
            foreach(var item in History) {
                ret += item + Environment.NewLine;
            }

            return ret;
        }
    }

    /// <summary>
    /// Print Info.
    /// </summary>
    /// <param name="debugString">output strings.</param>
    public static void Info(string debugString)
    {
        LastMessage = $"[INFO] {debugString}";

        if(Level >= LogLevel.Info) {
            System.Diagnostics.Debug.Print(LastMessage);
            History.Add(LastMessage);
        }
    }

    /// <summary>
    /// Print Error.
    /// </summary>
    /// <param name="message">output strings.</param>
    public static void Error(string message)
    {
        LastMessage = $"[ERROR] {message}";

        if(Level >= LogLevel.Error) {
            System.Diagnostics.Debug.Print(LastMessage);
            History.Add(LastMessage);
        }
    }

    /// <summary>
    /// Print Debug.
    /// </summary>
    /// <param name="message">output strings.</param>
    public static void Debug(string message)
    {
        LastMessage = $"[DEBUG] {message}";

        if(Level >= LogLevel.Debug) {
            System.Diagnostics.Debug.Print(LastMessage);
            History.Add(LastMessage);
        }
    }

    /// <summary>
    /// Clear log history.
    /// </summary>
    public static void Clear()
    {
        LastMessage = string.Empty;
        History.Clear();
    }
}
