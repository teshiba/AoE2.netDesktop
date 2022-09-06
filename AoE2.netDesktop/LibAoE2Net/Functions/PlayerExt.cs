﻿namespace AoE2NetDesktop.LibAoE2Net.Functions;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
    {
        return AoE2DeApp.GetCivImageLocation(player.GetCivEnName());
    }

    /// <summary>
    /// Get whether player color index is odd.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>Whether the color is odd.</returns>
    public static bool IsOddColor(this Player player)
        => player.Color % 2 != 0;

    /// <summary>
    /// Get match result.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>MatchResult.</returns>
    /// <exception cref="ArgumentNullException">players is null.</exception>
    public static MatchResult GetMatchResult(this Player player)
    {
        if(player is null) {
            throw new ArgumentNullException(nameof(player));
        }

        return player.Won switch {
            true => MatchResult.Victorious,
            false => MatchResult.Defeated,
            _ => MatchResult.InProgress,
        };
    }

    /// <summary>
    /// Get match result.
    /// </summary>
    /// <param name="players">player list.</param>
    /// <param name="team">Team type.</param>
    /// <returns>MatchResult.</returns>
    /// <exception cref="ArgumentNullException">players is null.</exception>
    public static MatchResult GetMatchResult(this List<Player> players, TeamType team)
    {
        if(players is null) {
            throw new ArgumentNullException(nameof(players));
        }

        var player = players.Where(SelectTeam(team)).First();

        return player.GetMatchResult();
    }

    /// <summary>
    /// Get average rate of even or odd Team color No.
    /// </summary>
    /// <param name="players">player.</param>
    /// <param name="team">team type.</param>
    /// <returns>team average rate value.</returns>
    public static int? GetAverageRate(this List<Player> players, TeamType team)
    {
        if(players is null) {
            throw new ArgumentNullException(nameof(players));
        }

        return (int?)players.Where(SelectTeam(team))
                            .Select(player => player.Rating)
                            .Average();
    }

    private static Func<Player, bool> SelectTeam(TeamType team)
    {
        return team switch {
            TeamType.EvenColorNo => player => !player.IsOddColor(),
            TeamType.OddColorNo => player => player.IsOddColor(),
            _ => throw new ArgumentOutOfRangeException(nameof(team)),
        };
    }
}
