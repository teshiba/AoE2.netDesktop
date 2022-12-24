namespace AoE2NetDesktop.LibAoE2Net.Parameters;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;

using System;

/// <summary>
/// TeamType extention functions.
/// </summary>
public static class TeamTypeEx
{
    /// <summary>
    /// Select odd or even Team funciton.
    /// </summary>
    /// <param name="team">team type.</param>
    /// <returns>select function.</returns>
    /// <exception cref="ArgumentOutOfRangeException">TeamType is out of range.</exception>
    public static Func<Player, bool> SelectTeam(this TeamType team)
    {
        return team switch {
            TeamType.EvenColorNo => player => !player.IsOddColor(),
            TeamType.OddColorNo => player => player.IsOddColor(),
            _ => throw new ArgumentOutOfRangeException(nameof(team)),
        };
    }
}