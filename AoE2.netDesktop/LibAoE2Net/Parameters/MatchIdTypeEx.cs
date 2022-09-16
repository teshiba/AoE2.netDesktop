namespace AoE2NetDesktop.LibAoE2Net.Parameters;

using System.Text.RegularExpressions;

/// <summary>
/// MatchIdType extention.
/// </summary>
public static class MatchIdTypeEx
{
    /// <summary>
    /// Get match ID type.
    /// </summary>
    /// <param name="id">id string.</param>
    /// <returns>MatchIdType.</returns>
    public static MatchIdType GetIdType(string id)
    {
        var ret = MatchIdType.Invalid;

        bool matchIdDecimal9Digits = Regex.IsMatch(id, @"\d{9}");
        bool matchUuidFormat = Regex.IsMatch(id, @"[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}");

        if(matchIdDecimal9Digits) {
            ret = MatchIdType.MatchId;
        } else if(matchUuidFormat) {
            ret = MatchIdType.Uuid;
        }

        return ret;
    }
}
