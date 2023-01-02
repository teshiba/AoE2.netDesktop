namespace AoE2NetDesktop.CtrlForm;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

/// <summary>
/// FormHistory controler.
/// </summary>
public class CtrlHistory : FormControler
{
    private static readonly Dictionary<string, LeaderboardId> LeaderboardNameList = new() {
        { "1v1 Random Map", LeaderboardId.RM1v1 },
        { "Team Random Map", LeaderboardId.RMTeam },
        { "1v1 Empire Wars", LeaderboardId.EW1v1 },
        { "Team Empire Wars", LeaderboardId.EWTeam },
        { "Unranked", LeaderboardId.Unranked },
        { "1v1 Death Match", LeaderboardId.DM1v1 },
        { "Team Death Match", LeaderboardId.DMTeam },
    };

    private static readonly Dictionary<string, DataSource> DataSourceNameList = new() {
        { "Map", DataSource.Map },
        { "Civilization", DataSource.Civilization },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="CtrlHistory"/> class.
    /// </summary>
    /// <param name="profileId">Profile ID.</param>
    public CtrlHistory(int profileId)
    {
        ProfileId = profileId;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CtrlHistory"/> class.
    /// </summary>
    /// <param name="profileId">Profile ID.</param>
    /// <param name="matches">Player match history.</param>
    public CtrlHistory(int profileId, PlayerMatchHistory matches)
    {
        ProfileId = profileId;
        PlayerMatchHistory = matches;
    }

    /// <summary>
    /// Gets PlayerRatingHistory.
    /// </summary>
    public PlayerRatingHistories PlayerRatingHistories { get; private set; } = new();

    /// <summary>
    /// Gets PlayerMatchHistory.
    /// </summary>
    public PlayerMatchHistory PlayerMatchHistory { get; private set; } = new();

    /// <summary>
    /// Gets or sets leaderboard List.
    /// </summary>
    public Dictionary<LeaderboardId, Leaderboard> Leaderboards { get; set; } = new();

    /// <summary>
    /// Gets profile ID.
    /// </summary>
    public int ProfileId { get; }

    /// <summary>
    /// Gets matched Player Infos.
    /// </summary>
    public Dictionary<int?, PlayerInfo> MatchedPlayerInfos { get; private set; } = new();

    /// <summary>
    /// Get leaderboard string.
    /// </summary>
    /// <returns>
    /// rate value and rating change value.
    /// </returns>
    public static string[] GetLeaderboardStrings()
        => LeaderboardNameList.Keys.ToArray();

    /// <summary>
    /// Get leaderboard string.
    /// </summary>
    /// <returns>
    /// rate value and rating change value.
    /// </returns>
    public static string[] GetDataSourceStrings()
        => DataSourceNameList.Keys.ToArray();

    /// <summary>
    /// Get leaderboard string.
    /// </summary>
    /// <param name="dataNameString">string of data source name.</param>
    /// <returns>DataSource.</returns>
    public static DataSource GetDataSource(string dataNameString)
    {
        var result = DataSourceNameList.TryGetValue(dataNameString, out DataSource ret);
        if(!result) {
            ret = DataSource.Undefined;
        }

        return ret;
    }

    /// <summary>
    /// Get leaderboard ID from string.
    /// </summary>
    /// <param name="leaderboardString">string of leaderboard ID.</param>
    /// <returns>LeaderBoardId.</returns>
    public static LeaderboardId GetLeaderboardId(string leaderboardString)
    {
        LeaderboardId ret;

        if(leaderboardString != null) {
            if(!LeaderboardNameList.TryGetValue(leaderboardString, out ret)) {
                ret = LeaderboardId.Undefined;
            }
        } else {
            ret = LeaderboardId.Undefined;
        }

        return ret;
    }

    /// <summary>
    /// Show History.
    /// </summary>
    /// <param name="playerName">player name.</param>
    /// <param name="profileId">profile ID.</param>
    /// <returns>FormHistory Instance.</returns>
    public static FormHistory GenerateFormHistory(string playerName, int? profileId)
    {
        FormHistory ret = null;

        if(profileId is int id) {
            ret = new FormHistory(id) {
                Text = $"{playerName}'s history - AoE2.net Desktop",
            };
        }

        return ret;
    }

    /// <summary>
    /// Create ListViewItem of leaderboard.
    /// </summary>
    /// <param name="leaderboard">Leaderboard data.</param>
    /// <param name="leaderboardView">Leaderboard View params.</param>
    /// <returns>ListViewItem for leaderboard.</returns>
    public static ListViewItem CreateListViewItem(Leaderboard leaderboard, LeaderboardView leaderboardView)
    {
        var ret = new ListViewItem(leaderboardView.Text) {
            Tag = leaderboardView.LeaderboardId,
            ForeColor = leaderboardView.Color,
            Checked = true,
        };

        ret.Font = new Font(ret.Font, FontStyle.Bold);
        ret.SubItems.Add(leaderboard.RankToString());
        ret.SubItems.Add(leaderboard.RatingToString());
        ret.SubItems.Add(leaderboard.HighestRatingToString());
        ret.SubItems.Add(leaderboard.GamesToString());
        ret.SubItems.Add(leaderboard.WinRateToString());
        ret.SubItems.Add(leaderboard.WinsToString());
        ret.SubItems.Add(leaderboard.LossesToString());
        ret.SubItems.Add(leaderboard.DropsToString());
        ret.SubItems.Add(leaderboard.StreakToString());
        ret.SubItems.Add(leaderboard.HighestStreakToString());
        ret.SubItems.Add(leaderboard.LowestStreakToString());

        return ret;
    }

    /// <summary>
    /// Create listView of PlayerMatchHistory.
    /// </summary>
    /// <returns>LeaderBoardId collections.</returns>
    public Dictionary<LeaderboardId, List<ListViewItem>> CreateListViewHistory()
    {
        var ret = new Dictionary<LeaderboardId, List<ListViewItem>> {
            { LeaderboardId.Unranked, new List<ListViewItem>() },
            { LeaderboardId.DM1v1, new List<ListViewItem>() },
            { LeaderboardId.DMTeam, new List<ListViewItem>() },
            { LeaderboardId.RM1v1, new List<ListViewItem>() },
            { LeaderboardId.RMTeam, new List<ListViewItem>() },
            { LeaderboardId.EW1v1, new List<ListViewItem>() },
            { LeaderboardId.EWTeam, new List<ListViewItem>() },
        };

        foreach(var match in PlayerMatchHistory) {
            var player = match.GetPlayer(ProfileId);
            var listViewItem = new ListViewItem(match.GetMapName());
            listViewItem.SubItems.Add(player.GetRatingString());
            listViewItem.SubItems.Add(player.GetWinMarkerString());
            listViewItem.SubItems.Add(player.GetCivName());
            listViewItem.SubItems.Add(player.GetColorString());
            listViewItem.SubItems.Add(match.GetOpenedTime().ToString());

            if(match.LeaderboardId != null) {
                var leaderboardId = (LeaderboardId)match.LeaderboardId;
                ret[leaderboardId].Add(listViewItem);
            }
        }

        return ret;
    }

    /// <summary>
    /// Create MatchedPlayers info.
    /// The player's rating is the rating when they played with you.
    /// </summary>
    /// <param name="matches">Player match history.</param>
    /// <returns>Matched players info.</returns>
    public Dictionary<int?, PlayerInfo> CreateMatchedPlayersInfo(PlayerMatchHistory matches)
    {
        var players = new Dictionary<int?, PlayerInfo>();

        foreach(var match in matches) {
            var selectedPlayer = match.GetPlayer(ProfileId);
            if(selectedPlayer != null) {
                foreach(var player in match.Players.Where(player => player != selectedPlayer)) {
                    var profilId = player.ProfilId ?? -1;
                    var playerName = player.Name ?? PlayerExt.PlayerNullName;
                    if(!players.ContainsKey(profilId)) {
                        var country = CountryCode.ConvertToFullName(player.Country);
                        var value = new PlayerInfo(selectedPlayer.ProfilId, playerName, profilId, country);
                        players.Add(profilId, value);
                    }

                    players[profilId].Matches.Add(match);
                }
            }
        }

        return players;
    }

    /// <summary>
    /// Open player's profile on AoE2.net.
    /// </summary>
    /// <param name="profileId">player profile ID.</param>
    /// <returns>Request URI.</returns>
    public string OpenProfile(int? profileId)
    {
        var ret = new Process();
        if(MatchedPlayerInfos.TryGetValue(profileId, out PlayerInfo playerInfo)) {
            try {
                ret = AoE2net.OpenAoE2net((int)playerInfo.ProfileId);
            } catch(Win32Exception noBrowser) {
                Debug.Print(noBrowser.Message);
            } catch(Exception other) {
                Debug.Print(other.Message);
            }
        } else {
            Debug.Print($"Unavailable Player Name: {profileId}.");
        }

        return ret.StartInfo.Arguments;
    }

    /// <summary>
    /// Open player's History on new History window.
    /// </summary>
    /// <param name="profileId">player profile ID.</param>
    /// <returns>Instance of FormHistory.</returns>
    public FormHistory GenerateFormHistory(int? profileId)
    {
        FormHistory ret = null;

        if(MatchedPlayerInfos.TryGetValue(profileId, out PlayerInfo playerInfo)) {
            ret = GenerateFormHistory(playerInfo.Name, playerInfo.ProfileId);
        } else {
            Debug.Print($"Unavailable Player ID: {profileId}.");
        }

        return ret;
    }

    /// <summary>
    /// Read player match history from AoE2.net.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task<bool> ReadPlayerMatchHistoryAsync()
    {
        bool ret;

        try {
            PlayerMatchHistory = await AoE2netHelpers.GetPlayerMatchHistoryAllAsync(ProfileId);
            PlayerRatingHistories = await AoE2netHelpers.GetPlayerRatingHistoryAllAsync(ProfileId);
            MatchedPlayerInfos = CreateMatchedPlayersInfo(PlayerMatchHistory);
            ret = true;
        } catch(Exception ex) {
            Log.Error(ex.Message);
            PlayerMatchHistory = new();
            PlayerRatingHistories = new();
            MatchedPlayerInfos = new();
            ret = false;
        }

        return ret;
    }

    /// <summary>
    /// Read player LeaderBoard from AoE2.net.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task<Dictionary<LeaderboardId, Leaderboard>> ReadLeaderBoardAsync()
    {
        try {
            var containers = new List<LeaderboardContainer>() {
                await GetLeaderboardAsync(LeaderboardId.RM1v1),
                await GetLeaderboardAsync(LeaderboardId.DM1v1),
                await GetLeaderboardAsync(LeaderboardId.EW1v1),
                await GetLeaderboardAsync(LeaderboardId.RMTeam),
                await GetLeaderboardAsync(LeaderboardId.DMTeam),
                await GetLeaderboardAsync(LeaderboardId.EWTeam),
                await GetLeaderboardAsync(LeaderboardId.Unranked),
            };
            Leaderboards = new Dictionary<LeaderboardId, Leaderboard> {
                { LeaderboardId.RM1v1, containers[0].Leaderboards[0] },
                { LeaderboardId.DM1v1, containers[1].Leaderboards[0] },
                { LeaderboardId.EW1v1, containers[2].Leaderboards[0] },
                { LeaderboardId.RMTeam, containers[3].Leaderboards[0] },
                { LeaderboardId.DMTeam, containers[4].Leaderboards[0] },
                { LeaderboardId.EWTeam, containers[5].Leaderboards[0] },
                { LeaderboardId.Unranked, containers[6].Leaderboards[0] },
            };
        } catch(Exception e) {
            Debug.Print($"GetLeaderboardAsync Error{e.Message}: {e.StackTrace}");
        }

        return Leaderboards;
    }

    private async Task<LeaderboardContainer> GetLeaderboardAsync(LeaderboardId leaderBoardId)
    {
        var ret = await AoE2net.GetLeaderboardAsync(leaderBoardId, 0, 1, ProfileId);

        if(ret.Leaderboards.Count == 0) {
            var ratings = await AoE2net.GetPlayerRatingHistoryAsync(ProfileId, leaderBoardId, 1);
            var leaderboard = new Leaderboard();
            if(ratings.Count != 0) {
                leaderboard.Rating = ratings[0].Rating;
                leaderboard.Games = ratings[0].NumWins + ratings[0].NumLosses;
                leaderboard.Wins = ratings[0].NumWins;
                leaderboard.Losses = ratings[0].NumLosses;
            }

            ret.Leaderboards.Add(leaderboard);
        }

        return ret;
    }
}
