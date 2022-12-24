namespace AoE2NetDesktop.Tests;

using AoE2NetDesktop.Utility.Forms;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

[TestClass]
public class DrawExTests
{
    [TestMethod]
    [DataRow(true, PixelOffsetMode.HighQuality, SmoothingMode.AntiAlias)]
    [DataRow(false, PixelOffsetMode.None, SmoothingMode.None)]
    public void DrawStringTest(bool drawHighQuality, PixelOffsetMode expValuePixelOffsetMode, SmoothingMode expValueSmoothingMode)
    {
        // Arrange
        Label label = new();
        var graphics = label.CreateGraphics();
        var e = new PaintEventArgs(graphics, new Rectangle(0, 0, 100, 100));

        DrawEx.DrawHighQuality = drawHighQuality;
        var style = new BorderedStringStyle(10, Color.Red, Color.Orange);

        // Act
        label.DrawString(e, style);

        // Assert
        Assert.AreEqual(expValuePixelOffsetMode, e.Graphics.PixelOffsetMode);
        Assert.AreEqual(expValueSmoothingMode, e.Graphics.SmoothingMode);
    }

    [TestMethod]
    [DataRow(ContentAlignment.BottomCenter)]
    [DataRow(ContentAlignment.BottomLeft)]
    [DataRow(ContentAlignment.BottomRight)]
    [DataRow(ContentAlignment.MiddleCenter)]
    [DataRow(ContentAlignment.MiddleLeft)]
    [DataRow(ContentAlignment.MiddleRight)]
    [DataRow(ContentAlignment.TopCenter)]
    [DataRow(ContentAlignment.TopLeft)]
    [DataRow(ContentAlignment.TopRight)]
    public void DrawStringTestAlign(ContentAlignment alignment)
    {
        // Arrange
        var label = new Label {
            TextAlign = alignment,
        };

        var graphics = label.CreateGraphics();
        var e = new PaintEventArgs(graphics, new Rectangle(0, 0, 100, 100));
        var style = new BorderedStringStyle(10, Color.Red, Color.Orange);

        // Act
        label.DrawString(e, style);

        // Assert
    }
}
