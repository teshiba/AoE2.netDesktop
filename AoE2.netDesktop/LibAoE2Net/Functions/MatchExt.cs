namespace AoE2NetDesktop.LibAoE2Net.Functions;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System;
using System.Linq;

/// <summary>
/// Extention of Match class.
/// </summary>
public static class MatchExt
{
    /// <summary>
    /// Get Opened Time that converted to local time.
    /// </summary>
    /// <param name="match">match.</param>
    /// <returns>local time value as DateTime type.</returns>
    public static DateTime GetOpenedTime(this Match match)
    {
        var ret = DateTimeOffset.FromUnixTimeSeconds(match.Opened ?? 0).LocalDateTime;
        return ret;
    }

    /// <summary>
    /// Get Elapsed Time from opened time that converted to local time.
    /// </summary>
    /// <param name="match">match.</param>
    /// <returns>local time value as DateTime type.</returns>
    public static TimeSpan GetElapsedTime(this Match match)
    {
        TimeSpan ret;
        var timeNow = DateTimeOffset.UtcNow;
        var timeNowSec = timeNow.ToUnixTimeSeconds();

        if(match.Finished == null) {
            ret = timeNow - DateTimeOffset.FromUnixTimeSeconds(match.Opened ?? timeNowSec);
        } else {
            ret = DateTimeOffset.FromUnixTimeSeconds(match.Finished ?? timeNowSec)
                - DateTimeOffset.FromUnixTimeSeconds(match.Opened ?? timeNowSec);
        }

        return ret;
    }

    /// <summary>
    /// Get specified Player.
    /// </summary>
    /// <param name="match">Search target.</param>
    /// <param name="profileId">profile ID.</param>
    /// <returns>Player.</returns>
    public static Player GetPlayer(this Match match, int profileId)
    {
        Player ret = null;
        foreach(var item in match.Players.Where(item => item.ProfilId == profileId)) {
            ret = item;
        }

        return ret;
    }
}
