namespace AoE2NetDesktop.Form
{
    using System;
    using System.Runtime.CompilerServices;
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
    }
}
