namespace AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System.Collections.Generic;

using AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// PlayerRatingHistory list class.
/// </summary>
public class PlayerRatingHistories : Dictionary<LeaderboardId, List<PlayerRating>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerRatingHistories"/> class.
    /// </summary>
    public PlayerRatingHistories()
    {
        Add(LeaderboardId.RM1v1, new List<PlayerRating>());
        Add(LeaderboardId.RMTeam, new List<PlayerRating>());
        Add(LeaderboardId.EW1v1, new List<PlayerRating>());
        Add(LeaderboardId.EWTeam, new List<PlayerRating>());
        Add(LeaderboardId.DM1v1, new List<PlayerRating>());
        Add(LeaderboardId.DMTeam, new List<PlayerRating>());
        Add(LeaderboardId.Unranked, new List<PlayerRating>());
    }
}
