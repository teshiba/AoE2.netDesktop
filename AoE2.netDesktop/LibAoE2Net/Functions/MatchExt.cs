namespace AoE2NetDesktop.LibAoE2Net.Functions;

using System;
using System.Collections.Generic;
using System.Linq;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.SysApi;

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
        => DateTimeExt.FromUnixTimeSeconds(match.Started ?? 0);

    /// <summary>
    /// Get Elapsed Time from opened time that converted to local time.
    /// </summary>
    /// <param name="match">match.</param>
    /// <returns>local time value as DateTime type.</returns>
    public static TimeSpan GetElapsedTime(this Match match)
    {
        TimeSpan ret;

        if(match.Started is long opened) {
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
    public static Player GetPlayer(this Match match, int? profileId)
    {
        Player ret = null;
        foreach(var item in match.Players.Where(item => item.ProfilId == profileId)) {
            ret = item;
        }

        return ret;
    }

    /// <summary>
    /// Get match result.
    /// </summary>
    /// <param name="match">match.</param>
    /// <param name="team">Team type.</param>
    /// <returns>MatchResult.</returns>
    /// <exception cref="ArgumentNullException">players is null.</exception>
    public static MatchResult GetMatchResult(this Match match, TeamType team)
    {
        if(match is null) {
            throw new ArgumentNullException(nameof(match));
        }

        MatchResult ret;

        if(match.Started != null) {
            ret = GetMatchResultWithRatingChange(match, team);
        } else {
            ret = MatchResult.NotStarted;
        }

        return ret;
    }

    /// <summary>
    /// Gets whether someone has a rating change value.
    /// </summary>
    /// <param name="match">match.</param>
    /// <param name="team">Team type.</param>
    /// <returns>true: someone has rating change.</returns>
    private static MatchResult GetMatchResultWithRatingChange(Match match, TeamType team)
    {
        var ret = MatchResult.InProgress;

        if(match.Finished != null) {
            ret = MatchResult.Finished;
        }

        foreach(Player player in match.Players) {
            var myTeam = player.GetTeamType();
            if(player.IsRatingIncreased() is bool isWon) {
                if(isWon) {
                    if(myTeam == team) {
                        ret = MatchResult.Victorious;
                    } else {
                        ret = MatchResult.Defeated;
                    }
                } else {
                    if(myTeam == team) {
                        ret = MatchResult.Defeated;
                    } else {
                        ret = MatchResult.Victorious;
                    }
                }
            }
        }

        return ret;
    }
}
