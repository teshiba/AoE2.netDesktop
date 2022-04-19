using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;
using System.Drawing;
using System.ComponentModel;

namespace AoE2NetDesktop.Form.Tests
{
    public partial class FormMainTests
    {
        private class FormMainPrivate : FormMain
        {
            public TestHttpClient httpClient;
            public Label labelErrText;
            public ToolStripMenuItem updateToolStripMenuItem;
            public Point mouseDownPoint;

            public FormMainPrivate()
                : base(Language.en)
            {
                httpClient = new TestHttpClient();
                AoE2net.ComClient = httpClient;
                labelErrText = this.GetControl<Label>("labelErrText");
                mouseDownPoint = this.GetField<Point>("mouseDownPoint");
                updateToolStripMenuItem = this.GetControl<ToolStripMenuItem>("updateToolStripMenuItem");

                TestUtilityExt.SetSettings(this, "SteamId", TestData.AvailableUserSteamId);
                TestUtilityExt.SetSettings(this, "ProfileId", TestData.AvailableUserProfileId);
                TestUtilityExt.SetSettings(this, "SelectedIdType", IdType.Profile);
            }

            public async void FormMain_KeyDown(Keys keys)
            {
                this.Invoke("FormMain_KeyDown", this, new KeyEventArgs(keys));
                await Awaiter.WaitAsync("FormMain_KeyDown");
            }

            public void FormMain_MouseDown(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseDown", this, e);
            }

            public void FormMain_MouseMove(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseMove", this, e);
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
            
        }
    }
}
