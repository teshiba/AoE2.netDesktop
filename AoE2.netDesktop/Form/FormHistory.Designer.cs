
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
            this.listViewHistory1v1 = new System.Windows.Forms.ListView();
            this.columnHeaderMap = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderWin = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderCiv = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderColor = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderDate = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderVersion = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripMatchedPlayers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openAoE2NetProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlFormHistory = new System.Windows.Forms.TabControl();
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
            this.tabPageMatchedPlayers = new System.Windows.Forms.TabPage();
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
            this.tabPageStatistics = new System.Windows.Forms.TabPage();
            this.splitContainerGraphBase = new System.Windows.Forms.SplitContainer();
            this.splitContainerRate = new System.Windows.Forms.SplitContainer();
            this.formsPlotRate1v1 = new ScottPlot.FormsPlot();
            this.formsPlotRateTeam = new ScottPlot.FormsPlot();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageMap = new System.Windows.Forms.TabPage();
            this.splitContainerMapRate = new System.Windows.Forms.SplitContainer();
            this.formsPlotWinRate1v1Map = new ScottPlot.FormsPlot();
            this.formsPlotWinRateTeamMap = new ScottPlot.FormsPlot();
            this.tabPageCiv = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.formsPlotCiv1v1 = new ScottPlot.FormsPlot();
            this.formsPlotCivTeam = new ScottPlot.FormsPlot();
            this.tabPageCountry = new System.Windows.Forms.TabPage();
            this.formsPlotCountry = new ScottPlot.FormsPlot();
            this.formsPlot2 = new ScottPlot.FormsPlot();
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
            this.contextMenuStripMatchedPlayers.SuspendLayout();
            this.tabControlFormHistory.SuspendLayout();
            this.tabPage1v1RandomMap.SuspendLayout();
            this.tabPageTeamRandomMap.SuspendLayout();
            this.tabPageMatchedPlayers.SuspendLayout();
            this.tabPageStatistics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGraphBase)).BeginInit();
            this.splitContainerGraphBase.Panel1.SuspendLayout();
            this.splitContainerGraphBase.Panel2.SuspendLayout();
            this.splitContainerGraphBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRate)).BeginInit();
            this.splitContainerRate.Panel1.SuspendLayout();
            this.splitContainerRate.Panel2.SuspendLayout();
            this.splitContainerRate.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMapRate)).BeginInit();
            this.splitContainerMapRate.Panel1.SuspendLayout();
            this.splitContainerMapRate.Panel2.SuspendLayout();
            this.splitContainerMapRate.SuspendLayout();
            this.tabPageCiv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageCountry.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewHistory1v1
            // 
            this.listViewHistory1v1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewHistory1v1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderMap,
            this.columnHeaderRate,
            this.columnHeaderWin,
            this.columnHeaderCiv,
            this.columnHeaderColor,
            this.columnHeaderDate,
            this.columnHeaderVersion});
            this.listViewHistory1v1.ContextMenuStrip = this.contextMenuStripMatchedPlayers;
            this.listViewHistory1v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHistory1v1.FullRowSelect = true;
            this.listViewHistory1v1.GridLines = true;
            this.listViewHistory1v1.HideSelection = false;
            this.listViewHistory1v1.Location = new System.Drawing.Point(3, 3);
            this.listViewHistory1v1.Name = "listViewHistory1v1";
            this.listViewHistory1v1.Size = new System.Drawing.Size(1216, 645);
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
            // contextMenuStripMatchedPlayers
            // 
            this.contextMenuStripMatchedPlayers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAoE2NetProfileToolStripMenuItem});
            this.contextMenuStripMatchedPlayers.Name = "contextMenuStripMatchedPlayers";
            this.contextMenuStripMatchedPlayers.Size = new System.Drawing.Size(200, 26);
            this.contextMenuStripMatchedPlayers.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStripMatchedPlayers_Opening);
            // 
            // openAoE2NetProfileToolStripMenuItem
            // 
            this.openAoE2NetProfileToolStripMenuItem.Name = "openAoE2NetProfileToolStripMenuItem";
            this.openAoE2NetProfileToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.openAoE2NetProfileToolStripMenuItem.Text = "Open AoE2.net profile...";
            this.openAoE2NetProfileToolStripMenuItem.Click += new System.EventHandler(this.OpenAoE2NetProfileToolStripMenuItem_Click);
            // 
            // tabControlFormHistory
            // 
            this.tabControlFormHistory.Controls.Add(this.tabPage1v1RandomMap);
            this.tabControlFormHistory.Controls.Add(this.tabPageTeamRandomMap);
            this.tabControlFormHistory.Controls.Add(this.tabPageMatchedPlayers);
            this.tabControlFormHistory.Controls.Add(this.tabPageStatistics);
            this.tabControlFormHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFormHistory.Location = new System.Drawing.Point(0, 0);
            this.tabControlFormHistory.Name = "tabControlFormHistory";
            this.tabControlFormHistory.SelectedIndex = 0;
            this.tabControlFormHistory.Size = new System.Drawing.Size(1230, 679);
            this.tabControlFormHistory.TabIndex = 2;
            // 
            // tabPage1v1RandomMap
            // 
            this.tabPage1v1RandomMap.Controls.Add(this.listViewHistory1v1);
            this.tabPage1v1RandomMap.Location = new System.Drawing.Point(4, 24);
            this.tabPage1v1RandomMap.Name = "tabPage1v1RandomMap";
            this.tabPage1v1RandomMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1v1RandomMap.Size = new System.Drawing.Size(1222, 651);
            this.tabPage1v1RandomMap.TabIndex = 0;
            this.tabPage1v1RandomMap.Text = "1v1 Random Map";
            this.tabPage1v1RandomMap.UseVisualStyleBackColor = true;
            // 
            // tabPageTeamRandomMap
            // 
            this.tabPageTeamRandomMap.Controls.Add(this.listViewHistoryTeam);
            this.tabPageTeamRandomMap.Location = new System.Drawing.Point(4, 24);
            this.tabPageTeamRandomMap.Name = "tabPageTeamRandomMap";
            this.tabPageTeamRandomMap.Size = new System.Drawing.Size(1222, 651);
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
            this.listViewHistoryTeam.Size = new System.Drawing.Size(1216, 645);
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
            // tabPageMatchedPlayers
            // 
            this.tabPageMatchedPlayers.Controls.Add(this.listViewMatchedPlayers);
            this.tabPageMatchedPlayers.Location = new System.Drawing.Point(4, 24);
            this.tabPageMatchedPlayers.Name = "tabPageMatchedPlayers";
            this.tabPageMatchedPlayers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMatchedPlayers.Size = new System.Drawing.Size(1222, 651);
            this.tabPageMatchedPlayers.TabIndex = 3;
            this.tabPageMatchedPlayers.Text = "Matched players";
            this.tabPageMatchedPlayers.UseVisualStyleBackColor = true;
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
            this.listViewMatchedPlayers.HideSelection = false;
            this.listViewMatchedPlayers.Location = new System.Drawing.Point(3, 3);
            this.listViewMatchedPlayers.Name = "listViewMatchedPlayers";
            this.listViewMatchedPlayers.Size = new System.Drawing.Size(1216, 645);
            this.listViewMatchedPlayers.TabIndex = 2;
            this.listViewMatchedPlayers.UseCompatibleStateImageBehavior = false;
            this.listViewMatchedPlayers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 160;
            // 
            // columnHeaderCountry
            // 
            this.columnHeaderCountry.Text = "Country";
            // 
            // columnHeaderRate1v1
            // 
            this.columnHeaderRate1v1.Text = "1v1 Rate";
            // 
            // columnHeaderRateTeam
            // 
            this.columnHeaderRateTeam.Text = "Team Rate";
            this.columnHeaderRateTeam.Width = 70;
            // 
            // columnHeaderTeamGameCount
            // 
            this.columnHeaderTeamGameCount.Text = "Team Games";
            this.columnHeaderTeamGameCount.Width = 80;
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
            this.columnHeaderLastDate.Width = 120;
            // 
            // tabPageStatistics
            // 
            this.tabPageStatistics.Controls.Add(this.splitContainerGraphBase);
            this.tabPageStatistics.Controls.Add(this.listViewStatistics);
            this.tabPageStatistics.Location = new System.Drawing.Point(4, 24);
            this.tabPageStatistics.Name = "tabPageStatistics";
            this.tabPageStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatistics.Size = new System.Drawing.Size(1222, 651);
            this.tabPageStatistics.TabIndex = 1;
            this.tabPageStatistics.Text = "Statistics";
            this.tabPageStatistics.UseVisualStyleBackColor = true;
            // 
            // splitContainerGraphBase
            // 
            this.splitContainerGraphBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerGraphBase.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainerGraphBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerGraphBase.Location = new System.Drawing.Point(3, 87);
            this.splitContainerGraphBase.Name = "splitContainerGraphBase";
            // 
            // splitContainerGraphBase.Panel1
            // 
            this.splitContainerGraphBase.Panel1.Controls.Add(this.splitContainerRate);
            // 
            // splitContainerGraphBase.Panel2
            // 
            this.splitContainerGraphBase.Panel2.Controls.Add(this.tabControl2);
            this.splitContainerGraphBase.Size = new System.Drawing.Size(1216, 561);
            this.splitContainerGraphBase.SplitterDistance = 596;
            this.splitContainerGraphBase.TabIndex = 6;
            // 
            // splitContainerRate
            // 
            this.splitContainerRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerRate.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainerRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRate.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRate.Name = "splitContainerRate";
            this.splitContainerRate.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRate.Panel1
            // 
            this.splitContainerRate.Panel1.Controls.Add(this.formsPlotRate1v1);
            // 
            // splitContainerRate.Panel2
            // 
            this.splitContainerRate.Panel2.Controls.Add(this.formsPlotRateTeam);
            this.splitContainerRate.Size = new System.Drawing.Size(596, 561);
            this.splitContainerRate.SplitterDistance = 273;
            this.splitContainerRate.TabIndex = 5;
            // 
            // formsPlotRate1v1
            // 
            this.formsPlotRate1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotRate1v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotRate1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotRate1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotRate1v1.Name = "formsPlotRate1v1";
            this.formsPlotRate1v1.Size = new System.Drawing.Size(594, 271);
            this.formsPlotRate1v1.TabIndex = 4;
            this.formsPlotRate1v1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormsPlotRate1v1_MouseMove);
            // 
            // formsPlotRateTeam
            // 
            this.formsPlotRateTeam.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotRateTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotRateTeam.Location = new System.Drawing.Point(0, 0);
            this.formsPlotRateTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotRateTeam.Name = "formsPlotRateTeam";
            this.formsPlotRateTeam.Size = new System.Drawing.Size(594, 282);
            this.formsPlotRateTeam.TabIndex = 3;
            this.formsPlotRateTeam.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormsPlotRateTeam_MouseMove);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageMap);
            this.tabControl2.Controls.Add(this.tabPageCiv);
            this.tabControl2.Controls.Add(this.tabPageCountry);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(614, 559);
            this.tabControl2.TabIndex = 7;
            // 
            // tabPageMap
            // 
            this.tabPageMap.Controls.Add(this.splitContainerMapRate);
            this.tabPageMap.Location = new System.Drawing.Point(4, 24);
            this.tabPageMap.Name = "tabPageMap";
            this.tabPageMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMap.Size = new System.Drawing.Size(606, 531);
            this.tabPageMap.TabIndex = 0;
            this.tabPageMap.Text = "Map";
            this.tabPageMap.UseVisualStyleBackColor = true;
            // 
            // splitContainerMapRate
            // 
            this.splitContainerMapRate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerMapRate.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainerMapRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMapRate.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMapRate.Name = "splitContainerMapRate";
            this.splitContainerMapRate.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMapRate.Panel1
            // 
            this.splitContainerMapRate.Panel1.Controls.Add(this.formsPlotWinRate1v1Map);
            // 
            // splitContainerMapRate.Panel2
            // 
            this.splitContainerMapRate.Panel2.Controls.Add(this.formsPlotWinRateTeamMap);
            this.splitContainerMapRate.Size = new System.Drawing.Size(600, 525);
            this.splitContainerMapRate.SplitterDistance = 248;
            this.splitContainerMapRate.TabIndex = 6;
            // 
            // formsPlotWinRate1v1Map
            // 
            this.formsPlotWinRate1v1Map.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRate1v1Map.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotWinRate1v1Map.Location = new System.Drawing.Point(0, 0);
            this.formsPlotWinRate1v1Map.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRate1v1Map.Name = "formsPlotWinRate1v1Map";
            this.formsPlotWinRate1v1Map.Size = new System.Drawing.Size(598, 246);
            this.formsPlotWinRate1v1Map.TabIndex = 5;
            // 
            // formsPlotWinRateTeamMap
            // 
            this.formsPlotWinRateTeamMap.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRateTeamMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotWinRateTeamMap.Location = new System.Drawing.Point(0, 0);
            this.formsPlotWinRateTeamMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRateTeamMap.Name = "formsPlotWinRateTeamMap";
            this.formsPlotWinRateTeamMap.Size = new System.Drawing.Size(598, 271);
            this.formsPlotWinRateTeamMap.TabIndex = 5;
            // 
            // tabPageCiv
            // 
            this.tabPageCiv.Controls.Add(this.splitContainer1);
            this.tabPageCiv.Location = new System.Drawing.Point(4, 24);
            this.tabPageCiv.Name = "tabPageCiv";
            this.tabPageCiv.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCiv.Size = new System.Drawing.Size(602, 668);
            this.tabPageCiv.TabIndex = 1;
            this.tabPageCiv.Text = "civilization";
            this.tabPageCiv.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.formsPlotCiv1v1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.formsPlotCivTeam);
            this.splitContainer1.Size = new System.Drawing.Size(596, 662);
            this.splitContainer1.SplitterDistance = 312;
            this.splitContainer1.TabIndex = 0;
            // 
            // formsPlotCiv1v1
            // 
            this.formsPlotCiv1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCiv1v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCiv1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotCiv1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCiv1v1.Name = "formsPlotCiv1v1";
            this.formsPlotCiv1v1.Size = new System.Drawing.Size(594, 310);
            this.formsPlotCiv1v1.TabIndex = 6;
            // 
            // formsPlotCivTeam
            // 
            this.formsPlotCivTeam.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCivTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCivTeam.Location = new System.Drawing.Point(0, 0);
            this.formsPlotCivTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCivTeam.Name = "formsPlotCivTeam";
            this.formsPlotCivTeam.Size = new System.Drawing.Size(594, 344);
            this.formsPlotCivTeam.TabIndex = 6;
            // 
            // tabPageCountry
            // 
            this.tabPageCountry.Controls.Add(this.formsPlotCountry);
            this.tabPageCountry.Controls.Add(this.formsPlot2);
            this.tabPageCountry.Location = new System.Drawing.Point(4, 24);
            this.tabPageCountry.Name = "tabPageCountry";
            this.tabPageCountry.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCountry.Size = new System.Drawing.Size(602, 668);
            this.tabPageCountry.TabIndex = 2;
            this.tabPageCountry.Text = "Country";
            this.tabPageCountry.UseVisualStyleBackColor = true;
            // 
            // formsPlotCountry
            // 
            this.formsPlotCountry.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCountry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCountry.Location = new System.Drawing.Point(3, 3);
            this.formsPlotCountry.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCountry.Name = "formsPlotCountry";
            this.formsPlotCountry.Size = new System.Drawing.Size(596, 662);
            this.formsPlotCountry.TabIndex = 7;
            // 
            // formsPlot2
            // 
            this.formsPlot2.BackColor = System.Drawing.Color.Transparent;
            this.formsPlot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlot2.Location = new System.Drawing.Point(3, 3);
            this.formsPlot2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot2.Name = "formsPlot2";
            this.formsPlot2.Size = new System.Drawing.Size(596, 662);
            this.formsPlot2.TabIndex = 8;
            // 
            // listViewStatistics
            // 
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
            this.listViewStatistics.FullRowSelect = true;
            this.listViewStatistics.GridLines = true;
            this.listViewStatistics.HideSelection = false;
            this.listViewStatistics.Location = new System.Drawing.Point(3, 3);
            this.listViewStatistics.Name = "listViewStatistics";
            this.listViewStatistics.Size = new System.Drawing.Size(1216, 84);
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
            this.columnHeaderHighestStreak.Width = 90;
            // 
            // columnHeaderLowestStreak
            // 
            this.columnHeaderLowestStreak.Text = "LowestStreak";
            this.columnHeaderLowestStreak.Width = 90;
            // 
            // FormHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 679);
            this.Controls.Add(this.tabControlFormHistory);
            this.Name = "FormHistory";
            this.Text = "FormHistory";
            this.Shown += new System.EventHandler(this.FormHistory_ShownAsync);
            this.contextMenuStripMatchedPlayers.ResumeLayout(false);
            this.tabControlFormHistory.ResumeLayout(false);
            this.tabPage1v1RandomMap.ResumeLayout(false);
            this.tabPageTeamRandomMap.ResumeLayout(false);
            this.tabPageMatchedPlayers.ResumeLayout(false);
            this.tabPageStatistics.ResumeLayout(false);
            this.splitContainerGraphBase.Panel1.ResumeLayout(false);
            this.splitContainerGraphBase.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerGraphBase)).EndInit();
            this.splitContainerGraphBase.ResumeLayout(false);
            this.splitContainerRate.Panel1.ResumeLayout(false);
            this.splitContainerRate.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRate)).EndInit();
            this.splitContainerRate.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageMap.ResumeLayout(false);
            this.splitContainerMapRate.Panel1.ResumeLayout(false);
            this.splitContainerMapRate.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMapRate)).EndInit();
            this.splitContainerMapRate.ResumeLayout(false);
            this.tabPageCiv.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageCountry.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControlFormHistory;
        private System.Windows.Forms.TabPage tabPage1v1RandomMap;
        private System.Windows.Forms.TabPage tabPageTeamRandomMap;
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
        private ScottPlot.FormsPlot formsPlotRateTeam;
        private System.Windows.Forms.SplitContainer splitContainerGraphBase;
        private System.Windows.Forms.SplitContainer splitContainerRate;
        private ScottPlot.FormsPlot formsPlotRate1v1;
        private System.Windows.Forms.ListView listViewHistoryTeam;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageMap;
        private System.Windows.Forms.SplitContainer splitContainerMapRate;
        private ScottPlot.FormsPlot formsPlotWinRate1v1Map;
        private ScottPlot.FormsPlot formsPlotWinRateTeamMap;
        private System.Windows.Forms.TabPage tabPageCiv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ScottPlot.FormsPlot formsPlotCiv1v1;
        private ScottPlot.FormsPlot formsPlotCivTeam;
        private System.Windows.Forms.TabPage tabPageMatchedPlayers;
        private System.Windows.Forms.ListView listViewMatchedPlayers;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderCountry;
        private System.Windows.Forms.ColumnHeader columnHeaderAllyGames;
        private System.Windows.Forms.ColumnHeader columnHeaderEnemyGames;
        private System.Windows.Forms.ColumnHeader columnHeaderTeamGameCount;
        private System.Windows.Forms.ColumnHeader columnHeaderLastDate;
        private System.Windows.Forms.ColumnHeader columnHeader1v1GameCount;
        private System.Windows.Forms.ColumnHeader columnHeaderRate1v1;
        private System.Windows.Forms.ColumnHeader columnHeaderRateTeam;
        private System.Windows.Forms.TabPage tabPageCountry;
        private ScottPlot.FormsPlot formsPlotCountry;
        private ScottPlot.FormsPlot formsPlot2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMatchedPlayers;
        private System.Windows.Forms.ToolStripMenuItem openAoE2NetProfileToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderDrop;
        private System.Windows.Forms.ColumnHeader columnHeaderStreak;
        private System.Windows.Forms.ColumnHeader columnHeaderHighestStreak;
        private System.Windows.Forms.ColumnHeader columnHeaderLowestStreak;
    }
}