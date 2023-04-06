using OpenWiiManager.Controls;

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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            databaseToolStripMenuItem = new ToolStripMenuItem();
            checkForUpdateToolStripMenuItem = new ToolStripMenuItem();
            purgeToolStripMenuItem = new ToolStripMenuItem();
            gamesToolStripMenuItem = new ToolStripMenuItem();
            refreshToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            viewToolStripMenuItem = new ToolStripMenuItem();
            expandColumnsToolStripMenuItem = new ToolStripMenuItem();
            shrinkColumnsToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            forceGarbageCollectionToolStripMenuItem = new ToolStripMenuItem();
            debugShowBalloonToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripTextBox = new ToolStripTextBoxWithHeight();
            statusStrip1 = new StatusStrip();
            backgroundTaskPopupButton = new ToolStripDropDownButton();
            backgroundOperationProgressBar = new ToolStripProgressBar();
            backgroundOperationLabel = new ToolStripStatusLabel();
            statusStripSpring = new ToolStripStatusLabel();
            notificationsButton = new ToolStripDropDownButton();
            splitter1 = new Splitter();
            listView1 = new ListViewEx();
            columnHeader1 = new ColumnHeader();
            columnHeader2 = new ColumnHeader();
            columnHeader3 = new ColumnHeader();
            columnHeader4 = new ColumnHeader();
            columnHeader8 = new ColumnHeader();
            columnHeader7 = new ColumnHeader();
            columnHeader5 = new ColumnHeader();
            columnHeader6 = new ColumnHeader();
            gameContextMenuStrip = new ContextMenuStrip(components);
            viewGameOnGameTDBToolStripMenuItem = new ToolStripMenuItem();
            playGameUsingDolphinToolStripMenuItem = new ToolStripMenuItem();
            showFilesInExplorerToolStripMenuItem = new ToolStripMenuItem();
            propertiesToolStripMenuItem = new ToolStripMenuItem();
            detailsToolStripMenuItem = new ToolStripMenuItem();
            imageList1 = new ImageList(components);
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            webPictureBox1 = new WebPictureBox();
            textBox1 = new TextBox();
            webPictureBox2 = new WebPictureBox();
            notificationToolTip = new ToolTip(components);
            searchDelayTimer = new System.Windows.Forms.Timer(components);
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            gameContextMenuStrip.SuspendLayout();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webPictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)webPictureBox2).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.AutoSize = false;
            menuStrip1.GripMargin = new Padding(0);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, viewToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem, searchToolStripTextBox });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(0);
            menuStrip1.Size = new Size(1130, 20);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { databaseToolStripMenuItem, gamesToolStripMenuItem, settingsToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Padding = new Padding(2, 0, 2, 0);
            fileToolStripMenuItem.Size = new Size(33, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // databaseToolStripMenuItem
            // 
            databaseToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { checkForUpdateToolStripMenuItem, purgeToolStripMenuItem });
            databaseToolStripMenuItem.Image = Properties.Resources.Database;
            databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            databaseToolStripMenuItem.Size = new Size(219, 22);
            databaseToolStripMenuItem.Text = "Database";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            checkForUpdateToolStripMenuItem.Image = Properties.Resources.Synchronise;
            checkForUpdateToolStripMenuItem.Name = "checkForUpdateToolStripMenuItem";
            checkForUpdateToolStripMenuItem.Size = new Size(165, 22);
            checkForUpdateToolStripMenuItem.Text = "Check for update";
            checkForUpdateToolStripMenuItem.Click += checkForUpdateToolStripMenuItem_Click;
            // 
            // purgeToolStripMenuItem
            // 
            purgeToolStripMenuItem.Image = Properties.Resources.Waste_Bin;
            purgeToolStripMenuItem.Name = "purgeToolStripMenuItem";
            purgeToolStripMenuItem.Size = new Size(165, 22);
            purgeToolStripMenuItem.Text = "Purge";
            purgeToolStripMenuItem.Click += purgeToolStripMenuItem_Click;
            // 
            // gamesToolStripMenuItem
            // 
            gamesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { refreshToolStripMenuItem });
            gamesToolStripMenuItem.Image = Properties.Resources.CD;
            gamesToolStripMenuItem.Name = "gamesToolStripMenuItem";
            gamesToolStripMenuItem.Size = new Size(219, 22);
            gamesToolStripMenuItem.Text = "Games";
            // 
            // refreshToolStripMenuItem
            // 
            refreshToolStripMenuItem.Image = Properties.Resources.Refresh;
            refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            refreshToolStripMenuItem.ShortcutKeys = Keys.F5;
            refreshToolStripMenuItem.Size = new Size(132, 22);
            refreshToolStripMenuItem.Text = "Refresh";
            refreshToolStripMenuItem.Click += refreshToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Image = Properties.Resources.Tools;
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Oemcomma;
            settingsToolStripMenuItem.Size = new Size(219, 22);
            settingsToolStripMenuItem.Text = "&Settings";
            settingsToolStripMenuItem.Click += settingsToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(216, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.Exit;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Q;
            exitToolStripMenuItem.Size = new Size(219, 22);
            exitToolStripMenuItem.Text = "&Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { expandColumnsToolStripMenuItem, shrinkColumnsToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Padding = new Padding(2, 0, 2, 0);
            viewToolStripMenuItem.Size = new Size(40, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // expandColumnsToolStripMenuItem
            // 
            expandColumnsToolStripMenuItem.Image = Properties.Resources.Column_Width;
            expandColumnsToolStripMenuItem.Name = "expandColumnsToolStripMenuItem";
            expandColumnsToolStripMenuItem.Size = new Size(162, 22);
            expandColumnsToolStripMenuItem.Text = "&Expand columns";
            expandColumnsToolStripMenuItem.Click += expandColumnsToolStripMenuItem_Click;
            // 
            // shrinkColumnsToolStripMenuItem
            // 
            shrinkColumnsToolStripMenuItem.Image = Properties.Pictograms.Column_Shrink_2;
            shrinkColumnsToolStripMenuItem.Name = "shrinkColumnsToolStripMenuItem";
            shrinkColumnsToolStripMenuItem.Size = new Size(162, 22);
            shrinkColumnsToolStripMenuItem.Text = "&Fit columns";
            shrinkColumnsToolStripMenuItem.Click += shrinkColumnsToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { forceGarbageCollectionToolStripMenuItem, debugShowBalloonToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Padding = new Padding(2, 0, 2, 0);
            toolsToolStripMenuItem.Size = new Size(42, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // forceGarbageCollectionToolStripMenuItem
            // 
            forceGarbageCollectionToolStripMenuItem.Image = Properties.Resources.Waste_Bin;
            forceGarbageCollectionToolStripMenuItem.Name = "forceGarbageCollectionToolStripMenuItem";
            forceGarbageCollectionToolStripMenuItem.Size = new Size(204, 22);
            forceGarbageCollectionToolStripMenuItem.Text = "&Force garbage collection";
            forceGarbageCollectionToolStripMenuItem.Click += forceGarbageCollectionToolStripMenuItem_Click;
            // 
            // debugShowBalloonToolStripMenuItem
            // 
            debugShowBalloonToolStripMenuItem.Name = "debugShowBalloonToolStripMenuItem";
            debugShowBalloonToolStripMenuItem.Size = new Size(204, 22);
            debugShowBalloonToolStripMenuItem.Text = "[Debug] Show balloon";
            debugShowBalloonToolStripMenuItem.Click += debugShowBalloonToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Padding = new Padding(2, 0, 2, 0);
            helpToolStripMenuItem.Size = new Size(40, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Image = Properties.Resources.About;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.ShortcutKeys = Keys.Shift | Keys.F1;
            aboutToolStripMenuItem.Size = new Size(218, 22);
            aboutToolStripMenuItem.Text = "&About...";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // searchToolStripTextBox
            // 
            searchToolStripTextBox.Alignment = ToolStripItemAlignment.Right;
            searchToolStripTextBox.AutoSize = false;
            searchToolStripTextBox.Name = "searchToolStripTextBox";
            searchToolStripTextBox.Size = new Size(230, 20);
            searchToolStripTextBox.TextChanged += searchToolStripTextBox_TextChanged;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { backgroundTaskPopupButton, backgroundOperationProgressBar, backgroundOperationLabel, statusStripSpring, notificationsButton });
            statusStrip1.Location = new Point(0, 650);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            statusStrip1.Size = new Size(1130, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // backgroundTaskPopupButton
            // 
            backgroundTaskPopupButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            backgroundTaskPopupButton.Image = Properties.Resources.Task;
            backgroundTaskPopupButton.ImageTransparentColor = Color.Magenta;
            backgroundTaskPopupButton.Name = "backgroundTaskPopupButton";
            backgroundTaskPopupButton.ShowDropDownArrow = false;
            backgroundTaskPopupButton.Size = new Size(20, 20);
            backgroundTaskPopupButton.Text = "Show running background tasks";
            backgroundTaskPopupButton.Click += backgroundTaskPopupButton_ButtonClick;
            // 
            // backgroundOperationProgressBar
            // 
            backgroundOperationProgressBar.MarqueeAnimationSpeed = 40;
            backgroundOperationProgressBar.Maximum = 0;
            backgroundOperationProgressBar.Name = "backgroundOperationProgressBar";
            backgroundOperationProgressBar.Size = new Size(40, 16);
            backgroundOperationProgressBar.Style = ProgressBarStyle.Marquee;
            backgroundOperationProgressBar.Visible = false;
            backgroundOperationProgressBar.Click += backgroundOperationLabel_Click;
            // 
            // backgroundOperationLabel
            // 
            backgroundOperationLabel.Name = "backgroundOperationLabel";
            backgroundOperationLabel.Size = new Size(0, 17);
            backgroundOperationLabel.Visible = false;
            backgroundOperationLabel.Click += backgroundOperationLabel_Click;
            // 
            // statusStripSpring
            // 
            statusStripSpring.Name = "statusStripSpring";
            statusStripSpring.Size = new Size(1062, 17);
            statusStripSpring.Spring = true;
            // 
            // notificationsButton
            // 
            notificationsButton.Image = Properties.Resources.Alarm;
            notificationsButton.ImageTransparentColor = Color.Magenta;
            notificationsButton.Name = "notificationsButton";
            notificationsButton.ShowDropDownArrow = false;
            notificationsButton.Size = new Size(33, 20);
            notificationsButton.Text = "0";
            notificationsButton.ToolTipText = "Notifications";
            notificationsButton.Click += notificationsButton_ButtonClick;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(767, 20);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 630);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // listView1
            // 
            listView1.AllowColumnReorder = true;
            listView1.BorderStyle = BorderStyle.None;
            listView1.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4, columnHeader8, columnHeader7, columnHeader5, columnHeader6 });
            listView1.ContextMenuStrip = gameContextMenuStrip;
            listView1.Dock = DockStyle.Fill;
            listView1.EmptyText = "No games to show";
            listView1.FullRowSelect = true;
            listView1.Location = new Point(0, 20);
            listView1.Name = "listView1";
            listView1.ShowItemToolTips = true;
            listView1.Size = new Size(767, 630);
            listView1.SmallImageList = imageList1;
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ColumnClick += listView1_ColumnClick;
            listView1.ItemSelectionChanged += listView1_ItemSelectionChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "File name";
            columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "ID";
            columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Title";
            columnHeader3.Width = 240;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "Region";
            // 
            // columnHeader8
            // 
            columnHeader8.Text = "Developer";
            // 
            // columnHeader7
            // 
            columnHeader7.Text = "Publisher";
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Language(s)";
            // 
            // columnHeader6
            // 
            columnHeader6.Text = "Release date";
            // 
            // gameContextMenuStrip
            // 
            gameContextMenuStrip.Items.AddRange(new ToolStripItem[] { viewGameOnGameTDBToolStripMenuItem, playGameUsingDolphinToolStripMenuItem, showFilesInExplorerToolStripMenuItem, propertiesToolStripMenuItem, detailsToolStripMenuItem });
            gameContextMenuStrip.Name = "gameContextMenuStrip";
            gameContextMenuStrip.Size = new Size(344, 114);
            gameContextMenuStrip.Opening += gameContextMenuStrip_Opening;
            // 
            // viewGameOnGameTDBToolStripMenuItem
            // 
            viewGameOnGameTDBToolStripMenuItem.Image = Properties.Resources.gametdb_16;
            viewGameOnGameTDBToolStripMenuItem.Name = "viewGameOnGameTDBToolStripMenuItem";
            viewGameOnGameTDBToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Shift | Keys.V;
            viewGameOnGameTDBToolStripMenuItem.Size = new Size(343, 22);
            viewGameOnGameTDBToolStripMenuItem.Text = "View game on GameTDB";
            viewGameOnGameTDBToolStripMenuItem.Click += viewGameOnGameTDBToolStripMenuItem_Click;
            // 
            // playGameUsingDolphinToolStripMenuItem
            // 
            playGameUsingDolphinToolStripMenuItem.Image = Properties.Resources.dolphin_16;
            playGameUsingDolphinToolStripMenuItem.Name = "playGameUsingDolphinToolStripMenuItem";
            playGameUsingDolphinToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.F12;
            playGameUsingDolphinToolStripMenuItem.Size = new Size(343, 22);
            playGameUsingDolphinToolStripMenuItem.Text = "Play game using Dolphin";
            playGameUsingDolphinToolStripMenuItem.Click += playGameUsingDolphinToolStripMenuItem_Click;
            // 
            // showFilesInExplorerToolStripMenuItem
            // 
            showFilesInExplorerToolStripMenuItem.Name = "showFilesInExplorerToolStripMenuItem";
            showFilesInExplorerToolStripMenuItem.Size = new Size(343, 22);
            showFilesInExplorerToolStripMenuItem.Text = "Show file(s) in Explorer";
            showFilesInExplorerToolStripMenuItem.Click += showFilesInExplorerToolStripMenuItem_Click;
            // 
            // propertiesToolStripMenuItem
            // 
            propertiesToolStripMenuItem.Image = Properties.Resources.Properties;
            propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            propertiesToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Return;
            propertiesToolStripMenuItem.Size = new Size(343, 22);
            propertiesToolStripMenuItem.Text = "Properties";
            propertiesToolStripMenuItem.Click += propertiesToolStripMenuItem_Click;
            // 
            // detailsToolStripMenuItem
            // 
            detailsToolStripMenuItem.Image = Properties.Resources.CD_View_1_;
            detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            detailsToolStripMenuItem.ShortcutKeys = Keys.Alt | Keys.Shift | Keys.I;
            detailsToolStripMenuItem.Size = new Size(343, 22);
            detailsToolStripMenuItem.Text = "Details...";
            detailsToolStripMenuItem.Click += detailsToolStripMenuItem_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Tick.png");
            imageList1.Images.SetKeyName(1, "Warning.png");
            imageList1.Images.SetKeyName(2, "Help-3.png");
            imageList1.Images.SetKeyName(3, "Error.png");
            imageList1.Images.SetKeyName(4, "Clock.png");
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(770, 20);
            panel1.MinimumSize = new Size(360, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(360, 630);
            panel1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.38095F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.61905F));
            tableLayoutPanel1.Controls.Add(webPictureBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(textBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(webPictureBox2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(360, 630);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // webPictureBox1
            // 
            webPictureBox1.Anchor = AnchorStyles.Right;
            webPictureBox1.FallbackImage = null;
            webPictureBox1.Location = new Point(9, 3);
            webPictureBox1.Name = "webPictureBox1";
            webPictureBox1.Size = new Size(176, 248);
            webPictureBox1.TabIndex = 0;
            webPictureBox1.TabStop = false;
            webPictureBox1.URL = null;
            // 
            // textBox1
            // 
            tableLayoutPanel1.SetColumnSpan(textBox1, 2);
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(3, 257);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(354, 220);
            textBox1.TabIndex = 1;
            // 
            // webPictureBox2
            // 
            webPictureBox2.Anchor = AnchorStyles.Left;
            webPictureBox2.FallbackImage = Properties.Resources.owm_disc;
            webPictureBox2.Location = new Point(191, 47);
            webPictureBox2.Name = "webPictureBox2";
            webPictureBox2.Size = new Size(160, 160);
            webPictureBox2.TabIndex = 0;
            webPictureBox2.TabStop = false;
            webPictureBox2.URL = null;
            // 
            // notificationToolTip
            // 
            notificationToolTip.IsBalloon = true;
            notificationToolTip.ShowAlways = true;
            // 
            // searchDelayTimer
            // 
            searchDelayTimer.Interval = 500;
            searchDelayTimer.Tick += searchDelayTimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1130, 672);
            Controls.Add(listView1);
            Controls.Add(splitter1);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Text = "Open Wii Manager";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            gameContextMenuStrip.ResumeLayout(false);
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webPictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)webPictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private ToolStripMenuItem detailsToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem expandColumnsToolStripMenuItem;
        private ToolStripMenuItem shrinkColumnsToolStripMenuItem;
        private ToolStripTextBoxWithHeight searchToolStripTextBox;
        private System.Windows.Forms.Timer searchDelayTimer;
    }
}