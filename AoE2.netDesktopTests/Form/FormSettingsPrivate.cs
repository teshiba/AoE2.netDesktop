namespace AoE2NetDesktop.Form.Tests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using AoE2NetDesktop.CtrlForm;
using AoE2NetDesktop.LibAoE2Net.Functions;
using AoE2NetDesktop.LibAoE2Net.Parameters;
using AoE2NetDesktop.Tests;
using AoE2NetDesktop.Utility;

using LibAoE2net;

public partial class FormSettingsTests
{
    private class FormSettingsPrivate : FormSettings
    {
        public TestHttpClient httpClient;
        public Button buttonSetId;
        public CheckBox checkBoxAlwaysOnTop;
        public CheckBox checkBoxHideTitle;
        public CheckBox checkBoxDrawQuality;
        public CheckBox checkBoxAutoReloadLastMatch;
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
            checkBoxAutoReloadLastMatch = this.GetControl<CheckBox>("checkBoxAutoReloadLastMatch");
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

        public new CtrlSettings Controler => base.Controler;

        ///////////////////////////////////////////////////////////////////////
        // private method
        ///////////////////////////////////////////////////////////////////////

        public void SetChromaKey(string htmlColor)
        {
            this.Invoke("SetChromaKey", htmlColor);
        }

        public void OnErrorHandler(Exception ex)
        {
            this.Invoke("OnErrorHandler", ex);
        }

        [SuppressMessage("Style", "VSTHRD200:Use \"Async\" suffix for async methods", Justification = SuppressReason.PrivateInvokeTest)]
        public void ReloadProfileAsync(IdType idtype, string idText)
        {
            this.Invoke("ReloadProfileAsync", idtype, idText);
        }

        ///////////////////////////////////////////////////////////////////////
        // Event handlers
        ///////////////////////////////////////////////////////////////////////
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
