﻿namespace AoE2NetDesktop.Form
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using LibAoE2net;

    /// <summary>
    /// App main form.
    /// </summary>
    public partial class FormMain : ControllableForm
    {
        private const int PlayerNumMax = AoE2DE.PlayerNumMax;

        /// <summary>
        /// Gets lastMatchLoader.
        /// </summary>
        public LastMatchLoader LastMatchLoader { get; }

        private void OnChangeIsHideTitle(bool isHide)
        {
            var top = RectangleToScreen(ClientRectangle).Top;
            var left = RectangleToScreen(ClientRectangle).Left;
            var height = RectangleToScreen(ClientRectangle).Height;

            SuspendLayout();

            if (isHide && FormBorderStyle != FormBorderStyle.None) {
                FormBorderStyle = FormBorderStyle.None;
                MinimumSize = new Size(410, 310);
                Top = top;
                Left = left;
                Height = height;
            } else if (!isHide && FormBorderStyle != FormBorderStyle.Sizable) {
                FormBorderStyle = FormBorderStyle.Sizable;
                MinimumSize = new Size(410, 340);
                Top -= RectangleToScreen(ClientRectangle).Top - Top;
                Left -= RectangleToScreen(ClientRectangle).Left - Left;
            }

            ResumeLayout();
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

        private void OnChangeIsAutoReloadLastMatch(bool isAutoReloadLastMatch)
        {
            if (isAutoReloadLastMatch) {
                LastMatchLoader.Start();
            } else {
                LastMatchLoader.Stop();
            }
        }

        private void OnChangeIsTransparency(bool isTransparency)
        {
            if (isTransparency) {
                TransparencyKey = ColorTranslator.FromHtml(CtrlSettings.PropertySetting.ChromaKey);
            } else {
                TransparencyKey = default;
            }
        }

        private void OpenSettings()
        {
            var formSettings = new FormSettings(CtrlSettings);
            formSettings.Show();
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

        private void SetChromaKey(string htmlColor)
        {
            Color chromaKey;

            try {
                chromaKey = ColorTranslator.FromHtml(htmlColor);
            } catch (Exception) {
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

        private async Task<Match> SetLastMatchDataAsync(int profileId)
        {
            Match ret;
            var playerLastmatch = await AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, profileId.ToString());
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

        private async Task<bool> RedrawLastMatchAsync(int profileId)
        {
            ClearLastMatch();
            return await UpdateLastMatchAsync(profileId);
        }

        private async void OnTimerAsync(object sender, EventArgs e)
        {
            await UpdateLastMatchAsync(CtrlSettings.ProfileId);
            Awaiter.Complete();
        }

        private async Task<bool> UpdateLastMatchAsync(int profileId)
        {
            var ret = false;
            updateToolStripMenuItem.Enabled = false;

            try {
                var match = await SetLastMatchDataAsync(profileId);
                ret = true;
            } catch (Exception ex) {
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            updateToolStripMenuItem.Enabled = true;
            return ret;
        }
    }
}