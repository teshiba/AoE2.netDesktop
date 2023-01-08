namespace AoE2NetDesktop.LibAoE2Net;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        PlayerMatchHistory matches;

        do {
            var startCount = ret.Count;
            matches = await AoE2net.GetPlayerMatchHistoryAsync(startCount, HistoryReadCountMax, profileId);

            foreach(var leaderboardId in Enum.GetValues<LeaderboardId>()) {
                var matchesEachLeaderboard = matches.Where((match) => match.LeaderboardId == leaderboardId);
                Player nextMatchPlayer = null;
                foreach(var match in matchesEachLeaderboard) {
                    var player = match.GetPlayer(profileId);
                    player.Won = GetPlayersWon(nextMatchPlayer, player);
                    nextMatchPlayer = player;
                }
            }

            ret.AddRange(matches);
        } while(matches.Count == HistoryReadCountMax);

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
                Debug.Print($"DEBUG         idText = {idText} {lastMatch}");
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
                    player.Rating ??= await GetRateAsync(leaderboardId, player).ConfigureAwait(false);
                    player.Name ??= await GetPlayerNameAsync(player).ConfigureAwait(false);
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

    private static bool? GetPlayersWon(Player nextMatchPlayer, Player player)
    {
        bool? ret = null;

        if(player.RatingChange is null) {
            if(nextMatchPlayer is not null && player.Rating is not null) {
                if(player.Rating < nextMatchPlayer.Rating) {
                    ret = true;
                } else if(nextMatchPlayer.Rating < player.Rating) {
                    ret = false;
                }
            }
        } else {
            ret = !player.RatingChange.Contains('-');
        }

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

    private static async Task<int?> GetRateAsync(LeaderboardId leaderboardId, Player player)
    {
        int? ret = null;

        if(player.ProfilId is int profileId) {
            var rate = await AoE2net.GetPlayerRatingHistoryAsync(profileId, leaderboardId, 1).ConfigureAwait(false);
            if(rate.Count != 0) {
                ret = rate[0].Rating;
            }
        }

        return ret;
    }

    private static async Task<string> GetPlayerNameAsync(Player player)
    {
        var ret = AINameNotation;

        if(player.ProfilId is int profileId) {
            var matches = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, profileId).ConfigureAwait(false);
            ret = matches[0].GetPlayer(profileId).Name;
        }

        return ret;
    }
}
