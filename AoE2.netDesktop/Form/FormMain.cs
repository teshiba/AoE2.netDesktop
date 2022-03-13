namespace AoE2NetDesktop.Form
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
        private const int PlayerNumMax = AoE2DE.PlayerNumMax;
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
            InitializeComponent();
            InitEventHandler();
            InitPlayersCtrlList();
            ClearLastMatch();
            LoadSettings();

            // formMain hold the app settings.
            CtrlSettings = new CtrlSettings() {
                OnChangeIsAlwaysOnTop = OnChangeIsAlwaysOnTop,
                OnChangeIsHideTitle = OnChangeIsHideTitle,
                OnChangeOpacity = OnChangeOpacity,
                OnChangeChromaKey = OnChangeChromaKey,
                OnChangeIsTransparency = OnChangeIsTransparency,
            };

            this.language = language;
        }

        /// <summary>
        /// Gets Settings.
        /// </summary>
        public CtrlSettings CtrlSettings { get; private set; }

        /// <inheritdoc/>
        protected override CtrlMain Controler { get => (CtrlMain)base.Controler; }

        private void InitEventHandler()
        {
            foreach (Control item in Controls) {
                item.MouseDown += Controls_MouseDown;
                item.MouseMove += Controls_MouseMove;
            }

            foreach (Control item in panelTeam1.Controls) {
                item.MouseDown += Controls_MouseDown;
                item.MouseMove += Controls_MouseMove;
            }

            foreach (Control item in panelTeam2.Controls) {
                item.MouseDown += Controls_MouseDown;
                item.MouseMove += Controls_MouseMove;
            }
        }

        private void LoadSettings()
        {
            OnChangeIsAlwaysOnTop(Settings.Default.MainFormIsAlwaysOnTop);
            OnChangeIsHideTitle(Settings.Default.MainFormIsHideTitle);
            OnChangeOpacity((double)Settings.Default.MainFormOpacityPercent * 0.01);
            OnChangeChromaKey(Settings.Default.ChromaKey);
            OnChangeIsTransparency(Settings.Default.MainFormTransparency);
        }

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
            for (int i = 0; i < PlayerNumMax; i++) {
                labelCiv[i].BackColor = Color.Transparent;
                labelName[i].BackColor = chromaKey;
                labelRate[i].BackColor = chromaKey;
                pictureBox[i].BackColor = chromaKey;
                pictureBox[i].SizeMode = PictureBoxSizeMode.Normal;
            }

            BackColor = chromaKey;
            panelTeam1.BackColor = chromaKey;
            panelTeam2.BackColor = chromaKey;
            labelAveRate1.BackColor = chromaKey;
            labelAveRate2.BackColor = chromaKey;
            labelMap.BackColor = chromaKey;
            labelGameId.BackColor = chromaKey;
            labelServer.BackColor = chromaKey;
        }

        private void RestoreWindowStatus()
        {
            Top = Settings.Default.WindowLocationMain.Y;
            Left = Settings.Default.WindowLocationMain.X;
            Width = Settings.Default.WindowSizeMain.Width;
            Height = Settings.Default.WindowSizeMain.Height;
        }

        private void SaveWindowPosition()
        {
            Settings.Default.WindowLocationMain = new Point(Left, Top);
            Settings.Default.WindowSizeMain = new Size(Width, Height);
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

        private async Task<Match> SetLastMatchData(int profileId)
        {
            Match ret;
            var playerLastmatch = await CtrlMain.GetPlayerLastMatchAsync(IdType.Profile, profileId.ToString());
            var playerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, profileId);
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

        private async Task<bool> UpdateLastMatch(int profileId)
        {
            var ret = false;

            updateToolStripMenuItem.Enabled = false;

            ClearLastMatch();
            try {
                var match = await SetLastMatchData(profileId);
                ret = true;
            } catch (Exception ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            updateToolStripMenuItem.Enabled = true;

            return ret;
        }

        private void ResizePanels()
        {
            panelTeam1.Width = (Width - 15) / 2;
            panelTeam2.Width = panelTeam1.Width;
            panelTeam1.Left = 3;
            panelTeam2.Left = 3 + panelTeam1.Width;
            panelTeam2.Top = 50;
            panelTeam1.Top = 50;

            labelErrText.Top = panelTeam1.Top + panelTeam1.Height + 3;
            labelErrText.Left = 3;
            labelErrText.Width = Width - 22;
            labelErrText.Height = Height - labelErrText.Top - 50;
        }

        ///////////////////////////////////////////////////////////////////////
        // Async event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void FormMain_Load(object sender, EventArgs e)
        {
            RestoreWindowStatus();
            ResizePanels();

            try {
                _ = await CtrlMain.InitAsync(language);

                // if the app is opened first, need to set user profile.
                if (!await CtrlSettings.ReadProfileAsync()) {
                    OpenSettings();
                }

                _ = await UpdateLastMatch(CtrlSettings.ProfileId);
            } catch (AggregateException ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            SetChromaKey(CtrlSettings.ChromaKey);

            Awaiter.Complete();
        }

        private async void UpdateToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            _ = await UpdateLastMatch(CtrlSettings.ProfileId);
            Awaiter.Complete();
        }

        private void OnChangeIsAlwaysOnTop(bool isAlwaysOnTop)
        {
            TopMost = isAlwaysOnTop;
        }

        private void OnChangeChromaKey(string chromaKey)
        {
            SetChromaKey(chromaKey);
        }

        private void OnChangeOpacity(double value)
        {
            Opacity = value;
        }

        private void OnChangeIsTransparency(bool isTransparency)
        {
            if (isTransparency) {
                TransparencyKey = ColorTranslator.FromHtml(Settings.Default.ChromaKey);
            } else {
                TransparencyKey = default;
            }
        }

        private void OnChangeIsHideTitle(bool isHide)
        {
            var top = RectangleToScreen(ClientRectangle).Top;
            var left = RectangleToScreen(ClientRectangle).Left;
            var height = RectangleToScreen(ClientRectangle).Height;

            SuspendLayout();

            if (isHide) {
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

        private void OpenSettings()
        {
            var formSettings = new FormSettings(CtrlSettings);
            formSettings.Show(this);
        }

        ///////////////////////////////////////////////////////////////////////
        // Event handlers
        ///////////////////////////////////////////////////////////////////////
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Controler.FormHistory?.Close();
            SaveWindowPosition();
            Settings.Default.Save();
        }

        private void LabelName_Paint(object sender, PaintEventArgs e)
        {
            var labelName = (Label)sender;
            var player = (Player)labelName.Tag;

            if (player?.ProfilId == CtrlSettings.ProfileId) {
                labelName.DrawString(e, 20, Color.Black, Color.DarkOrange);
            } else {
                labelName.DrawString(e, 20, Color.Black, Color.MediumSeaGreen);
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
            ((Label)sender).DrawString(e, 22, Color.Black, Color.DeepSkyBlue);
        }

        private void LabelCiv_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 15, Color.Black, Color.YellowGreen);
        }

        private void LabelAveRate_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 18, Color.Black, Color.DarkGoldenrod);
        }

        private void LabelColor_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 23, Color.Black, Color.White, new Point(6, 6));
        }

        private void LabelMap_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 28, Color.Black, Color.DarkKhaki);
        }

        private void LabelGameId_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 14, Color.Black, Color.LightSeaGreen);
        }

        private void LabelServer_Paint(object sender, PaintEventArgs e)
        {
            ((Label)sender).DrawString(e, 14, Color.Black, Color.LightSeaGreen);
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            ResizePanels();
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

        private void Controls_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                mouseDownPoint = new Point(e.X, e.Y);
            }
        }

        private void Controls_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left) {
                Left += e.X - mouseDownPoint.X;
                Top += e.Y - mouseDownPoint.Y;
            }
        }

        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            var form = (FormMain)sender;

            if (e.Button == MouseButtons.Right) {
                form.contextMenuStripMain.Show();
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
            case Keys.F5:
                updateToolStripMenuItem.PerformClick();
                break;
            default:
                Size size = GetWindowResizeParams(e);
                Size += size;
                break;
            }

            Awaiter.Complete();

            // local function
            static Size GetWindowResizeParams(KeyEventArgs e)
            {
                var changeSize = 0;

                if (e.Alt) {
                    if (e.Shift) {
                        changeSize = 1;
                    } else {
                        changeSize = 10;
                    }
                }

                var size = e.KeyCode switch {
                    Keys.Right => new Size(changeSize, 0),
                    Keys.Left => new Size(-changeSize, 0),
                    Keys.Up => new Size(0, -changeSize),
                    Keys.Down => new Size(0, changeSize),
                    _ => new Size(0, 0),
                };

                return size;
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }

        private void LabelName_DoubleClick(object sender, EventArgs e)
        {
            var labelName = (Label)sender;
            var player = (Player)labelName.Tag;

            var formHistory = CtrlHistory.GenerateFormHistory(player.Name, player.ProfilId);
            formHistory.Show();
        }

        private void ShowMyHistoryHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CtrlSettings.ShowMyHistory();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
