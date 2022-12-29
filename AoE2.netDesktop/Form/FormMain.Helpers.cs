namespace AoE2NetDesktop.Form;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.AoE2DE;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.SysApi;
using AoE2NetDesktop.Utility.Timer;

/// <summary>
/// App main form.
/// </summary>
public partial class FormMain : ControllableForm
{
    private const string LoadingText = $"Loading last match...";
    private const string GameIdLabel = "GameID : ";
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

    private void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        onChangePropertyHandler.TryGetValue(e.PropertyName, out Action<string> action);
        action?.Invoke(e.PropertyName);
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
        => ChangePropertyIsTransparency(propertyName);

    private void OnChangePropertyOpacity(string propertyName)
    {
        var opacityPercent = (decimal)Settings.Default[propertyName];
        Opacity = (double)opacityPercent * 0.01;
    }

    private void OnChangePropertyIsAlwaysOnTop(string propertyName)
        => TopMost = (bool)Settings.Default[propertyName];

    private void OnChangePropertyIsAutoReloadLastMatch(string propertyName)
        => ChangePropertyIsAutoReloadLastMatch(propertyName);

    private void OnChangePropertyVisibleGameTime(string propertyName)
        => ChangePropertyVisibleGameTime(propertyName);

    private void OnChangePropertyIsHideTitle(string propertyName)
        => ChangePropertyIsHideTitle(propertyName);

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
        labelGameId.Text = $"{GameIdLabel}--------";
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
        labelGameId1v1.Text = $"{GameIdLabel}--------";
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

        foreach(var item in control1V1s) {
            item.LabelColor.Text = string.Empty;
            item.LabelCiv.Text = string.Empty;
            item.LabelName.Text = string.Empty;
            item.LabelName.Tag = null;
            item.LabelRate.Text = string.Empty;
            item.PictureBoxCiv.ImageLocation = null;
            item.PictureBoxUnit.Image = null;
            item.LabelWins.Text = string.Empty;
            item.LabelLoses.Text = string.Empty;
        }
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

    private void SetMatchDataTeam(Match match)
    {
        var aveTeam1 = match.Players.GetAverageRate(TeamType.OddColorNo);
        var aveTeam2 = match.Players.GetAverageRate(TeamType.EvenColorNo);
        labelAveRate1.Text = $"Team1 Ave. Rate:{aveTeam1}";
        labelAveRate2.Text = $"Team2 Ave. Rate:{aveTeam2}";

        pictureBoxMap.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap.Text = $"Map: {match.GetMapName()}";
        labelServer.Text = $"Server : {match.Server}";
        labelStartTimeTeam.Text = CtrlMain.GetOpenedTimeString(match);

        labelMatchResultTeam1.Text = GetMatchResult(match, TeamType.OddColorNo).ToString();
        labelMatchResultTeam1.Tag = GetMatchResult(match, TeamType.OddColorNo);
        labelMatchResultTeam2.Text = GetMatchResult(match, TeamType.EvenColorNo).ToString();
        labelMatchResultTeam2.Tag = GetMatchResult(match, TeamType.EvenColorNo);
        labelElapsedTimeTeam.Text = GetElapsedTime(match);
    }

    private void SetMatchData1v1(Match match)
    {
        pictureBoxMap1v1.Image = CtrlMain.LoadMapIcon(match.MapType);
        labelMap1v1.Text = match.GetMapName();
        labelServer1v1.Text = $"Server : {match.Server}";

        labelStartTime1v1.Text = CtrlMain.GetOpenedTimeString(match);

        labelMatchResult1v1p1.Text = GetMatchResult(match, TeamType.OddColorNo).ToString();
        labelMatchResult1v1p1.Tag = GetMatchResult(match, TeamType.OddColorNo);
        labelMatchResult1v1p2.Text = GetMatchResult(match, TeamType.EvenColorNo).ToString();
        labelMatchResult1v1p2.Tag = GetMatchResult(match, TeamType.EvenColorNo);
        labelElapsedTime1v1.Text = GetElapsedTime(match);
    }

    private string GetElapsedTime(Match match)
    {
        string ret;

        if(match.Finished is null && requestMatchView != 0) {
            ret = DateTimeExt.InvalidTime;
        } else {
            ret = CtrlMain.GetElapsedTimeString(match);
        }

        return ret;
    }

    private MatchResult GetMatchResult(Match match, TeamType teamType)
    {
        MatchResult ret;

        if(match.Finished is null && requestMatchView != 0) {
            ret = MatchResult.Finished;
        } else {
            ret = match.GetMatchResult(teamType);
        }

        return ret;
    }

    private async Task<Match> SetLeaderboardData1v1Async(Match match)
    {
        foreach(var item in control1V1s) {
            var leaderboard = await AoE2net.GetLeaderboardAsync(match.LeaderboardId, 0, 1, item.Player.ProfilId);

            if(leaderboard.Leaderboards.Count != 0) {
                item.LabelWins.Text = CtrlMain.GetWinsString(leaderboard.Leaderboards[0]);
                item.LabelLoses.Text = CtrlMain.GetLossesString(leaderboard.Leaderboards[0]);
            }
        }

        return match;
    }

    private async Task<Match> SetPlayersData1v1Async(Match match)
    {
        if(match.Players[0].IsOddColor()) {
            control1V1s[0].Player = match.Players[0];
            control1V1s[1].Player = match.Players[1];
        } else {
            control1V1s[0].Player = match.Players[1];
            control1V1s[1].Player = match.Players[0];
        }

        var ret = await SetLeaderboardData1v1Async(match);

        foreach(var item in control1V1s) {
            item.LabelColor.Text = item.Player.GetColorString();
            item.LabelColor.BackColor = item.Player.GetColor();
            item.LabelName.Text = CtrlMain.GetPlayerNameString(item.Player.Name);
            item.LabelName.Font = CtrlMain.GetFontStyle(item.Player, item.LabelName.Font);
            item.LabelName.Tag = item.Player;
            item.PictureBoxCiv.ImageLocation = item.Player.GetCivImageLocation();
            item.PictureBoxUnit.Image = UnitImages.Load(item.Player.GetCivEnName(), item.Player.GetColor());
            item.LabelRate.Text = CtrlMain.GetRateString(item.Player.Rating);
            item.LabelCiv.Text = item.Player.GetCivName();
            item.LabelTeamResult.Text = $"";
        }

        return ret;
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

        Awaiter.Complete(enableDebugPrint: false);
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

    private async Task<Match> DrawMatchAsync(Match match, int? prevMatchNo)
    {
        var ret = match;

        if(prevMatchNo is not null) {
            requestMatchView = (int)prevMatchNo;
        }

        var gameIdText = $"{GameIdLabel}{match.MatchId}";

        if(labelGameId.Text != gameIdText) {
            labelGameId1v1.Text = gameIdText;
            labelGameId.Text = gameIdText;
            labelMatchNo1v1.Text = CtrlMain.GetMatchNoString(prevMatchNo);
            labelMatchNo.Text = CtrlMain.GetMatchNoString(prevMatchNo);

            if(match.NumPlayers == 2) {
                await SetPlayersData1v1Async(match);
                SetMatchData1v1(match);
            } else {
                SetPlayersData(match.Players);
                SetMatchDataTeam(match);
            }

            SwitchView(ret);

            CtrlMain.DisplayedMatch = match;
            if(requestMatchView == 0) {
                GameTimer.Start();
            } else {
                GameTimer.Stop();
            }
        }

        return ret;
    }

    private async Task<Match> UpdateRequestedMatchAsync()
    {
        Match ret = null;

        if(!isDrawing) {
            isDrawing = true;
            updateToolStripMenuItem.Enabled = false;
            while(requestMatchView != currentMatchView) {
                if(requestMatchView == 0) {
                    labelMatchNo.Text = LoadingText;
                    labelMatchNo1v1.Text = LoadingText;
                } else {
                    labelMatchNo.Text = $"Loading {requestMatchView} match ago...";
                    labelMatchNo1v1.Text = $"Loading {requestMatchView} match ago...";
                }

                displayStatus = DisplayStatus.RedrawingPrevMatch;
                progressBar.Restart();

                var drawingMatchNo = requestMatchView;

                try {
                    var playerMatchHistory = await AoE2net.GetPlayerMatchHistoryAsync(
                                                    drawingMatchNo, 1, CtrlSettings.ProfileId);
                    if(playerMatchHistory.Count != 0) {
                        var match = playerMatchHistory[0];
                        ret = await DrawMatchAsync(match, drawingMatchNo);
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
        labelMatchNo.Text = LoadingText;
        labelMatchNo1v1.Text = LoadingText;
        requestMatchView = 0;

        try {
            var lastmatch = await AoE2netHelpers.GetPlayerLastMatchAsync(IdType.Profile, profileId.ToString());

            if(labelGameId.Text != $"{GameIdLabel}{lastmatch.LastMatch.MatchId}") {
                ret = await DrawMatchAsync(lastmatch.LastMatch, 0);
            } else {
                ret = lastmatch.LastMatch;
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
