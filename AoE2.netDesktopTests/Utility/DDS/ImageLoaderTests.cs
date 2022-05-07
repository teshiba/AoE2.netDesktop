using AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AoE2NetDesktop.Utility.DDS.Tests
{
    [TestClass()]
    public class ImageLoaderTests
    {
        [TestMethod()]
        [DataRow(TestData.ddsfile, ImageLoaderError.Non)]
        [DataRow(TestData.ddsfileUnexpectedDwFlags, ImageLoaderError.InvalidDddsPfFlags)]
        [DataRow(TestData.ddsfileUnexpectedMagic, ImageLoaderError.InvalidMagic)]
        public void ImageLoaderTest(string filePath, ImageLoaderError expErr)
        {
            // Arrange

            // Act
            var testClass = new ImageLoader(filePath);

            // Assert
            Assert.AreEqual(expErr, testClass.ErrorCode);
        }
    }
}