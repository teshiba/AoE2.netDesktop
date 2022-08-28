namespace AoE2NetDesktop.Form;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.SysApi;
using AoE2NetDesktop.Utility.Timer;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

/// <summary>
/// App main form.
/// </summary>
public partial class FormMain : ControllableForm
{
    private Dictionary<string, Action<string>> onChangePropertyHandler;
    private FormSettings formSettings;

    /// <summary>
    /// Gets GameTimer.
    /// </summary>
    public GameTimer GameTimer { get; }

    /// <summary>
    /// Gets LastMatchLoader.
    /// </summary>
    public LastMatchLoader LastMatchLoader { get; }

    /// <summary>
    /// Gets Settings.
    /// </summary>
    public CtrlSettings CtrlSettings { get; private set; }

    private void InitOnChangePropertyHandler()
    {
        onChangePropertyHandler = new() {
            { nameof(Settings.Default.ChromaKey), OnChangePropertyChromaKey },
            { nameof(Settings.Default.MainFormIsHideTitle), OnChangePropertyIsHideTitle },
            { nameof(Settings.Default.MainFormIsAlwaysOnTop), OnChangePropertyIsAlwaysOnTop },
            { nameof(Settings.Default.MainFormOpacityPercent), OnChangePropertyOpacity },
            { nameof(Settings.Default.MainFormIsTransparency), OnChangePropertyIsTransparency },
            { nameof(Settings.Default.DrawHighQuality), OnChangePropertyDrawHighQuality },
            { nameof(Settings.Default.IsAutoReloadLastMatch), OnChangePropertyIsAutoReloadLastMatch },
            { nameof(Settings.Default.VisibleGameTime), OnChangePropertyVisibleGameTime },
        };

        Settings.Default.PropertyChanged += Default_PropertyChanged;
    }

    private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        onChangePropertyHandler.TryGetValue(e.PropertyName, out Action<string> action);
        if(action != null) {
            action.Invoke(e.PropertyName);
        }
    }

    private void SetOptionParams()
    {
        SetChromaKey(nameof(Settings.Default.ChromaKey));
        ChangePropertyIsHideTitle(nameof(Settings.Default.MainFormIsHideTitle));
        TopMost = Settings.Default.MainFormIsAlwaysOnTop;
        Opacity = (double)Settings.Default.MainFormOpacityPercent * 0.01;
        ChangePropertyIsTransparency(nameof(Settings.Default.MainFormIsTransparency));
        ChangePropertyIsAutoReloadLastMatch(nameof(Settings.Default.IsAutoReloadLastMatch));
        ChangePropertyVisibleGameTime(nameof(Settings.Default.VisibleGameTime));
        DrawEx.DrawHighQuality = Settings.Default.DrawHighQuality;
    }

    private void ChangePropertyIsTransparency(string propertyName)
    {
        if((bool)Settings.Default[propertyName]) {
            try {
                TransparencyKey = ColorTranslator.FromHtml(Settings.Default.ChromaKey);
            } catch(ArgumentException) {
                TransparencyKey = default;
            }
        } else {
            TransparencyKey = default;
        }
    }

    private void ChangePropertyIsAutoReloadLastMatch(string propertyName)
    {
        if((bool)Settings.Default[propertyName]) {
            LastMatchLoader.Start();
        } else {
            LastMatchLoader.Stop();
        }
    }

    private void ChangePropertyVisibleGameTime(string propertyName)
    {
        var visible = (bool)Settings.Default[propertyName];
        labelStartTime1v1.Visible = visible;
        labelStartTimeTeam.Visible = visible;
        labelElapsedTime1v1.Visible = visible;
        labelElapsedTimeTeam.Visible = visible;
    }

    private void OnChangePropertyChromaKey(string propertyName)
    {
        SetChromaKey((string)Settings.Default[propertyName]);
        ChangePropertyIsTransparency(nameof(Settings.Default.MainFormIsTransparency));
    }

    private void OnChangePropertyDrawHighQuality(string propertyName)
    {
        DrawEx.DrawHighQuality = (bool)Settings.Default[propertyName];
        Refresh();
    }

    private void OnChangePropertyIsTransparency(string propertyName)
    {
        ChangePropertyIsTransparency(propertyName);
    }

    private void OnChangePropertyOpacity(string propertyName)
    {
        var opacityPercent = (decimal)Settings.Default[propertyName];
        Opacity = (double)opacityPercent * 0.01;
    }

    private void OnChangePropertyIsAlwaysOnTop(string propertyName)
    {
        TopMost = (bool)Settings.Default[propertyName];
    }

    private void OnChangePropertyIsAutoReloadLastMatch(string propertyName)
    {
        ChangePropertyIsAutoReloadLastMatch(propertyName);
    }

    private void OnChangePropertyVisibleGameTime(string propertyName)
    {
        ChangePropertyVisibleGameTime(propertyName);
    }

    private void OnChangePropertyIsHideTitle(string propertyName)
    {
        ChangePropertyIsHideTitle(propertyName);
    }

    private void ChangePropertyIsHideTitle(string propertyName)
    {
        var top = RectangleToScreen(ClientRectangle).Top;
        var left = RectangleToScreen(ClientRectangle).Left;
        var width = RectangleToScreen(ClientRectangle).Width;

        SuspendLayout();

        if((bool)Settings.Default[propertyName] && FormBorderStyle != FormBorderStyle.None) {
            MinimumSize = new Size(860, 270);
            FormBorderStyle = FormBorderStyle.None;
            Top = top;
            Left = left;
            Width = width + 13;
        } else if(!(bool)Settings.Default[propertyName] && FormBorderStyle != FormBorderStyle.Sizable) {
            MinimumSize = new Size(860, 315);
            FormBorderStyle = FormBorderStyle.Sizable;
            Top -= RectangleToScreen(ClientRectangle).Top - Top;
            Left -= RectangleToScreen(ClientRectangle).Left - Left;
            Width = width - 13;
        } else {
            // nothing to do.
        }

        ResumeLayout();
    }

    private void InitEventHandler()
    {
        foreach(Control item in Controls) {
            foreach(Control panelItem in ((Panel)item).Controls) {
                panelItem.MouseDown += Controls_MouseDown;
                panelItem.MouseMove += Controls_MouseMove;
            }
        }
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
        pictureBoxMap.Image = CtrlMain.LoadMapIcon(null);
        labelMap.Text = $"Map: -----";
        labelServer.Text = $"Server : -----";
        labelGameId.Text = $"GameID : --------";
        labelAveRate1.Text = $"Team1 Ave. Rate: ----";
        labelAveRate2.Text = $"Team2 Ave. Rate: ----";
        labelErrText.Text = string.Empty;

        pictureBoxMap1v1.Image = CtrlMain.LoadMapIcon(null);
        labelMap1v1.Text = "-----------------------";
        labelServer1v1.Text = $"Server : -----";
        labelGameId1v1.Text = $"GameID : --------";

        const string IntiStartText = $"Start {DateTimeExt.InvalidDate} {DateTimeExt.InvalidTime}";
        const string ElapsedTimeText = $"Time {DateTimeExt.InvalidTime}";

        labelStartTime1v1.Text = IntiStartText;
        labelElapsedTime1v1.Text = ElapsedTimeText;
        labelStartTimeTeam.Text = IntiStartText;
        labelElapsedTimeTeam.Text = ElapsedTimeText;

        ClearPlayersLabel();
        Refresh();
    }

    private void ClearPlayersLabel()
    {
        foreach(var item in labelCiv) {
            item.Text = "----";
        }

        foreach(var item in labelName) {
            item.Text = "----";
            item.Tag = null;
        }

        foreach(var item in labelRate) {
            item.Text = "----";
        }

        foreach(var item in pictureBox) {
            item.Visible = false;
        }

        label1v1ColorP1.Text = string.Empty;
        labelName1v1P1.Text = string.Empty;
        labelName1v1P1.Tag = null;
        pictureBoxCiv1v1P1.ImageLocation = null;
        pictureBoxUnit1v1P1.Image = null;
        labelRate1v1P1.Text = string.Empty;
        labelWins1v1P1.Text = string.Empty;
        labelLoses1v1P1.Text = string.Empty;
        labelCiv1v1P1.Text = string.Empty;

        label1v1ColorP2.Text = string.Empty;
        labelName1v1P2.Text = string.Empty;
        labelName1v1P2.Tag = null;
        pictureBoxCiv1v1P2.ImageLocation = null;
        pictureBoxUnit1v1P2.Image = null;
        labelRate1v1P2.Text = string.Empty;
        labelWins1v1P2.Text = string.Empty;
        labelLoses1v1P2.Text = string.Empty;
        labelCiv1v1P2.Text = string.Empty;
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

    private void OpenSettings()
    {
        if(formSettings == null || formSettings.IsDisposed) {
            formSettings = new FormSettings(CtrlSettings);
        }

        formSettings.Show();
        formSettings.Activate();
    }

    private void ResizePanels()
    {
        const int ctrlMargin = 5;

        panelTeam1.Width = (Width - panelGameInfo.Width - 15) / 2;
        panelTeam2.Width = panelTeam1.Width;
        panelTeam1.Left = ctrlMargin;
        panelTeam2.Left = ctrlMargin + panelTeam1.Width;
        panelTeam2.Top = ctrlMargin;
        panelTeam1.Top = ctrlMargin;

        panelGameInfo.Left = panelTeam2.Left + panelTeam2.Width + ctrlMargin;

        labelErrText.Top = panelTeam1.Top + panelTeam1.Height + ctrlMargin;
        labelErrText.Left = ctrlMargin;
        labelErrText.Width = Width - 22;
        labelErrText.Height = Height - labelErrText.Top - 50;

        panel1v1.Top = ctrlMargin;
        panel1v1.Left = ctrlMargin;
    }

    private void SetChromaKey(string htmlColor)
    {
        Color chromaKey;

        try {
            chromaKey = ColorTranslator.FromHtml(htmlColor);
        } catch(ArgumentException) {
            chromaKey = Color.Empty;
        }

        SetChromaKey(chromaKey);
    }

    private void SetChromaKey(Color chromaKey)
    {
        foreach(Control item in Controls) {
            foreach(Control panelItem in ((Panel)item).Controls) {
                panelItem.BackColor = chromaKey;
            }
        }

        BackColor = chromaKey;
        panelTeam1.BackColor = chromaKey;
        panelTeam2.BackColor = chromaKey;

        for(int i = 0; i < labelColor.Count; i++) {
            labelColor[i].BackColor = AoE2DeApp.GetColor(i + 1);
        }
    }

    private void SetMatchData(Match match)
    {
        var aveTeam1 = CtrlMain.GetAverageRate(match.Players, TeamType.OddColorNo);
        var aveTeam2 = CtrlMain.GetAverageRate(match.Players, TeamType.EvenColorNo);
        pictureBoxMap.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap.Text = $"Map: {match.GetMapName()}";
        labelServer.Text = $"Server : {match.Server}";
        labelGameId.Text = $"GameID : {match.MatchId}";
        labelAveRate1.Text = $"Team1 Ave. Rate:{aveTeam1}";
        labelAveRate2.Text = $"Team2 Ave. Rate:{aveTeam2}";
    }

    private void SetMatchData1v1(Match match)
    {
        pictureBoxMap1v1.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap1v1.Text = match.GetMapName();
        labelServer1v1.Text = $"Server : {match.Server}";
        labelGameId1v1.Text = $"GameID : {match.MatchId}";
    }

    private void SetPlayersData1v1(Player player1, Player player2)
    {
        label1v1ColorP1.Text = player1.GetColorString();
        label1v1ColorP1.BackColor = player1.GetColor();
        labelName1v1P1.Text = CtrlMain.GetPlayerNameString(player1.Name);
        labelName1v1P1.Font = CtrlMain.GetFontStyle(player1, labelName1v1P1.Font);
        labelName1v1P1.Tag = player1;
        pictureBoxCiv1v1P1.ImageLocation = player1.GetCivImageLocation();
        pictureBoxUnit1v1P1.Image = UnitImages.Load(player1.GetCivEnName(), player1.GetColor());
        labelRate1v1P1.Text = CtrlMain.GetRateString(player1.Rating);
        labelWins1v1P1.Text = CtrlMain.GetWinsString(player1);
        labelLoses1v1P1.Text = CtrlMain.GetLossesString(player1);
        labelCiv1v1P1.Text = player1.GetCivName();
        labelTeamResultP1.Text = $"";

        label1v1ColorP2.Text = player2.GetColorString();
        label1v1ColorP2.BackColor = player2.GetColor();
        labelName1v1P2.Text = CtrlMain.GetPlayerNameString(player2.Name);
        labelName1v1P2.Font = CtrlMain.GetFontStyle(player2, labelName1v1P2.Font);
        labelName1v1P2.Tag = player2;
        pictureBoxCiv1v1P2.ImageLocation = player2.GetCivImageLocation();
        pictureBoxUnit1v1P2.Image = UnitImages.Load(player2.GetCivEnName(), player2.GetColor());
        labelRate1v1P2.Text = CtrlMain.GetRateString(player2.Rating);
        labelWins1v1P2.Text = CtrlMain.GetWinsString(player2);
        labelLoses1v1P2.Text = CtrlMain.GetLossesString(player2);
        labelCiv1v1P2.Text = player2.GetCivName();
        labelTeamResultP2.Text = $"";
    }

    private void SetPlayersData(List<Player> players)
    {
        ClearPlayersLabel();
        foreach(var player in players) {
            if(player.Color - 1 is int index
                && index < AoE2netHelpers.PlayerNumMax
                && index > -1) {
                labelName[index].Text = CtrlMain.GetPlayerNameString(player.Name);
                labelName[index].Font = CtrlMain.GetFontStyle(player, labelName[index].Font);
                labelName[index].Tag = player;
                pictureBox[index].Visible = true;
                pictureBox[index].ImageLocation = player.GetCivImageLocation();
                labelRate[index].Text = CtrlMain.GetRateString(player.Rating);
                labelCiv[index].Text = player.GetCivName();
            } else {
                labelErrText.Text = $"invalid player.Color[{player.Color}]";
            }
        }
    }

    private bool OnTimerGame()
    {
        // update text
        Invoke(() =>
        {
            labelStartTimeTeam.Text = CtrlMain.GetOpenedTime();
            labelElapsedTimeTeam.Text = CtrlMain.GetElapsedTime();
            labelStartTime1v1.Text = CtrlMain.GetOpenedTime();
            labelElapsedTime1v1.Text = CtrlMain.GetElapsedTime();
        });

        return CtrlMain.LastMatch.Finished == null;
    }

    private void OnTimerLastMatchLoader(object sender, EventArgs e)
    {
        LastMatchLoader.Stop();
        if(CtrlMain.IsAoE2deActive()) {
            labelAoE2DEActive.Invoke(() => { labelAoE2DEActive.Text = "AoE2DE active"; });
            CtrlMain.IsReloadingByTimer = true;
            Invoke(() => updateToolStripMenuItem.PerformClick());
        } else {
            labelAoE2DEActive.Invoke(() => { labelAoE2DEActive.Text = "AoE2DE NOT active"; });
            LastMatchLoader.Start();
        }

        Awaiter.Complete();
    }

    private async Task<Match> SetLastMatchDataAsync(Match match, LeaderboardId? leaderboard)
    {
        if(match.NumPlayers == 2) {
            var leaderboardP1 = await AoE2net.GetLeaderboardAsync(leaderboard, 0, 1, match.Players[0].ProfilId);
            var leaderboardP2 = await AoE2net.GetLeaderboardAsync(leaderboard, 0, 1, match.Players[1].ProfilId);

            if(leaderboardP1.Leaderboards.Count != 0) {
                var player1 = leaderboardP1.Leaderboards[0];
                match.Players[0].Games = player1.Games;
                match.Players[0].Wins = player1.Wins;
            }

            if(leaderboardP2.Leaderboards.Count != 0) {
                var player2 = leaderboardP2.Leaderboards[0];
                match.Players[1].Games = player2.Games;
                match.Players[1].Wins = player2.Wins;
            }

            SetPlayersData1v1(match.Players[0], match.Players[1]);
            SetMatchData1v1(match);
        } else {
            SetPlayersData(match.Players);
            SetMatchData(match);
        }

        return match;
    }

    private async Task<Match> RedrawLastMatchAsync()
    {
        return await RedrawLastMatchAsync(CtrlSettings.ProfileId);
    }

    private async Task<Match> RedrawLastMatchAsync(int profileId)
    {
        Match match = null;
        updateToolStripMenuItem.Enabled = false;

        try {
            var playerLastmatch = await AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, profileId.ToString());
            if(labelGameId.Text == $"GameID : {playerLastmatch.LastMatch.MatchId}") {
                match = playerLastmatch.LastMatch;
            } else {
                LeaderboardId? leaderboard;
                var playerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, profileId);
                if(playerMatchHistory.Count != 0
                    && playerMatchHistory[0].MatchId == playerLastmatch.LastMatch.MatchId) {
                    match = playerMatchHistory[0];
                    leaderboard = playerMatchHistory[0].LeaderboardId;
                } else {
                    match = playerLastmatch.LastMatch;
                    leaderboard = playerLastmatch.LastMatch.LeaderboardId;
                }

                match = await SetLastMatchDataAsync(match, leaderboard);
                SwitchView(match);
                GameTimer.Start();
            }
        } catch(Exception ex) {
            labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
        }

        updateToolStripMenuItem.Enabled = true;
        return match;
    }

    private void SwitchView(Match match)
    {
        if(match.NumPlayers == 2) {
            panel1v1.Visible = true;
            panelGameInfo.Visible = false;
            panelTeam1.Visible = false;
            panelTeam2.Visible = false;
        } else {
            panel1v1.Visible = false;
            panelGameInfo.Visible = true;
            panelTeam1.Visible = true;
            panelTeam2.Visible = true;
        }

        labelDateTime.Text = $"Last match data updated: {DateTime.Now}";
    }
}
