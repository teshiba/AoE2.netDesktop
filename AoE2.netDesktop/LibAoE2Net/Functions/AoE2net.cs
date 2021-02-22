﻿namespace LibAoE2net
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// AoE2net API class.
    /// </summary>
    public class AoE2net
    {
        private static readonly Uri BaseAddress = new Uri(@"https://aoe2.net/api/");

        /// <summary>
        /// Gets or sets communication client.
        /// </summary>
        public static ComClient ComClient { get; set; } = new ComClient() {
            BaseAddress = BaseAddress,
            Timeout = TimeSpan.FromSeconds(20),
        };

        /// <summary>
        /// Gets aoE2Version. ("aoe2de" or "aoe2hd").
        /// </summary>
        public static string AoE2Version { get; } = "aoe2de";

        /// <summary>
        /// Gets Player Last Match.
        /// Request the last match the player started playing, this will be the current match if they are still in game.
        /// </summary>
        /// <param name="steamId">steam id.</param>
        /// <returns><see cref="PlayerLastmatch"/> deserialized as JSON.</returns>
        public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(string steamId)
        {
            if (steamId is null) {
                throw new ArgumentNullException(nameof(steamId));
            }

            var apiEndPoint = $"player/lastmatch?game={AoE2Version}&steam_id={steamId}";

            return await GetFromJsonAsync<PlayerLastmatch>(apiEndPoint);
        }

        /// <summary>
        /// Gets Player Rating History.
        /// Request the rating history for a player.
        /// </summary>
        /// <param name="steamId">steamID64.</param>
        /// <param name="leaderBoardId">Leaderboard ID.</param>
        /// <param name="count">Number of matches to get (Must be 10000 or less)).</param>
        /// <returns>List of <see cref="PlayerRating"/> deserialized as JSON.</returns>
        public static async Task<List<PlayerRating>> GetPlayerRatingHistoryAsync(string steamId, LeaderBoardId leaderBoardId, int count)
        {
            if (steamId is null) {
                throw new ArgumentNullException(nameof(steamId));
            }

            return await GetPlayerRatingHistoryRequestAsync($"steam_id={steamId}", leaderBoardId, count);
        }

        /// <summary>
        /// Gets Player Rating History.
        /// Request the rating history for a player.
        /// </summary>
        /// <param name="profileId">Profile ID.</param>
        /// <param name="leaderBoardId">Leaderboard ID.</param>
        /// <param name="count">Number of matches to get (Must be 10000 or less)).</param>
        /// <returns>List of <see cref="PlayerRating"/> deserialized as JSON.</returns>
        public static async Task<List<PlayerRating>> GetPlayerRatingHistoryAsync(int profileId, LeaderBoardId leaderBoardId, int count)
        {
            return await GetPlayerRatingHistoryRequestAsync($"profile_id={profileId}", leaderBoardId, count);
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
            return await GetFromJsonAsync<Strings>(apiEndPoint);
        }

        /// <summary>
        /// Gets Image file location on AoE2.net.
        /// </summary>
        /// <param name="civ">civilization name.</param>
        /// <returns>Image file location.</returns>
        public static string GetCivImageLocation(string civ)
        {
            if (civ is null) {
                throw new ArgumentNullException(nameof(civ));
            }

            return $"https://aoe2.net/assets/images/crests/25x25/{civ.ToLower()}.png";
        }

        private static async Task<List<PlayerRating>> GetPlayerRatingHistoryRequestAsync(string id, LeaderBoardId leaderBoardId, int count)
        {
            string apiEndPoint = $"player/ratinghistory?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&{id}&count={count}";
            return await GetFromJsonAsync<List<PlayerRating>>(apiEndPoint);
        }

        private static async Task<T> GetFromJsonAsync<T>(string apiEndPoint)
            where T : new()
        {
            T ret;
            try {
                Debug.Print($"Send Request {BaseAddress}{apiEndPoint}");

                var jsonText = await ComClient.GetStringAsync(apiEndPoint);
                Debug.Print($"Get JSON {typeof(T)} {jsonText}");

                var serializer = new DataContractJsonSerializer(typeof(T));
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonText));
                ret = (T)serializer.ReadObject(stream);
            } catch (HttpRequestException e) {
                Debug.Print($"Request Error: {e.Message}");
                throw;
            } catch (TaskCanceledException e) {
                Debug.Print($"Timeout: {e.Message}");
                throw;
            }

            return ret;
        }
    }
}