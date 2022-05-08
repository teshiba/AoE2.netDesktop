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

    /// <summary>
    /// Get player last match.
    /// </summary>
    /// <param name="userIdType">ID type.</param>
    /// <param name="idText">ID text.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">if idText is null.</exception>
    public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(IdType userIdType, string idText)
    {
        if (idText is null) {
            throw new ArgumentNullException(nameof(idText));
        }

        var ret = userIdType switch {
            IdType.Steam => await AoE2net.GetPlayerLastMatchAsync(idText).ConfigureAwait(false),
            IdType.Profile => await AoE2net.GetPlayerLastMatchAsync(int.Parse(idText)).ConfigureAwait(false),
            _ => new PlayerLastmatch(),
        };

        foreach (var player in ret.LastMatch.Players) {
            if (player.Rating == null) {
                await TryFillRate(ret, player).ConfigureAwait(false);
            }

            if (player.Name == null) {
                await TryFillPlayerName(player).ConfigureAwait(false);
            }
        }

        return ret;
    }

    private static async Task TryFillRate(PlayerLastmatch ret, Player player)
    {
        List<PlayerRating> rate;
        var leaderBoardId = ret.LastMatch.LeaderboardId ?? 0;

        if (player.SteamId != null) {
            rate = await AoE2net.GetPlayerRatingHistoryAsync(player.SteamId, leaderBoardId, 1).ConfigureAwait(false);
        } else if (player.ProfilId is int profileId) {
            rate = await AoE2net.GetPlayerRatingHistoryAsync(profileId, leaderBoardId, 1).ConfigureAwait(false);
        } else {
            rate = new List<PlayerRating>();
        }

        if (rate.Count != 0) {
            player.Rating = rate[0].Rating;
        }
    }

    private static async Task TryFillPlayerName(Player player)
    {
        PlayerLastmatch lastMatch;

        if (player.SteamId != null) {
            lastMatch = await AoE2net.GetPlayerLastMatchAsync(player.SteamId).ConfigureAwait(false);
        } else if (player.ProfilId is int profileId) {
            lastMatch = await AoE2net.GetPlayerLastMatchAsync(profileId).ConfigureAwait(false);
        } else {
            lastMatch = new PlayerLastmatch() { Name = string.Empty };
        }

        player.Name = lastMatch.Name;
    }
}
