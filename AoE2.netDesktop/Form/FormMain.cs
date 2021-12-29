namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Net.Http;
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
        private const int LabelMapMargin = 20;
        private readonly List<Label> labelCiv = new ();
        private readonly List<Label> labelColor = new ();
        private readonly List<Label> labelRate = new ();
        private readonly List<Label> labelName = new ();
        private readonly List<PictureBox> pictureBox = new ();
        private readonly Language language;

        private Point mouseDownPoint;

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
            InitIDRadioButton();
            InitPlayersCtrlList();
            ShowAoE2netStatus(NetStatus.Disconnected);
        }

        /// <inheritdoc/>
        protected override CtrlMain Controler { get => (CtrlMain)base.Controler; }

        private void RestoreWindowStatus()
        {
            Top = Settings.Default.WindowLocationMain.Y;
            Left = Settings.Default.WindowLocationMain.X;
            Width = Settings.Default.WindowSizeMain.Width;
            Height = Settings.Default.WindowSizeMain.Height;
            upDownOpacity.Value = Settings.Default.MainFormOpacityPercent;
        }

        private void SaveWindowPosition()
        {
            Settings.Default.WindowLocationMain = new Point(Left, Top);
            Settings.Default.WindowSizeMain = new Size(Width, Height);
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
                item.Tag = null;
            }

            foreach (var item in labelRate) {
                item.Text = "----";
            }

            foreach (var item in pictureBox) {
                item.Visible = false;
            }
        }

        private async Task<Match> SetLastMatchData()
        {
            Match ret;
            var playerLastmatch = await CtrlMain.GetPlayerLastMatchAsync(IdType.Profile, textBoxSettingProfileId.Text);
            var playerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, int.Parse(textBoxSettingProfileId.Text));
            SetMatchData(playerLastmatch.LastMatch);

            if (playerMatchHistory.Count != 0
                && playerMatchHistory[0].MatchId == playerLastmatch.LastMatch.MatchId) {
                SetPlayersData(playerMatchHistory[0].Players);
                ret = playerMatchHistory[0];
            } else {
                SetPlayersData(playerLastmatch.LastMatch.Players);
                ret = playerLastmatch.LastMatch;
            }

            return ret;
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

            ArrangeGameIdAndServerNameLocation();
        }

        private void ArrangeGameIdAndServerNameLocation()
        {
            var graphics = labelMap.CreateGraphics();
            var stringSize = graphics.MeasureString(labelMap.Text, labelMap.Font);
            labelMap.Width = (int)stringSize.Width + LabelMapMargin;

            labelGameId.Left = labelMap.Right;
            labelServer.Left = labelMap.Right;
        }

        private void SetPlayersData(List<Player> players)
        {
            foreach (var player in players) {
                if (player.Color - 1 is int index
                    && index < PlayerNumMax
                    && index > -1) {
                    pictureBox[index].ImageLocation = AoE2net.GetCivImageLocation(player.GetCivEnName());
                    labelRate[index].Text = CtrlMain.GetRateString(player.Rating);
                    labelName[index].Text = CtrlMain.GetPlayerNameString(player.Name);
                    labelCiv[index].Text = player.GetCivName();
                    labelName[index].Font = CtrlMain.GetFontStyle(player, labelName[index].Font);
                    pictureBox[index].Visible = true;
                    labelName[index].Tag = player;
                } else {
                    labelErrText.Text = $"invalid player.Color[{player.Color}]";
                    break;
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
            ShowAoE2netStatus(NetStatus.Connecting);
            try {
                ret = await Controler.ReadPlayerDataAsync(idType, idText);

                textBoxSettingSteamId.Text = Controler.SteamId;
                textBoxSettingProfileId.Text = Controler.ProfileId.ToString();
                Settings.Default.SteamId = Controler.SteamId;
                Settings.Default.ProfileId = Controler.ProfileId;
                buttonUpdate.Enabled = ret;
                buttonViewHistory.Enabled = ret;
                ShowAoE2netStatus(NetStatus.Connected);
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

        private async Task<bool> UpdateLastMatch()
        {
            var ret = false;

            buttonUpdate.Enabled = false;

            ClearLastMatch();
            try {
                var match = await SetLastMatchData();
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
            panelTeam2.Top = 35;
            panelTeam1.Top = 35;
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
            RestoreWindowStatus();
            ResizePanels();
            ClearLastMatch();
            try {
                AoE2net.OnError = OnErrorHandler;
                _ = await CtrlMain.InitAsync(language);
                LoadSettings();
                _ = await ReadProfileAsync();
                _ = await UpdateLastMatch();
            } catch (AggregateException ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            Awaiter.Complete();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controler.FormHistory?.Close();
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

            if (player?.ProfilId.ToString() == textBoxSettingProfileId.Text) {
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

        private void CheckBoxHideTitle_CheckedChanged(object sender, EventArgs e)
        {
            var top = RectangleToScreen(ClientRectangle).Top;
            var left = RectangleToScreen(ClientRectangle).Left;
            var height = RectangleToScreen(ClientRectangle).Height;

            SuspendLayout();

            if (checkBoxHideTitle.Checked) {
                FormBorderStyle = FormBorderStyle.None;
                MinimumSize = new Size(290, 230);
                Top = top;
                Left = left;
                Height = height;
            } else {
                FormBorderStyle = FormBorderStyle.Sizable;
                MinimumSize = new Size(290, 295);
                Top -= RectangleToScreen(ClientRectangle).Top - Top;
                Left -= RectangleToScreen(ClientRectangle).Left - Left;
            }

            ResumeLayout();
        }

        private void TabControlMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5) {
                buttonUpdate.PerformClick();
            }
        }

        private void UpDownOpacity_ValueChanged(object sender, EventArgs e)
        {
            Opacity = (double)upDownOpacity.Value * 0.01;
            Settings.Default.MainFormOpacityPercent = upDownOpacity.Value;
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                mouseDownPoint = new Point(e.X, e.Y);
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                Left += e.X - mouseDownPoint.X;
                Top += e.Y - mouseDownPoint.Y;
            }
        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResizePanels();
        }
    }
}
