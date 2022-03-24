using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;
using System.Drawing;

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

            public async void FormMainOnKeyDown(Keys keys)
            {
                this.Invoke("FormMain_KeyDown", this, new KeyEventArgs(keys));
                await Awaiter.WaitAsync("FormMain_KeyDown");
            }

            public void FormMainOnMouseDown(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseDown", this, e);
            }

            public void FormMainOnMouseMove(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseMove", this, e);
            }
        }
    }
}
