namespace AoE2NetDesktop.From
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using AoE2NetDesktop;
    using LibAoE2net;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain : ControllableForm
    {
        private const int PlayerNumMax = 8;

        private readonly List<Label> labelCiv = new ();
        private readonly List<Label> labelColor = new ();
        private readonly List<Label> labelRate = new ();
        private readonly List<Label> labelName = new ();
        private readonly List<PictureBox> pictureBox = new ();
        private readonly Language language;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        /// <param name="language">Display language.</param>
        public FormMain(Language language)
            : base(new CtrlMain())
        {
            this.language = language;
            Controler.SelectedId = IdType.Steam;

            InitializeComponent();
            labelAveRate1.ForeColor = labelAveRate1.BackColor;
            labelAveRate2.ForeColor = labelAveRate2.BackColor;
            labelGameId.ForeColor = labelGameId.BackColor;
            labelServer.ForeColor = labelServer.BackColor;
            labelMap.ForeColor = labelMap.BackColor;
            InitIDRadioButton();
            InitPlayersCtrlList();
        }

        /// <inheritdoc/>
        protected override CtrlMain Controler { get => (CtrlMain)base.Controler; }

        private void RestoreWindowPosition()
        {
            Top = Settings.Default.WindowLocation.Y;
            Left = Settings.Default.WindowLocation.X;
            Width = Settings.Default.WindowSize.Width;
            Height = Settings.Default.WindowSize.Height;
        }

        private void SaveWindowPosition()
        {
            Settings.Default.WindowLocation = new Point(Left, Top);
            Settings.Default.WindowSize = new Size(Width, Height);
        }

        private void InitIDRadioButton()
        {
            Controler.SelectedId = (IdType)Settings.Default.SelectedIdType;

            switch (Controler.SelectedId) {
            case IdType.Steam:
                radioButtonSteamID.Checked = true;
                break;
            case IdType.Profile:
                radioButtonProfileID.Checked = true;
                break;
            }
        }

        private void InitPlayersCtrlList()
        {
            labelCiv.AddRange(new List<Label> {
                labelCivP1, labelCivP2, labelCivP3, labelCivP4,
                labelCivP5, labelCivP6, labelCivP7, labelCivP8,
            });

            labelName.AddRange(new List<Label> {
                labelNameP1, labelNameP2, labelNameP3, labelNameP4,
                labelNameP5, labelNameP6, labelNameP7, labelNameP8,
            });

            labelColor.AddRange(new List<Label> {
                labelColorP1, labelColorP2, labelColorP3, labelColorP4,
                labelColorP5, labelColorP6, labelColorP7, labelColorP8,
            });

            labelRate.AddRange(new List<Label> {
                labelRateP1, labelRateP2, labelRateP3, labelRateP4,
                labelRateP5, labelRateP6, labelRateP7, labelRateP8,
            });

            pictureBox.AddRange(new List<PictureBox> {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4,
                pictureBox5, pictureBox6, pictureBox7, pictureBox8,
            });

            foreach (var item in labelName) {
                item.ForeColor = item.BackColor;
            }

            foreach (var item in labelRate) {
                item.ForeColor = item.BackColor;
            }

            foreach (var item in labelCiv) {
                item.ForeColor = item.BackColor;
            }

            foreach (var item in labelColor) {
                item.ForeColor = item.BackColor;
            }
        }

        private void ClearLastMatch()
        {
            labelMap.Text = $"Map: -----";
            labelServer.Text = $"Server: -----";
            labelGameId.Text = $"GameID: --------";
            labelAveRate1.Text = $"Team1 Ave. Rate: ----";
            labelAveRate2.Text = $"Team2 Ave. Rate: ----";
            labelErrText.Text = string.Empty;

            foreach (var item in labelCiv) {
                item.Text = "----";
            }

            foreach (var item in labelName) {
                item.Text = "----";
            }

            foreach (var item in labelRate) {
                item.Text = "----";
            }

            foreach (var item in pictureBox) {
                item.Visible = false;
            }
        }

        private void SetLastMatchData(PlayerLastmatch playerLastmatch)
        {
            SetMatchData(playerLastmatch.LastMatch);
            SetPlayersData(playerLastmatch.LastMatch.Players);
        }

        private void SetMatchData(Match match)
        {
            var aveTeam1 = CtrlMain.GetAverageRate(match.Players, TeamType.OddColorNo);
            var aveTeam2 = CtrlMain.GetAverageRate(match.Players, TeamType.EvenColorNo);
            labelAveRate1.Text = $"Team1 Ave. Rate:{aveTeam1}";
            labelAveRate2.Text = $"Team2 Ave. Rate:{aveTeam2}";
            labelMap.Text = $"Map: {match.GetMapName()}";
            labelGameId.Text = $"GameID: {match.MatchId}";
            labelServer.Text = $"Server: {match.Server}";
        }

        private void SetPlayersData(List<Player> players)
        {
            foreach (var player in players) {
                if (player.Color - 1 is int index
                    && index < PlayerNumMax) {
                    pictureBox[index].ImageLocation = AoE2net.GetCivImageLocation(player.GetCivEnName());
                    labelRate[index].Text = CtrlMain.GetRateString(player.Rating);
                    labelName[index].Text = CtrlMain.GetPlayerNameString(player.Name);
                    labelCiv[index].Text = player.GetCivName();
                    labelName[index].Font = CtrlMain.GetFontStyle(player, labelName[index].Font);
                    pictureBox[index].Visible = true;
                    labelName[index].Tag = player;
                } else {
                    labelErrText.Text = $"invalid player.Color[{player.Color}]";
                }
            }
        }

        private void LoadSettings()
        {
            textBoxSettingSteamId.Text = Settings.Default.SteamId.ToString();
            textBoxSettingProfileId.Text = Settings.Default.ProfileId.ToString();
            Controler.SelectedId = (IdType)Settings.Default.SelectedIdType;
        }

        private async Task<bool> ReadProfileAsync()
        {
            var idText = string.Empty;

            switch (Controler.SelectedId) {
            case IdType.Steam:
                radioButtonSteamID.Checked = true;
                idText = textBoxSettingSteamId.Text;
                break;
            case IdType.Profile:
                radioButtonProfileID.Checked = true;
                idText = textBoxSettingProfileId.Text;
                break;
            }

            return await VerifyId(Controler.SelectedId, idText);
        }

        private async Task<bool> VerifyId(IdType idType, string idText)
        {
            bool ret;

            groupBoxPlayer.Enabled = false;

            buttonUpdate.Enabled = false;
            buttonViewHistory.Enabled = false;

            labelSettingsName.Text = $"   Name: --";
            labelSettingsCountry.Text = $"Country: --";

            try {
                ret = await Controler.ReadPlayerDataAsync(idType, idText);

                textBoxSettingSteamId.Text = Controler.SteamId;
                textBoxSettingProfileId.Text = Controler.ProfileId.ToString();
                Settings.Default.SteamId = Controler.SteamId;
                Settings.Default.ProfileId = Controler.ProfileId;
                buttonUpdate.Enabled = ret;
                buttonViewHistory.Enabled = ret;
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

        private async Task<bool> UpdateLastMatch()
        {
            var ret = false;

            buttonUpdate.Enabled = false;

            ClearLastMatch();
            try {
                var playerLastmatch = await CtrlMain.GetPlayerLastMatchAsync(IdType.Profile, textBoxSettingProfileId.Text);
                SetLastMatchData(playerLastmatch);
                ret = true;
            } catch (Exception ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            buttonUpdate.Enabled = true;

            return ret;
        }

        private void ResizePanels()
        {
            panelTeam1.Width = (tabPagePlayerLastMatch.Width - 10) / 2;
            panelTeam2.Width = panelTeam1.Width;
            panelTeam1.Left = 5;
            panelTeam2.Left = 5 + panelTeam1.Width + 5;
            panelTeam2.Top = 50;
            panelTeam1.Top = 50;
        }

        ///////////////////////////////////////////////////////////////////////
        // Event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void ButtonUpdate_Click(object sender, EventArgs e)
        {
            await UpdateLastMatch();
            Awaiter.Complete();
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            RestoreWindowPosition();
            ResizePanels();
            ClearLastMatch();
            try {
                _ = await CtrlMain.InitAsync(language);
                LoadSettings();
                _ = await ReadProfileAsync();
                _ = await UpdateLastMatch();
            } catch (Exception ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            Awaiter.Complete();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveWindowPosition();
            Settings.Default.Save();
        }

        private void CheckBoxAlwaysOnTop_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkBoxAlwaysOnTop.Checked;
        }

        private void LabelName_Paint(object sender, PaintEventArgs e)
        {
            var labelName = (Label)sender;
            var player = (Player)labelName.Tag;

            if (player?.SteamId == textBoxSettingSteamId.Text) {
                labelName.DrawString(e, 20, Color.Black, Color.DarkOrange);
            } else {
                labelName.DrawString(e, 20, Color.DarkGreen, Color.LightGreen);
            }
        }

        private void LabelNameP1_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP2_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP3_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP4_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP5_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP6_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP7_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelNameP8_Paint(object sender, PaintEventArgs e)
        {
            LabelName_Paint(sender, e);
            Awaiter.Complete();
        }

        private void LabelRate_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 15, Color.Black, Color.DeepSkyBlue);
        }

        private void LabelCiv_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 10, Color.Gray, Color.LightGoldenrodYellow);
        }

        private void LabelAveRate_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 12, Color.Silver, Color.Black);
        }

        private void LabelColor_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 22, Color.Black, Color.White, new Point(3, 3));
        }

        private void LabelMap_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 20, Color.Black, Color.White);
        }

        private void LabelGameId_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 12, Color.Gray, Color.LightGoldenrodYellow);
        }

        private void LabelServer_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 12, Color.Gray, Color.LightGoldenrodYellow);
        }

        private void RadioButtonProfileID_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton)sender;

            textBoxSettingProfileId.Enabled = radioButton.Checked;
            textBoxSettingSteamId.Enabled = !radioButton.Checked;
            if (radioButton.Checked) {
                Settings.Default.SelectedIdType = (int)IdType.Profile;
                Controler.SelectedId = IdType.Profile;
            }
        }

        private void RadioButtonSteamID_CheckedChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton)sender;

            textBoxSettingProfileId.Enabled = !radioButton.Checked;
            textBoxSettingSteamId.Enabled = radioButton.Checked;
            if (radioButton.Checked) {
                Settings.Default.SelectedIdType = (int)IdType.Steam;
                Controler.SelectedId = IdType.Steam;
            }
        }

        private void ButtonViewHistory_Click(object sender, EventArgs e)
        {
            Controler.ShowHistory();
            Awaiter.Complete();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            ResizePanels();
        }

        private async void ButtonSetId_ClickAsync(object sender, EventArgs e)
        {
            var idtype = Controler.SelectedId;
            var idText = string.Empty;

            switch (idtype) {
            case IdType.Steam:
                idText = textBoxSettingSteamId.Text;
                break;
            case IdType.Profile:
                idText = textBoxSettingProfileId.Text;
                break;
            }

            await VerifyId(idtype, idText);
            Awaiter.Complete();
        }
    }
}
