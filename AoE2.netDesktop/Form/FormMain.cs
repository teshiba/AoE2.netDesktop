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
    using global::LibAoE2net;

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
            InitEachPlayersCtrlList();
        }

        private static async Task<PlayerLastmatch> GetLastMatchDataFromAoE2Net(string steamId)
        {
            var ret = await AoE2net.GetPlayerLastMatchAsync(steamId);
            foreach (var item in ret.LastMatch.Players) {
                var playerRate = await AoE2net.GetPlayerRatingHistoryAsync(
                    item.SteamId,
                    ret.LastMatch.LeaderboardId,
                    1);
                if (playerRate != null && playerRate.Count != 0) {
                    item.Rating = playerRate[0].Rating;
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
        }

        private void ClearPlayerLastMatch()
        {
            labelMap.Text = $"Map: -----";
            labelServer.Text = $"Server: -----";
            labelAverageRate1.Text = $"Ave.Rate: ----";
            labelAverageRate2.Text = $"Ave.Rate: ----";
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
            labelServer.Text = $"Server: {playerLastmatch.LastMatch.Server}";
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
            labelAverageRate1.Text = $"Ave. Rate:{aveP1}";
            labelAverageRate2.Text = $"Ave. Rate:{aveP2}";
        }

        private void SetPlayersData(PlayerLastmatch playerLastmatch)
        {
            foreach (var item in playerLastmatch.LastMatch.Players) {
                var civ = apiStrings.Civ.GetString(item.Civ);
                var location = AoE2net.GetCivImageLocation(civ);
                var rate = item.Rating is null ? " N/A" : item.Rating.ToString();

                if (item.Color > 0) {
                    pictureBox[item.Color - 1].ImageLocation = location;
                    labelRate[item.Color - 1].Text = rate;
                    labelName[item.Color - 1].Text = item.Name ?? "-- AI --";
                    labelCiv[item.Color - 1].Text = civ ?? item.Civ.ToString();
                    pictureBox[item.Color - 1].Visible = true;
                }

                SetFontStyle(item);
            }
        }

        private void SetFontStyle(Players item)
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
    }
}
