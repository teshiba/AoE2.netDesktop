namespace AoE2NetDesktop.Form
{
    using System;
    using System.Drawing;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using LibAoE2net;

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
            ShowAoE2netStatus(NetStatus.Disconnected);
            SetChromaKey(Controler.ChromaKey);
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
                chromaKey = Color.Empty;
            }

            SetChromaKey(chromaKey);
        }

        private void SetChromaKey(Color chromaKey)
        {
            Controler.ChromaKey = $"#{chromaKey.R:X02}{chromaKey.G:X02}{chromaKey.B:X02}";
            textBoxChromaKey.Text = Controler.ChromaKey;
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
            upDownOpacity.Value = Settings.Default.MainFormOpacityPercent;
            textBoxSettingSteamId.Text = Settings.Default.SteamId;
            textBoxSettingProfileId.Text = Settings.Default.ProfileId.ToString();
            checkBoxAlwaysOnTop.Checked = Settings.Default.MainFormIsAlwaysOnTop;
            checkBoxHideTitle.Checked = Settings.Default.MainFormIsHideTitle;
            checkBoxTransparencyWindow.Checked = Settings.Default.MainFormTransparency;

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
            bool ret;

            ShowAoE2netStatus(NetStatus.Connecting);
            groupBoxPlayer.Enabled = false;
            labelSettingsName.Text = $"   Name: --";
            labelSettingsCountry.Text = $"Country: --";

            try {
                ret = await Controler.ReloadProfileAsync(idtype, idText);
                ShowAoE2netStatus(NetStatus.Connected);

                switch (Controler.SelectedIdType) {
                case IdType.Steam:
                    textBoxSettingProfileId.Text = Controler.ProfileId.ToString();
                    Settings.Default.ProfileId = Controler.ProfileId;
                    break;
                case IdType.Profile:
                    textBoxSettingSteamId.Text = Controler.SteamId;
                    Settings.Default.SteamId = Controler.SteamId;
                    break;
                case IdType.NotSelected:
                default:
                    throw new Exception($"Invalid IdType:{Controler.SelectedIdType}");
                }
            } catch (Exception ex) {
                ret = false;
                labelErrText.Text = ex.Message + ":" + ex.StackTrace;
            }

            labelSettingsName.Text = $"   Name: {Controler.UserName}";
            labelSettingsCountry.Text = $"Country: {Controler.UserCountry}";
            groupBoxPlayer.Enabled = true;

            Awaiter.Complete();

            return ret;
        }

        private void ShowAoE2netStatus(NetStatus status)
        {
            switch (status) {
            case NetStatus.Connected:
                labelAoE2NetStatus.Text = "Online";
                labelAoE2NetStatus.ForeColor = Color.Green;
                break;
            case NetStatus.Disconnected:
                labelAoE2NetStatus.Text = "Disconnected";
                labelAoE2NetStatus.ForeColor = Color.Firebrick;
                break;
            case NetStatus.ServerError:
                labelAoE2NetStatus.Text = "Server Error";
                labelAoE2NetStatus.ForeColor = Color.Olive;
                break;
            case NetStatus.ComTimeout:
                labelAoE2NetStatus.Text = "Timeout";
                labelAoE2NetStatus.ForeColor = Color.Purple;
                break;
            case NetStatus.Connecting:
                labelAoE2NetStatus.Text = "Connecting";
                labelAoE2NetStatus.ForeColor = Color.MediumSeaGreen;
                break;
            }
        }

        private void OnErrorHandler(Exception ex)
        {
            if (ex.GetType() == typeof(HttpRequestException)) {
                ShowAoE2netStatus(NetStatus.ServerError);
            }

            if (ex.GetType() == typeof(TaskCanceledException)) {
                ShowAoE2netStatus(NetStatus.ComTimeout);
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
            RestoreWindowStatus();

            AoE2net.OnError = OnErrorHandler;

            try {
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

                _ = await ReloadProfileAsync(idtype, idText);
            } catch (AggregateException ex) {
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
                break;
            case IdType.Profile:
                idText = textBoxSettingProfileId.Text;
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
            Controler.Opacity = (double)upDownOpacity.Value * 0.01;
            Settings.Default.MainFormOpacityPercent = upDownOpacity.Value;
        }

        private void CheckBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            Controler.IsAlwaysOnTop = ((CheckBox)sender).Checked;
            Settings.Default.MainFormIsAlwaysOnTop = Controler.IsAlwaysOnTop;
        }

        private void CheckBoxHideTitle_CheckedChanged(object sender, EventArgs e)
        {
            Controler.IsHideTitle = ((CheckBox)sender).Checked;
            Settings.Default.MainFormIsHideTitle = Controler.IsHideTitle;
        }

        private void CheckBoxTransparencyWindow_CheckedChanged(object sender, EventArgs e)
        {
            Controler.IsTransparency = ((CheckBox)sender).Checked;
            Settings.Default.MainFormTransparency = Controler.IsTransparency;
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
            TextBox textBox = (TextBox)sender;
            if (!textBox.Text.StartsWith("#")) {
                textBox.Text = $"#{textBox.Text}";
            }

            SetChromaKey(textBox.Text);
        }
    }
}
