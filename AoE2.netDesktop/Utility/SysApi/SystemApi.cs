namespace AoE2NetDesktop.Utility.SysApi;

using System;
using System.Diagnostics;

/// <summary>
/// System API class.
/// </summary>
public class SystemApi : ISystemApi
{
    private readonly IUser32Api user32api;

    /// <summary>
    /// Initializes a new instance of the <see cref="SystemApi"/> class.
    /// </summary>
    /// <param name="user32api">user32 API.</param>
    public SystemApi(IUser32Api user32api)
    {
        this.user32api = user32api;
    }

    /// <inheritdoc/>
    public string GetActiveProcess()
    {
        string ret;

        try {
            _ = user32api.GetWindowThreadProcessId(user32api.GetForegroundWindow(), out int processid);
            ret = Process.GetProcessById(processid).ProcessName;
        } catch(ArgumentException) {
            ret = string.Empty;
        }

        return ret;
    }

    /// <inheritdoc/>
    public string GetProcessFilePath(string processName)
    {
        string ret = string.Empty;
        var processes = Process.GetProcesses();

        foreach(var process in processes) {
            if(process.ProcessName == processName) {
                ret = process.MainModule.FileVersionInfo.FileName;
                ret = ret.Replace($"{processName}.exe", string.Empty);
            }
        }

        return ret;
    }

    /// <summary>
    /// Starts the process.
    /// </summary>
    /// <param name="requestUri">request Uri.</param>
    /// <returns>Process.</returns>
    public Process Start(string requestUri)
        => Process.Start(new ProcessStartInfo("cmd", $"/c start {requestUri}") { CreateNoWindow = true });
}
