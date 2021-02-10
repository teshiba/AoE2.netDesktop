namespace AoE2NetDesktop
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
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
        }

        private async Task<bool> UpdateLastMatchAsync()
        {
            ClearLastMatch();
            var ret = false;
            labelErrText.Text = string.Empty;

            try {
                data = await AoE2net.GetPlayerLastMatchAsync(Settings.Default.SteamId);

                labelMap.Text = $"[Map] {data.LastMatch.MapType}";
                labelServer.Text = $"[server] {data.LastMatch.Server}";

                foreach (var item in data.LastMatch.Players) {
                    labelCiv[item.Color - 1].Text = item.Civ.ToString();
                    labelName[item.Color - 1].Text = item.Name;

                    var currentFont = labelName[item.Color - 1].Font;
                    var fontStyle = FontStyle.Bold;
                    if (!(item.Won ?? true)) {
                        fontStyle |= FontStyle.Strikeout;
                    }

                    labelName[item.Color - 1].Font = new Font(currentFont, fontStyle);
                }

                ret = true;
            } catch (HttpRequestException e) {
                labelErrText.Text = e.Message;
            } catch (TaskCanceledException e) {
                labelErrText.Text = e.Message;
            }

            return ret;
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
            Settings.Default.SteamId = long.Parse(textBoxSettingSteamId.Text);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
