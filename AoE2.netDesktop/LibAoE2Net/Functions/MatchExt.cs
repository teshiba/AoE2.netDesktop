namespace LibAoE2net
{
    using System;

    /// <summary>
    /// Extention of Match class.
    /// </summary>
    public static class MatchExt
    {
        /// <summary>
        /// Get Opened Time that converted to local time.
        /// </summary>
        /// <param name="match">match.</param>
        /// <returns>local time value as DateTime type.</returns>
        public static DateTime GetOpenedTime(this Match match)
        {
            var ret = DateTimeOffset.FromUnixTimeSeconds(match.Opened ?? 0).LocalDateTime;
            return ret;
        }

        /// <summary>
        /// Get specified Player.
        /// </summary>
        /// <param name="match">Search target.</param>
        /// <param name="profileId">profile ID.</param>
        /// <returns>Player.</returns>
        public static Player GetPlayer(this Match match, int profileId)
        {
            Player ret = null;

            foreach (var item in match.Players) {
                if (item.ProfilId == profileId) {
                    ret = item;
                }
            }

            return ret;
        }
    }
}
