namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;

[TestClass]
public class ColorDialogExTests
{
    [TestMethod]
    public void GetColorFromDialogTestCancelOpening()
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

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetColorFromDialogTestwithOpeningAction()
    {
        // Arrange
        var expVal = Color.FromArgb(255, 255, 0, 0);
        Color actVal = default;
        var form = new System.Windows.Forms.Form();
        var testClass = new ColorDialogEx {
            Color = expVal,
            Opening = () => true,
        };

        var task1 = Task.Run(() =>
        {
            form.Shown += (sender, e) =>
            {
                // Act
                actVal = testClass.GetColorFromDialog();
                form.Close();
            };
            form.ShowDialog();
        });

        var task2 = Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            form.Invoke(() => form.Dispose());
        });

        Task.WaitAll(task1, task2);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    [SuppressMessage("Usage", "VSTHRD002:Avoid problematic synchronous waits", Justification = SuppressReason.IntentionalSyncTest)]
    public void GetColorFromDialogTestOpenDialog()
    {
        // Arrange
        var expVal = Color.FromArgb(255, 255, 0, 0);
        Color actVal = default;
        var form = new System.Windows.Forms.Form();
        var testClass = new ColorDialogEx {
            Color = expVal,
        };

        var task1 = Task.Run(() =>
        {
            form.Shown += (sender, e) =>
            {
                // Act
                actVal = testClass.GetColorFromDialog();
                form.Close();
            };
            form.ShowDialog();
        });

        var task2 = Task.Run(() =>
        {
            Task.Delay(1000).Wait();
            form.Invoke(() => form.Dispose());
        });

        Task.WaitAll(task1, task2);

        // Assert
        Assert.AreEqual(expVal, actVal);
    }

    [TestMethod]
    public void GetColorFromDialogTestOpenDialogOpeningNull()
    {
        // Arrange
        var testClass = new ColorDialogEx {
            Opening = null,
        };

        // Assert
        Assert.ThrowsException<NullReferenceException>(() =>
        {
            _ = testClass.GetColorFromDialog();
        });
    }
}
