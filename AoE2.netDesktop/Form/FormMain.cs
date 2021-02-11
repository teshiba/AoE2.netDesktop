namespace AoE2NetDesktop.From
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
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
        private readonly List<Label> labelName = new List<Label>();
        private readonly List<Label> labelColor = new List<Label>();
        private readonly List<Label> labelRate = new List<Label>();
        private readonly List<PictureBox> pictureBox = new List<PictureBox>();

        private Strings apiStrings;

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
            var ret = await AoE2net.GetPlayerLastMatchAsync(steamId);
            foreach (var player in ret.LastMatch.Players) {
                var rate = await AoE2net.GetPlayerRatingHistoryAsync(
                    player.SteamId,
                    ret.LastMatch.LeaderboardId,
                    1);
                if (rate != null && rate.Count != 0) {
                    player.Rating ??= rate[0].Rating;
                }
            }

            return ret;
        }

        private static void DrawBorderedString(Label label, PaintEventArgs e, float fontSize, Color borderColor, Color fillColor)
        {
            DrawBorderedString(label, e, fontSize, borderColor, fillColor, new Point(0, 0));
        }

        private static void DrawBorderedString(Label label, PaintEventArgs e, float fontSize, Color borderColor, Color fillColor, Point point)
        {
            var stringFormat = new StringFormat {
                FormatFlags = StringFormatFlags.NoWrap,
                Trimming = StringTrimming.None,
            };

            var graphicsPath = new GraphicsPath();
            graphicsPath.AddString(
                label.Text,
                label.Font.FontFamily,
                1,
                fontSize,
                point,
                stringFormat);

            var pen = new Pen(borderColor, 3) {
                LineJoin = LineJoin.Round,
            };

            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            e.Graphics.DrawPath(pen, graphicsPath);
            e.Graphics.FillPath(new SolidBrush(fillColor), graphicsPath);
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
                var playerLastmatch = await GetLastMatchDataFromAoE2Net(Settings.Default.SteamId);
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

            var mapName = apiStrings.MapType.GetString(playerLastmatch.LastMatch.MapType);
            if (mapName == null) {
                mapName = $"Unknown(Map No.{playerLastmatch.LastMatch.MapType})";
            }

            labelMap.Text = $"Map: {mapName}";
            labelGameId.Text = $"GameID: {playerLastmatch.LastMatch.MatchId}";
            labelServer.Text = $"Server: {playerLastmatch.LastMatch.Server}";
        }

        private void SetPlayersData(PlayerLastmatch playerLastmatch)
        {
            foreach (var player in playerLastmatch.LastMatch.Players) {
                var civ = apiStrings.Civ.GetString(player.Civ);
                var location = AoE2net.GetCivImageLocation(civ);
                var rate = player.Rating is null ? " N/A" : player.Rating.ToString();

                if (player.Color > 0) {
                    pictureBox[player.Color - 1].ImageLocation = location;
                    labelRate[player.Color - 1].Text = rate;
                    labelName[player.Color - 1].Text = player.Name ?? "-- AI --";
                    labelCiv[player.Color - 1].Text = civ ?? player.Civ.ToString();
                    pictureBox[player.Color - 1].Visible = true;

                    labelName[player.Color - 1].Tag = player;
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

        private void SetFontStyle(Player item)
        {
            if (item.Color > 0) {
                var fontStyle = FontStyle.Bold;

                if (!(item.Won ?? true)) {
                    fontStyle |= FontStyle.Strikeout;
                }

                var currentFont = labelName[item.Color - 1].Font;
                labelName[item.Color - 1].Font = new Font(currentFont, fontStyle);
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
            buttonUpdate.Enabled = false;
            LoadSettings();
            ClearPlayerLastMatch();
            apiStrings = await AoE2net.GetStringsAsync(Language.en);
            buttonUpdate.Enabled = true;
        }

        private void TabPageSettings_Leave(object sender, EventArgs e)
        {
            Settings.Default.SteamId = textBoxSettingSteamId.Text;
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
                DrawBorderedString(labelName, e, 20, Color.Black, Color.Salmon);
            } else {
                DrawBorderedString(labelName, e, 20, Color.Black, Color.LightGreen);
            }
        }

        private void LabelRate_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 15, Color.Black, Color.DarkOrange);
        }

        private void LabelCiv_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 10, Color.Gray, Color.LightGoldenrodYellow);
        }

        private void LabelAveRate_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 12, Color.Black, Color.White);
        }

        private void LabelColor_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 22, Color.Black, Color.White, new Point(3, 3));
        }

        private void LabelMap_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 20, Color.Black, Color.White);
        }

        private void LabelGameId_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 12, Color.Black, Color.White);
        }

        private void LabelServer_Paint(object sender, PaintEventArgs e)
        {
            DrawBorderedString((Label)sender, e, 12, Color.Black, Color.White);
        }
    }
}
