namespace LibAoE2net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// extention of Language enum.
    /// </summary>
    public static class StringsExt
    {
        private static Strings apiStrings;
        private static Strings enStrings;

        /// <summary>
        /// Initialize the class.
        /// </summary>
        /// <param name="language">language used.</param>
        /// <returns>controler instance.</returns>
        public static async Task<bool> InitAsync(Language language)
        {
            apiStrings = await AoE2net.GetStringsAsync(language);
            enStrings = await AoE2net.GetStringsAsync(Language.en);

            return true;
        }

        /// <summary>
        /// Gets the string of the ID in the list.
        /// </summary>
        /// <param name="stringIds">target list.</param>
        /// <param name="id">target id.</param>
        /// <returns>Found string.</returns>
        public static string GetString(this List<StringId> stringIds, int id)
        {
            string ret;
            try {
                ret = stringIds.Where(x => x.Id == id).First().String;
            } catch (InvalidOperationException) {
                ret = null;
            }

            return ret;
        }

        /// <summary>
        /// Get map name of the match.
        /// </summary>
        /// <param name="match">last match.</param>
        /// <returns>map name.</returns>
        public static string GetMapName(this Match match)
        {
            string mapName;

            if (match.MapType is int mapType) {
                mapName = apiStrings.MapType.GetString(mapType);
                if (mapName == null) {
                    mapName = $"Unknown(Map No.{mapType})";
                }
            } else {
                mapName = $"Unknown(Map No. N/A)";
            }

            return mapName;
        }

        /// <summary>
        /// Get player's civilization name in English.
        /// </summary>
        /// <param name="player">player.</param>
        /// <returns>civilization name in English.</returns>
        public static string GetCivEnName(this Player player)
            => GetCivName(enStrings, player);

        /// <summary>
        /// Get player's civilization name.
        /// </summary>
        /// <param name="player">player.</param>
        /// <returns>civilization name.</returns>
        public static string GetCivName(this Player player)
            => GetCivName(apiStrings, player);

        /// <summary>
        /// Get Opened Time.
        /// </summary>
        /// <param name="match">match.</param>
        /// <returns>time value as DateTime type.</returns>
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

        ///////////////////////////////////////////////////////////////////////
        // private
        ///////////////////////////////////////////////////////////////////////
        private static string GetCivName(Strings strings, Player player)
        {
            string ret;

            if (player.Civ is int id) {
                ret = strings.Civ.GetString(id);
                if (ret is null) {
                    ret = $"invalid civ:{id}";
                }
            } else {
                ret = $"invalid civ:null";
            }

            return ret;
        }
    }
}
