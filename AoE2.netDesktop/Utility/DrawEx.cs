namespace AoE2NetDesktop
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    /// <summary>
    /// Custom drawing class.
    /// </summary>
    public static class DrawEx
    {
        /// <summary>
        /// Draw String.
        /// </summary>
        /// <param name="label">Label to be drawn.</param>
        /// <param name="e">PaintEventArgs.</param>
        /// <param name="fontSize">font size.</param>
        /// <param name="borderColor">border color.</param>
        /// <param name="fillColor">fill color.</param>
        public static void DrawString(this Label label, PaintEventArgs e, float fontSize, Color borderColor, Color fillColor)
        {
            DrawString(label, e, fontSize, borderColor, fillColor, new Point(0, 0));
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
            var stringFormat = new StringFormat {
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.None,
            };

            var graphicsPath = new GraphicsPath();
            graphicsPath.AddString(
                label.Text,
                label.Font.FontFamily,
                (int)FontStyle.Bold,
                fontSize,
                point,
                stringFormat);

            var pen = new Pen(borderColor, 8) {
                LineJoin = LineJoin.Round,
            };

            // Hide the default text display by changing ForeColor and BackColor to the same color.
            label.ForeColor = label.BackColor;

            // Draw bordered text.
            e.Graphics.PixelOffsetMode = PixelOffsetMode.None;
            e.Graphics.SmoothingMode = SmoothingMode.None;
            e.Graphics.DrawPath(pen, graphicsPath);
            e.Graphics.FillPath(new SolidBrush(fillColor), graphicsPath);
        }
    }
}
