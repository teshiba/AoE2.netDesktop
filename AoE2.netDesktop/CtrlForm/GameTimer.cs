namespace AoE2NetDesktop.CtrlForm;

using System.Timers;

/// <summary>
/// GameTime redraw class.
/// </summary>
public class GameTimer : Timer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GameTimer"/> class.
    /// </summary>
    /// <param name="action">Action.</param>
    public GameTimer(ElapsedEventHandler action)
        : base(500)
    {
        Elapsed += action;
    }
}
