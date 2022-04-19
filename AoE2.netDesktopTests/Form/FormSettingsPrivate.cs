using LibAoE2net;
using System.Windows.Forms;
using AoE2NetDesktop.Tests;
using System;

namespace AoE2NetDesktop.Form.Tests
{
    public partial class FormSettingsTests
    {
        private class FormSettingsPrivate : FormSettings
        {
            public TestHttpClient httpClient;
            public Button buttonSetId;
            public CheckBox checkBoxAlwaysOnTop;
            public CheckBox checkBoxHideTitle;
            public CheckBox checkBoxDrawQuality;
            public Label labelAoE2NetStatus;
            public Label labelErrText;
            public Label labelSettingsName;
            public Label labelSettingsCountry;
            public TextBox textBoxSettingSteamId;
            public RadioButton radioButtonSteamID;
            public RadioButton radioButtonProfileID;
            public NumericUpDown upDownOpacity;
            public TextBox textBoxChromaKey;
            public PictureBox pictureBoxChromaKey;
            public string InvalidSteamIdString;

            public new CtrlSettings Controler => base.Controler;

            public FormSettingsPrivate()
                : base(new CtrlSettings())
            {
                httpClient = new TestHttpClient();
                AoE2net.ComClient = httpClient;
                InvalidSteamIdString = Controler.GetField<string>("InvalidSteamIdString");
                buttonSetId = this.GetControl<Button>("buttonSetId");
                checkBoxAlwaysOnTop = this.GetControl<CheckBox>("checkBoxAlwaysOnTop");
                checkBoxHideTitle = this.GetControl<CheckBox>("checkBoxHideTitle");
                checkBoxDrawQuality = this.GetControl<CheckBox>("checkBoxDrawQuality");
                labelAoE2NetStatus = this.GetControl<Label>("labelAoE2NetStatus");
                labelErrText = this.GetControl<Label>("labelErrText");
                labelSettingsName = this.GetControl<Label>("labelSettingsName");
                labelSettingsCountry = this.GetControl<Label>("labelSettingsCountry");
                radioButtonSteamID = this.GetControl<RadioButton>("radioButtonSteamID");
                radioButtonProfileID = this.GetControl<RadioButton>("radioButtonProfileID");
                textBoxSettingSteamId = this.GetControl<TextBox>("textBoxSettingSteamId");
                upDownOpacity = this.GetControl<NumericUpDown>("upDownOpacity");
                textBoxChromaKey = this.GetControl<TextBox>("textBoxChromaKey");
                pictureBoxChromaKey = this.GetControl<PictureBox>("pictureBoxChromaKey");

                TestUtilityExt.SetSettings(this, "SteamId", TestData.AvailableUserSteamId);
                TestUtilityExt.SetSettings(this, "ProfileId", TestData.AvailableUserProfileId);
                TestUtilityExt.SetSettings(this, "SelectedIdType", IdType.Profile);
            }

            ///////////////////////////////////////////////////////////////////////
            // private method
            ///////////////////////////////////////////////////////////////////////

            public void SetChromaKey(string htmlColor)
            {
                this.Invoke("SetChromaKey", htmlColor);
            }

            public void ReloadProfileAsync(IdType idtype, string idText)
            {
                this.Invoke("ReloadProfileAsync", idtype, idText);
            }

            public void OnErrorHandler(Exception ex)
            {
                this.Invoke("OnErrorHandler", ex);
            }

            ///////////////////////////////////////////////////////////////////////
            // Event handlers
            ///////////////////////////////////////////////////////////////////////
            public void FormMain_MouseDown(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseDown", this, e);
            }

            public void FormMain_MouseMove(MouseEventArgs e)
            {
                this.Invoke("FormMain_MouseMove", this, e);
            }

            public void PictureBoxChromaKey_Click(EventArgs e)
            {
                this.Invoke("PictureBoxChromaKey_Click", pictureBoxChromaKey, e);
            }

            public void TextBoxChromaKey_Leave(EventArgs e)
            {
                this.Invoke("TextBoxChromaKey_Leave", textBoxChromaKey, e);
            }
        }
    }
}