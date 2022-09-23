namespace AoE2NetDesktop.Form.Tests;
using AoE2NetDesktop.Utility.Forms;

using AoE2NetDesktopTests.TestUtility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

internal class Controler : FormControler
{
}

[TestClass]
public class ControllableFormTests
{
    [TestMethod]
    public void ControllableFormTest()
    {
        // Arrange
        var expVal = "Form";

        // Act
        var testClass = new ControllableForm();
        var actVal = testClass.GetType().BaseType.Name;

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void ControllableFormTestArg1()
    {
        // Arrange
        var expVal = new Controler();

        // Act
        var testClass = new ControllableForm(expVal);
        var actVal = testClass.GetProperty<FormControler>("Controler");

        // Assert
        Assert.AreEqual(expVal, actVal);
    }
}
