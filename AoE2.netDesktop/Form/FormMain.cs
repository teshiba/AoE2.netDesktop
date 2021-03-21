namespace AoE2NetDesktop.From
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using AoE2NetDesktop;
    using LibAoE2net;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain : Form
    {
        private const int PlayerNumMax = 8;

        private readonly List<Label> labelCiv = new List<Label>();
        private readonly List<Label> labelColor = new List<Label>();
        private readonly List<Label> labelRate = new List<Label>();
        private readonly List<Label> labelName = new List<Label>();
        private readonly List<PictureBox> pictureBox = new List<PictureBox>();
        private readonly Language language;

        private CtrlMain ctrlMain;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        /// <param name="language">Language.</param>
        public FormMain(Language language)
        {
            this.language = language;
            InitializeComponent();
            labelAveRate1.ForeColor = labelAveRate1.BackColor;
            labelAveRate2.ForeColor = labelAveRate2.BackColor;
            labelGameId.ForeColor = labelGameId.BackColor;
            labelServer.ForeColor = labelServer.BackColor;
            labelMap.ForeColor = labelMap.BackColor;
            InitEachPlayersCtrlList();
        }

        private void InitEachPlayersCtrlList()
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
            labelMap.Text = $"Map: {ctrlMain.GetMapName(match)}";
            labelGameId.Text = $"GameID: {match.MatchId}";
            labelServer.Text = $"Server: {match.Server}";
        }

        private void SetPlayersData(List<Player> players)
        {
            foreach (var player in players) {
                if (player.Color - 1 is int index
                    && index < PlayerNumMax) {
                    pictureBox[index].ImageLocation = AoE2net.GetCivImageLocation(ctrlMain.GetCivEnName(player));
                    labelRate[index].Text = CtrlMain.GetRateString(player.Rating);
                    labelName[index].Text = CtrlMain.GetPlayerNameString(player.Name);
                    labelCiv[index].Text = ctrlMain.GetCivName(player);
                    labelName[index].Font = CtrlMain.GetFontStyle(player, labelName[index].Font);
                    pictureBox[index].Visible = true;

                    labelName[index].Tag = player;
                }
            }
        }

        private void LoadSettings()
        {
            textBoxSettingSteamId.Text = Settings.Default.SteamId.ToString();
        }

        private void StartVerify()
        {
            buttonUpdate.Enabled = false;
            labelSettingsName.Text = $"   Name: --";
            labelSettingsCountry.Text = $"Country: --";
            ctrlMain.DelayStart(
                async () =>
                {
                    var ret = await ctrlMain.GetPlayerDataAsync(textBoxSettingSteamId.Text);
                    labelSettingsName.Text = $"   Name: {ctrlMain.UserName}";
                    labelSettingsCountry.Text = $"Country: {ctrlMain.UserCountry}";
                    Settings.Default.SteamId = textBoxSettingSteamId.Text;
                    buttonUpdate.Enabled = ret;
                });
        }

        ///////////////////////////////////////////////////////////////////////
        // Event handlers
        ///////////////////////////////////////////////////////////////////////
        private async void ButtonUpdate_Click(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = false;

            ClearLastMatch();
            try {
                var playerLastmatch = await CtrlMain.GetPlayerLastmatch(textBoxSettingSteamId.Text);
                SetLastMatchData(playerLastmatch);
            } catch (Exception ex) {
                labelErrText.Text = ex.Message;
            }

            buttonUpdate.Enabled = true;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            ClearLastMatch();
            try {
                ctrlMain = await CtrlMain.InitAsync(language);
                LoadSettings();
            } catch (Exception ex) {
                labelErrText.Text = ex.Message;
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
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

        private void TextBoxSettingSteamId_TextChanged(object sender, EventArgs e)
        {
            StartVerify();
        }
    }
}
