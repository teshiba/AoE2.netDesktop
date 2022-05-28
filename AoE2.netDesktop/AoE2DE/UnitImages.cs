namespace AoE2NetDesktop.AoE2DE;

using AoE2NetDesktop.Utility.DDS;

using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Map Icons class.
/// </summary>
public class UnitImages
{
    private const string Path = $@"widgetui\textures\ingame\units\";

    private static readonly Dictionary<string, string> FileNames = new() {
        { "Britons", "041_50730" },
        { "Franks", "046_50730" },
        { "Goths", "050_50730" },
        { "Teutons", "045_50730" },
        { "Japanese", "044_50730" },
        { "Chinese", "036_50730" },
        { "Byzantines", "035_50730" },
        { "Persians", "043_50730" },
        { "Saracens", "037_50730" },
        { "Turks", "039_50730" },
        { "Vikings", "038_50730" },
        { "Mongols", "042_50730" },
        { "Celts", "047_50730" },
        { "Spanish", "106_50730" },
        { "Aztecs", "110_50730" },
        { "Mayans", "108_50730" },
        { "Huns", "105_50730" },
        { "Koreans", "117_50730" },
        { "Italians", "133_50730" },
        { "Hindustanis", "385_50730" },
        { "Incas", "097_50730" },
        { "Magyars", "099_50730" },
        { "Slavs", "114_50730" },
        { "Portuguese", "190_50730" },
        { "Ethiopians", "195_50730" },
        { "Malians", "197_50730" },
        { "Berbers", "191_50730" },
        { "Khmer", "231_50730" },
        { "Malay", "233_50730" },
        { "Burmese", "230_50730" },
        { "Vietnamese", "232_50730" },
        { "Bulgarians", "249_50730" },
        { "Tatars", "251_50730" },
        { "Cumans", "252_50730" },
        { "Lithuanians", "253_50730" },
        { "Burgundians", "249_50730" },
        { "Sicilians", "356_50730" },
        { "Poles", "369_50730" },
        { "Bohemians", "370_50730" },
        { "Dravidians", "386_50730" },
        { "Bengalis", "389_50730" },
        { "Gurjaras", "390_50730" },
    };

    /// <summary>
    /// Get unique unit file name.
    /// </summary>
    /// <param name="civName">Civilization Name.</param>
    /// <returns>File name.</returns>
    public static string GetFileName(string civName)
    {
        var ret = string.Empty;

        if(civName is not null) {
            var appPath = AoE2DeApp.GetPath();

            ret = $"{appPath}{Path}265_50730.DDS";

            if(FileNames.TryGetValue(civName, out string fileName)) {
                ret = $"{appPath}{Path}{fileName}.DDS";
            }
        }

        return ret;
    }

    /// <summary>
    /// Get unique unit bitmap data.
    /// </summary>
    /// <param name="civName">Civilization Name.</param>
    /// <param name="backColor">Back color.</param>
    /// <returns>bitmap image data.</returns>
    public static Image Load(string civName, Color backColor)
    {
        return new ImageLoader(GetFileName(civName), backColor).BitmapImage;
    }
}
