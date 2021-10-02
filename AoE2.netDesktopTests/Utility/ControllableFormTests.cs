using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoE2NetDesktop.Tests;

namespace AoE2NetDesktop.Form.Tests
{

    internal class Controler : FormControler
    {
    }

    [TestClass()]
    public class ControllableFormTests
    {
        [TestMethod()]
        public void ControllableFormTest()
        {
            // Arrange
            var expVal = "Form";

            // Act
            var testClass = new ControllableForm();
            var actVal =  testClass.GetType().BaseType.Name;

            // Assert
             Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
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

}