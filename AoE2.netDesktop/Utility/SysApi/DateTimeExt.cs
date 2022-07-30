namespace AoE2NetDesktop.Utility.SysApi;

using System;
using System.Globalization;

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
    /// InvalidDate string.
    /// </summary>
    public const string InvalidDate = "----/--/--";

    /// <summary>
    /// Gets or sets timezone.
    /// </summary>
    public static TimeZoneInfo TimeZoneInfo { get; set; } = TimeZoneInfo.Local;

    /// <summary>
    /// Gets or sets date time format.
    /// </summary>
    public static DateTimeFormatInfo DateTimeFormatInfo { get; set; } = DateTimeFormatInfo.CurrentInfo;

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

    /// <summary>
    /// Get DateTime format from second.
    /// </summary>
    /// <param name="second">second.</param>
    /// <returns>date time string.</returns>
    public static string GetDateTimeFormat(long? second)
        => GetDateTimeFormat(FromUnixTimeSeconds(second));

    /// <summary>
    /// Get DateTime format from DateTime.
    /// </summary>
    /// <param name="dateTime">date time.</param>
    /// <returns>date time string.</returns>
    public static string GetDateTimeFormat(DateTime dateTime)
        => dateTime.ToString(DateTimeFormatInfo);
}