namespace LibAoE2net
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
        private static readonly string AoE2Version = "aoe2de";

        /// <summary>
        /// Gets Player Last Match.
        /// Request the last match the player started playing, this will be the current match if they are still in game.
        /// </summary>
        /// <param name="steamId">steam id.</param>
        /// <returns><see cref="PlayerLastmatch"/> deserialized as JSON.</returns>
        public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(string steamId)
        {
            PlayerLastmatch playerLastmatch = null;

            if (steamId != null) {
                var apiEndPoint = $"player/lastmatch?game={AoE2Version}&steam_id={steamId}";
                playerLastmatch = await GetFromJsonAsync<PlayerLastmatch>(apiEndPoint);
            }

            return playerLastmatch;
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
            List<PlayerRating> playerRatingHistory = null;

            if (steamId != null) {
                var apiEndPoint = $"player/ratinghistory?game={AoE2Version}&leaderboard_id={(int)leaderBoardId}&steam_id={steamId}&count={count}";
                playerRatingHistory = await GetFromJsonAsync<List<PlayerRating>>(apiEndPoint);
            }

            return playerRatingHistory;
        }

        /// <summary>
        /// Strings.
        /// Request a list of strings used by the API.
        /// </summary>
        /// <param name="language">steamID64.</param>
        /// <returns><see cref="Strings"/> deserialized as JSON.</returns>
        public static async Task<Strings> GetStringsAsync(Language language)
        {
            var apiEndPoint = $"https://aoe2.net/api/strings?game={AoE2Version}&language={language.ToApiString()}";
            var strings = await GetFromJsonAsync<Strings>(apiEndPoint);

            return strings;
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

        /// <summary>
        /// Send a GET request to the specified end point and return the value
        /// resulting from deserializing the response body as JSON in an asynchronous operation.
        /// </summary>
        /// <param name="apiEndPoint">API end point.</param>
        /// <returns>JSON deserialize object.</returns>
        private static async Task<T> GetFromJsonAsync<T>(string apiEndPoint)
            where T : new()
        {
            var client = new HttpClient() {
                BaseAddress = BaseAddress,
                Timeout = TimeSpan.FromSeconds(20),
            };

            T ret;
            try {
                Debug.Print($"Send Request {BaseAddress}{apiEndPoint}");

                var jsonText = await client.GetStringAsync(apiEndPoint);
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