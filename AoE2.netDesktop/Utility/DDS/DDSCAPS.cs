namespace AoE2NetDesktop.Utility.DDS;

/// <summary>
/// Specifies the complexity of the surfaces stored.
/// </summary>
public enum DDSCAPS
{
    /// <summary>
    /// Optional;
    /// must be used on any file that contains more than one surface
    /// (a mipmap, a cubic environment map, or mipmapped volume texture).
    /// </summary>
    COMPLEX = 0x8,

    /// <summary>
    /// Optional; should be used for a mipmap.
    /// </summary>
    MIPMAP = 0x400000,

    /// <summary>
    /// Required.
    /// </summary>
    TEXTURE = 0x1000,
}
