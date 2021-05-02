namespace AoE2NetDesktop.From
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Linq;
    using System.Threading.Tasks;
    using LibAoE2net;

    /// <summary>
    /// FormMain controler.
    /// </summary>
    public class CtrlMain : FormControler
    {
        private const string InvalidSteamIdString = "-- Invalid Steam ID --";
        private const int VerifyWaitMsec = 1500;

        private readonly System.Timers.Timer timerSteamIdVerify;

        private Strings apiStrings;
        private Strings enStrings;
        private Func<Task> delayedFunction;
        private PlayerLastmatch playerLastmatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="CtrlMain"/> class.
        /// </summary>
        public CtrlMain()
        {
            timerSteamIdVerify = new System.Timers.Timer(VerifyWaitMsec);
            timerSteamIdVerify.Elapsed += (sender, e) =>
            {
                timerSteamIdVerify.Stop();
                Invoke(delayedFunction);
            };
        }

        /// <summary>
        /// Gets get user country name.
        /// </summary>
        public string UserCountry { get => playerLastmatch?.Country ?? InvalidSteamIdString; }

        /// <summary>
        /// Gets user name.
        /// </summary>
        public string UserName { get => playerLastmatch?.Name ?? InvalidSteamIdString; }

        /// <summary>
        /// Get font style according to the player's status.
        /// </summary>
        /// <param name="player">player.</param>
        /// <param name="prototype">The existing System.Drawing.Font from which to create the new System.Drawing.Font.</param>
        /// <returns>new font.</returns>
        public static Font GetFontStyle(Player player, Font prototype)
        {
            var fontStyle = FontStyle.Bold;

            if (!(player.Won ?? true)) {
                fontStyle |= FontStyle.Strikeout;
            }

            return new Font(prototype, fontStyle);
        }

        /// <summary>
        /// Get average rate of even or odd Team color No.
        /// </summary>
        /// <param name="players">player.</param>
        /// <param name="team">team type.</param>
        /// <returns>team average rate value.</returns>
        public static int GetAverageRate(List<Player> players, TeamType team)
        {
            Func<Player, bool> predicate = team switch {
                TeamType.EvenColorNo => EvenFunc,
                TeamType.OddColorNo => OddFunc,
                _ => throw new ArgumentOutOfRangeException(nameof(team)),
            };

            return (int)players.Where(predicate)
                               .Select(player => player.Rating)
                               .Average();

            [ExcludeFromCodeCoverage]
            static bool EvenFunc(Player player) => player.Color % 2 == 0;
            [ExcludeFromCodeCoverage]
            static bool OddFunc(Player player) => player.Color % 2 != 0;
        }

        /// <summary>
        /// Get player last match.
        /// </summary>
        /// <param name="steamId">steam ID.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(string steamId)
        {
            if (steamId is null) {
                throw new ArgumentNullException(nameof(steamId));
            }

            var ret = await AoE2net.GetPlayerLastMatchAsync(steamId);

            foreach (var player in ret.LastMatch.Players) {
                List<PlayerRating> rate = null;
                if (player.SteamId != null) {
                    rate = await AoE2net.GetPlayerRatingHistoryAsync(
                        player.SteamId, ret.LastMatch.LeaderboardId ?? 0, 1);
                } else if (player.ProfilId is int profileId) {
                    rate = await AoE2net.GetPlayerRatingHistoryAsync(
                        profileId, ret.LastMatch.LeaderboardId ?? 0, 1);
                } else {
                    throw new FormatException($"Invalid profilId of Name:{player.Name}");
                }

                player.Rating ??= rate[0].Rating;
            }

            return ret;
        }

        /// <summary>
        /// Get the rate string.
        /// </summary>
        /// <param name="rate">rate.</param>
        /// <returns>rate string.</returns>
        public static string GetRateString(int? rate)
        {
            return rate?.ToString() ?? " N/A";
        }

        /// <summary>
        /// Get the player name string.
        /// </summary>
        /// <param name="name">player name.</param>
        /// <returns>player name string.</returns>
        public static string GetPlayerNameString(string name)
        {
            return name ?? "-- AI --";
        }

        /// <summary>
        /// Initialize the class.
        /// </summary>
        /// <param name="language">language used.</param>
        /// <returns>controler instance.</returns>
        public async Task<bool> InitAsync(Language language)
        {
            apiStrings = await AoE2net.GetStringsAsync(language);
            enStrings = await AoE2net.GetStringsAsync(Language.en);

            return true;
        }

        /// <summary>
        /// start the specified function with a delay.
        /// </summary>
        /// <param name="function">function.</param>
        public void DelayStart(Func<Task> function)
        {
            timerSteamIdVerify.Stop();
            delayedFunction = function;
            timerSteamIdVerify.Start();
        }

        /// <summary>
        /// Reload user data from AoE2.net.
        /// </summary>
        /// <param name="steamId">steam Id.</param>
        /// <returns>API result.</returns>
        public async Task<bool> GetPlayerDataAsync(string steamId)
        {
            try {
                playerLastmatch = await GetPlayerLastMatchAsync(steamId);
            } catch (Exception) {
                playerLastmatch = null;
                throw;
            }

            return true;
        }

        /// <summary>
        /// Get map name of the match.
        /// </summary>
        /// <param name="match">last match.</param>
        /// <returns>map name.</returns>
        public string GetMapName(Match match)
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
        public string GetCivEnName(Player player)
            => GetCivName(enStrings, player);

        /// <summary>
        /// Get player's civilization name.
        /// </summary>
        /// <param name="player">player.</param>
        /// <returns>civilization name.</returns>
        public string GetCivName(Player player)
            => GetCivName(apiStrings, player);

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
