namespace AoE2NetDesktop.Form;

/// <summary>
/// FormMain DisplayStatus.
/// </summary>
public enum DisplayStatus
{
    /// <summary>
    /// A match has shown.
    /// </summary>
    Shown,

    /// <summary>
    /// A match is clearing.
    /// </summary>
    Clearing,

    /// <summary>
    /// A match is redrawing.
    /// </summary>
    Redrawing,

    /// <summary>
    /// The main form is closing.
    /// </summary>
    Closing,

    /// <summary>
    /// A match has cleared.
    /// </summary>
    Cleared,

    /// <summary>
    /// The program is Initializing.
    /// </summary>
    Initializing,

    /// <summary>
    /// A previous match is redrawing.
    /// </summary>
    RedrawingPrevMatch,
}
