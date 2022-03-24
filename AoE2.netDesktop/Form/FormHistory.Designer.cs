
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
            this.labelComboBoxLeaderboard = new System.Windows.Forms.Label();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
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
            this.splitContainerPlayers = new System.Windows.Forms.SplitContainer();
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
            this.tabControlHistory = new System.Windows.Forms.TabControl();
            this.contextMenuStripMatchedPlayers.SuspendLayout();
            this.tabPageMatches.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.tabPageStatistics.SuspendLayout();
            this.tabPagePlayers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlayers)).BeginInit();
            this.splitContainerPlayers.Panel1.SuspendLayout();
            this.splitContainerPlayers.Panel2.SuspendLayout();
            this.splitContainerPlayers.SuspendLayout();
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
            this.tabPageMatches.Controls.Add(this.labelComboBoxLeaderboard);
            this.tabPageMatches.Controls.Add(this.splitContainer5);
            this.tabPageMatches.Controls.Add(this.comboBoxLeaderboard);
            this.tabPageMatches.Location = new System.Drawing.Point(4, 24);
            this.tabPageMatches.Name = "tabPageMatches";
            this.tabPageMatches.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatches.Size = new System.Drawing.Size(1260, 715);
            this.tabPageMatches.TabIndex = 5;
            this.tabPageMatches.Text = "Matches";
            this.tabPageMatches.UseVisualStyleBackColor = true;
            // 
            // labelComboBoxLeaderboard
            // 
            this.labelComboBoxLeaderboard.AutoSize = true;
            this.labelComboBoxLeaderboard.Location = new System.Drawing.Point(8, 13);
            this.labelComboBoxLeaderboard.Name = "labelComboBoxLeaderboard";
            this.labelComboBoxLeaderboard.Size = new System.Drawing.Size(73, 15);
            this.labelComboBoxLeaderboard.TabIndex = 9;
            this.labelComboBoxLeaderboard.Text = "Leaderboard";
            // 
            // splitContainer5
            // 
            this.splitContainer5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer5.Location = new System.Drawing.Point(3, 42);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.listViewMatchHistory);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.labelDataSource);
            this.splitContainer5.Panel2.Controls.Add(this.comboBoxDataSource);
            this.splitContainer5.Panel2.Controls.Add(this.formsPlotWinRate);
            this.splitContainer5.Size = new System.Drawing.Size(1254, 670);
            this.splitContainer5.SplitterDistance = 616;
            this.splitContainer5.TabIndex = 8;
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
            this.listViewMatchHistory.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listViewMatchHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMatchHistory.FullRowSelect = true;
            this.listViewMatchHistory.GridLines = true;
            this.listViewMatchHistory.Location = new System.Drawing.Point(0, 0);
            this.listViewMatchHistory.Name = "listViewMatchHistory";
            this.listViewMatchHistory.Size = new System.Drawing.Size(612, 666);
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
            this.columnHeaderDate.Width = 150;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "Version";
            this.columnHeaderVersion.Width = 80;
            // 
            // labelDataSource
            // 
            this.labelDataSource.AutoSize = true;
            this.labelDataSource.Location = new System.Drawing.Point(13, 15);
            this.labelDataSource.Name = "labelDataSource";
            this.labelDataSource.Size = new System.Drawing.Size(69, 15);
            this.labelDataSource.TabIndex = 10;
            this.labelDataSource.Text = "Data source";
            // 
            // comboBoxDataSource
            // 
            this.comboBoxDataSource.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.comboBoxDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDataSource.Enabled = false;
            this.comboBoxDataSource.FormattingEnabled = true;
            this.comboBoxDataSource.Location = new System.Drawing.Point(96, 12);
            this.comboBoxDataSource.Name = "comboBoxDataSource";
            this.comboBoxDataSource.Size = new System.Drawing.Size(110, 23);
            this.comboBoxDataSource.TabIndex = 7;
            this.comboBoxDataSource.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDataSource_SelectedIndexChanged);
            // 
            // formsPlotWinRate
            // 
            this.formsPlotWinRate.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRate.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.formsPlotWinRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotWinRate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.formsPlotWinRate.Location = new System.Drawing.Point(0, 0);
            this.formsPlotWinRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRate.Name = "formsPlotWinRate";
            this.formsPlotWinRate.Size = new System.Drawing.Size(630, 666);
            this.formsPlotWinRate.TabIndex = 6;
            // 
            // comboBoxLeaderboard
            // 
            this.comboBoxLeaderboard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLeaderboard.Enabled = false;
            this.comboBoxLeaderboard.FormattingEnabled = true;
            this.comboBoxLeaderboard.Location = new System.Drawing.Point(104, 10);
            this.comboBoxLeaderboard.Name = "comboBoxLeaderboard";
            this.comboBoxLeaderboard.Size = new System.Drawing.Size(157, 23);
            this.comboBoxLeaderboard.TabIndex = 8;
            this.comboBoxLeaderboard.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLeaderboard_SelectedIndexChanged);
            // 
            // tabPageStatistics
            // 
            this.tabPageStatistics.Controls.Add(this.formsPlotPlayerRate);
            this.tabPageStatistics.Controls.Add(this.listViewStatistics);
            this.tabPageStatistics.Location = new System.Drawing.Point(4, 24);
            this.tabPageStatistics.Name = "tabPageStatistics";
            this.tabPageStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatistics.Size = new System.Drawing.Size(1260, 715);
            this.tabPageStatistics.TabIndex = 1;
            this.tabPageStatistics.Text = "Statistics";
            this.tabPageStatistics.UseVisualStyleBackColor = true;
            // 
            // formsPlotPlayerRate
            // 
            this.formsPlotPlayerRate.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotPlayerRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotPlayerRate.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.formsPlotPlayerRate.Location = new System.Drawing.Point(3, 168);
            this.formsPlotPlayerRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotPlayerRate.Name = "formsPlotPlayerRate";
            this.formsPlotPlayerRate.Size = new System.Drawing.Size(1254, 544);
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
            this.listViewStatistics.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.listViewStatistics.FullRowSelect = true;
            this.listViewStatistics.GridLines = true;
            this.listViewStatistics.Location = new System.Drawing.Point(3, 3);
            this.listViewStatistics.Name = "listViewStatistics";
            this.listViewStatistics.Size = new System.Drawing.Size(1254, 165);
            this.listViewStatistics.TabIndex = 2;
            this.listViewStatistics.UseCompatibleStateImageBehavior = false;
            this.listViewStatistics.View = System.Windows.Forms.View.Details;
            this.listViewStatistics.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewStatistics_ItemChecked);
            this.listViewStatistics.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListViewStatistics_KeyDown);
            // 
            // columnHeaderLeaderboard
            // 
            this.columnHeaderLeaderboard.Text = "Leaderboard";
            this.columnHeaderLeaderboard.Width = 100;
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
            this.tabPagePlayers.Controls.Add(this.splitContainerPlayers);
            this.tabPagePlayers.Location = new System.Drawing.Point(4, 24);
            this.tabPagePlayers.Name = "tabPagePlayers";
            this.tabPagePlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlayers.Size = new System.Drawing.Size(1260, 715);
            this.tabPagePlayers.TabIndex = 3;
            this.tabPagePlayers.Text = "Players";
            this.tabPagePlayers.UseVisualStyleBackColor = true;
            // 
            // splitContainerPlayers
            // 
            this.splitContainerPlayers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainerPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerPlayers.Location = new System.Drawing.Point(3, 3);
            this.splitContainerPlayers.Name = "splitContainerPlayers";
            // 
            // splitContainerPlayers.Panel1
            // 
            this.splitContainerPlayers.Panel1.Controls.Add(this.listViewMatchedPlayers);
            // 
            // splitContainerPlayers.Panel2
            // 
            this.splitContainerPlayers.Panel2.Controls.Add(this.formsPlotCountry);
            this.splitContainerPlayers.Size = new System.Drawing.Size(1254, 709);
            this.splitContainerPlayers.SplitterDistance = 738;
            this.splitContainerPlayers.TabIndex = 9;
            this.splitContainerPlayers.DoubleClick += new System.EventHandler(this.SplitContainerPlayers_DoubleClick);
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
            this.listViewMatchedPlayers.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.listViewMatchedPlayers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewMatchedPlayers.FullRowSelect = true;
            this.listViewMatchedPlayers.GridLines = true;
            this.listViewMatchedPlayers.Location = new System.Drawing.Point(0, 0);
            this.listViewMatchedPlayers.Name = "listViewMatchedPlayers";
            this.listViewMatchedPlayers.Size = new System.Drawing.Size(734, 705);
            this.listViewMatchedPlayers.TabIndex = 2;
            this.listViewMatchedPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewMatchedPlayers.View = System.Windows.Forms.View.Details;
            this.listViewMatchedPlayers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewMatchedPlayers_ColumnClick);
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
            this.columnHeaderLastDate.Width = 150;
            // 
            // formsPlotCountry
            // 
            this.formsPlotCountry.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCountry.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.formsPlotCountry.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.formsPlotCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCountry.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.formsPlotCountry.Location = new System.Drawing.Point(0, 0);
            this.formsPlotCountry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCountry.Name = "formsPlotCountry";
            this.formsPlotCountry.Size = new System.Drawing.Size(508, 705);
            this.formsPlotCountry.TabIndex = 8;
            // 
            // tabControlHistory
            // 
            this.tabControlHistory.Controls.Add(this.tabPageMatches);
            this.tabControlHistory.Controls.Add(this.tabPagePlayers);
            this.tabControlHistory.Controls.Add(this.tabPageStatistics);
            this.tabControlHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlHistory.Location = new System.Drawing.Point(0, 0);
            this.tabControlHistory.Name = "tabControlHistory";
            this.tabControlHistory.SelectedIndex = 0;
            this.tabControlHistory.Size = new System.Drawing.Size(1268, 743);
            this.tabControlHistory.TabIndex = 3;
            this.tabControlHistory.SelectedIndexChanged += new System.EventHandler(this.TabControlHistory_SelectedIndexChanged);
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 743);
            this.Controls.Add(this.tabControlHistory);
            this.Name = "FormHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AoE2.net Desktop - History";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormHistory_FormClosing);
            this.Load += new System.EventHandler(this.FormHistory_Load);
            this.Shown += new System.EventHandler(this.FormHistory_ShownAsync);
            this.contextMenuStripMatchedPlayers.ResumeLayout(false);
            this.tabPageMatches.ResumeLayout(false);
            this.tabPageMatches.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.tabPageStatistics.ResumeLayout(false);
            this.tabPagePlayers.ResumeLayout(false);
            this.splitContainerPlayers.Panel1.ResumeLayout(false);
            this.splitContainerPlayers.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerPlayers)).EndInit();
            this.splitContainerPlayers.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer5;
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
    }
}