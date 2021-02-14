namespace AoE2NetDesktop.From
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using AoE2NetDesktop;
    using LibAoE2net;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain : Form
    {
        private readonly List<Label> labelCiv = new List<Label>();
        private readonly List<Label> labelColor = new List<Label>();
        private readonly List<Label> labelRate = new List<Label>();
        private readonly List<Label> labelName = new List<Label>();
        private readonly List<PictureBox> pictureBox = new List<PictureBox>();

        private Strings apiStringsEn;
        private int timerSteamIdVerifyCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            labelAveRate1.ForeColor = labelAveRate1.BackColor;
            labelAveRate2.ForeColor = labelAveRate2.BackColor;
            labelGameId.ForeColor = labelGameId.BackColor;
            labelServer.ForeColor = labelServer.BackColor;
            labelMap.ForeColor = labelMap.BackColor;
            InitEachPlayersCtrlList();
        }

        private static async Task<PlayerLastmatch> GetLastMatchDataFromAoE2Net(string steamId)
        {
            if (steamId is null) {
                throw new ArgumentNullException(nameof(steamId));
            }

            var ret = await AoE2net.GetPlayerLastMatchAsync(steamId);
            foreach (var player in ret.LastMatch.Players) {
                List<PlayerRating> rate;
                try {
                    if (player.SteamId != null) {
                        rate = await AoE2net.GetPlayerRatingHistoryAsync(
                            player.SteamId ?? string.Empty, ret.LastMatch.LeaderboardId ?? 0, 1);
                    } else {
                        rate = await AoE2net.GetPlayerRatingHistoryAsync(
                            player.ProfilId ?? 0, ret.LastMatch.LeaderboardId ?? 0, 1);
                    }
                } catch (HttpRequestException) {
                    rate = null;
                }

                if (rate != null && rate.Count != 0) {
                    player.Rating ??= rate[0].Rating;
                }
            }

            return ret;
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

        private void ClearPlayerLastMatch()
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

        private async Task<bool> UpdateLastMatchAsync()
        {
            ClearPlayerLastMatch();
            var ret = false;
            try {
                var playerLastmatch = await GetLastMatchDataFromAoE2Net(textBoxSettingSteamId.Text);
                SetLastMatchData(playerLastmatch);
                ret = true;
            } catch (HttpRequestException e) {
                labelErrText.Text = e.Message;
            } catch (TaskCanceledException e) {
                labelErrText.Text = e.Message;
            }

            return ret;
        }

        private void SetLastMatchData(PlayerLastmatch playerLastmatch)
        {
            SetPlayersData(playerLastmatch);
            SetAverageRate(playerLastmatch);

            int mapType = playerLastmatch.LastMatch.MapType ?? -1;
            if (mapType != -1) {
                var mapName = apiStringsEn.MapType.GetString(mapType);
                if (mapName == null) {
                    mapName = $"Unknown(Map No.{mapType})";
                }

                labelMap.Text = $"Map: {mapName}";
            }

            labelGameId.Text = $"GameID: {playerLastmatch.LastMatch.MatchId}";
            labelServer.Text = $"Server: {playerLastmatch.LastMatch.Server}";
        }

        private void SetPlayersData(PlayerLastmatch playerLastmatch)
        {
            foreach (var player in playerLastmatch.LastMatch.Players) {
                var civ = apiStringsEn.Civ.GetString(player.Civ ?? 0);
                var location = AoE2net.GetCivImageLocation(civ);
                var rate = player.Rating is null ? " N/A" : player.Rating.ToString();
                int index = player.Color - 1 ?? -1;
                if (index >= 0) {
                    pictureBox[index].ImageLocation = location;
                    labelRate[index].Text = rate;
                    labelName[index].Text = player.Name ?? "-- AI --";
                    labelCiv[index].Text = civ ?? player.Civ.ToString();
                    pictureBox[index].Visible = true;

                    labelName[index].Tag = player;
                }

                SetFontStyle(player);
            }
        }

        private void SetAverageRate(PlayerLastmatch playerLastmatch)
        {
            var aveP1 = ((int)playerLastmatch.LastMatch.Players
                                        .Where(player => player.Color % 2 != 0)
                                        .Select(player => player.Rating)
                                        .Average())
                                        .ToString();
            var aveP2 = ((int)playerLastmatch.LastMatch.Players
                                        .Where(player => player.Color % 2 == 0)
                                        .Select(player => player.Rating)
                                        .Average())
                                        .ToString();
            labelAveRate1.Text = $"Team1 Ave. Rate:{aveP1}";
            labelAveRate2.Text = $"Team2 Ave. Rate:{aveP2}";
        }

        private void SetFontStyle(Player player)
        {
            var index = player.Color - 1 ?? -1;
            if (index >= 0) {
                var fontStyle = FontStyle.Bold;

                if (!(player.Won ?? true)) {
                    fontStyle |= FontStyle.Strikeout;
                }

                var currentFont = labelName[index].Font;
                labelName[index].Font = new Font(currentFont, fontStyle);
            }
        }

        private void LoadSettings()
        {
            textBoxSettingSteamId.Text = Settings.Default.SteamId.ToString();
        }

        private async void ButtonUpdate_Click(object sender, EventArgs e)
        {
            buttonUpdate.Enabled = false;
            _ = await UpdateLastMatchAsync();
            buttonUpdate.Enabled = true;
        }

        private async void FormMain_Load(object sender, EventArgs e)
        {
            LoadSettings();
            ClearPlayerLastMatch();
            try {
                apiStringsEn = await AoE2net.GetStringsAsync(Language.en);
            } catch (HttpRequestException ex) {
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

        private async Task VerifySteamId()
        {
            try {
                var lastMatch = await AoE2net.GetPlayerLastMatchAsync(textBoxSettingSteamId.Text);
                labelSettingsName.Text = $"   Name: {lastMatch.Name}";
                labelSettingsCountry.Text = $"Country: {lastMatch.Country}";
                Settings.Default.SteamId = textBoxSettingSteamId.Text;
                buttonUpdate.Enabled = true;
            } catch (HttpRequestException) {
                labelSettingsName.Text = $"   Name: -- Invalid Steam ID --";
                labelSettingsCountry.Text = $"Country: -- Invalid Steam ID --";
                buttonUpdate.Enabled = false;
            } catch (TypeInitializationException ex) {
                System.Diagnostics.Debug.Print($"{ex}");
            }
        }

        private void TextBoxSettingSteamId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSettingSteamId.Text == AoE2netDemo.SteamId) {
                // Change to demo mode.
                timerSteamIdVerify.Stop();
                labelSettingsName.Text = $"   Name: Player1";
                labelSettingsCountry.Text = $"Country: JP";
                buttonUpdate.Enabled = true;
            } else {
                timerSteamIdVerify.Stop();
                timerSteamIdVerifyCount = 0;
                buttonUpdate.Enabled = false;
                timerSteamIdVerify.Start();
            }
        }

        private async void TimerSteamIdVerify_Tick(object sender, EventArgs e)
        {
            timerSteamIdVerifyCount += timerSteamIdVerify.Interval;
            if (timerSteamIdVerifyCount == 3000) {
                await VerifySteamId();
                timerSteamIdVerify.Stop();
            } else {
                var progress = timerSteamIdVerifyCount / timerSteamIdVerify.Interval;
                labelSettingsName.Text = $"   Name: {new string('-', progress)}";
                labelSettingsCountry.Text = $"Country: --";
            }
        }
    }
}
