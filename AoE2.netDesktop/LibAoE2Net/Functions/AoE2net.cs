﻿namespace AoE2NetDesktop.LibAoE2Net.Functions;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

/// <summary>
/// AoE2net API class.
/// </summary>
public static class AoE2net
{
    private static readonly Uri BaseAddress = new(@"https://aoe2.net/api/");
    private static readonly Uri CivImageAddress = new(@"https://aoe2.net/assets/images/crests/25x25/");
    private static readonly Uri ProfileIdBaseAddress = new(@"https://aoe2.net/#profile-");

    /// <summary>
    /// Gets or sets communication client.
    /// </summary>
    public static ComClient ComClient { get; set; } = new ComClient() {
        BaseAddress = BaseAddress,
        CivImageBaseAddress = CivImageAddress,
        Timeout = TimeSpan.FromSeconds(20),
    };

    /// <summary>
    /// Gets aoE2Version. ("aoe2de" or "aoe2hd").
    /// </summary>
    public static string AoE2Version { get; } = "aoe2de";

    /// <summary>
    /// Gets or sets action for recieving Exception.
    /// </summary>
    public static Action<Exception> OnError
    {
        get => ComClient.OnError;
        set => ComClient.OnError = value;
    }

    /// <summary>
    /// Reset static members of the <see cref="AoE2net"/> class.
    /// </summary>
    public static void Reset()
    {
        ComClient = new ComClient() {
            BaseAddress = BaseAddress,
            CivImageBaseAddress = CivImageAddress,
            Timeout = TimeSpan.FromSeconds(20),
        };
    }

    /// <summary>
    /// Gets Player Last Match.
    /// Request the last match the player started playing, this will be the current match if they are still in game.
    /// </summary>
    /// <param name="profileId">Profile ID.</param>
    /// <returns><see cref="PlayerLastmatch"/> deserialized as JSON.</returns>
    public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(int profileId)
    {
        var apiEndPoint = $"player/lastmatch?game={AoE2Version}&profile_id={profileId}";

        return await ComClient.GetFromJsonAsync<PlayerLastmatch>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets Player Last Match.
    /// Request the last match the player started playing, this will be the current match if they are still in game.
    /// </summary>
    /// <param name="steamId">steamID64.</param>
    /// <returns><see cref="PlayerLastmatch"/> deserialized as JSON.</returns>
    public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(string steamId)
    {
        if(steamId is null) {
            throw new ArgumentNullException(nameof(steamId));
        }

        var apiEndPoint = $"player/lastmatch?game={AoE2Version}&steam_id={steamId}";

        return await ComClient.GetFromJsonAsync<PlayerLastmatch>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets Player Rating History.
    /// Request the rating history for a player.
    /// </summary>
    /// <param name="steamId">steamID64.</param>
    /// <param name="leaderBoardId">Leaderboard ID.</param>
    /// <param name="count">Number of matches to get (Must be 10000 or less)).</param>
    /// <returns>List of <see cref="PlayerRating"/> deserialized as JSON.</returns>
    public static async Task<List<PlayerRating>> GetPlayerRatingHistoryAsync(string steamId, LeaderboardId leaderBoardId, int count)
    {
        if(steamId is null) {
            throw new ArgumentNullException(nameof(steamId));
        }

        string apiEndPoint = $"player/ratinghistory?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&steam_id={steamId}&count={count}";

        return await ComClient.GetFromJsonAsync<List<PlayerRating>>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets Player Rating History.
    /// Request the rating history for a player.
    /// </summary>
    /// <param name="profileId">Profile ID.</param>
    /// <param name="leaderBoardId">Leaderboard ID.</param>
    /// <param name="count">Number of matches to get (Must be 10000 or less)).</param>
    /// <returns>List of <see cref="PlayerRating"/> deserialized as JSON.</returns>
    public static async Task<List<PlayerRating>> GetPlayerRatingHistoryAsync(int profileId, LeaderboardId leaderBoardId, int count)
    {
        string apiEndPoint = $"player/ratinghistory?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&profile_id={profileId}&count={count}";

        return await ComClient.GetFromJsonAsync<List<PlayerRating>>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Strings.
    /// Request a list of strings used by the API.
    /// </summary>
    /// <param name="language">steamID64.</param>
    /// <returns><see cref="Strings"/> deserialized as JSON.</returns>
    public static async Task<Strings> GetStringsAsync(Language language)
    {
        var apiEndPoint = $"strings?game={AoE2Version}&language={language.ToApiString()}";
        return await ComClient.GetFromJsonAsync<Strings>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets Image file location on AoE2.net.
    /// </summary>
    /// <param name="civName">civilization name in English.</param>
    /// <returns>Image file location.</returns>
    public static string GetCivImageLocation(string civName)
    {
        string ret = null;

        if(civName != null) {
            ret = ComClient.GetCivImageLocation(civName);
        }

        return ret;
    }

    /// <summary>
    /// Request the match history for a player.
    /// </summary>
    /// <param name="start">Starting match (0 is the most recent match).</param>
    /// <param name="count">Number of matches to get (Must be 1000 or less)).</param>
    /// <param name="steamId">steamID64.</param>
    /// <returns>Player match history.</returns>
    public static async Task<PlayerMatchHistory> GetPlayerMatchHistoryAsync(int start, int count, string steamId)
    {
        if(steamId is null) {
            throw new ArgumentNullException(nameof(steamId));
        }

        var apiEndPoint = $"player/matches?game={AoE2Version}&steam_id={steamId}&start={start}&count={count}";

        return await ComClient.GetFromJsonAsync<PlayerMatchHistory>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Request the match history for a player.
    /// </summary>
    /// <param name="start">Starting match (0 is the most recent match).</param>
    /// <param name="count">Number of matches to get (Must be 1000 or less)).</param>
    /// <param name="profileId">Profile ID.</param>
    /// <returns>Player match history.</returns>
    public static async Task<PlayerMatchHistory> GetPlayerMatchHistoryAsync(int start, int count, int profileId)
    {
        var apiEndPoint = $"player/matches?game={AoE2Version}&profile_id={profileId}&start={start}&count={count}";

        return await ComClient.GetFromJsonAsync<PlayerMatchHistory>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Request the current leaderboards.
    /// </summary>
    /// <param name="leaderBoardId">LeaderBoardId.</param>
    /// <param name="start">Starting rank (Ignored if search, steam_id, or profile_id are defined).</param>
    /// <param name="count">Number of leaderboard entries to get (Must be 10000 or less)).</param>
    /// <param name="profileId">Profile ID.</param>
    /// <returns> Leaderboard for the specified user.</returns>
    public static async Task<LeaderboardContainer> GetLeaderboardAsync(LeaderboardId? leaderBoardId, int start, int count, int? profileId)
    {
        var apiEndPoint = $"leaderboard?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&profile_id={profileId}&start={start}&count={count}";

        return await ComClient.GetFromJsonAsync<LeaderboardContainer>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Request the current leaderboards.
    /// </summary>
    /// <param name="leaderBoardId">LeaderBoardId.</param>
    /// <param name="start">Starting rank (Ignored if search, steam_id, or profile_id are defined).</param>
    /// <param name="count">Number of leaderboard entries to get (Must be 10000 or less)).</param>
    /// <param name="steamId">steamID64.</param>
    /// <returns> Leaderboard for the specified user.</returns>
    public static async Task<LeaderboardContainer> GetLeaderboardAsync(LeaderboardId leaderBoardId, int start, int count, string steamId)
    {
        var apiEndPoint = $"leaderboard?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&steam_id={steamId}&start={start}&count={count}";

        return await ComClient.GetFromJsonAsync<LeaderboardContainer>(apiEndPoint).ConfigureAwait(false);
    }

    /// <summary>
    /// Open the AoE2.net player profile of the specified ID in your default browser.
    /// </summary>
    /// <param name="profileId">Profile ID.</param>
    /// <returns>browser process.</returns>
    public static Process OpenAoE2net(int profileId)
    {
        return ComClient.OpenBrowser($"{ProfileIdBaseAddress}{profileId}");
    }
}
