namespace AoE2NetDesktop.LibAoE2Net;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// Helper class of AoE2net API.
/// </summary>
public static class AoE2netHelpers
{
    /// <summary>
    /// Number of Max Players.
    /// </summary>
    public const int PlayerNumMax = 8;

    private const int HistoryReadCountMax = 1000;
    private const int RateReadCountMax = 10000;
    private const string AINameNotation = "A.I.";

    /// <summary>
    /// Read all player match history.
    /// </summary>
    /// <param name="profileId">profileId.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task<PlayerMatchHistory> GetPlayerMatchHistoryAllAsync(int profileId)
    {
        var ret = new PlayerMatchHistory();
        PlayerMatchHistory readResult;

        do {
            var startCount = ret.Count;
            readResult = await AoE2net.GetPlayerMatchHistoryAsync(startCount, HistoryReadCountMax, profileId);
            ret.AddRange(readResult);
        } while(readResult.Count == HistoryReadCountMax);

        return ret;
    }

    /// <summary>
    /// Read all player match history.
    /// </summary>
    /// <param name="profileId">profileId.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task<PlayerRatingHistories> GetPlayerRatingHistoryAllAsync(int profileId)
    {
        var ret = new PlayerRatingHistories();
        List<PlayerRating> readResult;

        foreach(var item in ret) {
            do {
                var startCount = item.Value.Count;
                readResult = await AoE2net.GetPlayerRatingHistoryAsync(profileId, item.Key, startCount, RateReadCountMax);
                ret[item.Key].AddRange(readResult);
            } while(readResult.Count == HistoryReadCountMax);
        }

        return ret;
    }

    /// <summary>
    /// Get player last match.
    /// </summary>
    /// <param name="userIdType">ID type.</param>
    /// <param name="idText">ID text.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">if idText is null.</exception>
    public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(IdType userIdType, string idText)
    {
        if(idText is null) {
            throw new ArgumentNullException(nameof(idText));
        }

        string country = null;
        string name = null;
        int? profileId = null;
        string steamId = null;
        var lastMatch = await GetLastMatchAsync(userIdType, idText).ConfigureAwait(false);

        switch(userIdType) {
        case IdType.Steam:
            steamId = idText;
            break;
        case IdType.Profile:
            profileId = int.Parse(idText);
            break;
        }

        if(lastMatch.Players.Count != 0) {
            if(userIdType == IdType.Profile) {
                var player = lastMatch.GetPlayer(int.Parse(idText));
                name = player.Name;
            }

            if(lastMatch.LeaderboardId is LeaderboardId leaderboardId) {
                var leaderboardContainer = await GetLeaderboardAsync(leaderboardId, userIdType, idText).ConfigureAwait(false);

                if(leaderboardContainer.Leaderboards.Count != 0) {
                    var leaderboard = leaderboardContainer.Leaderboards[0];
                    country = leaderboard.Country;
                    name = leaderboard.Name;
                    profileId = leaderboard.ProfileId;
                    steamId = leaderboard.SteamId;
                }

                foreach(var player in lastMatch.Players) {
                    if(player.Rating == null) {
                        await TryFillRateAsync(leaderboardId, player).ConfigureAwait(false);
                    }

                    if(player.Name == null) {
                        await TryFillPlayerNameAsync(player).ConfigureAwait(false);
                    }
                }
            }
        } else {
            lastMatch = new Match();
        }

        var ret = new PlayerLastmatch() {
            Country = country,
            Name = name,
            ProfileId = profileId,
            SteamId = steamId,
            LastMatch = lastMatch,
        };

        return ret;
    }

    private static async Task<LeaderboardContainer> GetLeaderboardAsync(LeaderboardId leaderboardId, IdType userIdType, string idText)
    {
        var ret = new LeaderboardContainer();

        if(userIdType == IdType.Steam) {
            ret = await AoE2net.GetLeaderboardAsync(leaderboardId, 0, 1, idText).ConfigureAwait(false);
        } else if(userIdType == IdType.Profile) {
            ret = await AoE2net.GetLeaderboardAsync(leaderboardId, 0, 1, int.Parse(idText)).ConfigureAwait(false);
        }

        return ret;
    }

    private static async Task<Match> GetLastMatchAsync(IdType userIdType, string idText)
    {
        var matches = new PlayerMatchHistory();

        if(userIdType == IdType.Steam) {
            matches = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, idText).ConfigureAwait(false);
        } else if(userIdType == IdType.Profile) {
            matches = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, int.Parse(idText)).ConfigureAwait(false);
        }

        return matches.Count != 0 ? matches[0] : new Match();
    }

    private static async Task TryFillRateAsync(LeaderboardId leaderboardId, Player player)
    {
        List<PlayerRating> rate;

        if(player.ProfilId is int profileId) {
            rate = await AoE2net.GetPlayerRatingHistoryAsync(profileId, leaderboardId, 1).ConfigureAwait(false);
        } else {
            rate = new List<PlayerRating>();
        }

        if(rate.Count != 0) {
            player.Rating = rate[0].Rating;
        }
    }

    private static async Task TryFillPlayerNameAsync(Player player)
    {
        PlayerMatchHistory matches;

        if(player.ProfilId is int profileId) {
            matches = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, profileId).ConfigureAwait(false);
            player.Name = matches[0].GetPlayer(profileId).Name;
        } else {
            player.Name = AINameNotation;
        }
    }
}
