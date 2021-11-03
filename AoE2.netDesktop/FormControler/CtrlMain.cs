namespace AoE2NetDesktop.Form
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
        private const string InvalidSteamIdString = "-- Invalid ID --";
        private const int VerifyWaitMsec = 1500;

        private readonly System.Timers.Timer timerSteamIdVerify;

        private Func<Task> delayedFunction;
        private PlayerLastmatch playerLastmatch = new ();

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
        /// Gets selected ID type.
        /// </summary>
        public FormHistory FormHistory { get; private set; }

        /// <summary>
        /// Gets or sets selected ID type.
        /// </summary>
        public IdType SelectedId { get; set; }

        /// <summary>
        /// Gets get user country name.
        /// </summary>
        public string SteamId { get => playerLastmatch.SteamId; }

        /// <summary>
        /// Gets get user country name.
        /// </summary>
        public int ProfileId { get => playerLastmatch.ProfileId ?? 0; }

        /// <summary>
        /// Gets get user country name.
        /// </summary>
        public string UserCountry { get => CountryCode.ConvertToFullName(playerLastmatch.Country); }

        /// <summary>
        /// Gets user name.
        /// </summary>
        public string UserName { get => playerLastmatch.Name ?? InvalidSteamIdString; }

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
        public static int? GetAverageRate(List<Player> players, TeamType team)
        {
            if (players is null) {
                throw new ArgumentNullException(nameof(players));
            }

            Func<Player, bool> predicate = team switch {
                TeamType.EvenColorNo => EvenFunc,
                TeamType.OddColorNo => OddFunc,
                _ => throw new ArgumentOutOfRangeException(nameof(team)),
            };

            return (int?)players.Where(predicate)
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
        /// <param name="userId">ID type.</param>
        /// <param name="idText">ID text.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public static async Task<PlayerLastmatch> GetPlayerLastMatchAsync(IdType userId, string idText)
        {
            if (idText is null) {
                throw new ArgumentNullException(nameof(idText));
            }

            var ret = userId switch {
                IdType.Steam => await AoE2net.GetPlayerLastMatchAsync(idText),
                IdType.Profile => await AoE2net.GetPlayerLastMatchAsync(int.Parse(idText)),
                _ => new PlayerLastmatch(),
            };

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

                if (rate.Count != 0) {
                    player.Rating ??= rate[0].Rating;
                }
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
        public static async Task<bool> InitAsync(Language language)
        {
            StringsExt.InitAsync();
            await StringsExt.InitAsync(language);

            return true;
        }

        /// <summary>
        /// Show History.
        /// </summary>
        public void ShowHistory()
        {
            FormHistory = new FormHistory(ProfileId);
            FormHistory.Show();
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
        /// Get user data from AoE2.net.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <param name="idText">steam Id.</param>
        /// <returns>API result.</returns>
        /// <returns>API run result.</returns>1
        public async Task<bool> ReadPlayerDataAsync(IdType id, string idText)
        {
            var ret = true;

            try {
                playerLastmatch = id switch {
                    IdType.Steam => await GetPlayerLastMatchAsync(id, idText),
                    IdType.Profile => await GetPlayerLastMatchAsync(id, idText),
                    _ => new PlayerLastmatch(),
                };
            } catch (Exception) {
                playerLastmatch = new PlayerLastmatch();
                throw;
            }

            return ret;
        }
    }
}
