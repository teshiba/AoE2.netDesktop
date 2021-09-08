namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

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
        /// Gets or sets playerMatchHistory.
        /// </summary>
        public PlayerMatchHistory PlayerMatchHistory { get; set; }

        /// <summary>
        /// Gets or sets leaderboard List.
        /// </summary>
        public Dictionary<LeaderBoardId, Leaderboard> Leaderboards { get; set; } = new ();

        /// <summary>
        /// Gets profile ID.
        /// </summary>
        public int ProfileId { get; }

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
        /// Read player match history from AoE2.net.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> ReadPlayerMatchHistoryAsync()
        {
            bool ret;
            int startCount = 0;

            try {
                PlayerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(startCount, ReadCountMax, ProfileId);
                ret = true;
            } catch (Exception) {
                PlayerMatchHistory = null;
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
                    leaderboard.Games = ratings[0].NumWins + ratings[0].NumLosses ?? 0;
                    leaderboard.Wins = ratings[0].NumWins ?? 0;
                    leaderboard.Losses = ratings[0].NumLosses ?? 0;
                }

                ret.Leaderboards.Add(leaderboard);
            }

            return ret;
        }
    }
}
