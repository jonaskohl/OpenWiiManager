namespace OpenWiiManager.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceGarbageCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugShowBalloonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.backgroundTaskPopupButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.backgroundOperationProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.backgroundOperationLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.notificationsButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.listView1 = new OpenWiiManager.Controls.ListViewEx();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.gameContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewGameOnGameTDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playGameUsingDolphinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFilesInExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.webPictureBox1 = new OpenWiiManager.Forms.WebPictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.webPictureBox2 = new OpenWiiManager.Forms.WebPictureBox();
            this.notificationToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gameContextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webPictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.webPictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(1130, 20);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.gamesToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(33, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdateToolStripMenuItem,
            this.purgeToolStripMenuItem});
            this.databaseToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Database;
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Synchronise;
            this.checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            this.checkForUpdateToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.checkForUpdateToolStripMenuItem.Text = "Check for update";
            this.checkForUpdateToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdateToolStripMenuItem_Click);
            // 
            // purgeToolStripMenuItem
            // 
            this.purgeToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Waste_Bin;
            this.purgeToolStripMenuItem.Name = "purgeToolStripMenuItem";
            this.purgeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.purgeToolStripMenuItem.Text = "Purge";
            this.purgeToolStripMenuItem.Click += new System.EventHandler(this.purgeToolStripMenuItem_Click);
            // 
            // gamesToolStripMenuItem
            // 
            this.gamesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.gamesToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.CD;
            this.gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
            this.gamesToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.gamesToolStripMenuItem.Text = "Games";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Refresh;
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Tools;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemcomma)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(214, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.forceGarbageCollectionToolStripMenuItem,
            this.debugShowBalloonToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // forceGarbageCollectionToolStripMenuItem
            // 
            this.forceGarbageCollectionToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Waste_Bin;
            this.forceGarbageCollectionToolStripMenuItem.Name = "forceGarbageCollectionToolStripMenuItem";
            this.forceGarbageCollectionToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.forceGarbageCollectionToolStripMenuItem.Text = "&Force garbage collection";
            this.forceGarbageCollectionToolStripMenuItem.Click += new System.EventHandler(this.forceGarbageCollectionToolStripMenuItem_Click);
            // 
            // debugShowBalloonToolStripMenuItem
            // 
            this.debugShowBalloonToolStripMenuItem.Name = "debugShowBalloonToolStripMenuItem";
            this.debugShowBalloonToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.debugShowBalloonToolStripMenuItem.Text = "[Debug] Show balloon";
            this.debugShowBalloonToolStripMenuItem.Click += new System.EventHandler(this.debugShowBalloonToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.About;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundTaskPopupButton,
            this.backgroundOperationProgressBar,
            this.backgroundOperationLabel,
            this.statusStripSpring,
            this.notificationsButton});
            this.statusStrip1.Location = new System.Drawing.Point(0, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(1130, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // backgroundTaskPopupButton
            // 
            this.backgroundTaskPopupButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backgroundTaskPopupButton.Image = global::OpenWiiManager.Properties.Resources.Task;
            this.backgroundTaskPopupButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backgroundTaskPopupButton.Name = "backgroundTaskPopupButton";
            this.backgroundTaskPopupButton.ShowDropDownArrow = false;
            this.backgroundTaskPopupButton.Size = new System.Drawing.Size(20, 20);
            this.backgroundTaskPopupButton.Text = "Show running background tasks";
            this.backgroundTaskPopupButton.Click += new System.EventHandler(this.backgroundTaskPopupButton_ButtonClick);
            // 
            // backgroundOperationProgressBar
            // 
            this.backgroundOperationProgressBar.MarqueeAnimationSpeed = 40;
            this.backgroundOperationProgressBar.Maximum = 0;
            this.backgroundOperationProgressBar.Name = "backgroundOperationProgressBar";
            this.backgroundOperationProgressBar.Size = new System.Drawing.Size(40, 16);
            this.backgroundOperationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.backgroundOperationProgressBar.Visible = false;
            this.backgroundOperationProgressBar.Click += new System.EventHandler(this.backgroundOperationLabel_Click);
            // 
            // backgroundOperationLabel
            // 
            this.backgroundOperationLabel.Name = "backgroundOperationLabel";
            this.backgroundOperationLabel.Size = new System.Drawing.Size(0, 17);
            this.backgroundOperationLabel.Visible = false;
            this.backgroundOperationLabel.Click += new System.EventHandler(this.backgroundOperationLabel_Click);
            // 
            // statusStripSpring
            // 
            this.statusStripSpring.Name = "statusStripSpring";
            this.statusStripSpring.Size = new System.Drawing.Size(1062, 17);
            this.statusStripSpring.Spring = true;
            // 
            // notificationsButton
            // 
            this.notificationsButton.Image = global::OpenWiiManager.Properties.Resources.Alarm;
            this.notificationsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.notificationsButton.Name = "notificationsButton";
            this.notificationsButton.ShowDropDownArrow = false;
            this.notificationsButton.Size = new System.Drawing.Size(33, 20);
            this.notificationsButton.Text = "0";
            this.notificationsButton.ToolTipText = "Notifications";
            this.notificationsButton.Click += new System.EventHandler(this.notificationsButton_ButtonClick);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(767, 20);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 630);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader8,
            this.columnHeader7,
            this.columnHeader5,
            this.columnHeader6});
            this.listView1.ContextMenuStrip = this.gameContextMenuStrip;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.EmptyText = "No games to show";
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(0, 20);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(767, 630);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "File name";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ID";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Title";
            this.columnHeader3.Width = 240;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Region";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Developer";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Publisher";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Language(s)";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Release date";
            // 
            // gameContextMenuStrip
            // 
            this.gameContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewGameOnGameTDBToolStripMenuItem,
            this.playGameUsingDolphinToolStripMenuItem,
            this.showFilesInExplorerToolStripMenuItem,
            this.propertiesToolStripMenuItem});
            this.gameContextMenuStrip.Name = "gameContextMenuStrip";
            this.gameContextMenuStrip.Size = new System.Drawing.Size(291, 92);
            this.gameContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.gameContextMenuStrip_Opening);
            // 
            // viewGameOnGameTDBToolStripMenuItem
            // 
            this.viewGameOnGameTDBToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.gametdb_16;
            this.viewGameOnGameTDBToolStripMenuItem.Name = "viewGameOnGameTDBToolStripMenuItem";
            this.viewGameOnGameTDBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.V)));
            this.viewGameOnGameTDBToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.viewGameOnGameTDBToolStripMenuItem.Text = "View game on GameTDB";
            this.viewGameOnGameTDBToolStripMenuItem.Click += new System.EventHandler(this.viewGameOnGameTDBToolStripMenuItem_Click);
            // 
            // playGameUsingDolphinToolStripMenuItem
            // 
            this.playGameUsingDolphinToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.dolphin_16;
            this.playGameUsingDolphinToolStripMenuItem.Name = "playGameUsingDolphinToolStripMenuItem";
            this.playGameUsingDolphinToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F12)));
            this.playGameUsingDolphinToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.playGameUsingDolphinToolStripMenuItem.Text = "Play game using Dolphin";
            this.playGameUsingDolphinToolStripMenuItem.Click += new System.EventHandler(this.playGameUsingDolphinToolStripMenuItem_Click);
            // 
            // showFilesInExplorerToolStripMenuItem
            // 
            this.showFilesInExplorerToolStripMenuItem.Name = "showFilesInExplorerToolStripMenuItem";
            this.showFilesInExplorerToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.showFilesInExplorerToolStripMenuItem.Text = "Show file(s) in Explorer";
            this.showFilesInExplorerToolStripMenuItem.Click += new System.EventHandler(this.showFilesInExplorerToolStripMenuItem_Click);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Properties;
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Return)));
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(290, 22);
            this.propertiesToolStripMenuItem.Text = "Properties";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.propertiesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Tick.png");
            this.imageList1.Images.SetKeyName(1, "Warning.png");
            this.imageList1.Images.SetKeyName(2, "Help-3.png");
            this.imageList1.Images.SetKeyName(3, "Error.png");
            this.imageList1.Images.SetKeyName(4, "Clock.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(770, 20);
            this.panel1.MinimumSize = new System.Drawing.Size(360, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 630);
            this.panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.38095F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.61905F));
            this.tableLayoutPanel1.Controls.Add(this.webPictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.webPictureBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 630);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // webPictureBox1
            // 
            this.webPictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.webPictureBox1.FallbackImage = null;
            this.webPictureBox1.Location = new System.Drawing.Point(9, 3);
            this.webPictureBox1.Name = "webPictureBox1";
            this.webPictureBox1.Size = new System.Drawing.Size(176, 248);
            this.webPictureBox1.TabIndex = 0;
            this.webPictureBox1.TabStop = false;
            this.webPictureBox1.URL = null;
            // 
            // textBox1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.textBox1, 2);
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 257);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(354, 220);
            this.textBox1.TabIndex = 1;
            // 
            // webPictureBox2
            // 
            this.webPictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.webPictureBox2.FallbackImage = global::OpenWiiManager.Properties.Resources.owm_disc;
            this.webPictureBox2.Location = new System.Drawing.Point(191, 47);
            this.webPictureBox2.Name = "webPictureBox2";
            this.webPictureBox2.Size = new System.Drawing.Size(160, 160);
            this.webPictureBox2.TabIndex = 0;
            this.webPictureBox2.TabStop = false;
            this.webPictureBox2.URL = null;
            // 
            // notificationToolTip
            // 
            this.notificationToolTip.IsBalloon = true;
            this.notificationToolTip.ShowAlways = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1130, 672);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Open Wii Manager";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gameContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webPictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.webPictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar backgroundOperationProgressBar;
        private ToolStripStatusLabel backgroundOperationLabel;
        private Splitter splitter1;
        private OpenWiiManager.Controls.ListViewEx listView1;
        private Panel panel1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripDropDownButton backgroundTaskPopupButton;
        private ToolStripStatusLabel statusStripSpring;
        private ToolStripDropDownButton notificationsButton;
        private ToolStripMenuItem databaseToolStripMenuItem;
        private ToolStripMenuItem checkForUpdateToolStripMenuItem;
        private ToolStripMenuItem purgeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ToolStripMenuItem gamesToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private TableLayoutPanel tableLayoutPanel1;
        private WebPictureBox webPictureBox1;
        private TextBox textBox1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem forceGarbageCollectionToolStripMenuItem;
        private ImageList imageList1;
        private WebPictureBox webPictureBox2;
        private ContextMenuStrip gameContextMenuStrip;
        private ToolStripMenuItem viewGameOnGameTDBToolStripMenuItem;
        private ToolStripMenuItem propertiesToolStripMenuItem;
        private ToolTip notificationToolTip;
        private ToolStripMenuItem debugShowBalloonToolStripMenuItem;
        private ToolStripMenuItem playGameUsingDolphinToolStripMenuItem;
        private ToolStripMenuItem showFilesInExplorerToolStripMenuItem;
    }
}