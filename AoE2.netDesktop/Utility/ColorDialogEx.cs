namespace AoE2NetDesktop.Form
{
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
        public Func<bool> Opening { get; set; }

        /// <summary>
        /// Open <see cref="ColorDialog"/> and get <see cref="Color"/>.
        /// </summary>
        /// <returns>setected color.</returns>
        public Color GetColorFromDialog()
        {
            var ret = Color;

            if ((bool)Opening?.Invoke()) {
                if (ShowDialog() == DialogResult.OK) {
                    ret = Color;
                }
            }

            return ret;
        }
    }
}