namespace AoE2NetDesktop.Utility.SysApi;

using System;

/// <summary>
/// DateTimeOffset Extentions.
/// </summary>
public static class DateTimeExt
{
    /// <summary>
    /// InvalidTime string.
    /// </summary>
    public const string InvalidTime = "--:--:--";

    /// <summary>
    /// Gets or sets timezone.
    /// </summary>
    public static TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;

    /// <summary>
    /// Converts a time to the time in a particular time zone.
    /// </summary>
    /// <param name="dateTimeOffset">date time offset.</param>
    /// <returns>DateTime value of particular time zone.</returns>
    public static DateTime SpZoneDateTime(this DateTimeOffset dateTimeOffset)
        => TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo).DateTime;

    /// <summary>
    /// Converts a Unix time to the time in a particular time zone.
    /// </summary>
    /// <param name="second"> A Unix time, expressed as the number of seconds that have elapsed since 1970-01-01T00:00:00Z
    ///     (January 1, 1970, at 12:00 AM UTC). For Unix times before this date, its value is negative.</param>
    /// <returns>DateTime value of particular time zone.</returns>
    public static DateTime FromUnixTimeSeconds(long? second)
        => DateTimeOffset.FromUnixTimeSeconds(second ?? 0).SpZoneDateTime();
}