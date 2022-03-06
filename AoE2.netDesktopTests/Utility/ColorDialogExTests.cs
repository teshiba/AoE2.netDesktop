using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoE2NetDesktop.Form;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AoE2NetDesktop.Form.Tests
{
    [TestClass()]
    public class ColorDialogExTests
    {
        [TestMethod()]
        public void GetColorFromDialogTest()
        {
            // Arrange
            var expVal = Color.FromArgb(255, 255, 0, 0);

            // Act
            var testClass = new ColorDialogEx {
                Color = expVal,
                Opening = () => false,
            };

            var actVal = testClass.GetColorFromDialog();

            // Assert
            Assert.AreEqual(expVal, actVal);
        }
    }
}