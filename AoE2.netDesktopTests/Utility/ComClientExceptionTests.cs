namespace AoE2NetDesktop.Utility.Tests;

using System;

using AoE2NetDesktop.Utility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ComClientExceptionTests
{
    [TestMethod]
    public void ComClientExceptionTest()
    {
        // Arrange
        var expVal = NetStatus.ComTimeout;

        // Act
        var testClass = new ComClientException();
        var actVal = testClass.Status;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void ComClientExceptionTestMessage()
    {
        // Arrange
        var expVal = "Exp Message.";

        // Act
        var testClass = new ComClientException(expVal);
        var actVal = testClass.Message;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void ComClientExceptionTestMessageAndNetStatus()
    {
        // Arrange
        var expNetStatus = NetStatus.Connecting;
        var expMessage = "Exp Message.";

        // Act
        var testClass = new ComClientException(expMessage, expNetStatus);
        var actVal = testClass;

        // Assert
        Assert.AreEqual(expNetStatus, actVal.Status);
        Assert.AreEqual(expMessage, actVal.Message);
    }

    [TestMethod]
    public void ComClientExceptionTestMessageAndNetStatusAndInner()
    {
        // Arrange
        var expNetStatus = NetStatus.Connecting;
        var expMessage = "Exp Message.";
        var expInnerMessage = "ex as inner.";
        var inner = new Exception(expInnerMessage);

        // Act
        var testClass = new ComClientException(expMessage, expNetStatus, inner);
        var actVal = testClass;

        // Assert
        Assert.AreEqual(expNetStatus, actVal.Status);
        Assert.AreEqual(expMessage, actVal.Message);
        Assert.AreEqual(expInnerMessage, actVal.InnerException.Message);
    }

    [TestMethod]
    public void ComClientExceptionTest3()
    {
        // Arrange
        var expVal = NetStatus.ComTimeout;

        // Act
        var testClass = new ComClientException();
        var actVal = testClass.Status;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}