namespace AoE2NetDesktop.LibAoE2Net.Functions;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Extention of Strings.
/// </summary>
public static class StringsExt
{
    private static Strings apiStrings;
    private static Strings enStrings;
    private static Task initTask;

    /// <summary>
    /// Initialize the class.
    /// </summary>
    public static void Init()
    {
        initTask = Task.Run(async () =>
        {
            enStrings = await AoE2net.GetStringsAsync(Language.en).ConfigureAwait(false);
            await InitApiStringsAsync(Language.en).ConfigureAwait(false);
        });
    }

    /// <summary>
    /// Initialize the class.
    /// </summary>
    /// <param name="language">language used.</param>
    /// <returns>controler instance.</returns>
    public static async Task<bool> InitAsync(Language language)
    {
        WaitInitTask();
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
        WaitInitTask();

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
        WaitInitTask();

        string mapName = apiStrings.MapType.GetString(match.MapType);

        if(enStrings.MapType.GetString(match.MapType) == "Custom") {
            mapName = $"{match.Rms}";
        }

        if(mapName == null) {
            mapName = $"Unknown(Map No.{match.MapType})";
        }

        return mapName;
    }

    /// <summary>
    /// Get player's civilization name in English.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>civilization name in English.</returns>
    public static string GetCivEnName(this Player player)
    {
        WaitInitTask();
        return GetCivName(enStrings, player);
    }

    /// <summary>
    /// Get player's civilization name.
    /// </summary>
    /// <param name="player">player.</param>
    /// <returns>civilization name.</returns>
    public static string GetCivName(this Player player)
    {
        WaitInitTask();
        return GetCivName(apiStrings, player);
    }

    ///////////////////////////////////////////////////////////////////////
    // private
    ///////////////////////////////////////////////////////////////////////
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncWait)]
    private static void WaitInitTask()
    {
        if(initTask == null) {
            Init();
        }

        initTask.Wait();
    }

    private static string GetCivName(Strings strings, Player player)
    {
        string ret = strings.Civ.GetString(player.Civ);
        if(ret is null) {
            if(player.Civ == 40) {
                ret = "Dravidians";
            } else if(player.Civ == 41) {
                ret = "Bengalis";
            } else if(player.Civ == 42) {
                ret = "Gurjaras";
            } else {
                ret = $"invalid civ:{player.Civ}";
            }
        }

        return ret;
    }

    private static async Task InitApiStringsAsync(Language language)
    {
        if(apiStrings?.Language != language.ToApiString()) {
            apiStrings = await AoE2net.GetStringsAsync(language).ConfigureAwait(false);
        }
    }
}
