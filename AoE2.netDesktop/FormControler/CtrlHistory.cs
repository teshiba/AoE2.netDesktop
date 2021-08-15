namespace AoE2NetDesktop.Form
{
    using System;
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
            } catch (System.Exception) {
                PlayerMatchHistory = null;
                ret = false;
            }

            return ret;
        }
    }
}
