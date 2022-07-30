namespace AoE2NetDesktop.LibAoE2Net;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        var ret = userIdType switch {
            IdType.Steam => await AoE2net.GetPlayerLastMatchAsync(idText).ConfigureAwait(false),
            IdType.Profile => await AoE2net.GetPlayerLastMatchAsync(int.Parse(idText)).ConfigureAwait(false),
            _ => new PlayerLastmatch(),
        };

        foreach(var player in ret.LastMatch.Players) {
            if(player.Rating == null) {
                await TryFillRateAsync(ret, player).ConfigureAwait(false);
            }

            if(player.Name == null) {
                await TryFillPlayerNameAsync(player).ConfigureAwait(false);
            }
        }

        return ret;
    }

    private static async Task TryFillRateAsync(PlayerLastmatch ret, Player player)
    {
        List<PlayerRating> rate;
        var leaderBoardId = ret.LastMatch.LeaderboardId ?? 0;

        if(player.SteamId != null) {
            rate = await AoE2net.GetPlayerRatingHistoryAsync(player.SteamId, leaderBoardId, 1).ConfigureAwait(false);
        } else if(player.ProfilId is int profileId) {
            rate = await AoE2net.GetPlayerRatingHistoryAsync(profileId, leaderBoardId, 1).ConfigureAwait(false);
        } else {
            rate = new List<PlayerRating>();
        }

        if(rate.Count != 0) {
            player.Rating = rate[0].Rating;
        }
    }

    private static async Task TryFillPlayerNameAsync(Player player)
    {
        PlayerLastmatch lastMatch;

        if(player.SteamId != null) {
            lastMatch = await AoE2net.GetPlayerLastMatchAsync(player.SteamId).ConfigureAwait(false);
        } else if(player.ProfilId is int profileId) {
            lastMatch = await AoE2net.GetPlayerLastMatchAsync(profileId).ConfigureAwait(false);
        } else {
            lastMatch = new PlayerLastmatch() { Name = string.Empty };
        }

        player.Name = lastMatch.Name;
    }
}
