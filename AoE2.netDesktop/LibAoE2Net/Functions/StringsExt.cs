namespace AoE2NetDesktop.LibAoE2Net.Functions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;

/// <summary>
/// Extention of Strings.
/// </summary>
public static class StringsExt
{
    private static Strings apiStrings;
    private static Strings enStrings;
    private static bool initDone = false;

    /// <summary>
    /// Initialize the class.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public static async Task InitAsync()
    {
        if(!initDone) {
            enStrings = await AoE2net.GetStringsAsync(Language.en).ConfigureAwait(false);
            await InitApiStringsAsync(Language.en).ConfigureAwait(false);
        }

        initDone = true;
    }

    /// <summary>
    /// Dispose the class.
    /// </summary>
    public static void Dispose()
    {
        apiStrings = null;
        enStrings = null;
        initDone = false;
    }

    /// <summary>
    /// Initialize the class.
    /// </summary>
    /// <param name="language">language used.</param>
    /// <returns>controler instance.</returns>
    public static async Task<bool> InitAsync(Language language)
    {
        CheckInitDone();

        await InitApiStringsAsync(language).ConfigureAwait(false);

        return true;
    }

    /// <summary>
    /// Gets the string of the ID in the list.
    /// </summary>
    /// <param name="stringIds">target list.</param>
    /// <param name="id">target id.</param>
    /// <returns>Found string.</returns>
    public static string GetString(this List<StringId> stringIds, int? id)
    {
        CheckInitDone();

        string ret;
        try {
            ret = stringIds.Where(x => x.Id == id).First().String;
        } catch(InvalidOperationException) {
            ret = null;
        }

        return ret;
    }

    /// <summary>
    /// Get map name of the match.
    /// </summary>
    /// <param name="match">last match.</param>
    /// <returns>map name.</returns>
    public static string GetMapName(this Match match)
    {
        CheckInitDone();

        var mapName = match.Rms ?? apiStrings.MapType.GetString(match.MapType);
        mapName ??= SelfDefined.ApiStrings.MapType.GetString(match.MapType);
        mapName ??= $"Unknown #{match.MapType}";

        return mapName;
    }

    /// <summary>
    /// Get player's civilization name in English.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>civilization name in English.</returns>
    public static string GetCivEnName(this Player player)
    {
        CheckInitDone();

        return GetCivName(enStrings, player);
    }

    /// <summary>
    /// Get player's civilization name.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>civilization name.</returns>
    public static string GetCivName(this Player player)
    {
        CheckInitDone();

        return GetCivName(apiStrings, player);
    }

    ///////////////////////////////////////////////////////////////////////
    // private
    ///////////////////////////////////////////////////////////////////////
    private static string GetCivName(Strings strings, Player player)
    {
        string ret = strings.Civ.GetString(player.Civ);
        ret ??= SelfDefined.ApiStrings.Civ.GetString(player.Civ);
        ret ??= $"invalid civ:{player.Civ}";

        return ret;
    }

    private static async Task InitApiStringsAsync(Language language)
    {
        if(apiStrings?.Language != language.ToApiString()) {
            apiStrings = await AoE2net.GetStringsAsync(language).ConfigureAwait(false);
        }
    }

    private static void CheckInitDone()
    {
        if(!initDone) {
            throw new InvalidOperationException($"{nameof(InitAsync)} have not called yet.");
        }
    }
}
