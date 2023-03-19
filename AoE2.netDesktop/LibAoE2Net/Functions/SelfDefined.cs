namespace AoE2NetDesktop.LibAoE2Net.Functions;

using System.Collections.Generic;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;

/// <summary>
/// Self-defined names, which aoe2.net does NOT define.
/// </summary>
public static class SelfDefined
{
    /// <summary>
    /// Gets self-defined apiStrings.
    /// </summary>
    public static Strings ApiStrings { get; private set; } = new Strings() {
        MapType = new List<StringId>() {
            new StringId() { Id = 175, String = "Morass" },
            new StringId() { Id = 176, String = "Shoals" },
        },
        Civ = new List<StringId> {
            new StringId() { Id = 40, String = "Dravidians" },
            new StringId() { Id = 41, String = "Bengalis" },
            new StringId() { Id = 42, String = "Gurjaras" },
        },
    };
}
