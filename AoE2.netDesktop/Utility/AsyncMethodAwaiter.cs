namespace AoE2NetDesktop.Utility;

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Await async methods.
/// </summary>
public class AsyncMethodAwaiter
{
    private readonly object lockObject = new();
    private readonly Dictionary<string, ManualResetEvent> state = new();

    /// <summary>
    /// Notify the completion of the method.
    /// </summary>
    /// <param name="methodName">Completed method name.</param>
    public void Complete([CallerMemberName] string methodName = null)
        => Complete(true, methodName);

    /// <summary>
    /// Notify the completion of the method.
    /// </summary>
    /// <param name="enableDebugPrint">Whether Debug.Print enable.</param>
    /// <param name="methodName">Completed method name.</param>
    public void Complete(bool enableDebugPrint, [CallerMemberName] string methodName = null)
    {
        lock(lockObject) {
            if(!IsInitState(methodName)) {
                state[methodName] = new ManualResetEvent(false);
            }

            if(enableDebugPrint) {
                Debug.Print($"Set {methodName}");
            }

            state[methodName].Set();
        }
    }

    /// <summary>
    /// Blocks the current thread until the Complete() is called.
    /// </summary>
    /// <param name="methodName">Method name to wait for completion.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task WaitAsync(string methodName)
    {
        lock(lockObject) {
            if(!IsInitState(methodName)) {
                state[methodName] = new ManualResetEvent(false);
            }
        }

        Debug.Print($"Wait {methodName}");
        await Task.Run(() => state[methodName].WaitOne());
        Debug.Print($"Complete {methodName}");

        lock(lockObject) {
            state.Remove(methodName);
        }
    }

    private bool IsInitState(string methodName)
        => state.TryGetValue(methodName, out _);
}
