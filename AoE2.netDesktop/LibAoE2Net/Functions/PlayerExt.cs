namespace AoE2NetDesktop.LibAoE2Net.Functions;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// Extention of Player class.
/// </summary>
public static class PlayerExt
{
    /// <summary>
    /// player name if player name is null.
    /// </summary>
    public const string PlayerNullName = "-- Name is NOT set --";

    /// <summary>
    /// Check Diplomacy.
    /// </summary>
    /// <param name="player1">a player.</param>
    /// <param name="player2">other player.</param>
    /// <returns>Diplomacy status.</returns>
    public static Diplomacy CheckDiplomacy(this Player player1, Player player2)
    {
        var ret = Diplomacy.Neutral;

        if(player1.Color != null && player2.Color != null) {
            if((int)player1.Color % 2 == (int)player2.Color % 2) {
                ret = Diplomacy.Ally;
            } else {
                ret = Diplomacy.Enemy;
            }
        }

        return ret;
    }

    /// <summary>
    /// Get Color Number string.
    /// </summary>
    /// <param name="player">Player.</param>
    /// <returns>Color string or "-" if Color is null.</returns>
    public static string GetColorString(this Player player)
        => player is null ?
        throw new ArgumentNullException(nameof(player))
        : player.Color?.ToString() ?? "-";

    /// <summary>
    /// Get Color.
    /// </summary>
    /// <param name="player">Player.</param>
    /// <returns>Color string or "-" if Color is null.</returns>
    public static Color GetColor(this Player player)
                => AoE2DeApp.GetColor(player.Color);

    /// <summary>
    /// Get rate string.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>
    /// rate value and rating change value.
    /// if rate is unavilable : "----".
    /// </returns>
    public static string GetRatingString(this Player player)
    {
        string ret = (player.Rating?.ToString() ?? "----")
                    + (player.RatingChange?.Contains('-') ?? true ? string.Empty : "+")
                    + player.RatingChange?.ToString();

        return ret;
    }

    /// <summary>
    /// Get the win marker.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>win marker string.</returns>
    public static string GetWinMarkerString(this Player player)
    {
        string ret;

        if(player.Won == null) {
            ret = "---";
        } else {
            ret = (bool)player.Won ? "o" : string.Empty;
        }

        return ret;
    }

    /// <summary>
    /// Gets Image file location on AoE2De app.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>Image file location.</returns>
    public static string GetCivImageLocation(this Player player)
        => AoE2DeApp.GetCivImageLocation(player.GetCivEnName());

    /// <summary>
    /// Get whether player color index is odd.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>Whether the color is odd.</returns>
    public static bool IsOddColor(this Player player)
        => player.Color % 2 != 0;

    /// <summary>
    /// Get <see cref="TeamType"/>.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>Whether the color is odd.</returns>
    public static TeamType GetTeamType(this Player player)
        => player.IsOddColor() ? TeamType.OddColorNo : TeamType.EvenColorNo;

    /// <summary>
    /// Get whether player rating increased.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>Whether the color is odd.</returns>
    public static bool? IsRatingIncreased(this Player player)
        => !player.RatingChange?.Contains('-');

    /// <summary>
    /// Get average rate of even or odd Team color No.
    /// </summary>
    /// <param name="players">player.</param>
    /// <param name="team">team type.</param>
    /// <returns>team average rate value.</returns>
    public static int? GetAverageRate(this List<Player> players, TeamType team)
    {
        return players is null
            ? throw new ArgumentNullException(nameof(players))
            : (int?)players.Where(team.SelectTeam())
                            .Select(player => player.Rating)
                            .Average();
    }
}
