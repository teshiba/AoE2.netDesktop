namespace AoE2NetDesktop.Utility.DDS;

/// <summary>
/// Surface pixel format class.
/// </summary>
public class DDS_PIXELFORMAT
{
    /// <summary>
    /// Gets or sets Structure size; set to 32 (bytes).
    /// </summary>
    public int DwSize { get; set; }

    /// <summary>
    /// Gets or sets Values which indicate what type of data is in the surface.
    /// </summary>
    public DDPF DwFlags { get; set; }

    /// <summary>
    /// Gets or sets Four-character codes for specifying compressed or custom formats.
    /// Possible values include: DXT1, DXT2, DXT3, DXT4, or DXT5.
    /// A FourCC of DX10 indicates the prescense of the DDS_HEADER_DXT10 extended header,
    /// and the dxgiFormat member of that structure indicates the true format.
    /// When using a four-character code, dwFlags must include DDPF_FOURCC.
    /// </summary>
    public int DwFourCC { get; set; }

    /// <summary>
    /// Gets or sets Number of bits in an RGB (possibly including alpha) format.
    /// Valid when dwFlags includes
    /// <see cref="DDPF.RGB"/> ,<see cref="DDPF.LUMINANCE"/> , or <see cref="DDPF.YUV"/>.
    /// </summary>
    public int DwRGBBitCount { get; set; }

    /// <summary>
    /// Gets or sets Red (or luminance or Y) mask for reading color data.
    /// For instance, given the A8R8G8B8 format, the red mask would be 0x00ff0000.
    /// </summary>
    public int DwRBitMask { get; set; }

    /// <summary>
    /// Gets or sets Green (or U) mask for reading color data.
    /// For instance, given the A8R8G8B8 format, the green mask would be 0x0000ff00.
    /// </summary>
    public int DwGBitMask { get; set; }

    /// <summary>
    /// Gets or sets Blue (or V) mask for reading color data.
    /// For instance, given the A8R8G8B8 format, the blue mask would be 0x000000ff.
    /// </summary>
    public int DwBBitMask { get; set; }

    /// <summary>
    /// Gets or sets Alpha mask for reading alpha data.
    /// dwFlags must include <see cref="DDPF.ALPHAPIXELS "/>or <see cref="DDPF.ALPHA"/>.
    /// For instance, given the A8R8G8B8 format, the alpha mask would be 0xff000000.
    /// </summary>
    public int DwABitMask { get; set; }
}
