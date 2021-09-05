namespace LibAoE2net
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    /// Extention of Language enum.
    /// </summary>
    public static class StringsExt
    {
        private static Strings apiStrings;
        private static Strings enStrings;
        private static Task initTask;

        /// <summary>
        /// Initialize the class.
        /// </summary>
        [ModuleInitializer]
        public static void InitAsync()
        {
            initTask = Task.Run(async () =>
            {
                enStrings = await AoE2net.GetStringsAsync(Language.en);
                await InitApiStringsAsync(Language.en);
            });
        }

        /// <summary>
        /// Initialize the class.
        /// </summary>
        /// <param name="language">language used.</param>
        /// <returns>controler instance.</returns>
        public static async Task<bool> InitAsync(Language language)
        {
            initTask.Wait();
            await InitApiStringsAsync(language);

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
            initTask.Wait();

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
            initTask.Wait();

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

        ///////////////////////////////////////////////////////////////////////
        // private
        ///////////////////////////////////////////////////////////////////////
        private static string GetCivName(Strings strings, Player player)
        {
            initTask.Wait();

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

        private static async Task InitApiStringsAsync(Language language)
        {
            if (apiStrings?.Language != language.ToApiString()) {
                apiStrings = await AoE2net.GetStringsAsync(language);
            }
        }
    }
}
