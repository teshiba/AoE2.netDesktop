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
    private int currentMatchView;
    private bool isDrawing;
    private int requestMatchView;
    private DisplayStatus displayStatus;

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

    private static async Task GetGameResultAsync(Player player, LeaderboardId? leaderboardId)
    {
        var leaderboardContainer = await AoE2net.GetLeaderboardAsync(leaderboardId, 0, 1, player.ProfilId);
        if(leaderboardContainer.Leaderboards.Count != 0) {
            var leaderboard = leaderboardContainer.Leaderboards[0];
            player.Games = leaderboard.Games;
            player.Wins = leaderboard.Wins;
        }
    }

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
            MinimumSize = new Size(960, 270);
            FormBorderStyle = FormBorderStyle.None;
            Top = top;
            Left = left;
            Width = width + 13;
        } else if(!(bool)Settings.Default[propertyName] && FormBorderStyle != FormBorderStyle.Sizable) {
            MinimumSize = new Size(960, 315);
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
        displayStatus = DisplayStatus.Clearing;
        pictureBoxMap.Image = CtrlMain.LoadMapIcon(null);
        labelMap.Text = $"Map: -----";
        labelServer.Text = $"Server : -----";
        labelGameId.Text = $"GameID : --------";
        labelAveRate1.Text = $"Team1 Ave. Rate: ----";
        labelAveRate2.Text = $"Team2 Ave. Rate: ----";
        labelErrText.Text = string.Empty;
        labelMatchResultTeam1.Text = MatchResult.Unknown.ToString();
        labelMatchResultTeam1.Tag = MatchResult.Unknown;
        labelMatchResultTeam2.Text = MatchResult.Unknown.ToString();
        labelMatchResultTeam2.Tag = MatchResult.Unknown;

        pictureBoxMap1v1.Image = CtrlMain.LoadMapIcon(null);
        labelMap1v1.Text = "-----------------------";
        labelServer1v1.Text = $"Server : -----";
        labelGameId1v1.Text = $"GameID : --------";
        labelMatchResult1v1p1.Text = MatchResult.Unknown.ToString();
        labelMatchResult1v1p1.Tag = MatchResult.Unknown;
        labelMatchResult1v1p2.Text = MatchResult.Unknown.ToString();
        labelMatchResult1v1p2.Tag = MatchResult.Unknown;

        const string IntiStartText = $"Start {DateTimeExt.InvalidDate} {DateTimeExt.InvalidTime}";
        const string ElapsedTimeText = $"Time {DateTimeExt.InvalidTime}";

        labelStartTime1v1.Text = IntiStartText;
        labelElapsedTime1v1.Text = ElapsedTimeText;
        labelStartTimeTeam.Text = IntiStartText;
        labelElapsedTimeTeam.Text = ElapsedTimeText;

        ClearPlayersLabel();
        Refresh();
        displayStatus = DisplayStatus.Cleared;
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

    private void SetMatchDataTeam(Match match, string specificMatchId, int? prevMatchNo)
    {
        if(specificMatchId == null) {
            labelMatchNo.Text = CtrlMain.GetMatchNoString(prevMatchNo);
            labelGameId.Text = $"GameID : {match.MatchId}";
        } else {
            labelMatchNo.Text = "Specific match";
            labelGameId.Text = $"GameID : {specificMatchId}";
        }

        var aveTeam1 = match.Players.GetAverageRate(TeamType.OddColorNo);
        var aveTeam2 = match.Players.GetAverageRate(TeamType.EvenColorNo);
        pictureBoxMap.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap.Text = $"Map: {match.GetMapName()}";
        labelServer.Text = $"Server : {match.Server}";
        labelAveRate1.Text = $"Team1 Ave. Rate:{aveTeam1}";
        labelAveRate2.Text = $"Team2 Ave. Rate:{aveTeam2}";
        labelMatchResultTeam1.Text = match.GetMatchResult(TeamType.OddColorNo).ToString();
        labelMatchResultTeam1.Tag = match.GetMatchResult(TeamType.OddColorNo);
        labelMatchResultTeam2.Text = match.GetMatchResult(TeamType.EvenColorNo).ToString();
        labelMatchResultTeam2.Tag = match.GetMatchResult(TeamType.EvenColorNo);
        labelStartTimeTeam.Text = CtrlMain.GetOpenedTimeString(match);
        labelElapsedTimeTeam.Text = CtrlMain.GetElapsedTimeString(match);
    }

    private void SetMatchData1v1(Match match, string specificMatchId, int? prevMatchNo)
    {
        if(specificMatchId == null) {
            labelMatchNo1v1.Text = CtrlMain.GetMatchNoString(prevMatchNo);
            labelGameId1v1.Text = $"GameID : {match.MatchId}";
        } else {
            labelMatchNo1v1.Text = "Specific match";
            labelGameId1v1.Text = $"GameID : {specificMatchId}";
        }

        pictureBoxMap1v1.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap1v1.Text = match.GetMapName();
        labelServer1v1.Text = $"Server : {match.Server}";
        labelMatchResult1v1p1.Text = match.GetMatchResult(TeamType.OddColorNo).ToString();
        labelMatchResult1v1p1.Tag = match.GetMatchResult(TeamType.OddColorNo);
        labelMatchResult1v1p2.Text = match.GetMatchResult(TeamType.EvenColorNo).ToString();
        labelMatchResult1v1p2.Tag = match.GetMatchResult(TeamType.EvenColorNo);
        labelStartTime1v1.Text = CtrlMain.GetOpenedTimeString(match);
        labelElapsedTime1v1.Text = CtrlMain.GetElapsedTimeString(match);
    }

    private void SetPlayersData1v1(List<Player> players)
    {
        Player player1;
        Player player2;

        if(players[0].IsOddColor()) {
            player1 = players[0];
            player2 = players[1];
        } else {
            player1 = players[1];
            player2 = players[0];
        }

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
        var ret = false;
        DisplayStatus currentStatus;

        lock(GameTimer) {
            currentStatus = displayStatus;
        }

        if(currentStatus != DisplayStatus.Closing) {
            // update text
            Invoke(() =>
            {
                labelStartTimeTeam.Text = CtrlMain.GetOpenedTimeString(CtrlMain.DisplayedMatch);
                labelElapsedTimeTeam.Text = CtrlMain.GetElapsedTimeString(CtrlMain.DisplayedMatch);
                labelStartTime1v1.Text = CtrlMain.GetOpenedTimeString(CtrlMain.DisplayedMatch);
                labelElapsedTime1v1.Text = CtrlMain.GetElapsedTimeString(CtrlMain.DisplayedMatch);
            });

            ret = CtrlMain.DisplayedMatch?.Finished == null;
        }

        Awaiter.Complete();
        return ret;
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
        }

        if(Settings.Default.IsAutoReloadLastMatch) {
            LastMatchLoader.Start();
        }

        Awaiter.Complete();
    }

    private async Task<Match> DrawMatchAsync(Match match, string specificMatchId, int? prevMatchNo)
    {
        var ret = match;

        if(prevMatchNo is not null) {
            requestMatchView = (int)prevMatchNo;
        }

        if(labelGameId.Text != $"GameID : {specificMatchId ?? match.MatchId}") {
            if(match is null) {
                ClearLastMatch();
                labelMatchNo.Text = "Load Error: Invalid ID";
            } else {
                if(match.NumPlayers == 2) {
                    await GetGameResultAsync(match.Players[0], match.LeaderboardId);
                    await GetGameResultAsync(match.Players[1], match.LeaderboardId);
                    SetPlayersData1v1(match.Players);
                    SetMatchData1v1(match, specificMatchId, prevMatchNo);
                } else {
                    SetPlayersData(match.Players);
                    SetMatchDataTeam(match, specificMatchId, prevMatchNo);
                }

                SwitchView(ret);
            }

            CtrlMain.DisplayedMatch = match;
            GameTimer.Start();
        }

        return ret;
    }

    private async Task<Match> UpdateRequestedMatchAsync()
    {
        Match ret = null;

        if(!isDrawing) {
            isDrawing = true;
            updateToolStripMenuItem.Enabled = false;
            while(requestMatchView != currentMatchView && requestMatchView != 0) {
                displayStatus = DisplayStatus.RedrawingPrevMatch;
                labelMatchNo.Text = $"Loading {requestMatchView} match ago...";
                progressBar.Restart();
                var drawingMatchNo = requestMatchView;

                try {
                    var playerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(
                                                    drawingMatchNo, 1, CtrlSettings.ProfileId);
                    if(playerMatchHistory.Count != 0) {
                        var match = playerMatchHistory[0];
                        ret = await DrawMatchAsync(match, null, drawingMatchNo);
                        currentMatchView = drawingMatchNo;
                    } else {
                        // If the requested previous match is over the range,
                        // it shows the oldest available match.
                        requestMatchView--;
                    }
                } catch(Exception ex) {
                    // if API calling failed, call next request.
                    labelMatchNo.Text = "Load Error";
                    labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
                    requestMatchView--;
                }
            }

            isDrawing = false;
            updateToolStripMenuItem.Enabled = true;
            displayStatus = DisplayStatus.Shown;
        }

        return ret;
    }

    private async Task<Match> RedrawLastMatchAsync(int profileId)
    {
        Match ret;
        displayStatus = DisplayStatus.Redrawing;
        updateToolStripMenuItem.Enabled = false;
        labelMatchNo.Text = $"Loading last match...";
        requestMatchView = 0;

        try {
            var lastmatch = await AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, profileId.ToString());
            ret = lastmatch.LastMatch;
            var matchId = lastmatch.LastMatch.MatchId;

            if(labelGameId.Text != $"GameID : {matchId}") {
                var history = await AoE2net.GetPlayerMatchHistoryAsync(0, 1, profileId);

                if(history.Count != 0) {
                    if(history[0].MatchId == matchId) {
                        ret = history[0];
                    }
                }

                ret = await DrawMatchAsync(ret, null, 0);
            }
        } finally {
            displayStatus = DisplayStatus.Shown;
            updateToolStripMenuItem.Enabled = true;
        }

        return ret;
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
