namespace AoE2NetDesktop.Utility.DDS;

using System.Drawing;
using System.IO;
using System.Text;

/// <summary>
/// DDS Image loader class.
/// </summary>
public class ImageLoader
{
    private const string DwMagic = "DDS ";

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageLoader"/> class.
    /// </summary>
    /// <param name="filePath">File path.</param>
    public ImageLoader(string filePath)
        : this(filePath, Color.Transparent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageLoader"/> class.
    /// </summary>
    /// <param name="filePath">File path.</param>
    /// <param name="backColor">Back color.</param>
    public ImageLoader(string filePath, Color backColor)
    {
        var rawData = File.ReadAllBytes(filePath);
        using var stream = new MemoryStream(rawData);
        using var reader = new BinaryReader(stream);

        if(Encoding.UTF8.GetString(reader.ReadBytes(4)) != DwMagic) {
            BitmapImage = new Bitmap(1, 1);
            ErrorCode = ImageLoaderError.InvalidMagic;
        } else {
            var header = ReadHeader(reader);

            if(header.Ddspf.DwFlags == (DDPF.RGB | DDPF.ALPHAPIXELS)) {
                var bitmapData = reader.ReadBytes(header.DwWidth * header.DwHeight * header.Ddspf.DwRGBBitCount);
                BitmapImage = ConvertToBitmap(bitmapData, header.DwWidth, header.DwHeight, backColor);
            } else {
                BitmapImage = new Bitmap(1, 1);
                ErrorCode = ImageLoaderError.InvalidDddsPfFlags;
            }
        }
    }

    /// <summary>
    /// Gets bitmapImage.
    /// </summary>
    public Bitmap BitmapImage { get; private set; }

    /// <summary>
    /// Gets bitmapImage.
    /// </summary>
    public ImageLoaderError ErrorCode { get; private set; } = ImageLoaderError.Non;

    private static DdsHeader ReadHeader(BinaryReader reader)
    {
        var header = new DdsHeader {
            DwSize = reader.ReadInt32(),
            DwFlags = (DDSD)reader.ReadInt32(),
            DwHeight = reader.ReadInt32(),
            DwWidth = reader.ReadInt32(),
            DwPitchOrLinearSize = reader.ReadInt32(),
            DwDepth = reader.ReadInt32(),
            DwMipMapCount = reader.ReadInt32(),
        };

        for(int i = 0; i < DdsHeader.DwReserved1Size; ++i) {
            header.DwReserved1[i] = reader.ReadInt32();
        }

        ReadPixelFormat(header.Ddspf, reader);

        header.DwCaps = (DDSCAPS)reader.ReadInt32();
        header.DwCaps2 = (DDSCAPS2)reader.ReadInt32();
        header.DwCaps3 = reader.ReadInt32();
        header.DwCaps4 = reader.ReadInt32();
        header.DwReserved2 = reader.ReadInt32();

        return header;
    }

    private static void ReadPixelFormat(DDS_PIXELFORMAT pixelFormat, BinaryReader reader)
    {
        pixelFormat.DwSize = reader.ReadInt32();
        pixelFormat.DwFlags = (DDPF)reader.ReadInt32();
        pixelFormat.DwFourCC = reader.ReadInt32();
        pixelFormat.DwRGBBitCount = reader.ReadInt32();
        pixelFormat.DwRBitMask = reader.ReadInt32();
        pixelFormat.DwGBitMask = reader.ReadInt32();
        pixelFormat.DwBBitMask = reader.ReadInt32();
        pixelFormat.DwABitMask = reader.ReadInt32();
    }

    private static Bitmap ConvertToBitmap(byte[] byteData, int width, int height, Color backColor)
    {
        var bitmap = new Bitmap(width, height);
        using var stream = new MemoryStream(byteData);
        using var reader = new BinaryReader(stream);

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                var pixelData = reader.ReadInt32();
                if((int)((pixelData & 0xff000000) >> 24) != 0x00) {
                    bitmap.SetPixel(x, y, Color.FromArgb(
                        (int)((pixelData & 0xff000000) >> 24),
                        pixelData & 0x000000ff,
                        (pixelData & 0x0000ff00) >> 8,
                        (pixelData & 0x00ff0000) >> 16));
                } else {
                    bitmap.SetPixel(x, y, backColor);
                }
            }
        }

        return bitmap;
    }
}
