namespace AoE2NetDesktop.Form.Tests;

using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.JsonFormat;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility;

using AoE2netDesktopTests.TestUtility;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public ToolStripMenuItem updateToolStripMenuItem;
        public ContextMenuStrip contextMenuStripMain;

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
            updateToolStripMenuItem = this.GetControl<ToolStripMenuItem>("updateToolStripMenuItem");
            contextMenuStripMain = this.GetControl<ContextMenuStrip>("contextMenuStripMain");

            TestUtilityExt.SetSettings("SteamId", TestData.AvailableUserSteamId);
            TestUtilityExt.SetSettings("ProfileId", TestData.AvailableUserProfileId);
            TestUtilityExt.SetSettings("SelectedIdType", IdType.Profile);
        }

        public FormSettings FormSettings
        {
            get => this.GetField<FormSettings>("formSettings");
            set
            {
                this.SetField("formSettings", value);
            }
        }

        public Point MouseDownPoint
        {
            get => this.GetField<Point>("mouseDownPoint");
            set
            {
                this.SetField("mouseDownPoint", value);
            }
        }

        [SuppressMessage("Usage", "VSTHRD100:Avoid async void methods", Justification = SuppressReason.GuiTest)]
        public async void FormMain_KeyDown(Keys keys)
        {
            this.Invoke("FormMain_KeyDown", this, new KeyEventArgs(keys));
            await Awaiter.WaitAsync("FormMain_KeyDown");
        }

        public void Controls_MouseDown(MouseEventArgs e)
        {
            this.Invoke("Controls_MouseDown", this, e);
        }

        public void Controls_MouseMove(MouseEventArgs e)
        {
            this.Invoke("Controls_MouseMove", this, e);
        }

        public void FormMain_MouseClick(MouseEventArgs e)
        {
            this.Invoke("FormMain_MouseClick", this, e);
        }

        public void OnChangeProperty(object sender, PropertyChangedEventArgs e)
        {
            this.Invoke("OnChangeProperty", sender, e);
        }

        public void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke("SettingsToolStripMenuItem_Click", sender, e);
        }

        public void LabelName_DoubleClick(object sender, EventArgs e)
        {
            this.Invoke("LabelName_DoubleClick", sender, e);
        }

        public void ShowMyHistoryHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke("ShowMyHistoryHToolStripMenuItem_Click", sender, e);
        }

        public void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invoke("ExitToolStripMenuItem_Click", sender, e);
        }

        public void PictureBoxMap_DoubleClick(object sender, EventArgs e)
        {
            this.Invoke("PictureBoxMap_DoubleClickAsync", sender, e);
        }

        public void PictureBoxMap1v1_DoubleClick(object sender, EventArgs e)
        {
            this.Invoke("PictureBoxMap1v1_DoubleClick", sender, e);
        }

        public async Task<Match> RedrawLastMatchAsync(int? profileId)
        {
            return await this.Invoke<Task<Match>>("RedrawLastMatchAsync", profileId).ConfigureAwait(false);
        }

        public void OpenSettings()
        {
            this.Invoke("OpenSettings");
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = SuppressReason.PrivateInvokeTest)]
        public void OnTimerAsync(object sender, EventArgs e)
        {
            this.Invoke("OnTimerAsync", sender, e);
        }
    }
}
