namespace AoE2netDesktopTests.TestUtility;
using AoE2NetDesktop.Utility.SysApi;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

/// <summary>
/// System API stub class.
/// </summary>
public class SystemApiStub : ISystemApi
{
    private const int EFAIL = -2147467259;

    private readonly IUser32Api user32api;
    private readonly Dictionary<int, string> processList = new() {
        { 0, "Idle" },
        { 1, "AoE2DE_s" },
    };

    private readonly Dictionary<string, string> processPathListAoE2DES = new() {
        { "Idle", string.Empty },
        { "AoE2DE_s", $@"C:\Program Files (x86)\Steam\steamapps\common\AoE2DE\" },
    };

    private readonly Dictionary<string, string> processPathList = new() {
        { "Idle", string.Empty },
        { "AoE2DE_s", string.Empty },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemApiStub"/> class.
    /// </summary>
    /// <param name="processId">Process ID.</param>
    public SystemApiStub(int processId)
    {
        user32api = new User32ApiStub() {
            ProcessId = processId,
        };
    }

    public bool ForceException { get; set; }

    public bool ForceWin32Exception { get; set; }

    public bool AoE2deNotRunning { get; set; }

    /// <inheritdoc/>
    public string GetActiveProcess()
    {
        _ = user32api.GetWindowThreadProcessId(user32api.GetForegroundWindow(), out int processId);

        return processList[processId];
    }

    /// <inheritdoc/>
    public string GetProcessFilePath(string processName)
    {
        string ret;

        if(AoE2deNotRunning) {
            ret = processPathList[processName];
        } else {
            ret = processPathListAoE2DES[processName];
        }

        return ret;
    }

    /// <summary>
    /// Open specified URI.
    /// </summary>
    /// <param name="requestUri">URI string.</param>
    /// <returns>start process.</returns>
    /// <exception cref="Win32Exception">Win32Exception.</exception>
    /// <exception cref="Exception">Exception.</exception>
    public Process Start(string requestUri)
    {
        if(ForceWin32Exception) {
            throw new Win32Exception(EFAIL, "Forced ForceWin32Exception");
        }

        if(ForceException) {
            throw new Exception("Forced Exception");
        }

        // return a process for test without start.
        var process = new Process {
            StartInfo = new ProcessStartInfo("cmd", $"/c start {requestUri}") { CreateNoWindow = true },
        };
        return process;
    }
}
