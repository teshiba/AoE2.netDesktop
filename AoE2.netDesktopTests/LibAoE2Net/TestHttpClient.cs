﻿using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibAoE2net
{
    /// <summary>
    /// Communication client Interface.
    /// </summary>
    public class TestHttpClient : ComClient
    {
        /// <summary>
        /// Gets json text PlayerLastmatch.
        /// </summary>
        public const string TestDataPath = @"../../../TestData";

        public bool ForceHttpRequestException { get; set; }
        public bool ForceTaskCanceledException { get; set; }
        public string PlayerLastMatchUri { get; set; }

        /// <summary>
        /// Send a GET request to the specified Uri and return the response body as a string
        /// in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public override Task<string> GetStringAsync(string requestUri)
        {
            if (ForceHttpRequestException) {
                throw new HttpRequestException("Forced HttpRequestException");
            }

            if (ForceTaskCanceledException) {
                throw new TaskCanceledException("Forced TaskCanceledException");
            }

            var apiEndPoint = requestUri.Substring(0, requestUri.IndexOf('?'));
            var ret = apiEndPoint switch {
                "player/lastmatch" => ReadplayerLastMatchAsync(requestUri),
                "player/ratinghistory" => ReadPlayerRatingHistoryAsync(requestUri),
                "strings" => ReadStringsAsync(requestUri),
                _ => null,
            };

            return ret;
        }

        private Task<string> ReadplayerLastMatchAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var steamId = args[4];
            var requestDataFileName = PlayerLastMatchUri ?? $"playerLastMatch{game}{steamId}.json";

            return File.ReadAllTextAsync($"{TestDataPath}/{requestDataFileName}");
        }

        private static Task<string> ReadPlayerRatingHistoryAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var leaderboardId = (LeaderBoardId)int.Parse(args[4]);
            var steamId = args[6];
            var count = args[8];

            return File.ReadAllTextAsync($"{TestDataPath}/playerRatingHistory{game}{steamId}{leaderboardId}{count}.json");
        }

        private static Task<string> ReadStringsAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var language = args[4];

            return File.ReadAllTextAsync($"{TestDataPath}/Strings-{game}-{language}.json");
        }
    }
}