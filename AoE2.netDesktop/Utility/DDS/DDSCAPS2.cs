namespace AoE2NetDesktop.Utility.DDS;

/// <summary>
/// Additional detail about the surfaces stored.
/// </summary>
public enum DDSCAPS2
{
    /// <summary>
    /// Required for a cube map.
    /// </summary>
    CUBEMAP = 0x200,

    /// <summary>
    /// Required when these surfaces are stored in a cube map.
    /// </summary>
    CUBEMAP_POSITIVEX = 0x400,

    /// <summary>
    /// Required when these surfaces are stored in a cube map.
    /// </summary>
    CUBEMAP_NEGATIVEX = 0x800,

    /// <summary>
    /// Required when these surfaces are stored in a cube map.
    /// </summary>
    CUBEMAP_POSITIVEY = 0x1000,

    /// <summary>
    /// Required when these surfaces are stored in a cube map.
    /// </summary>
    CUBEMAP_NEGATIVEY = 0x2000,

    /// <summary>
    /// Required when these surfaces are stored in a cube map.
    /// </summary>
    CUBEMAP_POSITIVEZ = 0x4000,

    /// <summary>
    /// Required when these surfaces are stored in a cube map.
    /// </summary>
    CUBEMAP_NEGATIVEZ = 0x8000,

    /// <summary>
    /// Required for a volume texture.
    /// </summary>
    VOLUME = 0x200000,
}
