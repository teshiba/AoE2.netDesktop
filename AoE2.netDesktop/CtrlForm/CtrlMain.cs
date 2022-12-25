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

using static LabelType;

/// <summary>
/// FormMain controler.
/// </summary>
public class CtrlMain : FormControler
{
    private const double TimeRateInGame = 1.7;

    /// <summary>
    ///  Bordered string styles.
    /// </summary>
    public static readonly Dictionary<LabelType, BorderedStringStyle> BorderStyles = new() {
        { ScoreValue1v1, new BorderedStringStyle(18, Color.Black, Color.DeepSkyBlue) },
        { ScoreLabel1v1, new BorderedStringStyle(18, Color.Black, Color.DarkGoldenrod) },
        { MyName, new BorderedStringStyle(20, Color.Black, Color.DarkOrange) },
        { PlayerName, new BorderedStringStyle(20, Color.Black, Color.MediumSeaGreen) },
        { RateValueTeam, new BorderedStringStyle(22, Color.Black, Color.DeepSkyBlue) },
        { CivNameTeam, new BorderedStringStyle(15, Color.Black, Color.YellowGreen) },
        { AveRateTeam, new BorderedStringStyle(18, Color.Black, Color.DarkGoldenrod) },
        { ColorNoTeam, new BorderedStringStyle(23, Color.Black, Color.White) },
        { MapNameTeam, new BorderedStringStyle(28, Color.Black, Color.DarkKhaki) },
        { GameId, new BorderedStringStyle(14, Color.Black, Color.LightSeaGreen) },
        { ServerName, new BorderedStringStyle(14, Color.Black, Color.LightSeaGreen) },
        { StartTime, new BorderedStringStyle(18, Color.Black, Color.White) },
        { ElapsedTime, new BorderedStringStyle(20, Color.Black, Color.White) },
        { Victorious, new BorderedStringStyle(18, Color.Black, Color.Green) },
        { Defeated, new BorderedStringStyle(18, Color.Black, Color.Red) },
        { InProgress, new BorderedStringStyle(18, Color.Black, Color.SlateGray) },
        { Unknown, new BorderedStringStyle(18, Color.Black, Color.DimGray) },
        { NotStarted, new BorderedStringStyle(18, Color.Black, Color.DarkGray) },
        { Finished, new BorderedStringStyle(18, Color.Black, Color.DarkGray) },
        { MatchNo, new BorderedStringStyle(14, Color.Black, Color.LightGoldenrodYellow) },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="CtrlMain"/> class.
    /// </summary>
    public CtrlMain()
    {
    }

    /// <summary>
    /// Gets or sets current displayed match.
    /// </summary>
    public static Match DisplayedMatch { get; set; }

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
        await StringsExt.InitAsync();
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
    /// <param name="leaderboard">player's leaderboard.</param>
    /// <returns>lose count.</returns>
    public static string GetLossesString(Leaderboard leaderboard)
    {
        var loses = leaderboard.Games - leaderboard.Wins;

        return loses?.ToString() ?? "N/A";
    }

    /// <summary>
    /// Get the number of wins.
    /// </summary>
    /// <param name="leaderboard">player's leaderboard.</param>
    /// <returns>win count.</returns>
    public static string GetWinsString(Leaderboard leaderboard)
    {
        return leaderboard.Wins?.ToString() ?? "N/A";
    }

    /// <summary>
    /// Gets Elapsed Time.
    /// </summary>
    /// <param name="match">match.</param>
    /// <returns>Elapsed time.</returns>
    public static string GetElapsedTimeString(Match match)
    {
        var ret = DateTimeExt.InvalidTime;

        if(match != null) {
            var realTime = match.GetElapsedTime().ToString(@"h\:mm\:ss");
            var inGameTime = new TimeSpan((long)(match.GetElapsedTime().Ticks * TimeRateInGame)).ToString(@"h\:mm\:ss");
            ret = $"{realTime} ({inGameTime} in game)";
        }

        return ret;
    }

    /// <summary>
    /// Gets Opened Time.
    /// </summary>
    /// <param name="match">match.</param>
    /// <returns>Opened time.</returns>
    public static string GetOpenedTimeString(Match match)
    {
        var ret = DateTimeExt.InvalidTime;

        if(match != null) {
            var timezone = DateTimeExt.TimeZoneInfo.ToString().Split(" ")[0].Replace("(", string.Empty).Replace(")", string.Empty);
            ret = $"{DateTimeExt.GetDateTimeFormat(match.GetOpenedTime())} {timezone}";
        }

        return ret;
    }

    /// <summary>
    /// Get match result color.
    /// </summary>
    /// <param name="matchResult">match result.</param>
    /// <returns>Bordered style.</returns>
    public static BorderedStringStyle GetBorderedStyle(MatchResult matchResult)
    {
        BorderedStringStyle ret = matchResult switch {
            MatchResult.Victorious => BorderStyles[Victorious],
            MatchResult.Defeated => BorderStyles[Defeated],
            MatchResult.InProgress => BorderStyles[InProgress],
            MatchResult.Unknown => BorderStyles[Unknown],
            MatchResult.NotStarted => BorderStyles[NotStarted],
            MatchResult.Finished => BorderStyles[Finished],
            _ => null,
        };
        return ret;
    }

    /// <summary>
    /// Get match result color.
    /// </summary>
    /// <param name="player">Target player.</param>
    /// <param name="profileId">your profileID.</param>
    /// <returns>Bordered style.</returns>
    public static BorderedStringStyle GetPlayerBorderedStyle(Player player, int? profileId)
    {
        BorderedStringStyle ret;

        if(player?.ProfilId == profileId) {
            ret = BorderStyles[MyName];
        } else {
            ret = BorderStyles[PlayerName];
        }

        return ret;
    }

    /// <summary>
    /// Get ,atch No. text.
    /// </summary>
    /// <param name="matchNo">match No.</param>
    /// <returns>Match No. text.</returns>
    public static string GetMatchNoString(int? matchNo)
        => (matchNo ?? 0) != 0 ? $"{matchNo} match ago" : "Last match";
}
