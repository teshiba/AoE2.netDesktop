namespace AoE2NetDesktop.Utility.Forms;

using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Dialog Extentions.
/// </summary>
public class ColorDialogEx : ColorDialog
{
    /// <summary>
    /// Gets or sets handler that is called before the dialog is opened.<br></br>
    /// if return <see langword="false"/>, Opening dialog will be canceled.
    /// </summary>
    public Func<bool> Opening { get; set; } = new Func<bool>(() => true);

    /// <summary>
    /// Open <see cref="ColorDialog"/> and get <see cref="Color"/>.
    /// </summary>
    /// <returns>setected color.</returns>
    public Color GetColorFromDialog()
    {
        var ret = Color;

        if(Opening == null) {
            throw new NullReferenceException($"{nameof(Opening)} is set null.");
        }

        if(Opening.Invoke()) {
            _ = ShowDialog();
            ret = Color;
        }

        return ret;
    }
}
