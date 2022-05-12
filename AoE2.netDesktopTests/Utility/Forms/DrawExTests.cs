namespace AoE2NetDesktop.Tests;

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AoE2NetDesktop.Utility.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class DrawExTests
{
    [TestMethod]
    [DataRow(true, PixelOffsetMode.HighQuality, SmoothingMode.AntiAlias)]
    [DataRow(false, PixelOffsetMode.None, SmoothingMode.None)]
    public void DrawStringTest(bool drawHighQuality, PixelOffsetMode expValuePixelOffsetMode, SmoothingMode expValueSmoothingMode)
    {
        // Arrange
        Label label = new ();
        var graphics = label.CreateGraphics();
        var e = new PaintEventArgs(graphics, new Rectangle(0, 0, 100, 100));

        DrawEx.DrawHighQuality = drawHighQuality;

        // Act
        label.DrawString(e, 10, Color.Red, Color.Orange);

        // Assert
        Assert.AreEqual(expValuePixelOffsetMode, e.Graphics.PixelOffsetMode);
        Assert.AreEqual(expValueSmoothingMode, e.Graphics.SmoothingMode);
    }
}
