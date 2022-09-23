namespace AoE2NetDesktop.Form;

/// <summary>
/// FormMain DisplayStatus.
/// </summary>
public enum DisplayStatus
{
    /// <summary>
    /// The program is uninitialized.
    /// </summary>
    Uninitialized,

    /// <summary>
    /// A match is clearing.
    /// </summary>
    Clearing,

    /// <summary>
    /// A match has cleared.
    /// </summary>
    Cleared,

    /// <summary>
    /// A match is redrawing.
    /// </summary>
    Redrawing,

    /// <summary>
    /// A previous match is redrawing.
    /// </summary>
    RedrawingPrevMatch,

    /// <summary>
    /// A match has shown.
    /// </summary>
    Shown,

    /// <summary>
    /// The main form is closing.
    /// </summary>
    Closing,
}
