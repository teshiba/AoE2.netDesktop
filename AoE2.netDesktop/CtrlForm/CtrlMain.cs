namespace AoE2NetDesktop.CtrlForm;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.DDS;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.SysApi;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

/// <summary>
/// FormMain controler.
/// </summary>
public class CtrlMain : FormControler
{
    private static readonly Dictionary<MatchResult, Color> MatchResultColor = new() {
        { MatchResult.Victorious, Color.Green },
        { MatchResult.Defeated, Color.Red },
        { MatchResult.InProgress, Color.Gray },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="CtrlMain"/> class.
    /// </summary>
    public CtrlMain()
    {
    }

    /// <summary>
    /// Gets or sets auto reload interval second.
    /// </summary>
    public static Match LastMatch { get; set; }

    /// <summary>
    /// Gets or sets auto reload interval second.
    /// </summary>
    public static int IntervalSec { get; set; } = 60 * 5;

    /// <summary>
    /// Gets or sets a value indicating whether reloading by timer.
    /// </summary>
    public static bool IsReloadingByTimer { get; set; } = false;

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

        if(!(player.Won ?? true)) {
            fontStyle |= FontStyle.Strikeout;
        }

        return new Font(prototype, fontStyle);
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
        StringsExt.Init();
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

    /// <summary>
    /// Get the number of losses.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>lose count.</returns>
    public static string GetLossesString(Player player)
    {
        var loses = player.Games - player.Wins;

        return loses?.ToString() ?? "N/A";
    }

    /// <summary>
    /// Get the number of wins.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>win count.</returns>
    public static string GetWinsString(Player player)
    {
        return player.Wins?.ToString() ?? "N/A";
    }

    /// <summary>
    /// Gets Elapsed Time.
    /// </summary>
    /// <returns>Elapsed time.</returns>
    public static string GetElapsedTime()
    {
        var ret = DateTimeExt.InvalidTime;

        if(LastMatch != null) {
            var realTime = LastMatch.GetElapsedTime().ToString(@"h\:mm\:ss");
            var inGameTime = new TimeSpan((long)(LastMatch.GetElapsedTime().Ticks * 1.7)).ToString(@"h\:mm\:ss");
            ret = $"{realTime} ({inGameTime} in game)";
        }

        return ret;
    }

    /// <summary>
    /// Gets Opened Time.
    /// </summary>
    /// <returns>Opened time.</returns>
    public static string GetOpenedTime()
    {
        var ret = DateTimeExt.InvalidTime;

        if(LastMatch != null) {
            var timezone = DateTimeExt.TimeZoneInfo.ToString().Split(" ")[0].Replace("(", string.Empty).Replace(")", string.Empty);
            ret = $"{DateTimeExt.GetDateTimeFormat(LastMatch.GetOpenedTime())} {timezone}";
        }

        return ret;
    }

    /// <summary>
    /// Get match result string by victory value.
    /// </summary>
    /// <param name="won">victory value of aoe2.net.</param>
    /// <returns>Match result.</returns>
    public static MatchResult GetMatchResult(bool? won)
    {
        return won switch {
            false => MatchResult.Defeated,
            true => MatchResult.Victorious,
            _ => MatchResult.InProgress,
        };
    }

    /// <summary>
    /// Get match result color.
    /// </summary>
    /// <param name="matchResult">match result.</param>
    /// <returns>result color.</returns>
    public static Color GetMatchResultColor(MatchResult matchResult)
    {
        var result = MatchResultColor.TryGetValue(matchResult, out Color ret);
        if(!result) {
            ret = Color.Transparent;
        }

        return ret;
    }
}
