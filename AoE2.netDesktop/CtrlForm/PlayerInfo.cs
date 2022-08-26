namespace AoE2NetDesktop.CtrlForm;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Player Informations.
/// </summary>
public class PlayerInfo
{
    private readonly int comparedProfileId;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
    /// </summary>
    /// <param name="comparedProfileId">PlayerInfo compares this profileId.</param>
    /// <param name="profileId">profileId for this PlayerInfo.</param>
    /// <param name="country">country Name for this PlayerInfo.</param>
    public PlayerInfo(int? comparedProfileId, int? profileId, string country)
    {
        Country = country;
        ProfileId = profileId;
        this.comparedProfileId = (int)comparedProfileId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerInfo"/> class.
    /// </summary>
    /// <param name="comparedProfileId">PlayerInfo compares this profileId.</param>
    /// <param name="profileId">profileId for this PlayerInfo.</param>
    public PlayerInfo(int? comparedProfileId, int? profileId)
        : this(comparedProfileId, profileId, null)
    {
    }

    /// <summary>
    /// Gets country Name.
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Gets profile ID.
    /// </summary>
    public int? ProfileId { get; }

    /// <summary>
    /// Gets 1v1 Random map rate.
    /// </summary>
    public int? RateRM1v1 => Matches
        .FindLast(item => item.LeaderboardId == LeaderboardId.RM1v1)
        ?.GetPlayer(ProfileId).Rating;

    /// <summary>
    /// Gets team Random map rate.
    /// </summary>
    public int? RateRMTeam => Matches
        .FindLast(item => item.LeaderboardId == LeaderboardId.RMTeam)
        ?.GetPlayer(ProfileId).Rating;

    /// <summary>
    /// Gets game count that player is ally.
    /// </summary>
    public int GamesAlly => Matches
        .Where(item => item.LeaderboardId == LeaderboardId.RMTeam)
        .Count(item => CompareDiplomacy(item, Diplomacy.Ally));

    /// <summary>
    /// Gets game count that player is enemy.
    /// </summary>
    public int GamesEnemy => Matches
        .Where(item => item.LeaderboardId == LeaderboardId.RMTeam)
        .Count(item => CompareDiplomacy(item, Diplomacy.Enemy));

    /// <summary>
    /// Gets game count of 1v1 random map.
    /// </summary>
    public int Games1v1 => Matches.Count(item => item.LeaderboardId == LeaderboardId.RM1v1);

    /// <summary>
    /// Gets game count of team random map.
    /// </summary>
    public int GamesTeam => Matches.Count(item => item.LeaderboardId == LeaderboardId.RMTeam);

    /// <summary>
    /// Gets last match date.
    /// </summary>
    public DateTime LastDate => Matches.Select(item => item.GetOpenedTime()).Max();

    /// <summary>
    /// Gets match history.
    /// </summary>
    public List<Match> Matches { get; } = new();

    private bool CompareDiplomacy(Match item, Diplomacy diplomacy)
    {
        var comparedplayer = item.GetPlayer(comparedProfileId);
        var player = item.GetPlayer(ProfileId);
        var ret = false;

        if(player != null) {
            ret = comparedplayer.CheckDiplomacy(player) == diplomacy;
        }

        return ret;
    }
}
