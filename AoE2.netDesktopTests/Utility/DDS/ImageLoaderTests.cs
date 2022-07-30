namespace AoE2NetDesktop.Utility.DDS.Tests;

using AoE2NetDesktop.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ImageLoaderTests
{
    [TestMethod]
    [DataRow(TestData.DdsFile, ImageLoaderError.Non)]
    [DataRow(TestData.DdsFileUnexpectedDwFlags, ImageLoaderError.InvalidDddsPfFlags)]
    [DataRow(TestData.DdsFileUnexpectedMagic, ImageLoaderError.InvalidMagic)]
    [DataRow(TestData.DdsNonExsistFile, ImageLoaderError.NotFound)]
    [DataRow(TestData.DdsNonExsistDir, ImageLoaderError.NotFound)]
    public void ImageLoaderTest(string filePath, ImageLoaderError expErr)
    {
        // Arrange

        // Act
        var testClass = new ImageLoader(filePath);

        // Assert
        Assert.AreEqual(expErr, testClass.ErrorCode);
    }
}
