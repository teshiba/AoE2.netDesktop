﻿namespace AoE2NetDesktop.Utility.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

/// <summary>
/// Custom drawing class.
/// </summary>
public static class DrawEx
{
    /// <summary>
    /// Gets or sets a value indicating whether draw Quality is high.
    /// </summary>
    public static bool DrawHighQuality { get; set; }

    /// <summary>
    /// Draw String.
    /// </summary>
    /// <param name="label">Label to be drawn.</param>
    /// <param name="e">PaintEventArgs.</param>
    /// <param name="style">fill color.</param>
    public static void DrawString(this Label label, PaintEventArgs e, BorderedStringStyle style)
    {
        label.DrawString(e, style.FontSize, style.BorderColor, style.FillColor, new Point(0, 0));
    }

    /// <summary>
    /// Draw String.
    /// </summary>
    /// <param name="label">Label to be drawn.</param>
    /// <param name="e">PaintEventArgs.</param>
    /// <param name="fontSize">font size.</param>
    /// <param name="borderColor">border color.</param>
    /// <param name="fillColor">fill color.</param>
    /// <param name="point">start position of drawing.</param>
    public static void DrawString(this Label label, PaintEventArgs e, float fontSize, Color borderColor, Color fillColor, Point point)
    {
        var alignment = ConvertStringAlignment(label.TextAlign);

        var stringFormat = new StringFormat {
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.None,
            Alignment = alignment.Alignment,
            LineAlignment = alignment.LineAlignment,
        };

        var graphicsPath = new GraphicsPath();
        graphicsPath.AddString(
            label.Text,
            label.Font.FontFamily,
            (int)FontStyle.Bold,
            fontSize,
            new Rectangle(point, label.Size),
            stringFormat);

        var pen = new Pen(borderColor, 8) {
            LineJoin = LineJoin.Round,
        };

        // Hide the default text display by changing ForeColor and BackColor to the same color.
        label.ForeColor = label.BackColor;

        // Draw bordered text.
        if(DrawHighQuality) {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        } else {
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.SmoothingMode = SmoothingMode.None;
        }

        e.Graphics.DrawPath(pen, graphicsPath);
        e.Graphics.FillPath(new SolidBrush(fillColor), graphicsPath);
    }

    private static StringFormat ConvertStringAlignment(ContentAlignment contentAlignment)
    {
        var stringFormat = new StringFormat();

        switch(contentAlignment) {
        case ContentAlignment.TopLeft:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near,
            };
            break;
        case ContentAlignment.TopCenter:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Near,
            };
            break;
        case ContentAlignment.TopRight:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Near,
            };
            break;
        case ContentAlignment.MiddleLeft:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
            };
            break;
        case ContentAlignment.MiddleCenter:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
            };
            break;
        case ContentAlignment.MiddleRight:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Center,
            };
            break;
        case ContentAlignment.BottomLeft:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Far,
            };
            break;
        case ContentAlignment.BottomCenter:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Far,
            };
            break;
        case ContentAlignment.BottomRight:
            stringFormat = new StringFormat {
                Alignment = StringAlignment.Far,
                LineAlignment = StringAlignment.Far,
            };
            break;
        }

        return stringFormat;
    }
}
