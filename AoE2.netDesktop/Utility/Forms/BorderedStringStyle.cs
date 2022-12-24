namespace AoE2NetDesktop.Utility.Forms;

using System.Drawing;

/// <summary>
/// Bordered string style.
/// </summary>
public class BorderedStringStyle
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BorderedStringStyle"/> class.
    /// </summary>
    /// <param name="fontSize">Font size.</param>
    /// <param name="borderColor">Border color.</param>
    /// <param name="fillColor">Fill color.</param>
    public BorderedStringStyle(float fontSize, Color borderColor, Color fillColor)
    {
        FontSize = fontSize;
        BorderColor = borderColor;
        FillColor = fillColor;
    }

    /// <summary>
    /// Gets fill color.
    /// </summary>
    public Color FillColor { get; }

    /// <summary>
    /// Gets border color.
    /// </summary>
    public Color BorderColor { get; }

    /// <summary>
    /// Gets font size.
    /// </summary>
    public float FontSize { get; }
}