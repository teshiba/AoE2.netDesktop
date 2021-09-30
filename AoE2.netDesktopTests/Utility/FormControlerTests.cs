using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AoE2NetDesktop.Form.Tests
{
    public class CtrlForm : FormControler
    {
        public CtrlForm()
        {
           Scheduler = TaskScheduler.Default;
        }
    }

    [TestClass()]
    public class FormControlerTests
    {
        [TestMethod()]
        public void InvokeTest()
        {
            // Arrange
            var expVal = string.Empty;
            var testClass = new CtrlForm();

            // Act
            static Task function()
            {
                return Task.Delay(1);
            }

            testClass.Invoke(function);

            // Assert
        }
    }
}