namespace AoE2NetDesktop.Form
{
    using AoE2NetDesktop.Utility.Forms;

    using System.Windows.Forms;

    /// <summary>
    /// App Settings form.
    /// </summary>
    partial class FormSettings : ControllableForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxChromaKey = new System.Windows.Forms.TextBox();
            this.pictureBoxChromaKey = new System.Windows.Forms.PictureBox();
            this.labelChromaKey = new System.Windows.Forms.Label();
            this.labelOpacity = new System.Windows.Forms.Label();
            this.upDownOpacity = new System.Windows.Forms.NumericUpDown();
            this.groupBoxPlayer = new System.Windows.Forms.GroupBox();
            this.buttonSetId = new System.Windows.Forms.Button();
            this.radioButtonSteamID = new System.Windows.Forms.RadioButton();
            this.radioButtonProfileID = new System.Windows.Forms.RadioButton();
            this.textBoxSettingSteamId = new System.Windows.Forms.TextBox();
            this.textBoxSettingProfileId = new System.Windows.Forms.TextBox();
            this.labelSettingsCountry = new System.Windows.Forms.Label();
            this.labelSettingsName = new System.Windows.Forms.Label();
            this.labelAoE2NetStatus = new System.Windows.Forms.Label();
            this.labelAoE2NetStatusLabel = new System.Windows.Forms.Label();
            this.checkBoxHideTitle = new System.Windows.Forms.CheckBox();
            this.checkBoxAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.labelErrText = new System.Windows.Forms.Label();
            this.checkBoxTransparencyWindow = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawQuality = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoReloadLastMatch = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChromaKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownOpacity)).BeginInit();
            this.groupBoxPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxChromaKey
            // 
            this.textBoxChromaKey.Location = new System.Drawing.Point(233, 34);
            this.textBoxChromaKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxChromaKey.Name = "textBoxChromaKey";
            this.textBoxChromaKey.Size = new System.Drawing.Size(66, 23);
            this.textBoxChromaKey.TabIndex = 24;
            this.textBoxChromaKey.Text = "#708090";
            this.textBoxChromaKey.Leave += new System.EventHandler(this.TextBoxChromaKey_Leave);
            // 
            // pictureBoxChromaKey
            // 
            this.pictureBoxChromaKey.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxChromaKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxChromaKey.ErrorImage = null;
            this.pictureBoxChromaKey.InitialImage = null;
            this.pictureBoxChromaKey.Location = new System.Drawing.Point(308, 34);
            this.pictureBoxChromaKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxChromaKey.Name = "pictureBoxChromaKey";
            this.pictureBoxChromaKey.Size = new System.Drawing.Size(33, 23);
            this.pictureBoxChromaKey.TabIndex = 23;
            this.pictureBoxChromaKey.TabStop = false;
            this.pictureBoxChromaKey.Click += new System.EventHandler(this.PictureBoxChromaKey_Click);
            // 
            // labelChromaKey
            // 
            this.labelChromaKey.AutoSize = true;
            this.labelChromaKey.Location = new System.Drawing.Point(152, 36);
            this.labelChromaKey.Name = "labelChromaKey";
            this.labelChromaKey.Size = new System.Drawing.Size(70, 15);
            this.labelChromaKey.TabIndex = 21;
            this.labelChromaKey.Text = "Chroma Key";
            // 
            // labelOpacity
            // 
            this.labelOpacity.AutoSize = true;
            this.labelOpacity.Location = new System.Drawing.Point(151, 11);
            this.labelOpacity.Name = "labelOpacity";
            this.labelOpacity.Size = new System.Drawing.Size(48, 15);
            this.labelOpacity.TabIndex = 22;
            this.labelOpacity.Text = "Opacity";
            // 
            // upDownOpacity
            // 
            this.upDownOpacity.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownOpacity.Location = new System.Drawing.Point(233, 8);
            this.upDownOpacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownOpacity.Name = "upDownOpacity";
            this.upDownOpacity.Size = new System.Drawing.Size(66, 23);
            this.upDownOpacity.TabIndex = 20;
            this.upDownOpacity.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.upDownOpacity.ValueChanged += new System.EventHandler(this.UpDownOpacity_ValueChanged);
            // 
            // groupBoxPlayer
            // 
            this.groupBoxPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxPlayer.Controls.Add(this.buttonSetId);
            this.groupBoxPlayer.Controls.Add(this.radioButtonSteamID);
            this.groupBoxPlayer.Controls.Add(this.radioButtonProfileID);
            this.groupBoxPlayer.Controls.Add(this.textBoxSettingSteamId);
            this.groupBoxPlayer.Controls.Add(this.textBoxSettingProfileId);
            this.groupBoxPlayer.Controls.Add(this.labelSettingsCountry);
            this.groupBoxPlayer.Controls.Add(this.labelSettingsName);
            this.groupBoxPlayer.Location = new System.Drawing.Point(10, 78);
            this.groupBoxPlayer.Name = "groupBoxPlayer";
            this.groupBoxPlayer.Size = new System.Drawing.Size(513, 136);
            this.groupBoxPlayer.TabIndex = 16;
            this.groupBoxPlayer.TabStop = false;
            this.groupBoxPlayer.Text = "Player";
            // 
            // buttonSetId
            // 
            this.buttonSetId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetId.Location = new System.Drawing.Point(430, 21);
            this.buttonSetId.Name = "buttonSetId";
            this.buttonSetId.Size = new System.Drawing.Size(78, 48);
            this.buttonSetId.TabIndex = 7;
            this.buttonSetId.Text = "Set ID";
            this.buttonSetId.UseVisualStyleBackColor = true;
            this.buttonSetId.Click += new System.EventHandler(this.ButtonSetId_ClickAsync);
            // 
            // radioButtonSteamID
            // 
            this.radioButtonSteamID.AutoSize = true;
            this.radioButtonSteamID.Checked = true;
            this.radioButtonSteamID.Location = new System.Drawing.Point(6, 22);
            this.radioButtonSteamID.Name = "radioButtonSteamID";
            this.radioButtonSteamID.Size = new System.Drawing.Size(73, 19);
            this.radioButtonSteamID.TabIndex = 4;
            this.radioButtonSteamID.TabStop = true;
            this.radioButtonSteamID.Text = "Steam-ID";
            this.radioButtonSteamID.UseVisualStyleBackColor = true;
            this.radioButtonSteamID.CheckedChanged += new System.EventHandler(this.RadioButtonSteamID_CheckedChanged);
            // 
            // radioButtonProfileID
            // 
            this.radioButtonProfileID.AutoSize = true;
            this.radioButtonProfileID.Location = new System.Drawing.Point(6, 48);
            this.radioButtonProfileID.Name = "radioButtonProfileID";
            this.radioButtonProfileID.Size = new System.Drawing.Size(75, 19);
            this.radioButtonProfileID.TabIndex = 5;
            this.radioButtonProfileID.Text = "Profile-ID";
            this.radioButtonProfileID.UseVisualStyleBackColor = true;
            this.radioButtonProfileID.CheckedChanged += new System.EventHandler(this.RadioButtonProfileID_CheckedChanged);
            // 
            // textBoxSettingSteamId
            // 
            this.textBoxSettingSteamId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSettingSteamId.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxSettingSteamId.Location = new System.Drawing.Point(93, 21);
            this.textBoxSettingSteamId.Name = "textBoxSettingSteamId";
            this.textBoxSettingSteamId.Size = new System.Drawing.Size(333, 23);
            this.textBoxSettingSteamId.TabIndex = 1;
            // 
            // textBoxSettingProfileId
            // 
            this.textBoxSettingProfileId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSettingProfileId.Enabled = false;
            this.textBoxSettingProfileId.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxSettingProfileId.Location = new System.Drawing.Point(93, 46);
            this.textBoxSettingProfileId.Name = "textBoxSettingProfileId";
            this.textBoxSettingProfileId.Size = new System.Drawing.Size(333, 23);
            this.textBoxSettingProfileId.TabIndex = 1;
            // 
            // labelSettingsCountry
            // 
            this.labelSettingsCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSettingsCountry.Location = new System.Drawing.Point(11, 105);
            this.labelSettingsCountry.Name = "labelSettingsCountry";
            this.labelSettingsCountry.Size = new System.Drawing.Size(497, 23);
            this.labelSettingsCountry.TabIndex = 3;
            this.labelSettingsCountry.Text = "Country: -----";
            // 
            // labelSettingsName
            // 
            this.labelSettingsName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSettingsName.Location = new System.Drawing.Point(11, 80);
            this.labelSettingsName.Name = "labelSettingsName";
            this.labelSettingsName.Size = new System.Drawing.Size(497, 23);
            this.labelSettingsName.TabIndex = 3;
            this.labelSettingsName.Text = "Name: -----";
            // 
            // labelAoE2NetStatus
            // 
            this.labelAoE2NetStatus.AutoSize = true;
            this.labelAoE2NetStatus.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAoE2NetStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.labelAoE2NetStatus.Location = new System.Drawing.Point(426, 8);
            this.labelAoE2NetStatus.Name = "labelAoE2NetStatus";
            this.labelAoE2NetStatus.Size = new System.Drawing.Size(101, 20);
            this.labelAoE2NetStatus.TabIndex = 19;
            this.labelAoE2NetStatus.Text = "Disconnected";
            // 
            // labelAoE2NetStatusLabel
            // 
            this.labelAoE2NetStatusLabel.AutoSize = true;
            this.labelAoE2NetStatusLabel.Font = new System.Drawing.Font("Yu Gothic UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAoE2NetStatusLabel.Location = new System.Drawing.Point(356, 8);
            this.labelAoE2NetStatusLabel.Name = "labelAoE2NetStatusLabel";
            this.labelAoE2NetStatusLabel.Size = new System.Drawing.Size(70, 20);
            this.labelAoE2NetStatusLabel.TabIndex = 18;
            this.labelAoE2NetStatusLabel.Text = "AoE2.net";
            // 
            // checkBoxHideTitle
            // 
            this.checkBoxHideTitle.AutoSize = true;
            this.checkBoxHideTitle.Location = new System.Drawing.Point(10, 35);
            this.checkBoxHideTitle.Name = "checkBoxHideTitle";
            this.checkBoxHideTitle.Size = new System.Drawing.Size(74, 19);
            this.checkBoxHideTitle.TabIndex = 17;
            this.checkBoxHideTitle.Text = "Hide title";
            this.checkBoxHideTitle.UseVisualStyleBackColor = true;
            this.checkBoxHideTitle.CheckedChanged += new System.EventHandler(this.CheckBoxHideTitle_CheckedChanged);
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.AutoSize = true;
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(10, 10);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(101, 19);
            this.checkBoxAlwaysOnTop.TabIndex = 15;
            this.checkBoxAlwaysOnTop.Text = "Always on top";
            this.checkBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysOnTop.CheckedChanged += new System.EventHandler(this.CheckBoxAlwaysOnTop_CheckedChanged);
            // 
            // labelErrText
            // 
            this.labelErrText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelErrText.BackColor = System.Drawing.Color.DarkGray;
            this.labelErrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelErrText.ForeColor = System.Drawing.Color.Red;
            this.labelErrText.Location = new System.Drawing.Point(10, 203);
            this.labelErrText.Name = "labelErrText";
            this.labelErrText.Size = new System.Drawing.Size(508, 6);
            this.labelErrText.TabIndex = 25;
            // 
            // checkBoxTransparencyWindow
            // 
            this.checkBoxTransparencyWindow.AutoSize = true;
            this.checkBoxTransparencyWindow.Location = new System.Drawing.Point(10, 62);
            this.checkBoxTransparencyWindow.Name = "checkBoxTransparencyWindow";
            this.checkBoxTransparencyWindow.Size = new System.Drawing.Size(142, 19);
            this.checkBoxTransparencyWindow.TabIndex = 26;
            this.checkBoxTransparencyWindow.Text = "Transparency Window";
            this.checkBoxTransparencyWindow.UseVisualStyleBackColor = true;
            this.checkBoxTransparencyWindow.CheckedChanged += new System.EventHandler(this.CheckBoxTransparencyWindow_CheckedChanged);
            // 
            // checkBoxDrawQuality
            // 
            this.checkBoxDrawQuality.AutoSize = true;
            this.checkBoxDrawQuality.Location = new System.Drawing.Point(157, 62);
            this.checkBoxDrawQuality.Name = "checkBoxDrawQuality";
            this.checkBoxDrawQuality.Size = new System.Drawing.Size(119, 19);
            this.checkBoxDrawQuality.TabIndex = 26;
            this.checkBoxDrawQuality.Text = "Draw high quality";
            this.checkBoxDrawQuality.UseVisualStyleBackColor = true;
            this.checkBoxDrawQuality.CheckedChanged += new System.EventHandler(this.CheckBoxDrawQuality_CheckedChanged);
            // 
            // checkBoxAutoReloadLastMatch
            // 
            this.checkBoxAutoReloadLastMatch.AutoSize = true;
            this.checkBoxAutoReloadLastMatch.Location = new System.Drawing.Point(382, 62);
            this.checkBoxAutoReloadLastMatch.Name = "checkBoxAutoReloadLastMatch";
            this.checkBoxAutoReloadLastMatch.Size = new System.Drawing.Size(145, 19);
            this.checkBoxAutoReloadLastMatch.TabIndex = 27;
            this.checkBoxAutoReloadLastMatch.Text = "Auto reload last match";
            this.checkBoxAutoReloadLastMatch.UseVisualStyleBackColor = true;
            this.checkBoxAutoReloadLastMatch.CheckedChanged += new System.EventHandler(this.CheckBoxAutoReloadLastMatch_CheckedChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 221);
            this.Controls.Add(this.checkBoxAutoReloadLastMatch);
            this.Controls.Add(this.checkBoxDrawQuality);
            this.Controls.Add(this.checkBoxTransparencyWindow);
            this.Controls.Add(this.textBoxChromaKey);
            this.Controls.Add(this.pictureBoxChromaKey);
            this.Controls.Add(this.labelChromaKey);
            this.Controls.Add(this.labelOpacity);
            this.Controls.Add(this.upDownOpacity);
            this.Controls.Add(this.groupBoxPlayer);
            this.Controls.Add(this.labelAoE2NetStatus);
            this.Controls.Add(this.labelAoE2NetStatusLabel);
            this.Controls.Add(this.checkBoxHideTitle);
            this.Controls.Add(this.checkBoxAlwaysOnTop);
            this.Controls.Add(this.labelErrText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 260);
            this.MinimumSize = new System.Drawing.Size(550, 260);
            this.Name = "FormSettings";
            this.ShowInTaskbar = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChromaKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownOpacity)).EndInit();
            this.groupBoxPlayer.ResumeLayout(false);
            this.groupBoxPlayer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBoxChromaKey;
        private PictureBox pictureBoxChromaKey;
        private Label labelChromaKey;
        private Label labelOpacity;
        private NumericUpDown upDownOpacity;
        private GroupBox groupBoxPlayer;
        private Button buttonSetId;
        private RadioButton radioButtonSteamID;
        private RadioButton radioButtonProfileID;
        private TextBox textBoxSettingSteamId;
        private TextBox textBoxSettingProfileId;
        private Label labelSettingsCountry;
        private Label labelSettingsName;
        private Label labelAoE2NetStatus;
        private Label labelAoE2NetStatusLabel;
        private CheckBox checkBoxHideTitle;
        private CheckBox checkBoxAlwaysOnTop;
        private Label labelErrText;
        private CheckBox checkBoxTransparencyWindow;
        private CheckBox checkBoxDrawQuality;
        private CheckBox checkBoxAutoReloadLastMatch;
    }
}