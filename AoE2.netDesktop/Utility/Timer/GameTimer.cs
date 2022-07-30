namespace AoE2NetDesktop.Utility.Timer;

using System;
using System.Timers;

/// <summary>
/// GameTime redraw class.
/// </summary>
public class GameTimer : Timer
{
    private readonly Func<bool> updateFormControlFunc;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameTimer"/> class.
    /// </summary>
    /// <param name="updateFormControlFunc">event handler.</param>
    public GameTimer(Func<bool> updateFormControlFunc)
        : base(500)
    {
        this.updateFormControlFunc = updateFormControlFunc;
        Elapsed += OnElapsed;
    }

    private void OnElapsed(object sender, ElapsedEventArgs e)
    {
        Stop();
        var isContinueTimer = updateFormControlFunc.Invoke();

        if(isContinueTimer) {
            Start();
        }
    }
}
