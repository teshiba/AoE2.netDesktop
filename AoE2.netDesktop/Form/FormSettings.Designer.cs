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
            this.groupBoxDisplayContents = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxBehavior = new System.Windows.Forms.GroupBox();
            this.checkBoxVisibleGameTime = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChromaKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownOpacity)).BeginInit();
            this.groupBoxPlayer.SuspendLayout();
            this.groupBoxDisplayContents.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBoxBehavior.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxChromaKey
            // 
            this.textBoxChromaKey.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxChromaKey.Location = new System.Drawing.Point(97, 134);
            this.textBoxChromaKey.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxChromaKey.Name = "textBoxChromaKey";
            this.textBoxChromaKey.Size = new System.Drawing.Size(66, 25);
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
            this.pictureBoxChromaKey.Location = new System.Drawing.Point(172, 135);
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
            this.labelChromaKey.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelChromaKey.Location = new System.Drawing.Point(15, 138);
            this.labelChromaKey.Name = "labelChromaKey";
            this.labelChromaKey.Size = new System.Drawing.Size(81, 17);
            this.labelChromaKey.TabIndex = 21;
            this.labelChromaKey.Text = "Chroma key";
            // 
            // labelOpacity
            // 
            this.labelOpacity.AutoSize = true;
            this.labelOpacity.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelOpacity.Location = new System.Drawing.Point(15, 109);
            this.labelOpacity.Name = "labelOpacity";
            this.labelOpacity.Size = new System.Drawing.Size(54, 17);
            this.labelOpacity.TabIndex = 22;
            this.labelOpacity.Text = "Opacity";
            // 
            // upDownOpacity
            // 
            this.upDownOpacity.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.upDownOpacity.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownOpacity.Location = new System.Drawing.Point(97, 105);
            this.upDownOpacity.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownOpacity.Name = "upDownOpacity";
            this.upDownOpacity.Size = new System.Drawing.Size(66, 25);
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
            this.groupBoxPlayer.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxPlayer.Location = new System.Drawing.Point(9, 193);
            this.groupBoxPlayer.Name = "groupBoxPlayer";
            this.groupBoxPlayer.Size = new System.Drawing.Size(608, 132);
            this.groupBoxPlayer.TabIndex = 16;
            this.groupBoxPlayer.TabStop = false;
            this.groupBoxPlayer.Text = "Player";
            // 
            // buttonSetId
            // 
            this.buttonSetId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetId.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.buttonSetId.Location = new System.Drawing.Point(525, 21);
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
            this.radioButtonSteamID.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radioButtonSteamID.Location = new System.Drawing.Point(7, 22);
            this.radioButtonSteamID.Name = "radioButtonSteamID";
            this.radioButtonSteamID.Size = new System.Drawing.Size(82, 21);
            this.radioButtonSteamID.TabIndex = 4;
            this.radioButtonSteamID.TabStop = true;
            this.radioButtonSteamID.Text = "Steam-ID";
            this.radioButtonSteamID.UseVisualStyleBackColor = true;
            this.radioButtonSteamID.CheckedChanged += new System.EventHandler(this.RadioButtonSteamID_CheckedChanged);
            // 
            // radioButtonProfileID
            // 
            this.radioButtonProfileID.AutoSize = true;
            this.radioButtonProfileID.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.radioButtonProfileID.Location = new System.Drawing.Point(7, 48);
            this.radioButtonProfileID.Name = "radioButtonProfileID";
            this.radioButtonProfileID.Size = new System.Drawing.Size(84, 21);
            this.radioButtonProfileID.TabIndex = 5;
            this.radioButtonProfileID.Text = "Profile-ID";
            this.radioButtonProfileID.UseVisualStyleBackColor = true;
            this.radioButtonProfileID.CheckedChanged += new System.EventHandler(this.RadioButtonProfileID_CheckedChanged);
            // 
            // textBoxSettingSteamId
            // 
            this.textBoxSettingSteamId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSettingSteamId.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxSettingSteamId.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxSettingSteamId.Location = new System.Drawing.Point(93, 21);
            this.textBoxSettingSteamId.Name = "textBoxSettingSteamId";
            this.textBoxSettingSteamId.Size = new System.Drawing.Size(428, 25);
            this.textBoxSettingSteamId.TabIndex = 1;
            // 
            // textBoxSettingProfileId
            // 
            this.textBoxSettingProfileId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSettingProfileId.Enabled = false;
            this.textBoxSettingProfileId.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxSettingProfileId.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxSettingProfileId.Location = new System.Drawing.Point(93, 46);
            this.textBoxSettingProfileId.Name = "textBoxSettingProfileId";
            this.textBoxSettingProfileId.Size = new System.Drawing.Size(428, 25);
            this.textBoxSettingProfileId.TabIndex = 1;
            // 
            // labelSettingsCountry
            // 
            this.labelSettingsCountry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSettingsCountry.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSettingsCountry.Location = new System.Drawing.Point(7, 105);
            this.labelSettingsCountry.Name = "labelSettingsCountry";
            this.labelSettingsCountry.Size = new System.Drawing.Size(591, 23);
            this.labelSettingsCountry.TabIndex = 3;
            this.labelSettingsCountry.Text = "Country: -----";
            // 
            // labelSettingsName
            // 
            this.labelSettingsName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSettingsName.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelSettingsName.Location = new System.Drawing.Point(7, 80);
            this.labelSettingsName.Name = "labelSettingsName";
            this.labelSettingsName.Size = new System.Drawing.Size(592, 23);
            this.labelSettingsName.TabIndex = 3;
            this.labelSettingsName.Text = "Name: -----";
            // 
            // labelAoE2NetStatus
            // 
            this.labelAoE2NetStatus.AutoSize = true;
            this.labelAoE2NetStatus.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAoE2NetStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.labelAoE2NetStatus.Location = new System.Drawing.Point(89, 142);
            this.labelAoE2NetStatus.Name = "labelAoE2NetStatus";
            this.labelAoE2NetStatus.Size = new System.Drawing.Size(89, 17);
            this.labelAoE2NetStatus.TabIndex = 19;
            this.labelAoE2NetStatus.Text = "Disconnected";
            // 
            // labelAoE2NetStatusLabel
            // 
            this.labelAoE2NetStatusLabel.AutoSize = true;
            this.labelAoE2NetStatusLabel.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelAoE2NetStatusLabel.Location = new System.Drawing.Point(19, 142);
            this.labelAoE2NetStatusLabel.Name = "labelAoE2NetStatusLabel";
            this.labelAoE2NetStatusLabel.Size = new System.Drawing.Size(62, 17);
            this.labelAoE2NetStatusLabel.TabIndex = 18;
            this.labelAoE2NetStatusLabel.Text = "AoE2.net";
            // 
            // checkBoxHideTitle
            // 
            this.checkBoxHideTitle.AutoSize = true;
            this.checkBoxHideTitle.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxHideTitle.Location = new System.Drawing.Point(15, 25);
            this.checkBoxHideTitle.Name = "checkBoxHideTitle";
            this.checkBoxHideTitle.Size = new System.Drawing.Size(82, 21);
            this.checkBoxHideTitle.TabIndex = 17;
            this.checkBoxHideTitle.Text = "Hide title";
            this.checkBoxHideTitle.UseVisualStyleBackColor = true;
            this.checkBoxHideTitle.CheckedChanged += new System.EventHandler(this.CheckBoxHideTitle_CheckedChanged);
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.AutoSize = true;
            this.checkBoxAlwaysOnTop.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(15, 49);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(114, 21);
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
            this.labelErrText.Location = new System.Drawing.Point(10, 340);
            this.labelErrText.Name = "labelErrText";
            this.labelErrText.Size = new System.Drawing.Size(608, 0);
            this.labelErrText.TabIndex = 25;
            // 
            // checkBoxTransparencyWindow
            // 
            this.checkBoxTransparencyWindow.AutoSize = true;
            this.checkBoxTransparencyWindow.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxTransparencyWindow.Location = new System.Drawing.Point(15, 73);
            this.checkBoxTransparencyWindow.Name = "checkBoxTransparencyWindow";
            this.checkBoxTransparencyWindow.Size = new System.Drawing.Size(158, 21);
            this.checkBoxTransparencyWindow.TabIndex = 26;
            this.checkBoxTransparencyWindow.Text = "Transparency window";
            this.checkBoxTransparencyWindow.UseVisualStyleBackColor = true;
            this.checkBoxTransparencyWindow.CheckedChanged += new System.EventHandler(this.CheckBoxTransparencyWindow_CheckedChanged);
            // 
            // checkBoxDrawQuality
            // 
            this.checkBoxDrawQuality.AutoSize = true;
            this.checkBoxDrawQuality.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxDrawQuality.Location = new System.Drawing.Point(15, 25);
            this.checkBoxDrawQuality.Name = "checkBoxDrawQuality";
            this.checkBoxDrawQuality.Size = new System.Drawing.Size(162, 21);
            this.checkBoxDrawQuality.TabIndex = 26;
            this.checkBoxDrawQuality.Text = "Draw high quality text";
            this.checkBoxDrawQuality.UseVisualStyleBackColor = true;
            this.checkBoxDrawQuality.CheckedChanged += new System.EventHandler(this.CheckBoxDrawQuality_CheckedChanged);
            // 
            // checkBoxAutoReloadLastMatch
            // 
            this.checkBoxAutoReloadLastMatch.AutoSize = true;
            this.checkBoxAutoReloadLastMatch.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxAutoReloadLastMatch.Location = new System.Drawing.Point(15, 25);
            this.checkBoxAutoReloadLastMatch.Name = "checkBoxAutoReloadLastMatch";
            this.checkBoxAutoReloadLastMatch.Size = new System.Drawing.Size(166, 21);
            this.checkBoxAutoReloadLastMatch.TabIndex = 27;
            this.checkBoxAutoReloadLastMatch.Text = "Auto reload last match";
            this.checkBoxAutoReloadLastMatch.UseVisualStyleBackColor = true;
            this.checkBoxAutoReloadLastMatch.CheckedChanged += new System.EventHandler(this.CheckBoxAutoReloadLastMatch_CheckedChanged);
            // 
            // groupBoxDisplayContents
            // 
            this.groupBoxDisplayContents.Controls.Add(this.checkBoxVisibleGameTime);
            this.groupBoxDisplayContents.Controls.Add(this.checkBoxHideTitle);
            this.groupBoxDisplayContents.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxDisplayContents.Location = new System.Drawing.Point(10, 8);
            this.groupBoxDisplayContents.Name = "groupBoxDisplayContents";
            this.groupBoxDisplayContents.Size = new System.Drawing.Size(176, 181);
            this.groupBoxDisplayContents.TabIndex = 29;
            this.groupBoxDisplayContents.TabStop = false;
            this.groupBoxDisplayContents.Text = "Display contents";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxDrawQuality);
            this.groupBox2.Controls.Add(this.checkBoxTransparencyWindow);
            this.groupBox2.Controls.Add(this.checkBoxAlwaysOnTop);
            this.groupBox2.Controls.Add(this.upDownOpacity);
            this.groupBox2.Controls.Add(this.pictureBoxChromaKey);
            this.groupBox2.Controls.Add(this.textBoxChromaKey);
            this.groupBox2.Controls.Add(this.labelOpacity);
            this.groupBox2.Controls.Add(this.labelChromaKey);
            this.groupBox2.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBox2.Location = new System.Drawing.Point(192, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(217, 181);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Appearance";
            // 
            // groupBoxBehavior
            // 
            this.groupBoxBehavior.Controls.Add(this.checkBoxAutoReloadLastMatch);
            this.groupBoxBehavior.Controls.Add(this.labelAoE2NetStatus);
            this.groupBoxBehavior.Controls.Add(this.labelAoE2NetStatusLabel);
            this.groupBoxBehavior.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxBehavior.Location = new System.Drawing.Point(415, 8);
            this.groupBoxBehavior.Name = "groupBoxBehavior";
            this.groupBoxBehavior.Size = new System.Drawing.Size(200, 179);
            this.groupBoxBehavior.TabIndex = 31;
            this.groupBoxBehavior.TabStop = false;
            this.groupBoxBehavior.Text = "Behavior";
            // 
            // checkBoxVisibleGameTime
            // 
            this.checkBoxVisibleGameTime.AutoSize = true;
            this.checkBoxVisibleGameTime.Font = new System.Drawing.Font("Yu Gothic UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxVisibleGameTime.Location = new System.Drawing.Point(15, 49);
            this.checkBoxVisibleGameTime.Name = "checkBoxVisibleGameTime";
            this.checkBoxVisibleGameTime.Size = new System.Drawing.Size(134, 21);
            this.checkBoxVisibleGameTime.TabIndex = 18;
            this.checkBoxVisibleGameTime.Text = "Visible game time";
            this.checkBoxVisibleGameTime.UseVisualStyleBackColor = true;
            this.checkBoxVisibleGameTime.CheckedChanged += new System.EventHandler(this.CheckBoxVisibleGameTime_CheckedChanged);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 333);
            this.Controls.Add(this.groupBoxBehavior);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxDisplayContents);
            this.Controls.Add(this.groupBoxPlayer);
            this.Controls.Add(this.labelErrText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormSettings_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxChromaKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownOpacity)).EndInit();
            this.groupBoxPlayer.ResumeLayout(false);
            this.groupBoxPlayer.PerformLayout();
            this.groupBoxDisplayContents.ResumeLayout(false);
            this.groupBoxDisplayContents.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxBehavior.ResumeLayout(false);
            this.groupBoxBehavior.PerformLayout();
            this.ResumeLayout(false);

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
        private GroupBox groupBoxDisplayContents;
        private GroupBox groupBox2;
        private GroupBox groupBoxBehavior;
        private CheckBox checkBoxVisibleGameTime;
    }
}