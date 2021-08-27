
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
            this.splitContainerGraphBase = new System.Windows.Forms.SplitContainer();
            this.splitContainerRate = new System.Windows.Forms.SplitContainer();
            this.formsPlotRate1v1 = new ScottPlot.FormsPlot();
            this.formsPlotRateTeam = new ScottPlot.FormsPlot();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageMap = new System.Windows.Forms.TabPage();
            this.splitContainerMapRate = new System.Windows.Forms.SplitContainer();
            this.formsPlotWinRate1v1EachMap = new ScottPlot.FormsPlot();
            this.formsPlotMapRate1v1 = new ScottPlot.FormsPlot();
            this.formsPlotWinRateTeamEachMap = new ScottPlot.FormsPlot();
            this.formsPlotMapRateTeam = new ScottPlot.FormsPlot();
            this.tabPageCiv = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.formsPlotCiv1v1 = new ScottPlot.FormsPlot();
            this.formsPlotCivTeam = new ScottPlot.FormsPlot();
            this.tabPagePlayer = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.formsPlotPlayer1v1 = new ScottPlot.FormsPlot();
            this.formsPlotPlayerTeam = new ScottPlot.FormsPlot();
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
            this.tabPagePlayer.SuspendLayout();
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
            this.listViewHistory1v1.Size = new System.Drawing.Size(1058, 740);
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
            this.tabControl1.Size = new System.Drawing.Size(1112, 771);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1v1RandomMap
            // 
            this.tabPage1v1RandomMap.Controls.Add(this.listViewHistory1v1);
            this.tabPage1v1RandomMap.Location = new System.Drawing.Point(4, 24);
            this.tabPage1v1RandomMap.Name = "tabPage1v1RandomMap";
            this.tabPage1v1RandomMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1v1RandomMap.Size = new System.Drawing.Size(1104, 743);
            this.tabPage1v1RandomMap.TabIndex = 0;
            this.tabPage1v1RandomMap.Text = "1v1 Random Map";
            this.tabPage1v1RandomMap.UseVisualStyleBackColor = true;
            // 
            // tabPageTeamRandomMap
            // 
            this.tabPageTeamRandomMap.Controls.Add(this.listViewHistoryTeam);
            this.tabPageTeamRandomMap.Location = new System.Drawing.Point(4, 24);
            this.tabPageTeamRandomMap.Name = "tabPageTeamRandomMap";
            this.tabPageTeamRandomMap.Size = new System.Drawing.Size(1104, 743);
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
            this.listViewHistoryTeam.Size = new System.Drawing.Size(1058, 740);
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
            this.tabPageStatistics.Controls.Add(this.splitContainerGraphBase);
            this.tabPageStatistics.Controls.Add(this.listViewStatistics);
            this.tabPageStatistics.Location = new System.Drawing.Point(4, 24);
            this.tabPageStatistics.Name = "tabPageStatistics";
            this.tabPageStatistics.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStatistics.Size = new System.Drawing.Size(1104, 743);
            this.tabPageStatistics.TabIndex = 1;
            this.tabPageStatistics.Text = "Statistics";
            this.tabPageStatistics.UseVisualStyleBackColor = true;
            // 
            // splitContainerGraphBase
            // 
            this.splitContainerGraphBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerGraphBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainerGraphBase.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainerGraphBase.Location = new System.Drawing.Point(0, 90);
            this.splitContainerGraphBase.Name = "splitContainerGraphBase";
            // 
            // splitContainerGraphBase.Panel1
            // 
            this.splitContainerGraphBase.Panel1.Controls.Add(this.splitContainerRate);
            // 
            // splitContainerGraphBase.Panel2
            // 
            this.splitContainerGraphBase.Panel2.Controls.Add(this.tabControl2);
            this.splitContainerGraphBase.Size = new System.Drawing.Size(1104, 653);
            this.splitContainerGraphBase.SplitterDistance = 542;
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
            this.splitContainerRate.Size = new System.Drawing.Size(542, 653);
            this.splitContainerRate.SplitterDistance = 319;
            this.splitContainerRate.TabIndex = 5;
            // 
            // formsPlotRate1v1
            // 
            this.formsPlotRate1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotRate1v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotRate1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotRate1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotRate1v1.Name = "formsPlotRate1v1";
            this.formsPlotRate1v1.Size = new System.Drawing.Size(540, 317);
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
            this.formsPlotRateTeam.Size = new System.Drawing.Size(540, 328);
            this.formsPlotRateTeam.TabIndex = 3;
            this.formsPlotRateTeam.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormsPlotRateTeam_MouseMove);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageMap);
            this.tabControl2.Controls.Add(this.tabPageCiv);
            this.tabControl2.Controls.Add(this.tabPagePlayer);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(556, 651);
            this.tabControl2.TabIndex = 7;
            // 
            // tabPageMap
            // 
            this.tabPageMap.Controls.Add(this.splitContainerMapRate);
            this.tabPageMap.Location = new System.Drawing.Point(4, 24);
            this.tabPageMap.Name = "tabPageMap";
            this.tabPageMap.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMap.Size = new System.Drawing.Size(548, 623);
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
            this.splitContainerMapRate.Panel1.Controls.Add(this.formsPlotWinRate1v1EachMap);
            this.splitContainerMapRate.Panel1.Controls.Add(this.formsPlotMapRate1v1);
            // 
            // splitContainerMapRate.Panel2
            // 
            this.splitContainerMapRate.Panel2.Controls.Add(this.formsPlotWinRateTeamEachMap);
            this.splitContainerMapRate.Panel2.Controls.Add(this.formsPlotMapRateTeam);
            this.splitContainerMapRate.Size = new System.Drawing.Size(542, 617);
            this.splitContainerMapRate.SplitterDistance = 292;
            this.splitContainerMapRate.TabIndex = 6;
            // 
            // formsPlotWinRate1v1EachMap
            // 
            this.formsPlotWinRate1v1EachMap.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRate1v1EachMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotWinRate1v1EachMap.Location = new System.Drawing.Point(0, 0);
            this.formsPlotWinRate1v1EachMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRate1v1EachMap.Name = "formsPlotWinRate1v1EachMap";
            this.formsPlotWinRate1v1EachMap.Size = new System.Drawing.Size(540, 290);
            this.formsPlotWinRate1v1EachMap.TabIndex = 5;
            // 
            // formsPlotMapRate1v1
            // 
            this.formsPlotMapRate1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotMapRate1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotMapRate1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotMapRate1v1.Name = "formsPlotMapRate1v1";
            this.formsPlotMapRate1v1.Size = new System.Drawing.Size(264, 247);
            this.formsPlotMapRate1v1.TabIndex = 6;
            // 
            // formsPlotWinRateTeamEachMap
            // 
            this.formsPlotWinRateTeamEachMap.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotWinRateTeamEachMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotWinRateTeamEachMap.Location = new System.Drawing.Point(0, 0);
            this.formsPlotWinRateTeamEachMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotWinRateTeamEachMap.Name = "formsPlotWinRateTeamEachMap";
            this.formsPlotWinRateTeamEachMap.Size = new System.Drawing.Size(540, 319);
            this.formsPlotWinRateTeamEachMap.TabIndex = 5;
            // 
            // formsPlotMapRateTeam
            // 
            this.formsPlotMapRateTeam.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotMapRateTeam.Location = new System.Drawing.Point(0, 0);
            this.formsPlotMapRateTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotMapRateTeam.Name = "formsPlotMapRateTeam";
            this.formsPlotMapRateTeam.Size = new System.Drawing.Size(264, 266);
            this.formsPlotMapRateTeam.TabIndex = 7;
            // 
            // tabPageCiv
            // 
            this.tabPageCiv.Controls.Add(this.splitContainer1);
            this.tabPageCiv.Location = new System.Drawing.Point(4, 24);
            this.tabPageCiv.Name = "tabPageCiv";
            this.tabPageCiv.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCiv.Size = new System.Drawing.Size(548, 623);
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
            this.splitContainer1.Size = new System.Drawing.Size(542, 617);
            this.splitContainer1.SplitterDistance = 291;
            this.splitContainer1.TabIndex = 0;
            // 
            // formsPlotCiv1v1
            // 
            this.formsPlotCiv1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCiv1v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCiv1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotCiv1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCiv1v1.Name = "formsPlotCiv1v1";
            this.formsPlotCiv1v1.Size = new System.Drawing.Size(540, 289);
            this.formsPlotCiv1v1.TabIndex = 6;
            // 
            // formsPlotCivTeam
            // 
            this.formsPlotCivTeam.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotCivTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotCivTeam.Location = new System.Drawing.Point(0, 0);
            this.formsPlotCivTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotCivTeam.Name = "formsPlotCivTeam";
            this.formsPlotCivTeam.Size = new System.Drawing.Size(540, 320);
            this.formsPlotCivTeam.TabIndex = 6;
            // 
            // tabPagePlayer
            // 
            this.tabPagePlayer.Controls.Add(this.splitContainer2);
            this.tabPagePlayer.Location = new System.Drawing.Point(4, 24);
            this.tabPagePlayer.Name = "tabPagePlayer";
            this.tabPagePlayer.Size = new System.Drawing.Size(548, 623);
            this.tabPagePlayer.TabIndex = 2;
            this.tabPagePlayer.Text = "Player";
            this.tabPagePlayer.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.formsPlotPlayer1v1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.formsPlotPlayerTeam);
            this.splitContainer2.Size = new System.Drawing.Size(548, 623);
            this.splitContainer2.SplitterDistance = 294;
            this.splitContainer2.TabIndex = 0;
            // 
            // formsPlotPlayer1v1
            // 
            this.formsPlotPlayer1v1.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotPlayer1v1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotPlayer1v1.Location = new System.Drawing.Point(0, 0);
            this.formsPlotPlayer1v1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotPlayer1v1.Name = "formsPlotPlayer1v1";
            this.formsPlotPlayer1v1.Size = new System.Drawing.Size(546, 292);
            this.formsPlotPlayer1v1.TabIndex = 6;
            // 
            // formsPlotPlayerTeam
            // 
            this.formsPlotPlayerTeam.BackColor = System.Drawing.Color.Transparent;
            this.formsPlotPlayerTeam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.formsPlotPlayerTeam.Location = new System.Drawing.Point(0, 0);
            this.formsPlotPlayerTeam.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlotPlayerTeam.Name = "formsPlotPlayerTeam";
            this.formsPlotPlayerTeam.Size = new System.Drawing.Size(546, 323);
            this.formsPlotPlayerTeam.TabIndex = 6;
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
            this.listViewStatistics.Size = new System.Drawing.Size(1104, 84);
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
            this.ClientSize = new System.Drawing.Size(1112, 771);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormHistory";
            this.Text = "FormHistory";
            this.Shown += new System.EventHandler(this.FormHistory_ShownAsync);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1v1RandomMap.ResumeLayout(false);
            this.tabPageTeamRandomMap.ResumeLayout(false);
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
            this.tabPagePlayer.ResumeLayout(false);
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
        private ScottPlot.FormsPlot formsPlotWinRate1v1EachMap;
        private System.Windows.Forms.SplitContainer splitContainerGraphBase;
        private System.Windows.Forms.SplitContainer splitContainerMapRate;
        private ScottPlot.FormsPlot formsPlotWinRateTeamEachMap;
        private ScottPlot.FormsPlot formsPlotMapRate1v1;
        private ScottPlot.FormsPlot formsPlotMapRateTeam;
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
        private System.Windows.Forms.TabPage tabPageCiv;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabPage tabPagePlayer;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ScottPlot.FormsPlot formsPlotCiv1v1;
        private ScottPlot.FormsPlot formsPlotCivTeam;
        private ScottPlot.FormsPlot formsPlotPlayer1v1;
        private ScottPlot.FormsPlot formsPlotPlayerTeam;
    }
}