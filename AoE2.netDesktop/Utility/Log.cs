namespace AoE2NetDesktop.Utility;

using System;
using System.Collections.Generic;

/// <summary>
/// Debug log class.
/// </summary>
public static class Log
{
    /// <summary>
    /// Default log level.
    /// </summary>
    public const LogLevel LevelDefault = LogLevel.Error;

    private static readonly List<string> History = new();
    private static LogLevel level = LevelDefault;

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
        if(Level >= LogLevel.Info) {
            LastMessage = $"[INFO] {debugString}";
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
        if(Level >= LogLevel.Error) {
            LastMessage = $"[ERROR] {message}";
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
        if(Level >= LogLevel.Debug) {
            LastMessage = $"[DEBUG] {message}";
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
