namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.Utility;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

[TestClass]
public class LabelExTests
{
    private static IEnumerable<object[]> OnErrorHandlerTestData => new List<object[]>
    {
        new object[] { NetStatus.ComTimeout, "Timeout", Color.Purple },
        new object[] { NetStatus.Connected, "Online", Color.Green },
        new object[] { NetStatus.Connecting, "Connecting", Color.MediumSeaGreen },
        new object[] { NetStatus.Disconnected, "Disconnected", Color.Firebrick },
        new object[] { NetStatus.InvalidRequest, "Invalid ID", Color.Red },
        new object[] { NetStatus.ServerError, "Server Error", Color.Olive },
        new object[] { (NetStatus)(-1), string.Empty, new Control().ForeColor },
    };

    [TestMethod]
    [DynamicData(nameof(OnErrorHandlerTestData))]

    public void SetAoE2netStatusTest(NetStatus netStatus, string expText, Color expColor)
    {
        // Arrange
        var testClass = new Label();

        // Act
        testClass.SetAoE2netStatus(netStatus);

        // Assert
        Assert.AreEqual(expText, testClass.Text);
        Assert.AreEqual(expColor, testClass.ForeColor);
    }
}
