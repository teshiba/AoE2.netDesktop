﻿namespace AoE2NetDesktop.AoE2DE;

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.Utility.SysApi;

/// <summary>
/// AoE2DE Application information class.
/// </summary>
public class AoE2DeApp
{
    /// <summary>
    /// AoE2DE process name.
    /// </summary>
    public const string ProcessName = "AoE2DE_s";

    /// <summary>
    ///  time rate in the game.
    /// </summary>
    public const double TimeRateInGame = 1.7;

    /// <summary>
    /// AoE2DE Max Player Number.
    /// </summary>
    public const int PlayerNumMax = 8;

    /// <summary>
    /// Player color definitions.
    /// </summary>
    public static readonly List<Color> PlayerColors = new() {
        Color.Blue,
        Color.Red,
        Color.Green,
        Color.Yellow,
        Color.Aqua,
        Color.Magenta,
        Color.Gray,
        Color.Orange,
    };

    private const string SteamAppDefaultPath = $@"C:\Program Files (x86)\Steam\steamapps\common\AoE2DE\";
    private const string CivsPath = $@"widgetui\textures\menu\civs\";

    /// <summary>
    /// Gets or sets system API.
    /// </summary>
    public static ISystemApi SystemApi { get; set; } = new SystemApi(new User32Api());

    /// <summary>
    /// Get AoE2De App path.
    /// </summary>
    /// <returns>File name.</returns>
    public static string GetPath()
    {
        var ret = SystemApi.GetProcessFilePath(ProcessName);
        if(string.IsNullOrEmpty(ret)) {
            ret = SteamAppDefaultPath;
        }

        return ret;
    }

    /// <summary>
    /// Gets Image file location on AoE2De app.
    /// </summary>
    /// <param name="civName">civilization name in English.</param>
    /// <returns>Image file location.</returns>
    public static string GetCivImageLocation(string civName)
    {
        string ret = null;

        // Hindustanis image file name is old name.
        if(civName == "Hindustanis") {
            civName = "indians";
        }

        if(civName != null) {
            ret = $"{GetPath()}{CivsPath}{civName.ToLower()}.png";
            if(!File.Exists(ret)) {
                Debug.Print($"Cannot find {ret}. try loading from AoE2.net");
                ret = AoE2net.GetCivImageLocation(civName.ToLower());
            }
        }

        return ret;
    }

    /// <summary>
    /// Get Color.
    /// </summary>
    /// <param name="colorNo">Color No. which is defined by AoE2DE.</param>
    /// <returns>Color string or "-" if Color is null.</returns>
    public static Color GetColor(int? colorNo)
    {
        var ret = Color.Transparent;

        if(Enumerable.Range(1, 8).Contains(colorNo ?? 0)) {
            ret = PlayerColors[(int)colorNo - 1];
        }

        return ret;
    }
}
