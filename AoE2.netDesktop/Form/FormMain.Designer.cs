namespace AoE2NetDesktop.Form
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.labelServer = new System.Windows.Forms.Label();
            this.labelGameId = new System.Windows.Forms.Label();
            this.panelTeam1 = new System.Windows.Forms.Panel();
            this.labelNameP7 = new System.Windows.Forms.Label();
            this.labelNameP3 = new System.Windows.Forms.Label();
            this.labelNameP5 = new System.Windows.Forms.Label();
            this.labelNameP1 = new System.Windows.Forms.Label();
            this.labelRateP7 = new System.Windows.Forms.Label();
            this.labelRateP3 = new System.Windows.Forms.Label();
            this.labelRateP5 = new System.Windows.Forms.Label();
            this.labelRateP1 = new System.Windows.Forms.Label();
            this.labelCivP7 = new System.Windows.Forms.Label();
            this.labelCivP3 = new System.Windows.Forms.Label();
            this.labelCivP5 = new System.Windows.Forms.Label();
            this.labelCivP1 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelColorP7 = new System.Windows.Forms.Label();
            this.labelColorP3 = new System.Windows.Forms.Label();
            this.labelColorP5 = new System.Windows.Forms.Label();
            this.labelAveRate1 = new System.Windows.Forms.Label();
            this.labelColorP1 = new System.Windows.Forms.Label();
            this.colorDialogChromaKey = new System.Windows.Forms.ColorDialog();
            this.labelErrText = new System.Windows.Forms.Label();
            this.panelTeam2 = new System.Windows.Forms.Panel();
            this.labelNameP2 = new System.Windows.Forms.Label();
            this.labelNameP8 = new System.Windows.Forms.Label();
            this.labelNameP6 = new System.Windows.Forms.Label();
            this.labelNameP4 = new System.Windows.Forms.Label();
            this.labelRateP2 = new System.Windows.Forms.Label();
            this.labelRateP6 = new System.Windows.Forms.Label();
            this.labelRateP4 = new System.Windows.Forms.Label();
            this.labelRateP8 = new System.Windows.Forms.Label();
            this.labelCivP2 = new System.Windows.Forms.Label();
            this.labelCivP6 = new System.Windows.Forms.Label();
            this.labelCivP4 = new System.Windows.Forms.Label();
            this.labelCivP8 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.labelAveRate2 = new System.Windows.Forms.Label();
            this.labelColorP8 = new System.Windows.Forms.Label();
            this.labelColorP4 = new System.Windows.Forms.Label();
            this.labelColorP6 = new System.Windows.Forms.Label();
            this.labelColorP2 = new System.Windows.Forms.Label();
            this.labelMap = new System.Windows.Forms.Label();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showMyHistoryHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelDateTime = new System.Windows.Forms.Label();
            this.panelDebug = new System.Windows.Forms.Panel();
            this.labelAoE2DEActive = new System.Windows.Forms.Label();
            this.panelTeam1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelTeam2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.contextMenuStripMain.SuspendLayout();
            this.panelDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelServer
            // 
            this.labelServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelServer.BackColor = System.Drawing.SystemColors.Control;
            this.labelServer.Location = new System.Drawing.Point(725, 29);
            this.labelServer.Name = "labelServer";
            this.labelServer.Size = new System.Drawing.Size(152, 19);
            this.labelServer.TabIndex = 2;
            this.labelServer.Text = "Server: ----------";
            this.labelServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelServer.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelServer_Paint);
            // 
            // labelGameId
            // 
            this.labelGameId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGameId.BackColor = System.Drawing.SystemColors.Control;
            this.labelGameId.Location = new System.Drawing.Point(724, 8);
            this.labelGameId.Name = "labelGameId";
            this.labelGameId.Size = new System.Drawing.Size(152, 19);
            this.labelGameId.TabIndex = 9;
            this.labelGameId.Text = "GameID: 88888888";
            this.labelGameId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelGameId.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelGameId_Paint);
            // 
            // panelTeam1
            // 
            this.panelTeam1.BackColor = System.Drawing.SystemColors.Control;
            this.panelTeam1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelTeam1.Controls.Add(this.labelNameP7);
            this.panelTeam1.Controls.Add(this.labelNameP3);
            this.panelTeam1.Controls.Add(this.labelNameP5);
            this.panelTeam1.Controls.Add(this.labelNameP1);
            this.panelTeam1.Controls.Add(this.labelRateP7);
            this.panelTeam1.Controls.Add(this.labelRateP3);
            this.panelTeam1.Controls.Add(this.labelRateP5);
            this.panelTeam1.Controls.Add(this.labelRateP1);
            this.panelTeam1.Controls.Add(this.labelCivP7);
            this.panelTeam1.Controls.Add(this.labelCivP3);
            this.panelTeam1.Controls.Add(this.labelCivP5);
            this.panelTeam1.Controls.Add(this.labelCivP1);
            this.panelTeam1.Controls.Add(this.pictureBox7);
            this.panelTeam1.Controls.Add(this.pictureBox5);
            this.panelTeam1.Controls.Add(this.pictureBox3);
            this.panelTeam1.Controls.Add(this.pictureBox1);
            this.panelTeam1.Controls.Add(this.labelColorP7);
            this.panelTeam1.Controls.Add(this.labelColorP3);
            this.panelTeam1.Controls.Add(this.labelColorP5);
            this.panelTeam1.Controls.Add(this.labelAveRate1);
            this.panelTeam1.Controls.Add(this.labelColorP1);
            this.panelTeam1.Location = new System.Drawing.Point(11, 53);
            this.panelTeam1.Name = "panelTeam1";
            this.panelTeam1.Size = new System.Drawing.Size(444, 265);
            this.panelTeam1.TabIndex = 6;
            // 
            // labelNameP7
            // 
            this.labelNameP7.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP7.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP7.Location = new System.Drawing.Point(144, 197);
            this.labelNameP7.Name = "labelNameP7";
            this.labelNameP7.Size = new System.Drawing.Size(295, 34);
            this.labelNameP7.TabIndex = 3;
            this.labelNameP7.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP7.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP7_Paint);
            this.labelNameP7.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelNameP3
            // 
            this.labelNameP3.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP3.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP3.Location = new System.Drawing.Point(144, 88);
            this.labelNameP3.Name = "labelNameP3";
            this.labelNameP3.Size = new System.Drawing.Size(295, 28);
            this.labelNameP3.TabIndex = 3;
            this.labelNameP3.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP3.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP3_Paint);
            this.labelNameP3.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelNameP5
            // 
            this.labelNameP5.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP5.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP5.Location = new System.Drawing.Point(144, 143);
            this.labelNameP5.Name = "labelNameP5";
            this.labelNameP5.Size = new System.Drawing.Size(295, 34);
            this.labelNameP5.TabIndex = 3;
            this.labelNameP5.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP5.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP5_Paint);
            this.labelNameP5.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelNameP1
            // 
            this.labelNameP1.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP1.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP1.Location = new System.Drawing.Point(144, 33);
            this.labelNameP1.Name = "labelNameP1";
            this.labelNameP1.Size = new System.Drawing.Size(295, 28);
            this.labelNameP1.TabIndex = 3;
            this.labelNameP1.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP1.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP1_Paint);
            this.labelNameP1.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelRateP7
            // 
            this.labelRateP7.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP7.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP7.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP7.Location = new System.Drawing.Point(85, 197);
            this.labelRateP7.Name = "labelRateP7";
            this.labelRateP7.Size = new System.Drawing.Size(57, 28);
            this.labelRateP7.TabIndex = 3;
            this.labelRateP7.Text = "####";
            this.labelRateP7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP7.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelRateP3
            // 
            this.labelRateP3.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP3.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP3.Location = new System.Drawing.Point(85, 87);
            this.labelRateP3.Name = "labelRateP3";
            this.labelRateP3.Size = new System.Drawing.Size(57, 28);
            this.labelRateP3.TabIndex = 3;
            this.labelRateP3.Text = "####";
            this.labelRateP3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP3.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelRateP5
            // 
            this.labelRateP5.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP5.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP5.Location = new System.Drawing.Point(85, 142);
            this.labelRateP5.Name = "labelRateP5";
            this.labelRateP5.Size = new System.Drawing.Size(57, 28);
            this.labelRateP5.TabIndex = 3;
            this.labelRateP5.Text = "####";
            this.labelRateP5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP5.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelRateP1
            // 
            this.labelRateP1.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP1.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP1.Location = new System.Drawing.Point(85, 32);
            this.labelRateP1.Name = "labelRateP1";
            this.labelRateP1.Size = new System.Drawing.Size(57, 28);
            this.labelRateP1.TabIndex = 3;
            this.labelRateP1.Text = "####";
            this.labelRateP1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP1.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelCivP7
            // 
            this.labelCivP7.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP7.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP7.Location = new System.Drawing.Point(50, 226);
            this.labelCivP7.Name = "labelCivP7";
            this.labelCivP7.Size = new System.Drawing.Size(262, 26);
            this.labelCivP7.TabIndex = 2;
            this.labelCivP7.Text = "CIVxxxxXXXX";
            this.labelCivP7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP7.UseMnemonic = false;
            this.labelCivP7.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // labelCivP3
            // 
            this.labelCivP3.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP3.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP3.Location = new System.Drawing.Point(50, 118);
            this.labelCivP3.Name = "labelCivP3";
            this.labelCivP3.Size = new System.Drawing.Size(262, 26);
            this.labelCivP3.TabIndex = 2;
            this.labelCivP3.Text = "CIVxxxxXXXX";
            this.labelCivP3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP3.UseMnemonic = false;
            this.labelCivP3.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // labelCivP5
            // 
            this.labelCivP5.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP5.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP5.Location = new System.Drawing.Point(50, 172);
            this.labelCivP5.Name = "labelCivP5";
            this.labelCivP5.Size = new System.Drawing.Size(262, 26);
            this.labelCivP5.TabIndex = 2;
            this.labelCivP5.Text = "CIVxxxxXXXX";
            this.labelCivP5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP5.UseMnemonic = false;
            this.labelCivP5.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // labelCivP1
            // 
            this.labelCivP1.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP1.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP1.Location = new System.Drawing.Point(50, 64);
            this.labelCivP1.Name = "labelCivP1";
            this.labelCivP1.Size = new System.Drawing.Size(262, 26);
            this.labelCivP1.TabIndex = 2;
            this.labelCivP1.Text = "CIVxxxxXXXX";
            this.labelCivP1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP1.UseMnemonic = false;
            this.labelCivP1.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Location = new System.Drawing.Point(53, 196);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(32, 32);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 6;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox5.Location = new System.Drawing.Point(53, 142);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(32, 32);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 7;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(53, 88);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(53, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // labelColorP7
            // 
            this.labelColorP7.BackColor = System.Drawing.Color.Gray;
            this.labelColorP7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP7.Location = new System.Drawing.Point(7, 193);
            this.labelColorP7.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP7.Name = "labelColorP7";
            this.labelColorP7.Size = new System.Drawing.Size(40, 40);
            this.labelColorP7.TabIndex = 4;
            this.labelColorP7.Text = "７";
            this.labelColorP7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP7.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelColorP3
            // 
            this.labelColorP3.BackColor = System.Drawing.Color.Green;
            this.labelColorP3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP3.Location = new System.Drawing.Point(7, 85);
            this.labelColorP3.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP3.Name = "labelColorP3";
            this.labelColorP3.Size = new System.Drawing.Size(40, 40);
            this.labelColorP3.TabIndex = 4;
            this.labelColorP3.Text = "３";
            this.labelColorP3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP3.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelColorP5
            // 
            this.labelColorP5.BackColor = System.Drawing.Color.Aqua;
            this.labelColorP5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP5.Location = new System.Drawing.Point(7, 139);
            this.labelColorP5.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP5.Name = "labelColorP5";
            this.labelColorP5.Size = new System.Drawing.Size(40, 40);
            this.labelColorP5.TabIndex = 4;
            this.labelColorP5.Text = "５";
            this.labelColorP5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP5.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelAveRate1
            // 
            this.labelAveRate1.BackColor = System.Drawing.SystemColors.Control;
            this.labelAveRate1.Location = new System.Drawing.Point(2, 3);
            this.labelAveRate1.Name = "labelAveRate1";
            this.labelAveRate1.Size = new System.Drawing.Size(213, 27);
            this.labelAveRate1.TabIndex = 3;
            this.labelAveRate1.Text = "Team1 Ave. Rate : ####";
            this.labelAveRate1.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelAveRate_Paint);
            // 
            // labelColorP1
            // 
            this.labelColorP1.BackColor = System.Drawing.Color.Blue;
            this.labelColorP1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP1.Location = new System.Drawing.Point(7, 31);
            this.labelColorP1.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP1.Name = "labelColorP1";
            this.labelColorP1.Size = new System.Drawing.Size(40, 40);
            this.labelColorP1.TabIndex = 4;
            this.labelColorP1.Text = "１";
            this.labelColorP1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP1.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // colorDialogChromaKey
            // 
            this.colorDialogChromaKey.Color = System.Drawing.Color.SlateGray;
            // 
            // labelErrText
            // 
            this.labelErrText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelErrText.BackColor = System.Drawing.Color.DarkGray;
            this.labelErrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelErrText.ForeColor = System.Drawing.Color.Red;
            this.labelErrText.Location = new System.Drawing.Point(4, 25);
            this.labelErrText.Name = "labelErrText";
            this.labelErrText.Size = new System.Drawing.Size(857, 143);
            this.labelErrText.TabIndex = 8;
            // 
            // panelTeam2
            // 
            this.panelTeam2.BackColor = System.Drawing.SystemColors.Control;
            this.panelTeam2.Controls.Add(this.labelNameP2);
            this.panelTeam2.Controls.Add(this.labelNameP8);
            this.panelTeam2.Controls.Add(this.labelNameP6);
            this.panelTeam2.Controls.Add(this.labelNameP4);
            this.panelTeam2.Controls.Add(this.labelRateP2);
            this.panelTeam2.Controls.Add(this.labelRateP6);
            this.panelTeam2.Controls.Add(this.labelRateP4);
            this.panelTeam2.Controls.Add(this.labelRateP8);
            this.panelTeam2.Controls.Add(this.labelCivP2);
            this.panelTeam2.Controls.Add(this.labelCivP6);
            this.panelTeam2.Controls.Add(this.labelCivP4);
            this.panelTeam2.Controls.Add(this.labelCivP8);
            this.panelTeam2.Controls.Add(this.pictureBox8);
            this.panelTeam2.Controls.Add(this.pictureBox6);
            this.panelTeam2.Controls.Add(this.pictureBox4);
            this.panelTeam2.Controls.Add(this.pictureBox2);
            this.panelTeam2.Controls.Add(this.labelAveRate2);
            this.panelTeam2.Controls.Add(this.labelColorP8);
            this.panelTeam2.Controls.Add(this.labelColorP4);
            this.panelTeam2.Controls.Add(this.labelColorP6);
            this.panelTeam2.Controls.Add(this.labelColorP2);
            this.panelTeam2.Location = new System.Drawing.Point(461, 53);
            this.panelTeam2.Name = "panelTeam2";
            this.panelTeam2.Size = new System.Drawing.Size(417, 265);
            this.panelTeam2.TabIndex = 5;
            // 
            // labelNameP2
            // 
            this.labelNameP2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNameP2.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP2.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP2.Location = new System.Drawing.Point(142, 33);
            this.labelNameP2.Name = "labelNameP2";
            this.labelNameP2.Size = new System.Drawing.Size(272, 34);
            this.labelNameP2.TabIndex = 3;
            this.labelNameP2.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP2.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP2_Paint);
            this.labelNameP2.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelNameP8
            // 
            this.labelNameP8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNameP8.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP8.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP8.Location = new System.Drawing.Point(142, 196);
            this.labelNameP8.Name = "labelNameP8";
            this.labelNameP8.Size = new System.Drawing.Size(272, 34);
            this.labelNameP8.TabIndex = 3;
            this.labelNameP8.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP8.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP8_Paint);
            this.labelNameP8.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelNameP6
            // 
            this.labelNameP6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNameP6.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP6.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP6.Location = new System.Drawing.Point(142, 142);
            this.labelNameP6.Name = "labelNameP6";
            this.labelNameP6.Size = new System.Drawing.Size(272, 34);
            this.labelNameP6.TabIndex = 3;
            this.labelNameP6.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP6.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP6_Paint);
            this.labelNameP6.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelNameP4
            // 
            this.labelNameP4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNameP4.BackColor = System.Drawing.SystemColors.Control;
            this.labelNameP4.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelNameP4.Location = new System.Drawing.Point(142, 88);
            this.labelNameP4.Name = "labelNameP4";
            this.labelNameP4.Size = new System.Drawing.Size(272, 34);
            this.labelNameP4.TabIndex = 3;
            this.labelNameP4.Text = "PlayerXxxxxxxxxxxxxxx";
            this.labelNameP4.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelNameP4_Paint);
            this.labelNameP4.DoubleClick += new System.EventHandler(this.LabelName_DoubleClick);
            // 
            // labelRateP2
            // 
            this.labelRateP2.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP2.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP2.Location = new System.Drawing.Point(84, 32);
            this.labelRateP2.Name = "labelRateP2";
            this.labelRateP2.Size = new System.Drawing.Size(57, 28);
            this.labelRateP2.TabIndex = 3;
            this.labelRateP2.Text = "####";
            this.labelRateP2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP2.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelRateP6
            // 
            this.labelRateP6.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP6.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP6.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP6.Location = new System.Drawing.Point(84, 141);
            this.labelRateP6.Name = "labelRateP6";
            this.labelRateP6.Size = new System.Drawing.Size(57, 28);
            this.labelRateP6.TabIndex = 3;
            this.labelRateP6.Text = "####";
            this.labelRateP6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP6.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelRateP4
            // 
            this.labelRateP4.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP4.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP4.Location = new System.Drawing.Point(84, 87);
            this.labelRateP4.Name = "labelRateP4";
            this.labelRateP4.Size = new System.Drawing.Size(57, 28);
            this.labelRateP4.TabIndex = 3;
            this.labelRateP4.Text = "####";
            this.labelRateP4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP4.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelRateP8
            // 
            this.labelRateP8.BackColor = System.Drawing.SystemColors.Control;
            this.labelRateP8.Font = new System.Drawing.Font("Yu Gothic UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelRateP8.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelRateP8.Location = new System.Drawing.Point(84, 196);
            this.labelRateP8.Name = "labelRateP8";
            this.labelRateP8.Size = new System.Drawing.Size(57, 28);
            this.labelRateP8.TabIndex = 3;
            this.labelRateP8.Text = "####";
            this.labelRateP8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelRateP8.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelRate_Paint);
            // 
            // labelCivP2
            // 
            this.labelCivP2.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP2.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP2.Location = new System.Drawing.Point(51, 64);
            this.labelCivP2.Name = "labelCivP2";
            this.labelCivP2.Size = new System.Drawing.Size(262, 26);
            this.labelCivP2.TabIndex = 2;
            this.labelCivP2.Text = "CIVxxxxXXXX";
            this.labelCivP2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP2.UseMnemonic = false;
            this.labelCivP2.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // labelCivP6
            // 
            this.labelCivP6.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP6.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP6.Location = new System.Drawing.Point(51, 171);
            this.labelCivP6.Name = "labelCivP6";
            this.labelCivP6.Size = new System.Drawing.Size(262, 26);
            this.labelCivP6.TabIndex = 2;
            this.labelCivP6.Text = "CIVxxxxXXXX";
            this.labelCivP6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP6.UseMnemonic = false;
            this.labelCivP6.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // labelCivP4
            // 
            this.labelCivP4.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP4.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP4.Location = new System.Drawing.Point(51, 118);
            this.labelCivP4.Name = "labelCivP4";
            this.labelCivP4.Size = new System.Drawing.Size(262, 26);
            this.labelCivP4.TabIndex = 2;
            this.labelCivP4.Text = "CIVxxxxXXXX";
            this.labelCivP4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP4.UseMnemonic = false;
            this.labelCivP4.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // labelCivP8
            // 
            this.labelCivP8.BackColor = System.Drawing.SystemColors.Control;
            this.labelCivP8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelCivP8.Font = new System.Drawing.Font("Yu Gothic UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelCivP8.Location = new System.Drawing.Point(51, 225);
            this.labelCivP8.Name = "labelCivP8";
            this.labelCivP8.Size = new System.Drawing.Size(262, 26);
            this.labelCivP8.TabIndex = 2;
            this.labelCivP8.Text = "CIVxxxxXXXX";
            this.labelCivP8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelCivP8.UseMnemonic = false;
            this.labelCivP8.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelCiv_Paint);
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(52, 195);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(32, 32);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 5;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.Location = new System.Drawing.Point(52, 141);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(32, 32);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(52, 88);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(32, 32);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 5;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(52, 34);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // labelAveRate2
            // 
            this.labelAveRate2.BackColor = System.Drawing.SystemColors.Control;
            this.labelAveRate2.Location = new System.Drawing.Point(2, 3);
            this.labelAveRate2.Name = "labelAveRate2";
            this.labelAveRate2.Size = new System.Drawing.Size(217, 27);
            this.labelAveRate2.TabIndex = 3;
            this.labelAveRate2.Text = "Team2 Ave. Rate : ####";
            this.labelAveRate2.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelAveRate_Paint);
            // 
            // labelColorP8
            // 
            this.labelColorP8.BackColor = System.Drawing.Color.Orange;
            this.labelColorP8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP8.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP8.Location = new System.Drawing.Point(7, 192);
            this.labelColorP8.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP8.Name = "labelColorP8";
            this.labelColorP8.Size = new System.Drawing.Size(40, 40);
            this.labelColorP8.TabIndex = 4;
            this.labelColorP8.Text = "８";
            this.labelColorP8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP8.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelColorP4
            // 
            this.labelColorP4.BackColor = System.Drawing.Color.Yellow;
            this.labelColorP4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP4.Location = new System.Drawing.Point(7, 85);
            this.labelColorP4.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP4.Name = "labelColorP4";
            this.labelColorP4.Size = new System.Drawing.Size(40, 40);
            this.labelColorP4.TabIndex = 4;
            this.labelColorP4.Text = "４";
            this.labelColorP4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP4.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelColorP6
            // 
            this.labelColorP6.BackColor = System.Drawing.Color.Magenta;
            this.labelColorP6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP6.Location = new System.Drawing.Point(7, 138);
            this.labelColorP6.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP6.Name = "labelColorP6";
            this.labelColorP6.Size = new System.Drawing.Size(40, 40);
            this.labelColorP6.TabIndex = 4;
            this.labelColorP6.Text = "６";
            this.labelColorP6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP6.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelColorP2
            // 
            this.labelColorP2.BackColor = System.Drawing.Color.Red;
            this.labelColorP2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelColorP2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelColorP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelColorP2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelColorP2.Location = new System.Drawing.Point(7, 31);
            this.labelColorP2.Margin = new System.Windows.Forms.Padding(0);
            this.labelColorP2.Name = "labelColorP2";
            this.labelColorP2.Size = new System.Drawing.Size(40, 40);
            this.labelColorP2.TabIndex = 4;
            this.labelColorP2.Text = "２";
            this.labelColorP2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelColorP2.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelColor_Paint);
            // 
            // labelMap
            // 
            this.labelMap.BackColor = System.Drawing.SystemColors.Control;
            this.labelMap.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelMap.Location = new System.Drawing.Point(1, 2);
            this.labelMap.Name = "labelMap";
            this.labelMap.Size = new System.Drawing.Size(711, 40);
            this.labelMap.TabIndex = 2;
            this.labelMap.Text = "Map : ------------------------";
            this.labelMap.Paint += new System.Windows.Forms.PaintEventHandler(this.LabelMap_Paint);
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.showMyHistoryHToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(187, 92);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.settingsToolStripMenuItem.Text = "Settings...(&S)";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // showMyHistoryHToolStripMenuItem
            // 
            this.showMyHistoryHToolStripMenuItem.Name = "showMyHistoryHToolStripMenuItem";
            this.showMyHistoryHToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.showMyHistoryHToolStripMenuItem.Text = "Show my history...(&H)";
            this.showMyHistoryHToolStripMenuItem.Click += new System.EventHandler(this.ShowMyHistoryHToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.updateToolStripMenuItem.Text = "Update(&U)";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.UpdateToolStripMenuItem_ClickAsync);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // labelDateTime
            // 
            this.labelDateTime.AutoSize = true;
            this.labelDateTime.Font = new System.Drawing.Font("Yu Gothic UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDateTime.Location = new System.Drawing.Point(113, 0);
            this.labelDateTime.Name = "labelDateTime";
            this.labelDateTime.Size = new System.Drawing.Size(367, 25);
            this.labelDateTime.TabIndex = 10;
            this.labelDateTime.Text = "Last match data updated: ----/--/-- --:--:--";
            // 
            // panelDebug
            // 
            this.panelDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDebug.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelDebug.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDebug.Controls.Add(this.labelAoE2DEActive);
            this.panelDebug.Controls.Add(this.labelErrText);
            this.panelDebug.Controls.Add(this.labelDateTime);
            this.panelDebug.Location = new System.Drawing.Point(11, 324);
            this.panelDebug.Name = "panelDebug";
            this.panelDebug.Size = new System.Drawing.Size(868, 178);
            this.panelDebug.TabIndex = 11;
            // 
            // labelAoE2DEActive
            // 
            this.labelAoE2DEActive.AutoSize = true;
            this.labelAoE2DEActive.Location = new System.Drawing.Point(3, 8);
            this.labelAoE2DEActive.Name = "labelAoE2DEActive";
            this.labelAoE2DEActive.Size = new System.Drawing.Size(109, 15);
            this.labelAoE2DEActive.TabIndex = 11;
            this.labelAoE2DEActive.Text = "AoE2DE NOT active";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(883, 506);
            this.ContextMenuStrip = this.contextMenuStripMain;
            this.Controls.Add(this.panelDebug);
            this.Controls.Add(this.panelTeam1);
            this.Controls.Add(this.panelTeam2);
            this.Controls.Add(this.labelServer);
            this.Controls.Add(this.labelGameId);
            this.Controls.Add(this.labelMap);
            this.MinimumSize = new System.Drawing.Size(390, 360);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AoE2.net Desktop";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseMove);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.panelTeam1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelTeam2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.contextMenuStripMain.ResumeLayout(false);
            this.panelDebug.ResumeLayout(false);
            this.panelDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelColorP7;
        private System.Windows.Forms.Label labelColorP3;
        private System.Windows.Forms.Label labelColorP8;
        private System.Windows.Forms.Label labelNameP7;
        private System.Windows.Forms.Label labelColorP4;
        private System.Windows.Forms.Label labelColorP5;
        private System.Windows.Forms.Label labelNameP8;
        private System.Windows.Forms.Label labelNameP3;
        private System.Windows.Forms.Label labelColorP6;
        private System.Windows.Forms.Label labelRateP7;
        private System.Windows.Forms.Label labelNameP4;
        private System.Windows.Forms.Label labelColorP1;
        private System.Windows.Forms.Label labelRateP8;
        private System.Windows.Forms.Label labelNameP5;
        private System.Windows.Forms.Label labelColorP2;
        private System.Windows.Forms.Label labelRateP3;
        private System.Windows.Forms.Label labelNameP6;
        private System.Windows.Forms.Label labelRateP5;
        private System.Windows.Forms.Label labelRateP4;
        private System.Windows.Forms.Label labelNameP1;
        private System.Windows.Forms.Label labelRateP6;
        private System.Windows.Forms.Label labelRateP1;
        private System.Windows.Forms.Label labelNameP2;
        private System.Windows.Forms.Label labelCivP7;
        private System.Windows.Forms.Label labelRateP2;
        private System.Windows.Forms.Label labelCivP8;
        private System.Windows.Forms.Label labelCivP3;
        private System.Windows.Forms.Label labelAveRate1;
        private System.Windows.Forms.Label labelCivP5;
        private System.Windows.Forms.Label labelCivP4;
        private System.Windows.Forms.Label labelCivP6;
        private System.Windows.Forms.Label labelCivP1;
        private System.Windows.Forms.Label labelAveRate2;
        private System.Windows.Forms.Label labelCivP2;
        private System.Windows.Forms.Label labelServer;
        private System.Windows.Forms.Label labelMap;
        private System.Windows.Forms.Panel panelTeam1;
        private System.Windows.Forms.Panel panelTeam2;
        private System.Windows.Forms.Label labelErrText;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelGameId;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialogChromaKey;
        private System.Windows.Forms.ToolStripMenuItem showMyHistoryHToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label labelDateTime;
        private System.Windows.Forms.Panel panelDebug;
        private System.Windows.Forms.Label labelAoE2DEActive;
    }
}