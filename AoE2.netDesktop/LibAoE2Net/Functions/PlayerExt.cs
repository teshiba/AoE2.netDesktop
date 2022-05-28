namespace AoE2NetDesktop.LibAoE2Net.Functions;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Extention of Player class.
/// </summary>
public static class PlayerExt
{
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
    {
        if(player is null) {
            throw new ArgumentNullException(nameof(player));
        }

        return player.Color?.ToString() ?? "-";
    }

    /// <summary>
    /// Get Color.
    /// </summary>
    /// <param name="player">Player.</param>
    /// <returns>Color string or "-" if Color is null.</returns>
    public static Color GetColor(this Player player)
    {
        var colorList = new Dictionary<int?, Color> {
            { 1, Color.Blue },
            { 2, Color.Red },
            { 3, Color.Green },
            { 4, Color.Yellow },
            { 5, Color.Aqua },
            { 6, Color.Magenta },
            { 7, Color.Gray },
            { 8, Color.Orange },
        };

        if(!colorList.TryGetValue(player.Color, out Color ret)) {
            ret = Color.Transparent;
        }

        return ret;
    }

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
}
