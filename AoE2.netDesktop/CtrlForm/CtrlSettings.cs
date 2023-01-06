namespace AoE2NetDesktop.CtrlForm;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using AoE2NetDesktop;
using AoE2NetDesktop.Form;
using AoE2NetDesktop.LibAoE2Net;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

/// <summary>
/// FormMain controler.
/// </summary>
public class CtrlSettings : FormControler
{
    private const string InvalidSteamIdString = "-- Invalid ID --";
    private PlayerLastmatch playerLastmatch = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="CtrlSettings"/> class.
    /// </summary>
    public CtrlSettings()
    {
        SelectedIdType = (IdType)Settings.Default.SelectedIdType;
        playerLastmatch = new PlayerLastmatch() {
            SteamId = Settings.Default.SteamId,
            ProfileId = Settings.Default.ProfileId,
        };
    }

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
    public string SteamId
        => playerLastmatch.SteamId;

    /// <summary>
    /// Gets get user profile ID.
    /// </summary>
    public int ProfileId
        => playerLastmatch.ProfileId ?? 0;

    /// <summary>
    /// Gets get user country name.
    /// </summary>
    public string UserCountry
        => CountryCode.ConvertToFullName(playerLastmatch.Country);

    /// <summary>
    /// Gets user name.
    /// </summary>
    public string UserName
        => playerLastmatch.Name ?? InvalidSteamIdString;

    /// <summary>
    /// Gets network status.
    /// </summary>
    public NetStatus NetStatus { get; internal set; } = NetStatus.Connecting;

    /// <summary>
    /// Show my play history.
    /// </summary>
    /// <param name="matchViewer">Related matchViewer instance.</param>
    public void ShowMyHistory(FormMain matchViewer)
    {
        FormMyHistory = new FormHistory(matchViewer, ProfileId);
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

        switch(idtype) {
        case IdType.Steam:
            playerLastmatch.SteamId = idText;
            break;
        case IdType.Profile:
            playerLastmatch.ProfileId = int.Parse(idText);
            break;
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
        } catch(HttpRequestException) {
            ret = false;
        } catch(TaskCanceledException) {
            ret = false;
        }

        return ret;
    }
}
