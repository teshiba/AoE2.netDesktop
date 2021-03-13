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
        /// <summary>
        /// Gets json text PlayerLastmatch.
        /// </summary>
        public const string TestDataPath = @"../../../TestData";

        /// <summary>
        /// Send a GET request to the specified Uri and return the response body as a string
        /// in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public override Task<string> GetStringAsync(string requestUri)
        {
            var index = requestUri.IndexOf('?');
            string apiEndPoint;

            if (index != -1) {
                apiEndPoint = requestUri.Substring(0, index);
            } else {
                apiEndPoint = requestUri;
            }

            var ret = apiEndPoint switch {
                "player/lastmatch" => File.ReadAllTextAsync($"{TestDataPath}/playerLastMatch.json"),
                "player/ratinghistory" => ReadPlayerRatingHistoryAsync(requestUri),
                "strings" => ReadStringsAsync(requestUri),
                "HttpRequestException" => throw new HttpRequestException(),
                "TaskCanceledException" => throw new TaskCanceledException(),
                _ => null,
            };

            return ret;
        }

        private static Task<string> ReadPlayerRatingHistoryAsync(string requestUri)
        {
            var args = requestUri.Split('=', '&', '?');
            var game = args[2];
            var leaderboardId = (LeaderBoardId)int.Parse(args[4]);
            var id = args[6];
            var count = args[8];

            return File.ReadAllTextAsync($"{TestDataPath}/playerRatingHistory{game}{id}{leaderboardId}{count}.json");
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