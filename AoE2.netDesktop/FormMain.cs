namespace AoE2NetDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain : Form
    {
        private readonly List<Label> labelCiv = new List<Label>();
        private readonly List<Label> labelName = new List<Label>();
        private readonly List<Label> labelColor = new List<Label>();
        private readonly List<Label> labelRate = new List<Label>();

        private PlayerLastmatch data;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

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
        }

        private async Task<bool> UpdateLastMatchAsync()
        {
            ClearLastMatch();
            var ret = false;
            labelErrText.Text = string.Empty;

            try {
                data = await AoE2net.GetPlayerLastMatchAsync(Settings.Default.SteamId);
                foreach (var item in data.LastMatch.Players) {
                    var playerRate = await AoE2net.GetPlayerRatingHistoryAsync(
                        item.SteamId,
                        data.LastMatch.LeaderboardId,
                        1);
                    if (playerRate.Count != 0) {
                        item.Rating = playerRate[0].Rating;
                    }
                }

                labelMap.Text = $"[Map] {data.LastMatch.MapType}";
                labelServer.Text = $"[server] {data.LastMatch.Server}";

                foreach (var item in data.LastMatch.Players) {
                    var rate = item.Rating is null ? "NoRate" : item.Rating.ToString();
                    if (item.Color > 0) {
                        labelRate[item.Color - 1].Text = rate;
                        labelCiv[item.Color - 1].Text = item.Civ.ToString();
                        labelName[item.Color - 1].Text = item.Name;
                    }

                    labelAverageRate2.Text = "Ave. Rate:"
                        + data.LastMatch.Players.Where(x => x.Color % 2 == 0)
                                                .Select(x => x.Rating)
                                                .Average()
                                                .ToString();
                    labelAverageRate1.Text = "Ave. Rate:"
                        + data.LastMatch.Players.Where(x => x.Color % 2 != 0)
                                                .Select(x => x.Rating)
                                                .Average()
                                                .ToString();

                    SetFontStyle(item);
                }

                ret = true;
            } catch (HttpRequestException e) {
                labelErrText.Text = e.Message;
            } catch (TaskCanceledException e) {
                labelErrText.Text = e.Message;
            }

            return ret;
        }

        private void SetFontStyle(Players item)
        {
            // Set font style
            var currentFont = labelName[item.Color - 1].Font;
            var fontStyle = FontStyle.Bold;
            if (!(item.Won ?? true)) {
                fontStyle |= FontStyle.Strikeout;
            }

            labelName[item.Color - 1].Font = new Font(currentFont, fontStyle);
        }

        private void ClearLastMatch()
        {
            foreach (var item in labelCiv) {
                item.Text = "---";
            }

            foreach (var item in labelName) {
                item.Text = "---";
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

        private void FormMain_Load(object sender, EventArgs e)
        {
            LoadSettings();
            ClearLastMatch();
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
