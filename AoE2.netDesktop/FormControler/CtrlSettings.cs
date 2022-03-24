namespace AoE2NetDesktop.Form
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using LibAoE2net;

    /// <summary>
    /// FormMain controler.
    /// </summary>
    public class CtrlSettings : FormControler
    {
        private const string InvalidSteamIdString = "-- Invalid ID --";
        private PlayerLastmatch playerLastmatch = new ();

        /// <summary>
        /// Initializes a new instance of the <see cref="CtrlSettings"/> class.
        /// </summary>
        public CtrlSettings()
        {
            PropertySetting = new PropertySettings {
                ChromaKey = Settings.Default.ChromaKey,
                IsAlwaysOnTop = Settings.Default.MainFormIsAlwaysOnTop,
                Opacity = (double)Settings.Default.MainFormOpacityPercent * 0.01,
                IsTransparency = Settings.Default.MainFormTransparency,
                IsHideTitle = Settings.Default.MainFormIsHideTitle,
                DrawHighQuality = Settings.Default.DrawHighQuality,
            };
            SelectedIdType = (IdType)Settings.Default.SelectedIdType;

            playerLastmatch = new PlayerLastmatch() {
                SteamId = Settings.Default.SteamId,
                ProfileId = Settings.Default.ProfileId,
            };
        }

        /// <summary>
        /// Gets or sets formProperty.
        /// </summary>
        public PropertySettings PropertySetting { get; set; }

        /// <summary>
        /// Gets FormHistory.
        /// </summary>
        public FormHistory FormMyHistory { get; private set; }

        /// <summary>
        /// Gets or sets selected ID type.
        /// </summary>
        public IdType SelectedIdType { get; set; }

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
            FormMyHistory = new FormHistory(ProfileId);
            FormMyHistory.Show();
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
            case IdType.NotSelected:
            default:
                throw new Exception($"Invalid IdType:{idtype}");
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
                var idText = SelectedIdType switch {
                    IdType.Steam => SteamId,
                    IdType.Profile => ProfileId.ToString(),
                    _ => throw new InvalidOperationException($"{SelectedIdType} is not defined as {nameof(IdType)} ."),
                };

                playerLastmatch = await AoE2netHelpers.GetPlayerLastMatchAsync(SelectedIdType, idText);
            } catch (HttpRequestException) {
                ret = false;
            }

            return ret;
        }
    }
}
