namespace AoE2NetDesktop.CtrlForm;

using AoE2NetDesktop.LibAoE2Net.Functions;

using System;
using System.Timers;

/// <summary>
/// GameTime redraw class.
/// </summary>
public class GameTimer : Timer
{
    private readonly Action updateFormControlFunc;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameTimer"/> class.
    /// </summary>
    /// <param name="updateFormControlFunc">event handler.</param>
    public GameTimer(Action updateFormControlFunc)
        : base(500)
    {
        this.updateFormControlFunc = updateFormControlFunc;
        Elapsed += OnElapsed;
    }

    /// <summary>
    /// Gets Opened Time.
    /// </summary>
    public static string OpenedTime
    {
        get
        {
            var timezone = TimeZoneInfo.Local.ToString().Split(" ")[0].Replace("(", string.Empty).Replace(")", string.Empty);

            return $"{CtrlMain.LastMatch?.GetOpenedTime()} {timezone}";
        }
    }

    /// <summary>
    /// Gets Elapsed Time.
    /// </summary>
    public static string ElapsedTime
    {
        get
        {
            var ret = "-:--:--";
            if(CtrlMain.LastMatch != null) {
                var realTime = CtrlMain.LastMatch.GetElapsedTime().ToString(@"h\:mm\:ss");
                var inGameTime = new TimeSpan((long)(CtrlMain.LastMatch.GetElapsedTime().Ticks * 1.7)).ToString(@"h\:mm\:ss");
                ret = $"{realTime} ({inGameTime} in game)";
            }

            return ret;
        }
    }

    private void OnElapsed(object sender, ElapsedEventArgs e)
    {
        Stop();
        updateFormControlFunc.Invoke();

        if(CtrlMain.LastMatch.Finished == null) {
            Start();
        }
    }
}
