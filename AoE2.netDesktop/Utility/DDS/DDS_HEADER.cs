namespace AoE2NetDesktop.Utility.DDS;

/// <summary>
/// Direct Draw Surface Header.
/// </summary>
public record DdsHeader
{
    /// <summary>
    /// dwReserved1 Size.
    /// </summary>
    public const int DwReserved1Size = 11;

    /// <summary>
    /// Gets or sets size of structure. This member must be set to 124.
    /// </summary>
    public int DwSize { get; set; }

    /// <summary>
    /// Gets or sets Flags to indicate which members contain valid data.
    /// </summary>
    public DDSD DwFlags { get; set; }

    /// <summary>
    /// Gets or sets Surface height (in pixels).
    /// </summary>
    public int DwHeight { get; set; }

    /// <summary>
    /// Gets or sets Surface width (in pixels).
    /// </summary>
    public int DwWidth { get; set; }

    /// <summary>
    /// Gets or sets The pitch or number of bytes per scan line in an uncompressed texture;
    /// the total number of bytes in the top level texture for a compressed texture.
    /// </summary>
    public int DwPitchOrLinearSize { get; set; }

    /// <summary>
    /// Gets or sets Depth of a volume texture (in pixels), otherwise unused.
    /// </summary>
    public int DwDepth { get; set; }

    /// <summary>
    /// Gets or sets Number of mipmap levels, otherwise unused.
    /// </summary>
    public int DwMipMapCount { get; set; }

    /// <summary>
    /// Gets or sets Unused.
    /// </summary>
    public int[] DwReserved1 { get; set; } = new int[DwReserved1Size];

    /// <summary>
    /// Gets or sets The pixel format.(see <see cref="DDS_PIXELFORMAT"/>).
    /// </summary>
    public DDS_PIXELFORMAT Ddspf { get; set; } = new ();

    /// <summary>
    /// Gets or sets Specifies the complexity of the surfaces stored.
    /// </summary>
    public DDSCAPS DwCaps { get; set; }

    /// <summary>
    /// Gets or sets Additional detail about the surfaces stored.
    /// </summary>
    public DDSCAPS2 DwCaps2 { get; set; }

    /// <summary>
    /// Gets or sets Caps3 (Unused).
    /// </summary>
    public int DwCaps3 { get; set; }

    /// <summary>
    /// Gets or sets Caps4 (Unused).
    /// </summary>
    public int DwCaps4 { get; set; }

    /// <summary>
    /// Gets or sets Reserved2 (Unused).
    /// </summary>
    public int DwReserved2 { get; set; }
}