namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AoE2NetDesktop.From;
    using LibAoE2net;

    /// <summary>
    /// FormHistory controler.
    /// </summary>
    public class CtrlHistory : FormControler
    {
        private const int ReadCountMax = 1000;
        private static readonly Dictionary<string, LeaderBoardId> LeaderboardNameList = new () {
            { "1v1 Random Map", LeaderBoardId.OneVOneRandomMap },
            { "Team Random Map", LeaderBoardId.TeamRandomMap },
            { "1v1 Empire Wars", LeaderBoardId.OneVOneEmpireWars },
            { "Team Empire Wars", LeaderBoardId.TeamEmpireWars },
            { "Unranked", LeaderBoardId.Unranked },
            { "1v1 Death Match", LeaderBoardId.OneVOneDeathmatch },
            { "Team Death Match", LeaderBoardId.TeamDeathmatch },
        };

        private static readonly Dictionary<string, DataSource> DataSourceNameList = new () {
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
        /// Gets playerMatchHistory.
        /// </summary>
        public PlayerMatchHistory PlayerMatchHistory { get; private set; } = new ();

        /// <summary>
        /// Gets or sets leaderboard List.
        /// </summary>
        public Dictionary<LeaderBoardId, Leaderboard> Leaderboards { get; set; } = new ();

        /// <summary>
        /// Gets profile ID.
        /// </summary>
        public int ProfileId { get; }

        /// <summary>
        /// Gets matched Player Infos.
        /// </summary>
        public Dictionary<string, PlayerInfo> MatchedPlayerInfos { get; private set; } = new ();

        /// <summary>
        /// Get rate string.
        /// </summary>
        /// <param name="player">player.</param>
        /// <returns>
        /// rate value and rating change value.
        /// if rate is unavilable : "----".
        /// </returns>
        public static string GetRatingString(Player player)
        {
            string ret = (player.Rating?.ToString() ?? "----")
                        + (player.RatingChange?.Contains('-') ?? true ? string.Empty : "+")
                        + player.RatingChange?.ToString();

            return ret;
        }

        /// <summary>
        /// Get leaderboard string.
        /// </summary>
        /// <returns>
        /// rate value and rating change value.
        /// </returns>
        public static string[] GetLeaderboardStrings()
        {
            return LeaderboardNameList.Keys.ToArray();
        }

        /// <summary>
        /// Get leaderboard string.
        /// </summary>
        /// <returns>
        /// rate value and rating change value.
        /// </returns>
        public static string[] GetDataSourceStrings()
        {
            return DataSourceNameList.Keys.ToArray();
        }

        /// <summary>
        /// Get leaderboard string.
        /// </summary>
        /// <param name="dataNameString">string of data source name.</param>
        /// <returns>DataSource.</returns>
        public static DataSource GetDataSource(string dataNameString)
        {
            var result = DataSourceNameList.TryGetValue(dataNameString, out DataSource ret);
            if (!result) {
                ret = DataSource.Undefined;
            }

            return ret;
        }

        /// <summary>
        /// Get leaderboard ID from string.
        /// </summary>
        /// <param name="leaderboardString">string of leaderboard ID.</param>
        /// <returns>LeaderBoardId.</returns>
        public static LeaderBoardId GetLeaderboardId(string leaderboardString)
        {
            LeaderBoardId ret;

            if (leaderboardString != null) {
                if (!LeaderboardNameList.TryGetValue(leaderboardString, out ret)) {
                    ret = LeaderBoardId.Undefined;
                }
            } else {
                ret = LeaderBoardId.Undefined;
            }

            return ret;
        }

        /// <summary>
        /// Get the win marker.
        /// </summary>
        /// <param name="won">win or lose.</param>
        /// <returns>win marker string.</returns>
        public static string GetWinMarkerString(bool? won)
        {
            string ret;

            if (won == null) {
                ret = "---";
            } else {
                ret = (bool)won ? "o" : string.Empty;
            }

            return ret;
        }

        /// <summary>
        /// Create ListViewItem of leaderboard.
        /// </summary>
        /// <param name="leaderboardName">Leaderboard name.</param>
        /// <param name="leaderboard">leaderboard data.</param>
        /// <returns>ListViewItem for leaderboard.</returns>
        public static ListViewItem CreateListViewLeaderboard(string leaderboardName, Leaderboard leaderboard)
        {
            var ret = new ListViewItem(leaderboardName);

            ret.SubItems.Add(leaderboard.Rank?.ToString() ?? "-");
            ret.SubItems.Add(leaderboard.Rating?.ToString() ?? "-");
            ret.SubItems.Add(leaderboard.HighestRating?.ToString() ?? "-");
            ret.SubItems.Add(leaderboard.Games?.ToString() ?? "0");

            var games = leaderboard.Games ?? 0;
            if (games == 0) {
                ret.SubItems.Add("00.0%");
            } else {
                var winRate = (double)(leaderboard.Wins ?? 0) / games * 100;
                ret.SubItems.Add($"{winRate:F1}%");
            }

            ret.SubItems.Add(leaderboard.Wins?.ToString() ?? "0");
            ret.SubItems.Add(leaderboard.Losses?.ToString() ?? "0");
            ret.SubItems.Add(leaderboard.Drops?.ToString() ?? "0");
            ret.SubItems.Add(leaderboard.Streak?.ToString() ?? "0");
            ret.SubItems.Add(leaderboard.HighestStreak?.ToString() ?? "0");
            ret.SubItems.Add(leaderboard.LowestStreak?.ToString() ?? "0");

            return ret;
        }

        /// <summary>
        /// Create listView of PlayerMatchHistory.
        /// </summary>
        /// <returns>LeaderBoardId collections.</returns>
        public Dictionary<LeaderBoardId, List<ListViewItem>> CreateListViewHistory()
        {
            var ret = new Dictionary<LeaderBoardId, List<ListViewItem>> {
                { LeaderBoardId.Unranked,           new List<ListViewItem>() },
                { LeaderBoardId.OneVOneDeathmatch,  new List<ListViewItem>() },
                { LeaderBoardId.TeamDeathmatch,     new List<ListViewItem>() },
                { LeaderBoardId.OneVOneRandomMap,   new List<ListViewItem>() },
                { LeaderBoardId.TeamRandomMap,      new List<ListViewItem>() },
                { LeaderBoardId.OneVOneEmpireWars,  new List<ListViewItem>() },
                { LeaderBoardId.TeamEmpireWars,     new List<ListViewItem>() },
            };

            foreach (var match in PlayerMatchHistory) {
                var player = match.GetPlayer(ProfileId);
                var listViewItem = new ListViewItem(match.GetMapName());
                listViewItem.SubItems.Add(GetRatingString(player));
                listViewItem.SubItems.Add(GetWinMarkerString(player.Won));
                listViewItem.SubItems.Add(player.GetCivName());
                listViewItem.SubItems.Add(player.GetColorString());
                listViewItem.SubItems.Add(match.GetOpenedTime().ToString());
                listViewItem.SubItems.Add(match.Version);

                if (match.LeaderboardId != null) {
                    var leaderboardId = (LeaderBoardId)match.LeaderboardId;
                    ret[leaderboardId].Add(listViewItem);
                }
            }

            return ret;
        }

        /// <summary>
        /// Create MatchedPlayers info.
        /// </summary>
        /// <param name="matches">Player match history.</param>
        /// <returns>Matched players info.</returns>
        public Dictionary<string, PlayerInfo> CreateMatchedPlayersInfo(PlayerMatchHistory matches)
        {
            var players = new Dictionary<string, PlayerInfo>();

            foreach (var match in matches) {
                var selectedPlayer = match.GetPlayer(ProfileId);
                foreach (var player in match.Players) {
                    if (player != selectedPlayer) {
                        var name = player.Name ?? $"<Name null: ID: {player.ProfilId} >";

                        if (!players.ContainsKey(name)) {
                            players.Add(name, new PlayerInfo());
                        }

                        players[name].Country = CountryCode.ConvertToFullName(player.Country);
                        players[name].ProfileId = player.ProfilId;

                        switch (match.LeaderboardId) {
                        case LeaderBoardId.OneVOneRandomMap:
                            players[name].Rate1v1RM = player.Rating;
                            players[name].Games1v1++;
                            break;
                        case LeaderBoardId.TeamRandomMap:
                            players[name].RateTeamRM = player.Rating;
                            players[name].GamesTeam++;

                            switch (selectedPlayer.CheckDiplomacy(player)) {
                            case Diplomacy.Ally:
                                players[name].GamesAlly++;
                                break;
                            case Diplomacy.Enemy:
                                players[name].GamesEnemy++;
                                break;
                            }

                            break;
                        }

                        players[name].LastDate = match.GetOpenedTime();
                    }
                }
            }

            MatchedPlayerInfos = players;

            return players;
        }

        /// <summary>
        /// Open player's profile on AoE2.net.
        /// </summary>
        /// <param name="playerName">player name.</param>
        public void OpenProfile(string playerName)
        {
            if (MatchedPlayerInfos.TryGetValue(playerName, out PlayerInfo playerInfo)) {
                try {
                    AoE2net.OpenAoE2net((int)playerInfo.ProfileId);
                } catch (Win32Exception noBrowser) {
                    Debug.Print(noBrowser.Message);
                } catch (Exception other) {
                    Debug.Print(other.Message);
                }
            } else {
                Debug.Print($"Unavailable Player Name: {playerName}.");
            }
        }

        /// <summary>
        /// Read player match history from AoE2.net.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> ReadPlayerMatchHistoryAsync()
        {
            bool ret;
            int startCount = 0;

            try {
                PlayerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(startCount, ReadCountMax, ProfileId);
                MatchedPlayerInfos = CreateMatchedPlayersInfo(PlayerMatchHistory);
                ret = true;
            } catch (Exception) {
                PlayerMatchHistory = null;
                MatchedPlayerInfos = null;
                ret = false;
            }

            return ret;
        }

        /// <summary>
        /// Read player LeaderBoard from AoE2.net.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Dictionary<LeaderBoardId, Leaderboard>> ReadLeaderBoardAsync()
        {
            try {
                var leaderboardContainers = new List<LeaderboardContainer>() {
                    await GetLeaderboardAsync(LeaderBoardId.OneVOneRandomMap),
                    await GetLeaderboardAsync(LeaderBoardId.OneVOneDeathmatch),
                    await GetLeaderboardAsync(LeaderBoardId.OneVOneEmpireWars),
                    await GetLeaderboardAsync(LeaderBoardId.TeamRandomMap),
                    await GetLeaderboardAsync(LeaderBoardId.TeamDeathmatch),
                    await GetLeaderboardAsync(LeaderBoardId.TeamEmpireWars),
                    await GetLeaderboardAsync(LeaderBoardId.Unranked),
                };
                Leaderboards = new Dictionary<LeaderBoardId, Leaderboard> {
                    { LeaderBoardId.OneVOneRandomMap, leaderboardContainers[0].Leaderboards[0] },
                    { LeaderBoardId.OneVOneDeathmatch, leaderboardContainers[1].Leaderboards[0] },
                    { LeaderBoardId.OneVOneEmpireWars, leaderboardContainers[2].Leaderboards[0] },
                    { LeaderBoardId.TeamRandomMap, leaderboardContainers[3].Leaderboards[0] },
                    { LeaderBoardId.TeamDeathmatch, leaderboardContainers[4].Leaderboards[0] },
                    { LeaderBoardId.TeamEmpireWars, leaderboardContainers[5].Leaderboards[0] },
                    { LeaderBoardId.Unranked, leaderboardContainers[6].Leaderboards[0] },
                };
            } catch (Exception e) {
                Debug.Print($"GetLeaderboardAsync Error{e.Message}: {e.StackTrace}");
            }

            return Leaderboards;
        }

        private async Task<LeaderboardContainer> GetLeaderboardAsync(LeaderBoardId leaderBoardId)
        {
            var ret = await AoE2net.GetLeaderboardAsync(leaderBoardId, 0, 1, ProfileId);

            if (ret.Leaderboards.Count == 0) {
                var ratings = await AoE2net.GetPlayerRatingHistoryAsync(ProfileId, leaderBoardId, 1);
                var leaderboard = new Leaderboard();
                if (ratings.Count != 0) {
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
}
