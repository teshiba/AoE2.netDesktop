
namespace AoE2NetDesktop.Form
{
    partial class FormHistory
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
            this.contextMenuStripMatchedPlayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openAoE2NetProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPageMatches = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainerMatches = new System.Windows.Forms.SplitContainer();
            this.listViewMatchHistory = new System.Windows.Forms.ListView();
            this.columnHeaderMap = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWin = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCiv = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColor = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVersion = new System.Windows.Forms.ColumnHeader();
            this.labelDataSource = new System.Windows.Forms.Label();
            this.comboBoxDataSource = new System.Windows.Forms.ComboBox();
            this.formsPlotWinRate = new ScottPlot.FormsPlot();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelComboBoxLeaderboard = new System.Windows.Forms.Label();
            this.comboBoxLeaderboard = new System.Windows.Forms.ComboBox();
            this.tabPageStatistics = new System.Windows.Forms.TabPage();
            this.formsPlotPlayerRate = new ScottPlot.FormsPlot();
            this.listViewStatistics = new System.Windows.Forms.ListView();
            this.columnHeaderLeaderboard = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRank = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRating = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderHighestRating = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderGames = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWinRate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWins = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLosses = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDrop = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStreak = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderHighestStreak = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLowestStreak = new System.Windows.Forms.ColumnHeader();
            this.tabPagePlayers = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.splitContainerPlayers = new System.Windows.Forms.SplitContainer();
            this.listViewFilterCountry = new System.Windows.Forms.ListView();
            this.Country = new System.Windows.Forms.ColumnHeader();
            this.listViewMatchedPlayers = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCountry = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRate1v1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRateTeam = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderTeamGameCount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAllyGames = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEnemyGames = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1v1GameCount = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLastDate = new System.Windows.Forms.ColumnHeader();
            this.formsPlotCountry = new ScottPlot.FormsPlot();
            this.panelPlayers = new System.Windows.Forms.Panel();
            this.checkBoxSetFilter = new System.Windows.Forms.CheckBox();
            this.checkBoxEnableCountryFilter = new System.Windows.Forms.CheckBox();
            this.textBoxFindName = new System.Windows.Forms.TextBox();
            this.labelFind = new System.Windows.Forms.Label();
            this.checkBoxIgnoreCase = new System.Windows.Forms.CheckBox();
            this.tabControlHistory = new System.Windows.Forms.TabControl();
            this.contextMenuStripMatchedPlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMatches)).BeginInit();
            this.splitContainerMatches.Panel1.SuspendLayout();
            this.splitContainerMatches.Panel2.SuspendLayout();
            this.splitContainerMatches.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageStatistics.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlayers)).BeginInit();
            this.splitContainerPlayers.Panel1.SuspendLayout();
            this.splitContainerPlayers.Panel2.SuspendLayout();
            this.splitContainerPlayers.SuspendLayout();
            this.panelPlayers.SuspendLayout();
            this.tabControlHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripMatchedPlayers
            // 
            this.contextMenuStripMatchedPlayers.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripMatchedPlayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAoE2NetProfileToolStripMenuItem,
            this.openHistoryToolStripMenuItem});
            this.contextMenuStripMatchedPlayers.Name = "contextMenuStripMatchedPlayers";
            this.contextMenuStripMatchedPlayers.Size = new System.Drawing.Size(191, 48);
            this.contextMenuStripMatchedPlayers.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripMatchedPlayers_Opening);
            // 
            // openAoE2NetProfileToolStripMenuItem
            // 
            this.openAoE2NetProfileToolStripMenuItem.Name = "openAoE2NetProfileToolStripMenuItem";
            this.openAoE2NetProfileToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openAoE2NetProfileToolStripMenuItem.Text = "Open AoE2.net profile";
            this.openAoE2NetProfileToolStripMenuItem.Click += new System.EventHandler(this.OpenAoE2NetProfileToolStripMenuItem_Click);
            // 
            // openHistoryToolStripMenuItem
            // 
            this.openHistoryToolStripMenuItem.Name = "openHistoryToolStripMenuItem";
            this.openHistoryToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.openHistoryToolStripMenuItem.Text = "Open History";
            this.openHistoryToolStripMenuItem.Click += new System.EventHandler(this.OpenHistoryToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 72);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 72);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPageMatches
            // 
            this.tabPageMatches.Controls.Add(this.panel2);
            this.tabPageMatches.Controls.Add(this.panel1);
            this.tabPageMatches.Location = new System.Drawing.Point(4, 30);
            this.tabPageMatches.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageMatches.Name = "tabPageMatches";
            this.tabPageMatches.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageMatches.Size = new System.Drawing.Size(776, 327);
            this.tabPageMatches.TabIndex = 5;
            this.tabPageMatches.Text = "Matches";
            this.tabPageMatches.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainerMatches);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 45);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(768, 278);
            this.panel2.TabIndex = 11;
            // 
            // splitContainerMatches
            // 
            this.splitContainerMatches.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerMatches.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMatches.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMatches.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerMatches.Name = "splitContainerMatches";
            // 
            // splitContainerMatches.Panel1
            // 
            this.splitContainerMatches.Panel1.Controls.Add(this.listViewMatchHistory);
            // 
            // splitContainerMatches.Panel2
            // 
            this.splitContainerMatches.Panel2.Controls.Add(this.labelDataSource);
            this.splitContainerMatches.Panel2.Controls.Add(this.comboBoxDataSource);
            this.splitContainerMatches.Panel2.Controls.Add(this.formsPlotWinRate);
            this.splitContainerMatches.Size = new System.Drawing.Size(768, 278);
            this.splitContainerMatches.SplitterDistance = 374;
            this.splitContainerMatches.SplitterWidth = 5;
            this.splitContainerMatches.TabIndex = 8;
            // 
            // listViewMatchHistory
            // 
            this.listViewMatchHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewMatchHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMap,
            this.columnHeaderRate,
            this.columnHeaderWin,
            this.columnHeaderCiv,
            this.columnHeaderColor,
            this.columnHeaderDate,
            this.columnHeaderVersion});
            this.listViewMatchHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMatchHistory.FullRowSelect = true;
            this.listViewMatchHistory.GridLines = true;
            this.listViewMatchHistory.Location = new System.Drawing.Point(0, 0);
            this.listViewMatchHistory.Margin = new System.Windows.Forms.Padding(4);
            this.listViewMatchHistory.Name = "listViewMatchHistory";
            this.listViewMatchHistory.Size = new System.Drawing.Size(370, 274);
            this.listViewMatchHistory.TabIndex = 0;
            this.listViewMatchHistory.UseCompatibleStateImageBehavior = false;
            this.listViewMatchHistory.View = System.Windows.Forms.View.Details;
            this.listViewMatchHistory.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewMatchHistory_ColumnClick);
            // 
            // columnHeaderMap
            // 
            this.columnHeaderMap.Text = "Map";
            this.columnHeaderMap.Width = 120;
            // 
            // columnHeaderRate
            // 
            this.columnHeaderRate.Text = "Rate";
            this.columnHeaderRate.Width = 90;
            // 
            // columnHeaderWin
            // 
            this.columnHeaderWin.Text = "Win";
            // 
            // columnHeaderCiv
            // 
            this.columnHeaderCiv.Text = "Civilization";
            this.columnHeaderCiv.Width = 110;
            // 
            // columnHeaderColor
            // 
            this.columnHeaderColor.Text = "Color";
            this.columnHeaderColor.Width = 50;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Date";
            this.columnHeaderDate.Width = 170;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "Version";
            this.columnHeaderVersion.Width = 80;
            // 
            // labelDataSource
            // 
            this.labelDataSource.AutoSize = true;
            this.labelDataSource.Location = new System.Drawing.Point(17, 21);
            this.labelDataSource.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDataSource.Name = "labelDataSource";
            this.labelDataSource.Size = new System.Drawing.Size(92, 21);
            this.labelDataSource.TabIndex = 10;
            this.labelDataSource.Text = "Data source";
            // 
            // comboBoxDataSource
            // 
            this.comboBoxDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataSource.Enabled = false;
            this.comboBoxDataSource.FormattingEnabled = true;
            this.comboBoxDataSource.Location = new System.Drawing.Point(123, 17);
            this.comboBoxDataSource.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxDataSource.Name = "comboBoxDataSource";
            this.comboBoxDataSource.Size = new System.Drawing.Size(140, 29);
            this.comboBoxDataSource.TabIndex = 7;
            this.comboBoxDataSource.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDataSource_SelectedIndexChanged);
            // 
            // formsPlotWinRate
            // 
            this.formsPlotWinRate.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotWinRate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.formsPlotWinRate.Location = new System.Drawing.Point(0, 0);
            this.formsPlotWinRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRate.Name = "formsPlotWinRate";
            this.formsPlotWinRate.Size = new System.Drawing.Size(385, 274);
            this.formsPlotWinRate.TabIndex = 6;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelComboBoxLeaderboard);
            this.panel1.Controls.Add(this.comboBoxLeaderboard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 41);
            this.panel1.TabIndex = 10;
            // 
            // labelComboBoxLeaderboard
            // 
            this.labelComboBoxLeaderboard.AutoSize = true;
            this.labelComboBoxLeaderboard.Location = new System.Drawing.Point(8, 8);
            this.labelComboBoxLeaderboard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelComboBoxLeaderboard.Name = "labelComboBoxLeaderboard";
            this.labelComboBoxLeaderboard.Size = new System.Drawing.Size(98, 21);
            this.labelComboBoxLeaderboard.TabIndex = 9;
            this.labelComboBoxLeaderboard.Text = "Leaderboard";
            // 
            // comboBoxLeaderboard
            // 
            this.comboBoxLeaderboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLeaderboard.Enabled = false;
            this.comboBoxLeaderboard.FormattingEnabled = true;
            this.comboBoxLeaderboard.Location = new System.Drawing.Point(131, 4);
            this.comboBoxLeaderboard.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxLeaderboard.Name = "comboBoxLeaderboard";
            this.comboBoxLeaderboard.Size = new System.Drawing.Size(201, 29);
            this.comboBoxLeaderboard.TabIndex = 8;
            this.comboBoxLeaderboard.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLeaderboard_SelectedIndexChanged);
            // 
            // tabPageStatistics
            // 
            this.tabPageStatistics.Controls.Add(this.formsPlotPlayerRate);
            this.tabPageStatistics.Controls.Add(this.listViewStatistics);
            this.tabPageStatistics.Location = new System.Drawing.Point(4, 24);
            this.tabPageStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageStatistics.Name = "tabPageStatistics";
            this.tabPageStatistics.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageStatistics.Size = new System.Drawing.Size(1315, 570);
            this.tabPageStatistics.TabIndex = 1;
            this.tabPageStatistics.Text = "Statistics";
            this.tabPageStatistics.UseVisualStyleBackColor = true;
            // 
            // formsPlotPlayerRate
            // 
            this.formsPlotPlayerRate.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotPlayerRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotPlayerRate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.formsPlotPlayerRate.Location = new System.Drawing.Point(4, 233);
            this.formsPlotPlayerRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotPlayerRate.Name = "formsPlotPlayerRate";
            this.formsPlotPlayerRate.Size = new System.Drawing.Size(1307, 333);
            this.formsPlotPlayerRate.TabIndex = 6;
            this.formsPlotPlayerRate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormsPlotPlayerRate_MouseMove);
            // 
            // listViewStatistics
            // 
            this.listViewStatistics.CheckBoxes = true;
            this.listViewStatistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLeaderboard,
            this.columnHeaderRank,
            this.columnHeaderRating,
            this.columnHeaderHighestRating,
            this.columnHeaderGames,
            this.columnHeaderWinRate,
            this.columnHeaderWins,
            this.columnHeaderLosses,
            this.columnHeaderDrop,
            this.columnHeaderStreak,
            this.columnHeaderHighestStreak,
            this.columnHeaderLowestStreak});
            this.listViewStatistics.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewStatistics.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listViewStatistics.FullRowSelect = true;
            this.listViewStatistics.GridLines = true;
            this.listViewStatistics.Location = new System.Drawing.Point(4, 4);
            this.listViewStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.listViewStatistics.Name = "listViewStatistics";
            this.listViewStatistics.Size = new System.Drawing.Size(1307, 229);
            this.listViewStatistics.TabIndex = 2;
            this.listViewStatistics.UseCompatibleStateImageBehavior = false;
            this.listViewStatistics.View = System.Windows.Forms.View.Details;
            this.listViewStatistics.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewStatistics_ItemChecked);
            this.listViewStatistics.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewStatistics_KeyDown);
            // 
            // columnHeaderLeaderboard
            // 
            this.columnHeaderLeaderboard.Text = "Leaderboard";
            this.columnHeaderLeaderboard.Width = 110;
            // 
            // columnHeaderRank
            // 
            this.columnHeaderRank.Text = "Rank";
            // 
            // columnHeaderRating
            // 
            this.columnHeaderRating.Text = "Rate";
            // 
            // columnHeaderHighestRating
            // 
            this.columnHeaderHighestRating.Text = "Highest Rating";
            this.columnHeaderHighestRating.Width = 120;
            // 
            // columnHeaderGames
            // 
            this.columnHeaderGames.Text = "Games";
            this.columnHeaderGames.Width = 70;
            // 
            // columnHeaderWinRate
            // 
            this.columnHeaderWinRate.Text = "Win%";
            // 
            // columnHeaderWins
            // 
            this.columnHeaderWins.Text = "Wins";
            // 
            // columnHeaderLosses
            // 
            this.columnHeaderLosses.Text = "Losses";
            // 
            // columnHeaderDrop
            // 
            this.columnHeaderDrop.Text = "Drop";
            // 
            // columnHeaderStreak
            // 
            this.columnHeaderStreak.Text = "Streak";
            // 
            // columnHeaderHighestStreak
            // 
            this.columnHeaderHighestStreak.Text = "HighestStreak";
            this.columnHeaderHighestStreak.Width = 120;
            // 
            // columnHeaderLowestStreak
            // 
            this.columnHeaderLowestStreak.Text = "LowestStreak";
            this.columnHeaderLowestStreak.Width = 120;
            // 
            // tabPagePlayers
            // 
            this.tabPagePlayers.Controls.Add(this.panel4);
            this.tabPagePlayers.Controls.Add(this.panelPlayers);
            this.tabPagePlayers.Location = new System.Drawing.Point(4, 24);
            this.tabPagePlayers.Margin = new System.Windows.Forms.Padding(4);
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.Padding = new System.Windows.Forms.Padding(4);
            this.tabPagePlayers.Size = new System.Drawing.Size(1315, 570);
            this.tabPagePlayers.TabIndex = 3;
            this.tabPagePlayers.Text = "Players";
            this.tabPagePlayers.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.splitContainerPlayers);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 50);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1307, 516);
            this.panel4.TabIndex = 15;
            // 
            // splitContainerPlayers
            // 
            this.splitContainerPlayers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPlayers.Location = new System.Drawing.Point(0, 0);
            this.splitContainerPlayers.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainerPlayers.Name = "splitContainerPlayers";
            // 
            // splitContainerPlayers.Panel1
            // 
            this.splitContainerPlayers.Panel1.Controls.Add(this.listViewFilterCountry);
            this.splitContainerPlayers.Panel1.Controls.Add(this.listViewMatchedPlayers);
            // 
            // splitContainerPlayers.Panel2
            // 
            this.splitContainerPlayers.Panel2.Controls.Add(this.formsPlotCountry);
            this.splitContainerPlayers.Size = new System.Drawing.Size(1307, 516);
            this.splitContainerPlayers.SplitterDistance = 845;
            this.splitContainerPlayers.SplitterWidth = 5;
            this.splitContainerPlayers.TabIndex = 9;
            this.splitContainerPlayers.DoubleClick += new System.EventHandler(this.SplitContainerPlayers_DoubleClick);
            // 
            // listViewFilterCountry
            // 
            this.listViewFilterCountry.CheckBoxes = true;
            this.listViewFilterCountry.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Country});
            this.listViewFilterCountry.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listViewFilterCountry.GridLines = true;
            this.listViewFilterCountry.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewFilterCountry.Location = new System.Drawing.Point(225, 45);
            this.listViewFilterCountry.Margin = new System.Windows.Forms.Padding(4);
            this.listViewFilterCountry.MultiSelect = false;
            this.listViewFilterCountry.Name = "listViewFilterCountry";
            this.listViewFilterCountry.Size = new System.Drawing.Size(1052, 565);
            this.listViewFilterCountry.TabIndex = 14;
            this.listViewFilterCountry.UseCompatibleStateImageBehavior = false;
            this.listViewFilterCountry.View = System.Windows.Forms.View.List;
            this.listViewFilterCountry.Visible = false;
            this.listViewFilterCountry.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewFilterCountory_ItemChecked);
            // 
            // Country
            // 
            this.Country.Text = "Country";
            this.Country.Width = 200;
            // 
            // listViewMatchedPlayers
            // 
            this.listViewMatchedPlayers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewMatchedPlayers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderCountry,
            this.columnHeaderRate1v1,
            this.columnHeaderRateTeam,
            this.columnHeaderTeamGameCount,
            this.columnHeaderAllyGames,
            this.columnHeaderEnemyGames,
            this.columnHeader1v1GameCount,
            this.columnHeaderLastDate});
            this.listViewMatchedPlayers.ContextMenuStrip = this.contextMenuStripMatchedPlayers;
            this.listViewMatchedPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMatchedPlayers.FullRowSelect = true;
            this.listViewMatchedPlayers.GridLines = true;
            this.listViewMatchedPlayers.Location = new System.Drawing.Point(0, 0);
            this.listViewMatchedPlayers.Margin = new System.Windows.Forms.Padding(4);
            this.listViewMatchedPlayers.Name = "listViewMatchedPlayers";
            this.listViewMatchedPlayers.Size = new System.Drawing.Size(841, 512);
            this.listViewMatchedPlayers.TabIndex = 2;
            this.listViewMatchedPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewMatchedPlayers.View = System.Windows.Forms.View.Details;
            this.listViewMatchedPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewMatchedPlayers_ColumnClick);
            this.listViewMatchedPlayers.Enter += new System.EventHandler(this.ListViewMatchedPlayers_Enter);
            this.listViewMatchedPlayers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewMatchedPlayers_MouseDoubleClick);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 160;
            // 
            // columnHeaderCountry
            // 
            this.columnHeaderCountry.Text = "Country";
            this.columnHeaderCountry.Width = 90;
            // 
            // columnHeaderRate1v1
            // 
            this.columnHeaderRate1v1.Text = "1v1 Rate";
            this.columnHeaderRate1v1.Width = 90;
            // 
            // columnHeaderRateTeam
            // 
            this.columnHeaderRateTeam.Text = "Team Rate";
            this.columnHeaderRateTeam.Width = 90;
            // 
            // columnHeaderTeamGameCount
            // 
            this.columnHeaderTeamGameCount.Text = "Team Games";
            this.columnHeaderTeamGameCount.Width = 100;
            // 
            // columnHeaderAllyGames
            // 
            this.columnHeaderAllyGames.Text = "Ally";
            this.columnHeaderAllyGames.Width = 40;
            // 
            // columnHeaderEnemyGames
            // 
            this.columnHeaderEnemyGames.Text = "Enemy";
            // 
            // columnHeader1v1GameCount
            // 
            this.columnHeader1v1GameCount.Text = "1v1";
            this.columnHeader1v1GameCount.Width = 40;
            // 
            // columnHeaderLastDate
            // 
            this.columnHeaderLastDate.Text = "Last Date";
            this.columnHeaderLastDate.Width = 170;
            // 
            // formsPlotCountry
            // 
            this.formsPlotCountry.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCountry.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.formsPlotCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCountry.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.formsPlotCountry.Location = new System.Drawing.Point(0, 0);
            this.formsPlotCountry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCountry.Name = "formsPlotCountry";
            this.formsPlotCountry.Size = new System.Drawing.Size(453, 512);
            this.formsPlotCountry.TabIndex = 8;
            // 
            // panelPlayers
            // 
            this.panelPlayers.Controls.Add(this.checkBoxSetFilter);
            this.panelPlayers.Controls.Add(this.checkBoxEnableCountryFilter);
            this.panelPlayers.Controls.Add(this.textBoxFindName);
            this.panelPlayers.Controls.Add(this.labelFind);
            this.panelPlayers.Controls.Add(this.checkBoxIgnoreCase);
            this.panelPlayers.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPlayers.Location = new System.Drawing.Point(4, 4);
            this.panelPlayers.Margin = new System.Windows.Forms.Padding(4);
            this.panelPlayers.Name = "panelPlayers";
            this.panelPlayers.Size = new System.Drawing.Size(1307, 46);
            this.panelPlayers.TabIndex = 14;
            // 
            // checkBoxSetFilter
            // 
            this.checkBoxSetFilter.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxSetFilter.AutoSize = true;
            this.checkBoxSetFilter.Location = new System.Drawing.Point(608, 5);
            this.checkBoxSetFilter.Name = "checkBoxSetFilter";
            this.checkBoxSetFilter.Size = new System.Drawing.Size(79, 31);
            this.checkBoxSetFilter.TabIndex = 16;
            this.checkBoxSetFilter.Text = "Set filter";
            this.checkBoxSetFilter.UseVisualStyleBackColor = true;
            this.checkBoxSetFilter.CheckedChanged += new System.EventHandler(this.CheckBoxFilter_CheckedChanged);
            // 
            // checkBoxEnableCountryFilter
            // 
            this.checkBoxEnableCountryFilter.AutoSize = true;
            this.checkBoxEnableCountryFilter.Location = new System.Drawing.Point(436, 8);
            this.checkBoxEnableCountryFilter.Name = "checkBoxEnableCountryFilter";
            this.checkBoxEnableCountryFilter.Size = new System.Drawing.Size(169, 25);
            this.checkBoxEnableCountryFilter.TabIndex = 14;
            this.checkBoxEnableCountryFilter.Text = "Enable country filter";
            this.checkBoxEnableCountryFilter.UseVisualStyleBackColor = true;
            this.checkBoxEnableCountryFilter.CheckedChanged += new System.EventHandler(this.CheckBoxEnableCountryFilter_CheckedChanged);
            // 
            // textBoxFindName
            // 
            this.textBoxFindName.Location = new System.Drawing.Point(51, 4);
            this.textBoxFindName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxFindName.Name = "textBoxFindName";
            this.textBoxFindName.Size = new System.Drawing.Size(233, 29);
            this.textBoxFindName.TabIndex = 12;
            this.textBoxFindName.TextChanged += new System.EventHandler(this.TextBoxFindName_TextChanged);
            // 
            // labelFind
            // 
            this.labelFind.AutoSize = true;
            this.labelFind.Location = new System.Drawing.Point(5, 10);
            this.labelFind.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFind.Name = "labelFind";
            this.labelFind.Size = new System.Drawing.Size(40, 21);
            this.labelFind.TabIndex = 11;
            this.labelFind.Text = "Find";
            // 
            // checkBoxIgnoreCase
            // 
            this.checkBoxIgnoreCase.AutoSize = true;
            this.checkBoxIgnoreCase.Checked = true;
            this.checkBoxIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIgnoreCase.Location = new System.Drawing.Point(309, 7);
            this.checkBoxIgnoreCase.Margin = new System.Windows.Forms.Padding(4);
            this.checkBoxIgnoreCase.Name = "checkBoxIgnoreCase";
            this.checkBoxIgnoreCase.Size = new System.Drawing.Size(108, 25);
            this.checkBoxIgnoreCase.TabIndex = 13;
            this.checkBoxIgnoreCase.Text = "Ignore case";
            this.checkBoxIgnoreCase.UseVisualStyleBackColor = true;
            this.checkBoxIgnoreCase.CheckedChanged += new System.EventHandler(this.CheckBoxIgnoreCase_CheckedChanged);
            // 
            // tabControlHistory
            // 
            this.tabControlHistory.Controls.Add(this.tabPageMatches);
            this.tabControlHistory.Controls.Add(this.tabPagePlayers);
            this.tabControlHistory.Controls.Add(this.tabPageStatistics);
            this.tabControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHistory.Location = new System.Drawing.Point(0, 0);
            this.tabControlHistory.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlHistory.Name = "tabControlHistory";
            this.tabControlHistory.SelectedIndex = 0;
            this.tabControlHistory.Size = new System.Drawing.Size(784, 361);
            this.tabControlHistory.TabIndex = 3;
            this.tabControlHistory.SelectedIndexChanged += new System.EventHandler(this.TabControlHistory_SelectedIndexChanged);
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.tabControlHistory);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "FormHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AoE2.net Desktop - History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHistory_FormClosing);
            this.Load += new System.EventHandler(this.FormHistory_Load);
            this.Shown += new System.EventHandler(this.FormHistory_ShownAsync);
            this.contextMenuStripMatchedPlayers.ResumeLayout(false);
            this.tabPageMatches.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainerMatches.Panel1.ResumeLayout(false);
            this.splitContainerMatches.Panel2.ResumeLayout(false);
            this.splitContainerMatches.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMatches)).EndInit();
            this.splitContainerMatches.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPageStatistics.ResumeLayout(false);
            this.tabPagePlayers.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.splitContainerPlayers.Panel1.ResumeLayout(false);
            this.splitContainerPlayers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlayers)).EndInit();
            this.splitContainerPlayers.ResumeLayout(false);
            this.panelPlayers.ResumeLayout(false);
            this.panelPlayers.PerformLayout();
            this.tabControlHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMatchedPlayers;
        private System.Windows.Forms.ToolStripMenuItem openAoE2NetProfileToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPageMatches;
        private System.Windows.Forms.Label labelComboBoxLeaderboard;
        private System.Windows.Forms.SplitContainer splitContainerMatches;
        private System.Windows.Forms.ListView listViewMatchHistory;
        private System.Windows.Forms.ColumnHeader columnHeaderMap;
        private System.Windows.Forms.ColumnHeader columnHeaderRate;
        private System.Windows.Forms.ColumnHeader columnHeaderWin;
        private System.Windows.Forms.ColumnHeader columnHeaderCiv;
        private System.Windows.Forms.ColumnHeader columnHeaderColor;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.Label labelDataSource;
        private System.Windows.Forms.ComboBox comboBoxDataSource;
        private ScottPlot.FormsPlot formsPlotWinRate;
        private System.Windows.Forms.ComboBox comboBoxLeaderboard;
        private System.Windows.Forms.TabPage tabPageStatistics;
        private System.Windows.Forms.ListView listViewStatistics;
        private System.Windows.Forms.ColumnHeader columnHeaderLeaderboard;
        private System.Windows.Forms.ColumnHeader columnHeaderRank;
        private System.Windows.Forms.ColumnHeader columnHeaderRating;
        private System.Windows.Forms.ColumnHeader columnHeaderHighestRating;
        private System.Windows.Forms.ColumnHeader columnHeaderGames;
        private System.Windows.Forms.ColumnHeader columnHeaderWinRate;
        private System.Windows.Forms.ColumnHeader columnHeaderWins;
        private System.Windows.Forms.ColumnHeader columnHeaderLosses;
        private System.Windows.Forms.ColumnHeader columnHeaderDrop;
        private System.Windows.Forms.ColumnHeader columnHeaderStreak;
        private System.Windows.Forms.ColumnHeader columnHeaderHighestStreak;
        private System.Windows.Forms.ColumnHeader columnHeaderLowestStreak;
        private System.Windows.Forms.TabPage tabPagePlayers;
        private System.Windows.Forms.SplitContainer splitContainerPlayers;
        private System.Windows.Forms.ListView listViewMatchedPlayers;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderCountry;
        private System.Windows.Forms.ColumnHeader columnHeaderRate1v1;
        private System.Windows.Forms.ColumnHeader columnHeaderRateTeam;
        private System.Windows.Forms.ColumnHeader columnHeaderTeamGameCount;
        private System.Windows.Forms.ColumnHeader columnHeaderAllyGames;
        private System.Windows.Forms.ColumnHeader columnHeaderEnemyGames;
        private System.Windows.Forms.ColumnHeader columnHeader1v1GameCount;
        private System.Windows.Forms.ColumnHeader columnHeaderLastDate;
        private ScottPlot.FormsPlot formsPlotCountry;
        private System.Windows.Forms.TabControl tabControlHistory;
        private ScottPlot.FormsPlot formsPlotPlayerRate;
        private System.Windows.Forms.ToolStripMenuItem openHistoryToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxFindName;
        private System.Windows.Forms.Label labelFind;
        private System.Windows.Forms.CheckBox checkBoxIgnoreCase;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panelPlayers;
        private System.Windows.Forms.ColumnHeader Country;
        private System.Windows.Forms.ListView listViewFilterCountry;
        private System.Windows.Forms.CheckBox checkBoxEnableCountryFilter;
        private System.Windows.Forms.CheckBox checkBoxSetFilter;
    }
}