namespace AoE2NetDesktop.LibAoE2Net.Functions;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.Utility.SysApi;

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
        var ret = DateTimeExt.FromUnixTimeSeconds(match.Opened ?? 0);
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

        if(match.Opened is long opened) {
            if(match.Finished is long finished) {
                ret = TimeSpan.FromSeconds(finished - opened);
            } else {
                var timeNow = DateTimeOffsetExt.UtcNow().ToUnixTimeSeconds();
                ret = TimeSpan.FromSeconds(timeNow - opened);
            }
        } else {
            ret = default;
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

    /// <summary>
    /// Get specified Player.
    /// </summary>
    /// <param name="match">Search target.</param>
    /// <param name="steamId">steam ID.</param>
    /// <returns>Player.</returns>
    public static Player GetPlayer(this Match match, string steamId)
    {
        Player ret = null;
        foreach(var item in match.Players.Where(item => item.SteamId == steamId)) {
            ret = item;
        }

        return ret;
    }
}
