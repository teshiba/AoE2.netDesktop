namespace AoE2NetDesktop.Utility.SysApi;

using System;

/// <summary>
/// DateTimeOffset Extentions.
/// </summary>
public static class DateTimeOffsetExt
{
        /// <summary>
    /// Gets or sets the current Utc time.
    /// </summary>
    /// <returns>the current Utc time value.</returns>
    public static Func<DateTimeOffset> UtcNow { get; set; } = () => DateTimeOffset.UtcNow;

    /// <summary>
    /// Returns the number of seconds that have elapsed since 1970-01-01T00:00:00Z.
    /// </summary>
    /// <param name="dateTime"> A date and time.</param>
    /// <returns>The number of seconds that have elapsed since 1970-01-01T00:00:00Z.</returns>
    public static long ToUnixTimeSeconds(this DateTime dateTime)
        => new DateTimeOffset(dateTime, default).ToUnixTimeSeconds();
}
