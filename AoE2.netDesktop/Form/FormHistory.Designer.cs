
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
            this.listViewHistory1v1 = new System.Windows.Forms.ListView();
            this.columnHeaderMap = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWin = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCiv = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColor = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVersion = new System.Windows.Forms.ColumnHeader();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1v1RandomMap = new System.Windows.Forms.TabPage();
            this.tabPageTeamRandomMap = new System.Windows.Forms.TabPage();
            this.listViewHistoryTeam = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.tabPageStatistics = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.formsPlotRate = new ScottPlot.FormsPlot();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.formsPlotMapRate1v1 = new ScottPlot.FormsPlot();
            this.formsPlotWinRate1v1EachMap = new ScottPlot.FormsPlot();
            this.formsPlotMapRateTeam = new ScottPlot.FormsPlot();
            this.formsPlotWinRateTeamEachMap = new ScottPlot.FormsPlot();
            this.listViewStatistics = new System.Windows.Forms.ListView();
            this.columnHeaderLeaderboard = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRank = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRating = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderHighestRating = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderGames = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWins = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLosses = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWinRate = new System.Windows.Forms.ColumnHeader();
            this.tabControl1.SuspendLayout();
            this.tabPage1v1RandomMap.SuspendLayout();
            this.tabPageTeamRandomMap.SuspendLayout();
            this.tabPageStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewHistory1v1
            // 
            this.listViewHistory1v1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHistory1v1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewHistory1v1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMap,
            this.columnHeaderRate,
            this.columnHeaderWin,
            this.columnHeaderCiv,
            this.columnHeaderColor,
            this.columnHeaderDate,
            this.columnHeaderVersion});
            this.listViewHistory1v1.FullRowSelect = true;
            this.listViewHistory1v1.GridLines = true;
            this.listViewHistory1v1.HideSelection = false;
            this.listViewHistory1v1.Location = new System.Drawing.Point(3, 3);
            this.listViewHistory1v1.Name = "listViewHistory1v1";
            this.listViewHistory1v1.Size = new System.Drawing.Size(1099, 775);
            this.listViewHistory1v1.TabIndex = 0;
            this.listViewHistory1v1.UseCompatibleStateImageBehavior = false;
            this.listViewHistory1v1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderMap
            // 
            this.columnHeaderMap.Text = "Map";
            this.columnHeaderMap.Width = 120;
            // 
            // columnHeaderRate
            // 
            this.columnHeaderRate.Text = "Rate";
            // 
            // columnHeaderWin
            // 
            this.columnHeaderWin.Text = "Win";
            this.columnHeaderWin.Width = 40;
            // 
            // columnHeaderCiv
            // 
            this.columnHeaderCiv.Text = "Civilization";
            this.columnHeaderCiv.Width = 90;
            // 
            // columnHeaderColor
            // 
            this.columnHeaderColor.Text = "Color";
            this.columnHeaderColor.Width = 50;
            // 
            // columnHeaderDate
            // 
            this.columnHeaderDate.Text = "Date";
            this.columnHeaderDate.Width = 120;
            // 
            // columnHeaderVersion
            // 
            this.columnHeaderVersion.Text = "Version";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1v1RandomMap);
            this.tabControl1.Controls.Add(this.tabPageTeamRandomMap);
            this.tabControl1.Controls.Add(this.tabPageStatistics);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1275, 790);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1v1RandomMap
            // 
            this.tabPage1v1RandomMap.Controls.Add(this.listViewHistory1v1);
            this.tabPage1v1RandomMap.Location = new System.Drawing.Point(4, 24);
            this.tabPage1v1RandomMap.Name = "tabPage1v1RandomMap";
            this.tabPage1v1RandomMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1v1RandomMap.Size = new System.Drawing.Size(1267, 762);
            this.tabPage1v1RandomMap.TabIndex = 0;
            this.tabPage1v1RandomMap.Text = "1v1 Random Map";
            this.tabPage1v1RandomMap.UseVisualStyleBackColor = true;
            // 
            // tabPageTeamRandomMap
            // 
            this.tabPageTeamRandomMap.Controls.Add(this.listViewHistoryTeam);
            this.tabPageTeamRandomMap.Location = new System.Drawing.Point(4, 24);
            this.tabPageTeamRandomMap.Name = "tabPageTeamRandomMap";
            this.tabPageTeamRandomMap.Size = new System.Drawing.Size(1267, 762);
            this.tabPageTeamRandomMap.TabIndex = 2;
            this.tabPageTeamRandomMap.Text = "Team Random Map";
            this.tabPageTeamRandomMap.UseVisualStyleBackColor = true;
            // 
            // listViewHistoryTeam
            // 
            this.listViewHistoryTeam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewHistoryTeam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewHistoryTeam.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listViewHistoryTeam.FullRowSelect = true;
            this.listViewHistoryTeam.GridLines = true;
            this.listViewHistoryTeam.HideSelection = false;
            this.listViewHistoryTeam.Location = new System.Drawing.Point(3, 3);
            this.listViewHistoryTeam.Name = "listViewHistoryTeam";
            this.listViewHistoryTeam.Size = new System.Drawing.Size(1099, 775);
            this.listViewHistoryTeam.TabIndex = 1;
            this.listViewHistoryTeam.UseCompatibleStateImageBehavior = false;
            this.listViewHistoryTeam.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Map";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Rate";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Win";
            this.columnHeader3.Width = 40;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Civilization";
            this.columnHeader4.Width = 90;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Color";
            this.columnHeader5.Width = 50;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date";
            this.columnHeader6.Width = 120;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Version";
            // 
            // tabPageStatistics
            // 
            this.tabPageStatistics.Controls.Add(this.splitContainer1);
            this.tabPageStatistics.Controls.Add(this.listViewStatistics);
            this.tabPageStatistics.Location = new System.Drawing.Point(4, 24);
            this.tabPageStatistics.Name = "tabPageStatistics";
            this.tabPageStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatistics.Size = new System.Drawing.Size(1267, 762);
            this.tabPageStatistics.TabIndex = 1;
            this.tabPageStatistics.Text = "Statistics";
            this.tabPageStatistics.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Location = new System.Drawing.Point(0, 90);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.formsPlotRate);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1267, 672);
            this.splitContainer1.SplitterDistance = 496;
            this.splitContainer1.TabIndex = 6;
            // 
            // formsPlotRate
            // 
            this.formsPlotRate.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotRate.Location = new System.Drawing.Point(0, 0);
            this.formsPlotRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotRate.Name = "formsPlotRate";
            this.formsPlotRate.Size = new System.Drawing.Size(494, 670);
            this.formsPlotRate.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.formsPlotMapRate1v1);
            this.splitContainer2.Panel1.Controls.Add(this.formsPlotWinRate1v1EachMap);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.formsPlotMapRateTeam);
            this.splitContainer2.Panel2.Controls.Add(this.formsPlotWinRateTeamEachMap);
            this.splitContainer2.Size = new System.Drawing.Size(767, 672);
            this.splitContainer2.SplitterDistance = 322;
            this.splitContainer2.TabIndex = 6;
            // 
            // formsPlotMapRate1v1
            // 
            this.formsPlotMapRate1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotMapRate1v1.Dock = System.Windows.Forms.DockStyle.Left;
            this.formsPlotMapRate1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotMapRate1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotMapRate1v1.Name = "formsPlotMapRate1v1";
            this.formsPlotMapRate1v1.Size = new System.Drawing.Size(264, 320);
            this.formsPlotMapRate1v1.TabIndex = 6;
            // 
            // formsPlotWinRate1v1EachMap
            // 
            this.formsPlotWinRate1v1EachMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlotWinRate1v1EachMap.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRate1v1EachMap.Location = new System.Drawing.Point(272, 0);
            this.formsPlotWinRate1v1EachMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRate1v1EachMap.Name = "formsPlotWinRate1v1EachMap";
            this.formsPlotWinRate1v1EachMap.Size = new System.Drawing.Size(493, 317);
            this.formsPlotWinRate1v1EachMap.TabIndex = 5;
            // 
            // formsPlotMapRateTeam
            // 
            this.formsPlotMapRateTeam.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotMapRateTeam.Dock = System.Windows.Forms.DockStyle.Left;
            this.formsPlotMapRateTeam.Location = new System.Drawing.Point(0, 0);
            this.formsPlotMapRateTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotMapRateTeam.Name = "formsPlotMapRateTeam";
            this.formsPlotMapRateTeam.Size = new System.Drawing.Size(264, 344);
            this.formsPlotMapRateTeam.TabIndex = 7;
            // 
            // formsPlotWinRateTeamEachMap
            // 
            this.formsPlotWinRateTeamEachMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.formsPlotWinRateTeamEachMap.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRateTeamEachMap.Location = new System.Drawing.Point(272, 0);
            this.formsPlotWinRateTeamEachMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRateTeamEachMap.Name = "formsPlotWinRateTeamEachMap";
            this.formsPlotWinRateTeamEachMap.Size = new System.Drawing.Size(485, 345);
            this.formsPlotWinRateTeamEachMap.TabIndex = 5;
            // 
            // listViewStatistics
            // 
            this.listViewStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewStatistics.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderLeaderboard,
            this.columnHeaderRank,
            this.columnHeaderRating,
            this.columnHeaderHighestRating,
            this.columnHeaderGames,
            this.columnHeaderWins,
            this.columnHeaderLosses,
            this.columnHeaderWinRate});
            this.listViewStatistics.FullRowSelect = true;
            this.listViewStatistics.GridLines = true;
            this.listViewStatistics.HideSelection = false;
            this.listViewStatistics.Location = new System.Drawing.Point(0, 0);
            this.listViewStatistics.Name = "listViewStatistics";
            this.listViewStatistics.Size = new System.Drawing.Size(1267, 84);
            this.listViewStatistics.TabIndex = 2;
            this.listViewStatistics.UseCompatibleStateImageBehavior = false;
            this.listViewStatistics.View = System.Windows.Forms.View.Details;
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
            this.columnHeaderHighestRating.Width = 100;
            // 
            // columnHeaderGames
            // 
            this.columnHeaderGames.Text = "Games";
            // 
            // columnHeaderWins
            // 
            this.columnHeaderWins.Text = "Wins";
            // 
            // columnHeaderLosses
            // 
            this.columnHeaderLosses.Text = "Losses";
            // 
            // columnHeaderWinRate
            // 
            this.columnHeaderWinRate.Text = "Win%";
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1275, 790);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormHistory";
            this.Text = "FormHistory";
            this.Shown += new System.EventHandler(this.FormHistory_ShownAsync);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1v1RandomMap.ResumeLayout(false);
            this.tabPageTeamRandomMap.ResumeLayout(false);
            this.tabPageStatistics.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewHistory1v1;
        private System.Windows.Forms.ColumnHeader columnHeaderMap;
        private System.Windows.Forms.ColumnHeader columnHeaderDate;
        private System.Windows.Forms.ColumnHeader columnHeaderRate;
        private System.Windows.Forms.ColumnHeader columnHeaderWin;
        private System.Windows.Forms.ColumnHeader columnHeaderCiv;
        private System.Windows.Forms.ColumnHeader columnHeaderColor;
        private System.Windows.Forms.ColumnHeader columnHeaderVersion;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1v1RandomMap;
        private System.Windows.Forms.TabPage tabPageTeamRandomMap;
        private System.Windows.Forms.ListView listViewHistoryTeam;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TabPage tabPageStatistics;
        private System.Windows.Forms.ListView listViewStatistics;
        private System.Windows.Forms.ColumnHeader columnHeaderLeaderboard;
        private System.Windows.Forms.ColumnHeader columnHeaderRank;
        private System.Windows.Forms.ColumnHeader columnHeaderRating;
        private System.Windows.Forms.ColumnHeader columnHeaderHighestRating;
        private System.Windows.Forms.ColumnHeader columnHeaderGames;
        private System.Windows.Forms.ColumnHeader columnHeaderWins;
        private System.Windows.Forms.ColumnHeader columnHeaderLosses;
        private System.Windows.Forms.ColumnHeader columnHeaderWinRate;
        private ScottPlot.FormsPlot formsPlotRate;
        private ScottPlot.FormsPlot formsPlotWinRate1v1EachMap;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ScottPlot.FormsPlot formsPlotWinRateTeamEachMap;
        private ScottPlot.FormsPlot formsPlotMapRate1v1;
        private ScottPlot.FormsPlot formsPlotMapRateTeam;
    }
}