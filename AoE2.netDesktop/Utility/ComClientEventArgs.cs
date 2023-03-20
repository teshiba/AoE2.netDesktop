namespace AoE2NetDesktop.Utility;

using System;

/// <summary>
/// ComClient event argument.
/// </summary>
public class ComClientEventArgs
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ComClientEventArgs"/> class.
    /// </summary>
    /// <param name="exception">ComClient exception.</param>
    public ComClientEventArgs(Exception exception)
    {
        ComException = exception;
    }

    /// <summary>
    /// Gets or Sets com client exception.
    /// </summary>
    public Exception ComException { get; set; }
}
