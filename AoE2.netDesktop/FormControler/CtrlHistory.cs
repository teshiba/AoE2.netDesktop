namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Threading.Tasks;
    using AoE2NetDesktop.From;
    using LibAoE2net;

    /// <summary>
    /// FormHistory controler.
    /// </summary>
    public class CtrlHistory : FormControler
    {
        private const int ReadCountMax = 1000;
        private readonly string steamId = null;
        private readonly int profileId = 0;
        private readonly IdType selectedId = IdType.NotSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="CtrlHistory"/> class.
        /// </summary>
        /// <param name="selectedId">user ID.</param>
        public CtrlHistory(IdType selectedId)
        {
            this.selectedId = selectedId;
            switch (selectedId) {
            case IdType.Steam:
                steamId = Settings.Default.SteamId;
                profileId = 0;
                break;
            case IdType.Profile:
                steamId = string.Empty;
                profileId = Settings.Default.ProfileId;
                break;
            default:
                break;
            }
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
        /// Show history form.
        /// </summary>
        public void Show()
        {
            if (selectedId != IdType.NotSelected) {
                new FormHistory(this).Show();
            } else {
                throw new InvalidOperationException("Not init selected ID");
            }
        }

        /// <summary>
        /// Get selected player.
        /// </summary>
        /// <param name="match">target match.</param>
        /// <returns>selected player.</returns>
        public Player GetSelectedPlayer(Match match)
        {
            Player ret = null;

            foreach (var item in match.Players) {
                if (item.SteamId == steamId || item.ProfilId == profileId) {
                    ret = item;
                }
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
                PlayerMatchHistory = selectedId switch {
                    IdType.Steam => await AoE2net.GetPlayerMatchHistoryAsync(startCount, ReadCountMax, steamId),
                    IdType.Profile => await AoE2net.GetPlayerMatchHistoryAsync(startCount, ReadCountMax, profileId),
                    _ => throw new InvalidEnumArgumentException($"invalid {nameof(IdType)}"),
                };
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
            bool ret;

            try {
                Leaderboards = selectedId switch {
                    IdType.Steam => await GetLeaderboardSteamId(),
                    IdType.Profile => await GetLeaderboardProfileId(),
                    _ => throw new InvalidEnumArgumentException($"invalid {nameof(IdType)}"),
                };
                ret = true;
            } catch (Exception) {
                PlayerMatchHistory = null;
                ret = false;
            }

            return ret;
        }

        private async Task<Dictionary<LeaderBoardId, Leaderboard>> GetLeaderboardSteamId()
        {
            var oneVOneRm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.OneVOneRandomMap, 0, 1, steamId);
            var oneVOneDm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.OneVOneDeathmatch, 0, 1, steamId);
            var teamRm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.TeamRandomMap, 0, 1, steamId);
            var teamDm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.TeamDeathmatch, 0, 1, steamId);

            if (oneVOneRm.Leaderboards.Count == 0) {
                oneVOneRm.Leaderboards.Add(new Leaderboard());
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

            var ret = new Dictionary<LeaderBoardId, Leaderboard> {
                { LeaderBoardId.OneVOneRandomMap, oneVOneRm.Leaderboards[0] },
                { LeaderBoardId.OneVOneDeathmatch, oneVOneDm.Leaderboards[0] },
                { LeaderBoardId.TeamRandomMap, teamRm.Leaderboards[0] },
                { LeaderBoardId.TeamDeathmatch, teamDm.Leaderboards[0] },
            };

            return ret;
        }

        private async Task<Dictionary<LeaderBoardId, Leaderboard>> GetLeaderboardProfileId()
        {
            var oneVOneRm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.OneVOneRandomMap, 0, 1, profileId);
            var oneVOneDm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.OneVOneDeathmatch, 0, 1, profileId);
            var teamRm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.TeamRandomMap, 0, 1, profileId);
            var teamDm = await AoE2net.GetLeaderboardAsync(LeaderBoardId.TeamDeathmatch, 0, 1, profileId);

            var ret = new Dictionary<LeaderBoardId, Leaderboard> {
                { LeaderBoardId.OneVOneRandomMap, oneVOneRm.Leaderboards[0] },
                { LeaderBoardId.OneVOneDeathmatch, oneVOneDm.Leaderboards[0] },
                { LeaderBoardId.TeamRandomMap, teamRm.Leaderboards[0] },
                { LeaderBoardId.TeamDeathmatch, teamDm.Leaderboards[0] },
            };

            return ret;
        }
    }
}
