namespace AoE2NetDesktop.Form
{
    using System.Drawing;
    using System.Windows.Forms;
    using LibAoE2net;

    /// <summary>
    /// Label extension functions.
    /// </summary>
    public static class LabelEx
    {
        /// <summary>
        /// Set AoE2net status.
        /// </summary>
        /// <param name="label">windows form Label.</param>
        /// <param name="status">network status.</param>
        public static void SetAoE2netStatus(this Label label, NetStatus status)
        {
            (string statusText, Color foreColor) param = status switch {
                NetStatus.ComTimeout => new ("Timeout", Color.Purple),
                NetStatus.Connected => new ("Online", Color.Green),
                NetStatus.Connecting => new ("Connecting", Color.MediumSeaGreen),
                NetStatus.Disconnected => new ("Disconnected", Color.Firebrick),
                NetStatus.InvalidRequest => new ("Invalid ID", Color.Red),
                NetStatus.ServerError => new ("Server Error", Color.Olive),
                _ => new (string.Empty, default),
            };

            label.Text = param.statusText;
            label.ForeColor = param.foreColor;
        }
    }
}