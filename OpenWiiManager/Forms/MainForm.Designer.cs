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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.purgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(935, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.databaseToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdateToolStripMenuItem,
            this.purgeToolStripMenuItem});
            this.databaseToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Database;
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.databaseToolStripMenuItem.Text = "Database";
            // 
            // checkForUpdateToolStripMenuItem
            // 
            this.checkForUpdateToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Recurring;
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
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Tools;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Oemcomma)));
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.Exit;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::OpenWiiManager.Properties.Resources.About;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 544);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(935, 22);
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
            this.statusStripSpring.Size = new System.Drawing.Size(794, 17);
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
            this.splitter1.Location = new System.Drawing.Point(732, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 520);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 24);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(935, 520);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(735, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 520);
            this.panel1.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(935, 566);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
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
    }
}