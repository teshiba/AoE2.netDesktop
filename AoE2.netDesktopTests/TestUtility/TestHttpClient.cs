using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibAoE2net
{
    /// <summary>
    /// Communication client Interface.
    /// </summary>
    public class TestHttpClient : ComClient
    {
        private const int EFAIL = -2147467259;

        /// <summary>
        /// Gets json text PlayerLastmatch.
        /// </summary>
        public const string TestDataPath = @"../../../TestData";

        public bool ForceHttpRequestException { get; set; }
        public bool ForceException { get; set; }
        public bool ForceWin32Exception { get; set; }
        public bool ForceTaskCanceledException { get; set; }
        public string PlayerLastMatchUri { get; set; }

        public string LastRequest { get; set; }
        /// <summary>
        /// Send a GET request to the specified Uri and return the response body as a string
        /// in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public override Task<string> GetStringAsync(string requestUri)
        {
            if (ForceHttpRequestException) {
                LastRequest = nameof(ForceHttpRequestException);
                throw new HttpRequestException("Forced HttpRequestException");
            }

            if (ForceTaskCanceledException) {
                LastRequest = nameof(ForceTaskCanceledException);
                throw new TaskCanceledException("Forced TaskCanceledException");
            }

            if (ForceException) {
                LastRequest = nameof(ForceException);
                throw new Exception("Force Exception");
            }

            var apiEndPoint = requestUri[..requestUri.IndexOf('?')];
            var ret = apiEndPoint switch {
                "player/lastmatch" => ReadplayerLastMatchAsync(requestUri),
                "player/ratinghistory" => ReadPlayerRatingHistoryAsync(requestUri),
                "player/matches" => ReadGetPlayerMatchHistoryAsync(requestUri),
                "leaderboard" => ReadLeaderboardAsync(requestUri),
                "strings" => ReadStringsAsync(requestUri),
                _ => null,
            };

            return ret;
        }

        /// <summary>
        /// Open specified URI.
        /// </summary>
        /// <param name="requestUri">URI string.</param>
        public override Process Start(string requestUri)
        {
            if (ForceWin32Exception) {
                throw new Win32Exception(EFAIL, "Forced ForceWin32Exception");
            }

            if (ForceException) {
                throw new Exception("Forced Exception");
            }

            LastRequest = $"Start {requestUri}";

            return new Process();
        }

        private Task<string> ReadplayerLastMatchAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var steamId = args[4];
            var requestDataFileName = PlayerLastMatchUri ?? $"playerLastMatch{game}{steamId}.json";
            string readUri = $"{TestDataPath}/{requestDataFileName}";

            LastRequest = $"Read {readUri}";

            return ReadTextFIleAsync($"{TestDataPath}/{requestDataFileName}");
        }

        private Task<string> ReadPlayerRatingHistoryAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var leaderboardId = (LeaderboardId)int.Parse(args[4]);
            var steamId = args[6];
            var count = args[8];
            var readUri = $"{TestDataPath}/playerRatingHistory{game}{steamId}{leaderboardId}{count}.json";

            LastRequest = $"Read {readUri}";

            return ReadTextFIleAsync(readUri);
        }

        private Task<string> ReadStringsAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var language = args[4];
            var readUri = $"{TestDataPath}/Strings-{game}-{language}.json";

            LastRequest = $"Read {readUri}";

            return ReadTextFIleAsync(readUri);
        }

        private Task<string> ReadGetPlayerMatchHistoryAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var steamId = args[4];
            var readUri = $"{TestDataPath}/playerMatchHistory{game}{steamId}.json";

            LastRequest = $"Read {readUri}";

            return ReadTextFIleAsync(readUri);
        }

        private Task<string> ReadLeaderboardAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var leaderboardId = (LeaderboardId)int.Parse(args[4]);
            var profileId = args[6];
            var readUri = $"{TestDataPath}/leaderboard{game}{leaderboardId}{profileId}.json";

            LastRequest = $"Read {readUri}";

            return ReadTextFIleAsync(readUri);
        }

        private static async Task<string> ReadTextFIleAsync(string filePath)
        {
            string ret;

            try {
                Debug.Print($"Open {Path.GetFullPath(filePath)}");
                ret = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
            } catch (Exception ex) {
                Debug.Print($"Test stub http read: {ex.Message}");
                throw new HttpRequestException(ex.Message, null, System.Net.HttpStatusCode.NotFound);
            }

            return ret;
        }
    }
}