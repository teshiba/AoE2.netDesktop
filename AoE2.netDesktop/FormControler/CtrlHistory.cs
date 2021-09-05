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
        public async Task<bool> ReadLeaderBoardAsync()
        {
            var ret = false;
            var leaderboardContainer = await GetLeaderboardProfileId();

            if (leaderboardContainer?.Count == 4) {
                var oneVOneRm = leaderboardContainer[0];
                var oneVOneDm = leaderboardContainer[1];
                var teamRm = leaderboardContainer[2];
                var teamDm = leaderboardContainer[3];

                if (oneVOneRm.Leaderboards.Count == 0) {
                    var ratings = await AoE2net.GetPlayerRatingHistoryAsync(ProfileId, LeaderBoardId.OneVOneRandomMap, 1);
                    var leaderboard = new Leaderboard();
                    if (ratings.Count != 0) {
                        leaderboard.Rating = ratings[0].Rating;
                        leaderboard.Games = ratings[0].NumWins + ratings[0].NumLosses ?? 0;
                        leaderboard.Wins = ratings[0].NumWins ?? 0;
                        leaderboard.Losses = ratings[0].NumLosses ?? 0;
                    }

                    oneVOneRm.Leaderboards.Add(leaderboard);
                }

                if (oneVOneDm.Leaderboards.Count == 0) {
                    oneVOneDm.Leaderboards.Add(new Leaderboard());
                }

                if (teamRm.Leaderboards.Count == 0) {
                    teamRm.Leaderboards.Add(new Leaderboard());
                }

                if (teamDm.Leaderboards.Count == 0) {
                    teamDm.Leaderboards.Add(new Leaderboard());
                }

                Leaderboards = new Dictionary<LeaderBoardId, Leaderboard> {
                    { LeaderBoardId.OneVOneRandomMap, oneVOneRm.Leaderboards[0] },
                    { LeaderBoardId.OneVOneDeathmatch, oneVOneDm.Leaderboards[0] },
                    { LeaderBoardId.TeamRandomMap, teamRm.Leaderboards[0] },
                    { LeaderBoardId.TeamDeathmatch, teamDm.Leaderboards[0] },
                };

                ret = true;
            }

            return ret;
        }

        private async Task<List<LeaderboardContainer>> GetLeaderboardProfileId()
        {
            List<LeaderboardContainer> ret = null;

            try {
                ret = new List<LeaderboardContainer>() {
                    await AoE2net.GetLeaderboardAsync(LeaderBoardId.OneVOneRandomMap, 0, 1, ProfileId),
                    await AoE2net.GetLeaderboardAsync(LeaderBoardId.OneVOneDeathmatch, 0, 1, ProfileId),
                    await AoE2net.GetLeaderboardAsync(LeaderBoardId.TeamRandomMap, 0, 1, ProfileId),
                    await AoE2net.GetLeaderboardAsync(LeaderBoardId.TeamDeathmatch, 0, 1, ProfileId),
                };
            } catch (Exception e) {
                Debug.Print($"GetLeaderboardAsync Error{e.Message}: {e.StackTrace}");
            }

            return ret;
        }
    }
}
