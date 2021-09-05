using System.Drawing;
using System.Windows.Forms;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoE2NetDesktop.Tests
{
    [TestClass()]
    public class DrawExTests
    {
        [TestMethod()]
        public void DrawStringTest()
        {
            // Arrange
            Label label = new();
            var graphics = label.CreateGraphics();
            var e = new PaintEventArgs(graphics, new Rectangle(0, 0, 100, 100));

            // Act
            label.DrawString(e, 10, Color.Red, Color.Orange);

            // Assert
        }

        [TestMethod()]
        public void DrawStringTest1()
        {
            // Arrange
            Label label = new();
            var graphics = label.CreateGraphics();
            var e = new PaintEventArgs(graphics, new Rectangle(0, 0, 100, 100));

            // Act
            label.DrawString(e, 10, Color.Red, Color.Orange);

            // Assert
        }
    }
}