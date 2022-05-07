namespace AoE2NetDesktop.Utility.DDS;

/// <summary>
/// Flags to indicate which members contain valid data.
/// </summary>
public enum DDSD
{
    /// <summary>
    /// Required in every .dds file.
    /// </summary>
    CAPS = 0x1,

    /// <summary>
    /// Required in every .dds file.
    /// </summary>
    HEIGHT = 0x2,

    /// <summary>
    /// Required in every .dds file.
    /// </summary>
    WIDTH = 0x4,

    /// <summary>
    /// Required when pitch is provided for an uncompressed texture.
    /// </summary>
    PITCH = 0x8,

    /// <summary>
    /// Required in every .dds file.
    /// </summary>
    PIXELFORMAT = 0x1000,

    /// <summary>
    /// Required in a mipmapped texture.
    /// </summary>
    MIPMAPCOUNT = 0x20000,

    /// <summary>
    /// Required when pitch is provided for a compressed texture.
    /// </summary>
    LINEARSIZE = 0x80000,

    /// <summary>
    /// Required in a depth texture.
    /// </summary>
    DEPTH = 0x800000,
}
