namespace AoE2netDesktopTests.TestUtility;

using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility.User32;

using System.Collections.Generic;

/// <summary>
/// System API stub class.
/// </summary>
public class SystemApiStub : ISystemApi
{
    private readonly IUser32Api user32api;
    private readonly Dictionary<int, string> processList = new() {
        { 0, "Idle" },
        { 1, "AoE2DE_s" },
    };

    private readonly Dictionary<string, string> processPathList = new() {
        { "Idle", string.Empty },
        { "AoE2DE_s", TestData.Path + "/" },
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

    /// <inheritdoc/>
    public string GetActiveProcess()
    {
        _ = user32api.GetWindowThreadProcessId(user32api.GetForegroundWindow(), out int processId);

        return processList[processId];
    }

    /// <inheritdoc/>
    public string GetProcessFilePath(string processName)
    {
        return processPathList[processName];
    }
}
