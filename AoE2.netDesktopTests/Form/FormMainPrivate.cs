using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;
using System.Drawing;
using System;

namespace AoE2NetDesktop.Form.Tests
{
    public partial class FormMainTests
    {
        private class FormMainPrivate : FormMain
        {
            public TestHttpClient httpClient;
            public Button buttonSetId;
            public Button buttonUpdate;
            public Button buttonViewHistory;
            public CheckBox checkBoxAlwaysOnTop;
            public CheckBox checkBoxHideTitle;
            public Label labelAoE2NetStatus;
            public Label labelErrText;
            public Label labelSettingsName;
            public Label labelSettingsCountry;
            public TabControl tabControlMain;
            public TextBox textBoxSettingSteamId;
            public RadioButton radioButtonSteamID;
            public RadioButton radioButtonProfileID;
            public NumericUpDown upDownOpacity;
            public TextBox textBoxChromaKey;

            public Point mouseDownPoint;
            public string InvalidSteamIdString;

            public FormMainPrivate()
                : base(Language.en)
            {
                httpClient = new TestHttpClient();
                AoE2net.ComClient = httpClient;
                InvalidSteamIdString = Controler.GetField<string>("InvalidSteamIdString");
                buttonSetId = this.GetControl<Button>("buttonSetId");
                buttonUpdate = this.GetControl<Button>("buttonUpdate");
                buttonViewHistory = this.GetControl<Button>("buttonViewHistory");
                checkBoxAlwaysOnTop = this.GetControl<CheckBox>("checkBoxAlwaysOnTop");
                checkBoxHideTitle = this.GetControl<CheckBox>("checkBoxHideTitle");
                labelAoE2NetStatus = this.GetControl<Label>("labelAoE2NetStatus");
                labelErrText = this.GetControl<Label>("labelErrText");
                labelSettingsName = this.GetControl<Label>("labelSettingsName");
                labelSettingsCountry = this.GetControl<Label>("labelSettingsCountry");
                radioButtonSteamID = this.GetControl<RadioButton>("radioButtonSteamID");
                radioButtonProfileID = this.GetControl<RadioButton>("radioButtonProfileID");
                tabControlMain = this.GetControl<TabControl>("tabControlMain");
                textBoxSettingSteamId = this.GetControl<TextBox>("textBoxSettingSteamId");
                upDownOpacity = this.GetControl<NumericUpDown>("upDownOpacity");
                mouseDownPoint = this.GetField<Point>("mouseDownPoint");
                textBoxChromaKey = this.GetControl<TextBox>("textBoxChromaKey");

                TestUtilityExt.SetSettings(this, "AoE2NetDesktop", "SteamId", TestData.AvailableUserSteamId);
                TestUtilityExt.SetSettings(this, "AoE2NetDesktop", "ProfileId", TestData.AvailableUserProfileId);
                TestUtilityExt.SetSettings(this, "AoE2NetDesktop", "SelectedIdType", IdType.Profile);
            }

            public async void TabControlMainOnKeyDown(Keys keys)
            {
                this.Invoke("TabControlMain_KeyDown", tabControlMain, new KeyEventArgs(keys));
                await Awaiter.WaitAsync("ButtonUpdate_Click");
            }

            public void FormMainOnMouseDown(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseDown", this, e);
            }

            public void FormMainOnMouseMove(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseMove", this, e);
            }

            public void PictureBoxChromaKeyOnClick(EventArgs e)
            {
                this.Invoke("PictureBoxChromaKey_Click", this, e);
            }
        }
    }
}