namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
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
        /// Gets DataPloter.
        /// </summary>
        public DataPlot DataPloter { get; private set; }

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
        /// <returns>Listviews.</returns>
        public Dictionary<LeaderBoardId, ListViewItem[]> CreateListViewHistory()
        {
            var listView1v1Rm = new List<ListViewItem>();
            var listViewTeamRm = new List<ListViewItem>();

            foreach (var match in PlayerMatchHistory) {
                var player = match.GetPlayer(ProfileId);
                var listViewItem = new ListViewItem(match.GetMapName());
                listViewItem.SubItems.Add(GetRatingString(player));
                listViewItem.SubItems.Add(GetWinMarkerString(player.Won));
                listViewItem.SubItems.Add(player.GetCivName());
                listViewItem.SubItems.Add(player.GetColorString());
                listViewItem.SubItems.Add(match.GetOpenedTime().ToString());
                listViewItem.SubItems.Add(match.Version);

                switch (match.LeaderboardId) {
                case LeaderBoardId.OneVOneRandomMap:
                    listView1v1Rm.Add(listViewItem);
                    break;
                case LeaderBoardId.TeamRandomMap:
                    listViewTeamRm.Add(listViewItem);
                    break;
                }
            }

            return new Dictionary<LeaderBoardId, ListViewItem[]> {
                { LeaderBoardId.OneVOneRandomMap, listView1v1Rm.ToArray() },
                { LeaderBoardId.TeamRandomMap, listViewTeamRm.ToArray() },
            };
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
                        if (!players.ContainsKey(player.Name)) {
                            players.Add(player.Name, new PlayerInfo());
                        }

                        players[player.Name].Country = player.Country;
                        players[player.Name].ProfileId = player.ProfilId;

                        switch (match.LeaderboardId) {
                        case LeaderBoardId.OneVOneRandomMap:
                            players[player.Name].Rate1v1RM = player.Rating;
                            players[player.Name].Games1v1++;
                            break;
                        case LeaderBoardId.TeamRandomMap:
                            players[player.Name].RateTeamRM = player.Rating;
                            players[player.Name].GamesTeam++;

                            switch (selectedPlayer.CheckDiplomacy(player)) {
                            case Diplomacy.Ally:
                                players[player.Name].GamesAlly++;
                                break;
                            case Diplomacy.Enemy:
                                players[player.Name].GamesEnemy++;
                                break;
                            }

                            break;
                        }

                        players[player.Name].LastDate = match.GetOpenedTime();
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
                DataPloter = new DataPlot(PlayerMatchHistory, ProfileId);
                ret = true;
            } catch (Exception) {
                PlayerMatchHistory = null;
                MatchedPlayerInfos = null;
                DataPloter = null;
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
                    await GetLeaderboardAsync(LeaderBoardId.TeamRandomMap),
                    await GetLeaderboardAsync(LeaderBoardId.TeamDeathmatch),
                };
                Leaderboards = new Dictionary<LeaderBoardId, Leaderboard> {
                    { LeaderBoardId.OneVOneRandomMap, leaderboardContainers[0].Leaderboards[0] },
                    { LeaderBoardId.OneVOneDeathmatch, leaderboardContainers[1].Leaderboards[0] },
                    { LeaderBoardId.TeamRandomMap, leaderboardContainers[2].Leaderboards[0] },
                    { LeaderBoardId.TeamDeathmatch, leaderboardContainers[3].Leaderboards[0] },
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
