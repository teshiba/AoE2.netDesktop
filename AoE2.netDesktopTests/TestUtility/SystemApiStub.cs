namespace AoE2NetDesktopTests.TestUtility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

using AoE2NetDesktop.Utility.SysApi;

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

    private readonly Dictionary<string, string> processPathListNotInstalled = new() {
        { "Idle", string.Empty },
        { "AoE2DE_s", @"c:\AoE2DE_s\is\not\installed\at\steamapps\common\AoE2DE\" },
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

    public AppStatus AoE2deAppStatus { get; set; }

    /// <inheritdoc/>
    public string GetActiveProcess()
    {
        _ = user32api.GetWindowThreadProcessId(user32api.GetForegroundWindow(), out int processId);

        return processList[processId];
    }

    /// <inheritdoc/>
    public string GetProcessFilePath(string processName)
    {
        string ret = AoE2deAppStatus switch {
            AppStatus.NotInstalled => processPathListNotInstalled[processName],
            AppStatus.NotRunning => processPathList[processName],
            AppStatus.Runninng => processPathListAoE2DES[processName],
            _ => processPathListNotInstalled[processName],
        };
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
