namespace AoE2NetDesktop.CtrlForm;

using System.Windows.Forms;

using AoE2NetDesktop.LibAoE2Net.JsonFormat;

/// <summary>
/// 1v1 form control management class.
/// </summary>
public class Control1v1
{
    /// <summary>
    /// Gets or sets labelColor control.
    /// </summary>
    public Label LabelColor { get; set; }

    /// <summary>
    /// Gets or sets labelName control.
    /// </summary>
    public Label LabelName { get; set; }

    /// <summary>
    /// Gets or sets labelRate control.
    /// </summary>
    public Label LabelRate { get; set; }

    /// <summary>
    /// Gets or sets labelCiv control.
    /// </summary>
    public Label LabelCiv { get; set; }

    /// <summary>
    /// Gets or sets labelTeamResult control.
    /// </summary>
    public Label LabelTeamResult { get; set; }

    /// <summary>
    ///  Gets or sets labelWins control.
    /// </summary>
    public Label LabelWins { get; set; }

    /// <summary>
    ///  Gets or sets labelLoses control.
    /// </summary>
    public Label LabelLoses { get; set; }

    /// <summary>
    ///  Gets or sets pictureBoxCiv control.
    /// </summary>
    public PictureBox PictureBoxCiv { get; set; }

    /// <summary>
    ///  Gets or sets pictureBoxUnit control.
    /// </summary>
    public PictureBox PictureBoxUnit { get; set; }

    /// <summary>
    ///  Gets or sets player classl.
    /// </summary>
    public Player Player { get; set; }
}
