namespace AoE2NetDesktop.Utility.Timer;

using System.Timers;

/// <summary>
/// LastMatch loader class.
/// </summary>
public class LastMatchLoader : Timer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LastMatchLoader"/> class.
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="intervalSec">interval time [second].</param>
    public LastMatchLoader(ElapsedEventHandler action, int intervalSec)
        : base(intervalSec * 1000)
    {
        Elapsed += action;
    }
}
