namespace AoE2NetDesktop.Form;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;
using AoE2NetDesktop.Utility.Forms;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        SetChromaKey(Settings.Default.ChromaKey);
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
        } catch(ArgumentException) {
            chromaKey = ColorTranslator.FromHtml("#000000");
        }

        SetChromaKey(chromaKey);
    }

    private void SetChromaKey(Color chromaKey)
    {
        Settings.Default.ChromaKey = $"#{chromaKey.R:X02}{chromaKey.G:X02}{chromaKey.B:X02}";
        textBoxChromaKey.Text = Settings.Default.ChromaKey;
        pictureBoxChromaKey.BackColor = chromaKey;
    }

    private void InitIDRadioButton()
    {
        switch(Controler.SelectedIdType) {
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
        upDownOpacity.Value = Settings.Default.MainFormOpacityPercent;
        checkBoxAlwaysOnTop.Checked = Settings.Default.MainFormIsAlwaysOnTop;
        checkBoxHideTitle.Checked = Settings.Default.MainFormIsHideTitle;
        checkBoxTransparencyWindow.Checked = Settings.Default.MainFormIsTransparency;
        checkBoxDrawQuality.Checked = Settings.Default.DrawHighQuality;
        checkBoxAutoReloadLastMatch.Checked = Settings.Default.IsAutoReloadLastMatch;
        checkBoxVisibleGameTime.Checked = Settings.Default.VisibleGameTime;

        switch(Controler.SelectedIdType) {
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

        if(ret) {
            labelAoE2NetStatus.SetAoE2netStatus(NetStatus.Connected);
            switch(Controler.SelectedIdType) {
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
        if(ex.GetType() == typeof(HttpRequestException)) {
            if(ex.Message.Contains("404")) {
                labelAoE2NetStatus.SetAoE2netStatus(NetStatus.InvalidRequest);
            } else {
                labelAoE2NetStatus.SetAoE2netStatus(NetStatus.ServerError);
            }
        }

        if(ex.GetType() == typeof(TaskCanceledException)) {
            labelAoE2NetStatus.SetAoE2netStatus(NetStatus.ComTimeout);
        }
    }

    private void RestoreWindowStatus()
    {
        Top = Settings.Default.WindowLocationSettings.Y;
        Left = Settings.Default.WindowLocationSettings.X;
    }

    private void SaveWindowPosition()
    {
        Settings.Default.WindowLocationSettings = new Point(Left, Top);
    }

    ///////////////////////////////////////////////////////////////////////
    // Event handlers (Async)
    ///////////////////////////////////////////////////////////////////////

    [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = SuppressReason.GuiTest)]
    [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = SuppressReason.GuiTest)]
    private async void FormSettings_LoadAsync(object sender, EventArgs e)
    {
        AoE2net.OnError = OnErrorHandler;

        RestoreWindowStatus();
        LoadSettings();

        var idtype = Controler.SelectedIdType;
        var idText = string.Empty;

        switch(idtype) {
        case IdType.Steam:
            idText = Controler.SteamId;
            break;
        case IdType.Profile:
            idText = Controler.ProfileId.ToString();
            break;
        }

        // if inital value, skip the loading.
        if(idText != "0") {
            try {
                _ = await ReloadProfileAsync(idtype, idText);
            } catch(Exception ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }
        }

        Awaiter.Complete();
    }

    [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = SuppressReason.GuiTest)]
    [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = SuppressReason.GuiTest)]
    private async void ButtonSetId_ClickAsync(object sender, EventArgs e)
    {
        var idtype = Controler.SelectedIdType;
        var idText = string.Empty;

        switch(idtype) {
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
        } catch(Exception ex) {
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
        if(radioButton.Checked) {
            Settings.Default.SelectedIdType = (int)IdType.Profile;
            Controler.SelectedIdType = IdType.Profile;
        }
    }

    private void RadioButtonSteamID_CheckedChanged(object sender, EventArgs e)
    {
        var radioButton = (RadioButton)sender;

        textBoxSettingProfileId.Enabled = !radioButton.Checked;
        textBoxSettingSteamId.Enabled = radioButton.Checked;
        if(radioButton.Checked) {
            Settings.Default.SelectedIdType = (int)IdType.Steam;
            Controler.SelectedIdType = IdType.Steam;
        }
    }

    private void UpDownOpacity_ValueChanged(object sender, EventArgs e)
    {
        Settings.Default.MainFormOpacityPercent = upDownOpacity.Value;
    }

    private void CheckBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.MainFormIsAlwaysOnTop = ((CheckBox)sender).Checked;
    }

    private void CheckBoxHideTitle_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.MainFormIsHideTitle = ((CheckBox)sender).Checked;
    }

    private void CheckBoxTransparencyWindow_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.MainFormIsTransparency = ((CheckBox)sender).Checked;
        Awaiter.Complete();
    }

    private void CheckBoxDrawQuality_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.DrawHighQuality = ((CheckBox)sender).Checked;
    }

    private void CheckBoxAutoReloadLastMatch_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.IsAutoReloadLastMatch = ((CheckBox)sender).Checked;
    }

    private void CheckBoxVisibleGameTime_CheckedChanged(object sender, EventArgs e)
    {
        Settings.Default.VisibleGameTime = ((CheckBox)sender).Checked;
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
        if(!textBox.Text.StartsWith("#")) {
            textBox.Text = $"#{textBox.Text}";
        }

        SetChromaKey(textBox.Text);
    }
}
