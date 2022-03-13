namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using LibAoE2net;

    /// <summary>
    /// FormMain controler.
    /// </summary>
    public class CtrlSettings : FormControler
    {
        private const string InvalidSteamIdString = "-- Invalid ID --";

        private PlayerLastmatch playerLastmatch = new ();
        private bool isAlwaysOnTop;
        private bool isHideTitle;
        private double opacity;
        private string chromaKey;
        private bool isTransparency;

        /// <summary>
        /// Initializes a new instance of the <see cref="CtrlSettings"/> class.
        /// </summary>
        public CtrlSettings()
        {
            ChromaKey = Settings.Default.ChromaKey;
            SelectedIdType = (IdType)Settings.Default.SelectedIdType;
            playerLastmatch = new PlayerLastmatch() {
                SteamId = Settings.Default.SteamId,
                ProfileId = Settings.Default.ProfileId,
            };
        }

        /// <summary>
        /// Gets selected ID type.
        /// </summary>
        public FormHistory FormHistory { get; private set; }

        /// <summary>
        /// Gets or sets selected ID type.
        /// </summary>
        public IdType SelectedIdType { get; set; }

        /// <summary>
        /// Gets or sets ChromaKey.
        /// </summary>
        public string ChromaKey
        {
            get => chromaKey;
            set {
                chromaKey = value;
                OnChangeChromaKey?.Invoke(chromaKey);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether window title is hide.
        /// </summary>
        public bool IsHideTitle
        {
            get => isHideTitle;
            set {
                isHideTitle = value;
                OnChangeIsHideTitle?.Invoke(isHideTitle);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether window is AlwaysOnTop.
        /// </summary>
        public bool IsAlwaysOnTop
        {
            get => isAlwaysOnTop;
            set {
                isAlwaysOnTop = value;
                OnChangeIsAlwaysOnTop?.Invoke(isAlwaysOnTop);
            }
        }

        /// <summary>
        /// Gets or sets main form opacity.
        /// </summary>
        public double Opacity
        {
            get => opacity;
            set {
                opacity = value;
                OnChangeOpacity?.Invoke(opacity);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether main form is transparency.
        /// </summary>
        public bool IsTransparency
        {
            get => isTransparency;
            set {
                isTransparency = value;
                OnChangeIsTransparency?.Invoke(isTransparency);
            }
        }

        /// <summary>
        /// Gets or sets Action when ChromaKey is changed.
        /// </summary>
        public Action<string> OnChangeChromaKey { get; set; }

        /// <summary>
        /// Gets or sets Action when IsHideTitle is changed.
        /// </summary>
        public Action<bool> OnChangeIsHideTitle { get; set; }

        /// <summary>
        /// Gets or sets Action when IsAlwaysOnTop is changed.
        /// </summary>
        public Action<bool> OnChangeIsAlwaysOnTop { get; set; }

        /// <summary>
        /// Gets or sets Action when Opacity is changed.
        /// </summary>
        public Action<double> OnChangeOpacity { get; set; }

        /// <summary>
        /// Gets or sets Action when Transparency is changed.
        /// </summary>
        public Action<bool> OnChangeIsTransparency { get; set; }

        /// <summary>
        /// Gets get user Steam ID.
        /// </summary>
        public string SteamId { get => playerLastmatch.SteamId; }

        /// <summary>
        /// Gets get user profile ID.
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
        /// Show my play history.
        /// </summary>
        public void ShowMyHistory()
        {
            FormHistory = new FormHistory(ProfileId);
            FormHistory.Show();
        }

        /// <summary>
        /// Reload profile.
        /// </summary>
        /// <param name="idtype">target ID type.</param>
        /// <param name="idText">target ID text.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> ReloadProfileAsync(IdType idtype, string idText)
        {
            SelectedIdType = idtype;
            playerLastmatch = new PlayerLastmatch();

            switch (idtype) {
            case IdType.Steam:
                playerLastmatch.SteamId = idText;
                break;
            case IdType.Profile:
                playerLastmatch.ProfileId = int.Parse(idText);
                break;
            default:
                break;
            }

            return await ReadProfileAsync();
        }

        /// <summary>
        /// Get user data from AoE2.net.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<bool> ReadProfileAsync()
        {
            var ret = true;
            try {
                playerLastmatch = SelectedIdType switch {
                    IdType.Steam => await AoE2net.GetPlayerLastMatchAsync(SteamId),
                    IdType.Profile => await AoE2net.GetPlayerLastMatchAsync(ProfileId),
                    _ => new PlayerLastmatch(),
                };

                foreach (var player in playerLastmatch.LastMatch.Players) {
                    List<PlayerRating> rate = null;
                    var leaderBoardId = playerLastmatch.LastMatch.LeaderboardId ?? 0;

                    if (player.SteamId != null) {
                        rate = await AoE2net.GetPlayerRatingHistoryAsync(player.SteamId, leaderBoardId, 1);
                    } else if (player.ProfilId is int profileId) {
                        rate = await AoE2net.GetPlayerRatingHistoryAsync(profileId, leaderBoardId, 1);
                    } else {
                        throw new FormatException($"Invalid profilId of Name:{player.Name}");
                    }

                    if (rate.Count != 0) {
                        player.Rating ??= rate[0].Rating;
                    }
                }
            } catch (HttpRequestException) {
                ret = false;
            }

            return ret;
        }
    }
}
