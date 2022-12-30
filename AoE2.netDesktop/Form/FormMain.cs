namespace AoE2NetDesktop.Form;

using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility.Forms;
using AoE2NetDesktop.Utility.Timer;

using static AoE2NetDesktop.CtrlForm.LabelType;

/// <summary>
/// App main form.
/// </summary>
public partial class FormMain : ControllableForm
{
    private readonly TimerProgressBar progressBar;
    private readonly Language language;

    private Point mouseDownPoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormMain"/> class.
    /// </summary>
    /// <param name="language">Display language.</param>
    public FormMain(Language language)
        : base(new CtrlMain())
    {
        InitializeComponent();
        Init1v1CtrlList();
        InitEventHandler();
        InitOnChangePropertyHandler();
        InitPlayersCtrlList();
        ClearLastMatch();
        InitShortcut();

        // formMain hold the app settings.
        CtrlSettings = new CtrlSettings();
        LastMatchLoader = new LastMatchLoader(OnTimerLastMatchLoader, CtrlMain.IntervalSec);
        GameTimer = new GameTimer(OnTimerGame);

        progressBar = new TimerProgressBar(progressBarLoading);
        displayStatus = DisplayStatus.Uninitialized;
        SetOptionParams();

        this.language = language;
        Icon = Properties.Resources.aoe2netDesktopAppIcon;
    }

    ///////////////////////////////////////////////////////////////////////
    // Async event handlers
    ///////////////////////////////////////////////////////////////////////
#pragma warning disable VSTHRD100 // Avoid async void methods
#pragma warning disable VSTHRD200 // Use "Async" suffix for async methods

    private async void FormMain_Shown(object sender, EventArgs e)
    {
        await InitAsync();
        Awaiter.Complete();
    }

    private void FormMain_Load(object sender, EventArgs e)
    {
        RestoreWindowStatus();
        ResizePanels();
        SetChromaKey(Settings.Default.ChromaKey);
        Awaiter.Complete();
    }

    private async void UpdateToolStripMenuItem_ClickAsync(object sender, EventArgs e)
    {
        await RedrawLastMatch();
        Awaiter.Complete();
    }

    private void PictureBoxMap_DoubleClickAsync(object sender, EventArgs e)
        => updateToolStripMenuItem.PerformClick();

    private void PictureBoxMap1v1_DoubleClick(object sender, EventArgs e)
        => updateToolStripMenuItem.PerformClick();

    private async Task RedrawLastMatch()
    {
        switch(displayStatus) {
        case DisplayStatus.Shown:

            if(!CtrlMain.IsReloadingByTimer) {
                progressBar.Start();
                ClearLastMatch();
            } else {
                CtrlMain.IsReloadingByTimer = false;
            }

            try {
                await RedrawLastMatchAsync(CtrlSettings.ProfileId);
            } catch(Exception ex) {
                labelMatchNo.Text = "Load Error";
                labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";
            }

            progressBar.Stop();
            break;

        case DisplayStatus.Uninitialized:
            await InitAsync();
            break;

        case DisplayStatus.Clearing:
        case DisplayStatus.Redrawing:
        case DisplayStatus.Closing:
        case DisplayStatus.Cleared:
        case DisplayStatus.RedrawingPrevMatch:
        default:
            labelErrText.Text = $"Invalid displayStatus: {displayStatus}";
            break;
        }
    }

    private async Task InitAsync()
    {
        progressBar.Start();
        labelMatchNo.Text = "Initializing...";

        try {
            _ = await CtrlMain.InitAsync(language);
            formSettings = new FormSettings(CtrlSettings);

            // if the app is opened first, need to set user profile.
            if(!await CtrlSettings.ReadProfileAsync()) {
                OpenSettings();
            }

            await RedrawLastMatchAsync(CtrlSettings.ProfileId);
        } catch(Exception ex) {
            labelMatchNo.Text = "Load Error";
            labelErrText.Text = $"{ex.Message} : {ex.StackTrace}";

            if(displayStatus == DisplayStatus.Uninitialized) {
                labelMatchNo.Text = "Server Error";
            } else {
                displayStatus = DisplayStatus.Shown;
            }
        }

        progressBar.Stop();
    }

    private async void FormMain_KeyDownAsync(object sender, KeyEventArgs e)
    {
        await GetShortcutFunction(e.KeyCode, e.Shift, e.Alt)();
        Awaiter.Complete();
    }

#pragma warning restore VSTHRD200 // Use "Async" suffix for async methods
#pragma warning restore VSTHRD100 // Avoid async void methods

    ///////////////////////////////////////////////////////////////////////
    // FormMain Event handlers
    ///////////////////////////////////////////////////////////////////////

    private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
    {
        lock(GameTimer) {
            displayStatus = DisplayStatus.Closing;
        }

        progressBar.Stop();
        GameTimer.Stop();
        GameTimer.Dispose();
        CtrlSettings.FormMyHistory?.Close();
        SaveWindowPosition();
        Settings.Default.Save();
        Settings.Default.PropertyChanged -= Default_PropertyChanged;
    }

    private void FormMain_Resize(object sender, EventArgs e)
        => ResizePanels();

    private void FormMain_MouseClick(object sender, MouseEventArgs e)
    {
        if(e.Button == MouseButtons.Right) {
            contextMenuStripMain.Show();
        }
    }

    private void FormMain_Activated(object sender, EventArgs e)
    {
        // if this form is active, get game status from aoe2.net and update last match info.
        if(Settings.Default.IsAutoReloadLastMatch
        && CtrlMain.DisplayedMatch?.Finished == null
        && displayStatus == DisplayStatus.Shown
        && currentMatchView == 0) {
            CtrlMain.IsReloadingByTimer = true;
            updateToolStripMenuItem.PerformClick();
        }
    }

    ///////////////////////////////////////////////////////////////////////
    // Mouse Event handlers
    ///////////////////////////////////////////////////////////////////////

    private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        => OpenSettings();

    private void ShowMyHistoryHToolStripMenuItem_Click(object sender, EventArgs e)
        => CtrlSettings.ShowMyHistory();

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        => Close();

    private void Controls_MouseDown(object sender, MouseEventArgs e)
    {
        if((e.Button & MouseButtons.Left) == MouseButtons.Left) {
            mouseDownPoint = new Point(e.X, e.Y);
        }
    }

    private void Controls_MouseMove(object sender, MouseEventArgs e)
    {
        if((e.Button & MouseButtons.Left) == MouseButtons.Left) {
            Left += e.X - mouseDownPoint.X;
            Top += e.Y - mouseDownPoint.Y;
        }
    }

    private void LabelName_DoubleClick(object sender, EventArgs e)
    {
        var labelName = (Label)sender;
        var player = (Player)labelName.Tag;

        if(player != null) {
            var formHistory = CtrlHistory.GenerateFormHistory(player.Name, player.ProfilId);
            if(formHistory != null) {
                formHistory.Show();
            } else {
                labelErrText.Text = $"invalid player Name:{player.Name} ProfilId:{player.ProfilId}";
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////
    // Paint Event handlers
    ///////////////////////////////////////////////////////////////////////
    private void LabelRate1v1P2_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreValue1v1]);
        Awaiter.Complete();
    }

    private void LabelWins1v1P2_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreValue1v1]);
        Awaiter.Complete();
    }

    private void LabelLoses1v1P2_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreValue1v1]);
        Awaiter.Complete();
    }

    private void LabelLoses1v1P1_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreValue1v1]);
        Awaiter.Complete();
    }

    private void LabelWins1v1P1_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreValue1v1]);
        Awaiter.Complete();
    }

    private void LabelRate1v1P1_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreValue1v1]);
        Awaiter.Complete();
    }

    private void LabelRate1v1_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreLabel1v1]);
        Awaiter.Complete();
    }

    private void LabelWins1v1_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreLabel1v1]);
        Awaiter.Complete();
    }

    private void LabelLoses1v1_Paint(object sender, PaintEventArgs e)
    {
        ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreLabel1v1]);
        Awaiter.Complete();
    }

    private void LabelName_Paint(object sender, PaintEventArgs e)
    {
        var labelName = (Label)sender;
        var player = (Player)labelName.Tag;
        labelName.DrawString(e, CtrlMain.GetPlayerBorderedStyle(player, CtrlSettings.ProfileId));
    }

    private void LabelName1v1P1_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelName1v1P2_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP1_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP2_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP3_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP4_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP5_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP6_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP7_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelNameP8_Paint(object sender, PaintEventArgs e)
    {
        LabelName_Paint(sender, e);
        Awaiter.Complete();
    }

    private void LabelRate_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[RateValueTeam]);

    private void LabelCiv_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[CivNameTeam]);

    private void LabelAveRate_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ScoreLabel1v1]);

    private void LabelColor_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ColorNoTeam]);

    private void LabelMap_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[MapNameTeam]);

    private void LabelGameId_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[GameId]);

    private void LabelServer_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ServerName]);

    private void LabelMatchResult_Paint(object sender, PaintEventArgs e)
    {
        var label = (Label)sender;
        var style = CtrlMain.GetBorderedStyle((MatchResult)label.Tag);
        label.DrawString(e, style);
    }

    private void LabelStartTimeTeam_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[StartTime]);

    private void LabelElapsedTimeTeam_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ElapsedTime]);

    private void LabelMatchNo_Paint(object sender, PaintEventArgs e)
                => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[MatchNo]);

    private void LabelStartTime1v1_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[StartTime]);

    private void LabelElapsedTime1v1_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[ElapsedTime]);

    private void LabelMatchNo1v1_Paint(object sender, PaintEventArgs e)
        => ((Label)sender).DrawString(e, CtrlMain.BorderStyles[MatchNo]);
}
