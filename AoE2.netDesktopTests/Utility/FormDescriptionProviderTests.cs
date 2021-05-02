using Microsoft.VisualStudio.TestTools.UnitTesting;
using AoE2NetDesktop.From;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace AoE2NetDesktop.From.Tests
{
    [TestClass()]
    public class FormDescriptionProviderTests
    {
        [TestMethod()]
        public void GetReflectionTypeTest()
        {
            // Arrange
            var expVal = typeof(Form);

            // Act
            var testClass = new FormDescriptionProvider();
            var actVal = testClass.GetReflectionType(typeof(Form), new Form());

            // Assert
            Assert.AreEqual(expVal, actVal);
        }

        [TestMethod()]
        public void CreateInstanceTest()
        {
            // Arrange
            var expVal = typeof(Form);

            // Act
            var testClass = new FormDescriptionProvider();
            object[] args = null;
            Type[] argTypes = null;
            Type objectType = null;
            IServiceProvider provider = null;
            var actVal = testClass.CreateInstance(provider, objectType, argTypes, args);

            // Assert
            Assert.AreEqual(expVal, actVal.GetType());
        }
    }
}