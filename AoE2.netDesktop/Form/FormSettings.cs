namespace AoE2NetDesktop.Form;

using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

/// <summary>
/// App Settings form.
/// </summary>
public partial class FormSettings : ControllableForm
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FormSettings"/> class.
    /// </summary>
    /// <param name="ctrlSettings">FormSettings controler.</param>
    public FormSettings(CtrlSettings ctrlSettings)
        : base(ctrlSettings)
    {
        InitializeComponent();
        InitIDRadioButton();
        labelAoE2NetStatus.SetAoE2netStatus(NetStatus.Disconnected);
        SetChromaKey(Controler.PropertySetting.ChromaKey);
    }

    /// <summary>
    /// Gets or sets ColorDialog.
    /// </summary>
    public ColorDialogEx ColorDialog { get; set; } = new ColorDialogEx();

    /// <inheritdoc/>
    protected override CtrlSettings Controler { get => (CtrlSettings)base.Controler; }

    private void SetChromaKey(string htmlColor)
    {
        Color chromaKey;

        try {
            chromaKey = ColorTranslator.FromHtml(htmlColor);
        } catch (ArgumentException) {
            chromaKey = ColorTranslator.FromHtml("#000000");
        }

        SetChromaKey(chromaKey);
    }

    private void SetChromaKey(Color chromaKey)
    {
        Controler.PropertySetting.ChromaKey = $"#{chromaKey.R:X02}{chromaKey.G:X02}{chromaKey.B:X02}";
        textBoxChromaKey.Text = Controler.PropertySetting.ChromaKey;
        pictureBoxChromaKey.BackColor = chromaKey;
        Settings.Default.ChromaKey = ColorTranslator.ToHtml(chromaKey);
    }

    private void InitIDRadioButton()
    {
        switch (Controler.SelectedIdType) {
        case IdType.Steam:
            radioButtonSteamID.Checked = true;
            break;
        case IdType.Profile:
            radioButtonProfileID.Checked = true;
            break;
        }
    }

    private void LoadSettings()
    {
        textBoxSettingSteamId.Text = Settings.Default.SteamId;
        textBoxSettingProfileId.Text = Settings.Default.ProfileId.ToString();

        LoadMainFormSettings();
    }

    private void LoadMainFormSettings()
    {
        upDownOpacity.Value = (decimal)Controler.PropertySetting.Opacity * 100;
        checkBoxAlwaysOnTop.Checked = Controler.PropertySetting.IsAlwaysOnTop;
        checkBoxHideTitle.Checked = Controler.PropertySetting.IsHideTitle;
        checkBoxTransparencyWindow.Checked = Controler.PropertySetting.IsTransparency;
        checkBoxDrawQuality.Checked = Controler.PropertySetting.DrawHighQuality;
        checkBoxAutoReloadLastMatch.Checked = Controler.PropertySetting.IsAutoReloadLastMatch;

        switch (Controler.SelectedIdType) {
        case IdType.Steam:
            radioButtonSteamID.Checked = true;
            break;
        case IdType.Profile:
            radioButtonProfileID.Checked = true;
            break;
        }
    }

    private async Task<bool> ReloadProfileAsync(IdType idtype, string idText)
    {
        labelAoE2NetStatus.SetAoE2netStatus(NetStatus.Connecting);
        groupBoxPlayer.Enabled = false;
        labelSettingsName.Text = $"   Name: --";
        labelSettingsCountry.Text = $"Country: --";

        var ret = await Controler.ReloadProfileAsync(idtype, idText);

        if (ret) {
            labelAoE2NetStatus.SetAoE2netStatus(NetStatus.Connected);
            switch (Controler.SelectedIdType) {
            case IdType.Steam:
                textBoxSettingProfileId.Text = Controler.ProfileId.ToString();
                Settings.Default.ProfileId = Controler.ProfileId;
                break;
            case IdType.Profile:
                textBoxSettingSteamId.Text = Controler.SteamId;
                Settings.Default.SteamId = Controler.SteamId;
                break;
#if false // This code does not pass.
            case IdType.NotSelected:
            default:
                throw new Exception($"Invalid IdType:{Controler.SelectedIdType}");
#endif
            }
        }

        labelSettingsName.Text = $"   Name: {Controler.UserName}";
        labelSettingsCountry.Text = $"Country: {Controler.UserCountry}";
        groupBoxPlayer.Enabled = true;

        Awaiter.Complete();

        return ret;
    }

    private void OnErrorHandler(Exception ex)
    {
        if (ex.GetType() == typeof(HttpRequestException)) {
            if (ex.Message.Contains("404")) {
                labelAoE2NetStatus.SetAoE2netStatus(NetStatus.InvalidRequest);
            } else {
                labelAoE2NetStatus.SetAoE2netStatus(NetStatus.ServerError);
            }
        }

        if (ex.GetType() == typeof(TaskCanceledException)) {
            labelAoE2NetStatus.SetAoE2netStatus(NetStatus.ComTimeout);
        }
    }

    private void RestoreWindowStatus()
    {
        Top = Settings.Default.WindowLocationSettings.Y;
        Left = Settings.Default.WindowLocationSettings.X;
        Width = Settings.Default.WindowSizeSettings.Width;
        Height = Settings.Default.WindowSizeSettings.Height;
    }

    private void SaveWindowPosition()
    {
        Settings.Default.WindowLocationSettings = new Point(Left, Top);
        Settings.Default.WindowSizeSettings = new Size(Width, Height);
    }

    ///////////////////////////////////////////////////////////////////////
    // Event handlers (Async)
    ///////////////////////////////////////////////////////////////////////

    private async void FormSettings_Load(object sender, EventArgs e)
    {
        AoE2net.OnError = OnErrorHandler;

        RestoreWindowStatus();
        LoadSettings();

        var idtype = Controler.SelectedIdType;
        var idText = string.Empty;

        switch (idtype) {
        case IdType.Steam:
            idText = Controler.SteamId;
            break;
        case IdType.Profile:
            idText = Controler.ProfileId.ToString();
            break;
        }

        try {
            _ = await ReloadProfileAsync(idtype, idText);
        } catch (Exception ex) {
            labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
        }

        Awaiter.Complete();
    }

    private async void ButtonSetId_ClickAsync(object sender, EventArgs e)
    {
        var idtype = Controler.SelectedIdType;
        var idText = string.Empty;

        switch (idtype) {
        case IdType.Steam:
            idText = textBoxSettingSteamId.Text;
            Settings.Default.SteamId = idText;
            break;
        case IdType.Profile:
            idText = textBoxSettingProfileId.Text;
            Settings.Default.ProfileId = int.Parse(idText);
            break;
        }

        try {
            _ = await ReloadProfileAsync(idtype, idText);
        } catch (Exception ex) {
            labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
        }

        Awaiter.Complete();
    }

    ///////////////////////////////////////////////////////////////////////
    // Event handlers
    ///////////////////////////////////////////////////////////////////////
    private void RadioButtonProfileID_CheckedChanged(object sender, EventArgs e)
    {
        var radioButton = (RadioButton)sender;

        textBoxSettingProfileId.Enabled = radioButton.Checked;
        textBoxSettingSteamId.Enabled = !radioButton.Checked;
        if (radioButton.Checked) {
            Settings.Default.SelectedIdType = (int)IdType.Profile;
            Controler.SelectedIdType = IdType.Profile;
        }
    }

    private void RadioButtonSteamID_CheckedChanged(object sender, EventArgs e)
    {
        var radioButton = (RadioButton)sender;

        textBoxSettingProfileId.Enabled = !radioButton.Checked;
        textBoxSettingSteamId.Enabled = radioButton.Checked;
        if (radioButton.Checked) {
            Settings.Default.SelectedIdType = (int)IdType.Steam;
            Controler.SelectedIdType = IdType.Steam;
        }
    }

    private void UpDownOpacity_ValueChanged(object sender, EventArgs e)
    {
        Controler.PropertySetting.Opacity = (double)upDownOpacity.Value * 0.01;
        Settings.Default.MainFormOpacityPercent = upDownOpacity.Value;
    }

    private void CheckBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
    {
        Controler.PropertySetting.IsAlwaysOnTop = ((CheckBox)sender).Checked;
        Settings.Default.MainFormIsAlwaysOnTop = Controler.PropertySetting.IsAlwaysOnTop;
    }

    private void CheckBoxHideTitle_CheckedChanged(object sender, EventArgs e)
    {
        Controler.PropertySetting.IsHideTitle = ((CheckBox)sender).Checked;
        Settings.Default.MainFormIsHideTitle = Controler.PropertySetting.IsHideTitle;
    }

    private void CheckBoxTransparencyWindow_CheckedChanged(object sender, EventArgs e)
    {
        Controler.PropertySetting.IsTransparency = ((CheckBox)sender).Checked;
        Settings.Default.MainFormTransparency = Controler.PropertySetting.IsTransparency;
    }

    private void CheckBoxDrawQuality_CheckedChanged(object sender, EventArgs e)
    {
        Controler.PropertySetting.DrawHighQuality = ((CheckBox)sender).Checked;
        Settings.Default.DrawHighQuality = Controler.PropertySetting.DrawHighQuality;
    }

    private void CheckBoxAutoReloadLastMatch_CheckedChanged(object sender, EventArgs e)
    {
        Controler.PropertySetting.IsAutoReloadLastMatch = ((CheckBox)sender).Checked;
        Settings.Default.IsAutoReloadLastMatch = Controler.PropertySetting.IsAutoReloadLastMatch;
    }

    private void PictureBoxChromaKey_Click(object sender, EventArgs e)
    {
        SetChromaKey(ColorDialog.GetColorFromDialog());
    }

    private void FormSettings_FormClosing(object sender, FormClosingEventArgs e)
    {
        SaveWindowPosition();
        Settings.Default.Save();
    }

    private void TextBoxChromaKey_Leave(object sender, EventArgs e)
    {
        var textBox = (TextBox)sender;
        if (!textBox.Text.StartsWith("#")) {
            textBox.Text = $"#{textBox.Text}";
        }

        SetChromaKey(textBox.Text);
    }
}
