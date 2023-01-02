namespace AoE2NetDesktop.Form.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Utility;

using AoE2NetDesktopTests.TestData;
using AoE2NetDesktopTests.TestUtility;

public partial class FormMainTests
{
    private class FormMainPrivate : FormMain
    {
        public TestHttpClient httpClient;
        public Label labelErrText;
        public Label labelGameId;
        public Label labelAoE2DEActive;
        public Label label1v1ColorP1;
        public Label label1v1ColorP2;
        public Label labelName1v1P1;
        public Label labelName1v1P2;
        public Label labelMatchNo;
        public Label labelMatchNo1v1;
        public ToolStripMenuItem updateToolStripMenuItem;
        public ContextMenuStrip contextMenuStripMain;
        internal PictureBox pictureBoxMap;

        public FormMainPrivate()
            : base(Language.en)
        {
            httpClient = (TestHttpClient)AoE2net.ComClient;
            labelErrText = this.GetControl<Label>("labelErrText");
            labelGameId = this.GetControl<Label>("labelGameId");
            labelAoE2DEActive = this.GetControl<Label>("labelAoE2DEActive");
            label1v1ColorP1 = this.GetControl<Label>("label1v1ColorP1");
            label1v1ColorP2 = this.GetControl<Label>("label1v1ColorP2");
            labelName1v1P1 = this.GetControl<Label>("labelName1v1P1");
            labelName1v1P2 = this.GetControl<Label>("labelName1v1P2");
            labelMatchNo = this.GetControl<Label>("labelMatchNo");
            labelMatchNo1v1 = this.GetControl<Label>("labelMatchNo1v1");
            pictureBoxMap = this.GetControl<PictureBox>("pictureBoxMap");
            updateToolStripMenuItem = this.GetControl<ToolStripMenuItem>("updateToolStripMenuItem");
            contextMenuStripMain = this.GetControl<ContextMenuStrip>("contextMenuStripMain");

            TestUtilityExt.SetSettings("SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings("ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings("SelectedIdType", IdType.Profile);
        }

        public int RequestMatchView
        {
            get => this.GetField<int>("requestMatchView");
            set => this.SetField("requestMatchView", value);
        }

        public int CurrentMatchView
        {
            get => this.GetField<int>("currentMatchView");
            set => this.SetField("currentMatchView", value);
        }

        public TimerProgressBar ProgressBar
        {
            get => this.GetField<TimerProgressBar>("progressBar");
            set => this.SetField("progressBar", value);
        }

        public DisplayStatus DisplayStatus
        {
            get => this.GetField<DisplayStatus>("displayStatus");
            set => this.SetField("displayStatus", value);
        }

        public FormSettings FormSettings
        {
            get => this.GetField<FormSettings>("formSettings");
            set => this.SetField("formSettings", value);
        }

        public Point MouseDownPoint
        {
            get => this.GetField<Point>("mouseDownPoint");
            set => this.SetField("mouseDownPoint", value);
        }

        public void FormMain_Activated(EventArgs e)
            => this.Invoke("FormMain_Activated", this, e);

        public async Task FormMain_KeyDownAsync(Keys keys)
        {
            this.Invoke("FormMain_KeyDownAsync", this, new KeyEventArgs(keys));
            await Awaiter.WaitAsync("FormMain_KeyDownAsync");
        }

        public void Controls_MouseDown(MouseEventArgs e)
            => this.Invoke("Controls_MouseDown", this, e);

        public void Controls_MouseMove(MouseEventArgs e)
            => this.Invoke("Controls_MouseMove", this, e);

        public void FormMain_MouseClick(MouseEventArgs e)
            => this.Invoke("FormMain_MouseClick", this, e);

        public void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
            => this.Invoke("SettingsToolStripMenuItem_Click", sender, e);

        public void LabelName_DoubleClick(Label sender, EventArgs e)
            => this.Invoke("LabelName_DoubleClick", sender, e);

        public void ShowMyHistoryHToolStripMenuItem_Click(object sender, EventArgs e)
            => this.Invoke("ShowMyHistoryHToolStripMenuItem_Click", sender, e);

        public void ExitToolStripMenuItem_Click(object sender, EventArgs e)
            => this.Invoke("ExitToolStripMenuItem_Click", sender, e);

        public void LabelGameId_Click(Label sender, EventArgs e)
            => this.Invoke("LabelGameId_Click", sender, e);

        public void LabelGameId1v1_Click(Label sender, EventArgs e)
            => this.Invoke("LabelGameId1v1_Click", sender, e);

        public void PictureBoxMap_DoubleClick(PictureBox sender, EventArgs e)
            => this.Invoke("PictureBoxMap_DoubleClickAsync", sender, e);

        public void PictureBoxMap1v1_DoubleClick(PictureBox sender, EventArgs e)
            => this.Invoke("PictureBoxMap1v1_DoubleClick", sender, e);

        public async Task<Match> RedrawLastMatchAsync(int? profileId)
            => await this.Invoke<Task<Match>>("RedrawLastMatchAsync", profileId).ConfigureAwait(false);

        public void OpenSettings()
            => this.Invoke("OpenSettings");

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = SuppressReason.PrivateInvokeTest)]
        public void OnTimerAsync(object sender, EventArgs e)
            => this.Invoke("OnTimerAsync", sender, e);
    }
}
