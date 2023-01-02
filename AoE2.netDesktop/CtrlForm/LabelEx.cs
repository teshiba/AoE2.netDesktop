namespace AoE2NetDesktop.CtrlForm;

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using AoE2NetDesktop.Utility;

/// <summary>
/// Label extension functions.
/// </summary>
public static class LabelEx
{
    private static readonly Dictionary<NetStatus, (string statusText, Color foreColor)> NetStatusViewList = new() {
        { NetStatus.ComTimeout, ("Timeout", Color.Purple) },
        { NetStatus.Connected, ("Online", Color.Green) },
        { NetStatus.Connecting, ("Connecting", Color.MediumSeaGreen) },
        { NetStatus.Disconnected, ("Disconnected", Color.Firebrick) },
        { NetStatus.InvalidRequest, ("Invalid ID", Color.Red) },
        { NetStatus.ServerError, ("Server Error", Color.Olive) },
    };

    /// <summary>
    /// Set AoE2net status.
    /// </summary>
    /// <param name="label">windows form Label.</param>
    /// <param name="status">network status.</param>
    public static void SetAoE2netStatus(this Label label, NetStatus status)
    {
        if(NetStatusViewList.TryGetValue(status, out (string statusText, Color foreColor) param)) {
            label.Text = param.statusText;
            label.ForeColor = param.foreColor;
        } else {
            label.Text = string.Empty;
            label.ForeColor = new Control().ForeColor;
        }
    }
}
