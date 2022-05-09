namespace AoE2NetDesktop.CtrlForm;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.DDS;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.User32;

/// <summary>
/// FormMain controler.
/// </summary>
public class CtrlMain : FormControler
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CtrlMain"/> class.
    /// </summary>
    public CtrlMain()
    {
    }

    /// <summary>
    /// Gets or sets auto reload interval second.
    /// </summary>
    public static int IntervalSec { get; set; } = 60 * 5;

    /// <summary>
    /// Gets or sets a value indicating whether reloading by timer.
    /// </summary>
    public static bool IsTimerReloading { get; set; } = false;

    /// <summary>
    /// Gets or sets system API.
    /// </summary>
    public static ISystemApi SystemApi { get; set; } = new SystemApi(new User32Api());

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

        static bool EvenFunc(Player player) => player.Color % 2 == 0;
        static bool OddFunc(Player player) => player.Color % 2 != 0;
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
    /// <param name="language">target language.</param>
    /// <returns>controler instance.</returns>
    public static async Task<bool> InitAsync(Language language)
    {
        StringsExt.InitAsync();
        await StringsExt.InitAsync(language);

        return true;
    }

    /// <summary>
    /// Gets whether AoE2de is the active window.
    /// </summary>
    /// <returns>true: AoE2de is the active window.</returns>
    public static bool IsAoE2deActive()
    {
        return SystemApi.GetActiveProcess() == AoE2DeApp.ProcessName;
    }

    /// <summary>
    /// Load map icon.
    /// </summary>
    /// <param name="mapType">map type ID.</param>
    /// <returns>Icon image.</returns>
    public static Image LoadMapIcon(int? mapType)
    {
        var filePath = MapIcons.GetFileName(mapType);
        return new ImageLoader(filePath).BitmapImage;
    }
}
