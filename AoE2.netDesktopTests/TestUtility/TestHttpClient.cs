namespace LibAoE2net;

using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility;

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

/// <summary>
/// Communication client Interface.
/// </summary>
public class TestHttpClient : ComClient
{
    private const int EFAIL = -2147467259;

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
    /// <param name="requestUri">URI string.</param>
    /// <returns>string.</returns>
    public override Task<string> GetStringAsync(string requestUri)
    {
        if(ForceHttpRequestException) {
            LastRequest = nameof(ForceHttpRequestException);
            throw new HttpRequestException("Forced HttpRequestException");
        }

        if(ForceTaskCanceledException) {
            LastRequest = nameof(ForceTaskCanceledException);
            throw new TaskCanceledException("Forced TaskCanceledException");
        }

        if(ForceException) {
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
    /// <returns>start process.</returns>
    /// <exception cref="Win32Exception">Win32Exception.</exception>
    /// <exception cref="Exception">Exception.</exception>
    public override Process Start(string requestUri)
    {
        if(ForceWin32Exception) {
            throw new Win32Exception(EFAIL, "Forced ForceWin32Exception");
        }

        if(ForceException) {
            throw new Exception("Forced Exception");
        }

        LastRequest = $"Start {requestUri}";

        // return a dummy process for test.
        return new Process();
    }

    /// <summary>
    /// Gets Image file location on AoE2.net.
    /// </summary>
    /// <param name="civName">civilization name in English.</param>
    /// <returns>Image file location.</returns>
    public override string GetCivImageLocation(string civName)
    {
        string readUri = $"{TestData.Path}/dummy.png";

        LastRequest = $"Read {readUri}";

        Debug.Print($"Return {readUri}");

        return readUri;
    }

    private static async Task<string> ReadTextFIleAsync(string filePath)
    {
        string ret;

        try {
            Debug.Print($"Open {Path.GetFullPath(filePath)}");
            ret = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
        } catch(Exception ex) {
            Debug.Print($"Test stub http read: {ex.Message}");
            throw new HttpRequestException(ex.Message, null, System.Net.HttpStatusCode.NotFound);
        }

        return ret;
    }

    private Task<string> ReadplayerLastMatchAsync(string requestUri)
    {
        var args = requestUri.Split('=', '&', '?');
        var game = args[2];
        var steamId = args[4];
        var requestDataFileName = PlayerLastMatchUri ?? $"playerLastMatch{game}{steamId}.json";
        string readUri = $"{TestData.Path}/{requestDataFileName}";

        LastRequest = $"Read {readUri}";

        return ReadTextFIleAsync(readUri);
    }

    private Task<string> ReadPlayerRatingHistoryAsync(string requestUri)
    {
        var args = requestUri.Split('=', '&', '?');
        var game = args[2];
        var leaderboardId = (LeaderboardId)int.Parse(args[4]);
        var steamId = args[6];
        var start = args[8];
        var count = args[10];
        var readUri = $"{TestData.Path}/playerRatingHistory{game}{steamId}{leaderboardId}{count}_{start}.json";

        LastRequest = $"Read {readUri}";

        return ReadTextFIleAsync(readUri);
    }

    private Task<string> ReadStringsAsync(string requestUri)
    {
        var args = requestUri.Split('=', '&', '?');
        var game = args[2];
        var language = args[4];
        var readUri = $"{TestData.Path}/Strings-{game}-{language}.json";

        LastRequest = $"Read {readUri}";

        return ReadTextFIleAsync(readUri);
    }

    private Task<string> ReadGetPlayerMatchHistoryAsync(string requestUri)
    {
        var args = requestUri.Split('=', '&', '?');
        var game = args[2];
        var steamId = args[4];
        var readUri = $"{TestData.Path}/playerMatchHistory{game}{steamId}.json";

        LastRequest = $"Read {readUri}";

        return ReadTextFIleAsync(readUri);
    }

    private Task<string> ReadLeaderboardAsync(string requestUri)
    {
        var args = requestUri.Split('=', '&', '?');
        var game = args[2];
        var leaderboardId = (LeaderboardId)int.Parse(args[4]);
        var profileId = args[6];
        var readUri = $"{TestData.Path}/leaderboard{game}{leaderboardId}{profileId}.json";

        LastRequest = $"Read {readUri}";

        return ReadTextFIleAsync(readUri);
    }
}
