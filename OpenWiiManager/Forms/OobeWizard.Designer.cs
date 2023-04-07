namespace OpenWiiManager.Forms
{
    partial class OobeWizard
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
            if (disposing && (components != null))
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OobeWizard));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.label3 = new System.Windows.Forms.Label();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.themedLabel1 = new System.Windows.Forms.Label();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.themedTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.themedLabel2 = new System.Windows.Forms.Label();
            this.wizardPage3 = new AeroWizard.WizardPage();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.wizardPage4 = new AeroWizard.WizardPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.wizardPage5 = new AeroWizard.WizardPage();
            this.vistaFolderBrowserDialog1 = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.themedTableLayoutPanel1.SuspendLayout();
            this.wizardPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.wizardPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.wizardPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonToolTipText = "Go back to previous page";
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.CancelButtonText = "C&ancel";
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.FinishButtonText = "&Done";
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.wizardPage1);
            this.wizardControl1.Pages.Add(this.wizardPage2);
            this.wizardControl1.Pages.Add(this.wizardPage3);
            this.wizardControl1.Pages.Add(this.wizardPage4);
            this.wizardControl1.Pages.Add(this.wizardPage5);
            this.wizardControl1.Size = new System.Drawing.Size(600, 393);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Text = "wizardControl1";
            this.wizardControl1.Title = "Open Wii Manager";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 45);
            this.label3.TabIndex = 0;
            this.label3.Text = "You can now start using Open Wii Manager!\r\n\r\nHave fun!";
            // 
            // wizardPage1
            // 
            this.wizardPage1.AllowCancel = false;
            this.wizardPage1.Controls.Add(this.themedLabel1);
            this.wizardPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage1.Location = new System.Drawing.Point(0, 0);
            this.wizardPage1.Margin = new System.Windows.Forms.Padding(0);
            this.wizardPage1.Name = "wizardPage1";
            this.wizardPage1.NextPage = this.wizardPage2;
            this.wizardPage1.ShowCancel = false;
            this.wizardPage1.Size = new System.Drawing.Size(553, 239);
            this.wizardPage1.TabIndex = 0;
            this.wizardPage1.Text = "Welcome to Open Wii Manager!";
            // 
            // themedLabel1
            // 
            this.themedLabel1.AutoSize = true;
            this.themedLabel1.Location = new System.Drawing.Point(3, 0);
            this.themedLabel1.Name = "themedLabel1";
            this.themedLabel1.Size = new System.Drawing.Size(233, 15);
            this.themedLabel1.TabIndex = 0;
            this.themedLabel1.Text = "Let\'s set up Open Wii Manager for first use!";
            // 
            // wizardPage2
            // 
            this.wizardPage2.AllowCancel = false;
            this.wizardPage2.AllowNext = false;
            this.wizardPage2.Controls.Add(this.themedTableLayoutPanel1);
            this.wizardPage2.Controls.Add(this.themedLabel2);
            this.wizardPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage2.Location = new System.Drawing.Point(0, 0);
            this.wizardPage2.Margin = new System.Windows.Forms.Padding(0);
            this.wizardPage2.Name = "wizardPage2";
            this.wizardPage2.ShowCancel = false;
            this.wizardPage2.Size = new System.Drawing.Size(553, 239);
            this.wizardPage2.TabIndex = 1;
            this.wizardPage2.Text = "Select game collection";
            // 
            // themedTableLayoutPanel1
            // 
            this.themedTableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.themedTableLayoutPanel1.AutoSize = true;
            this.themedTableLayoutPanel1.ColumnCount = 2;
            this.themedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.themedTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.themedTableLayoutPanel1.Controls.Add(this.browseFolderButton, 1, 0);
            this.themedTableLayoutPanel1.Controls.Add(this.pathLabel, 0, 0);
            this.themedTableLayoutPanel1.Location = new System.Drawing.Point(0, 55);
            this.themedTableLayoutPanel1.Name = "themedTableLayoutPanel1";
            this.themedTableLayoutPanel1.RowCount = 1;
            this.themedTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.themedTableLayoutPanel1.Size = new System.Drawing.Size(543, 36);
            this.themedTableLayoutPanel1.TabIndex = 2;
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(510, 3);
            this.browseFolderButton.Name = "browseFolderButton";
            this.browseFolderButton.Size = new System.Drawing.Size(30, 30);
            this.browseFolderButton.TabIndex = 1;
            this.browseFolderButton.UseVisualStyleBackColor = true;
            this.browseFolderButton.Click += new System.EventHandler(this.browseFolderButton_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pathLabel.AutoSize = true;
            this.pathLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.pathLabel.Location = new System.Drawing.Point(3, 10);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(70, 15);
            this.pathLabel.TabIndex = 2;
            this.pathLabel.Text = "Select folder";
            // 
            // themedLabel2
            // 
            this.themedLabel2.Location = new System.Drawing.Point(0, 0);
            this.themedLabel2.Name = "themedLabel2";
            this.themedLabel2.Size = new System.Drawing.Size(345, 34);
            this.themedLabel2.TabIndex = 0;
            this.themedLabel2.Text = "First things first.\r\nLet\'s select the folder where you store your Wii game backup" +
    "s.";
            // 
            // wizardPage3
            // 
            this.wizardPage3.Controls.Add(this.pictureBox2);
            this.wizardPage3.Controls.Add(this.label1);
            this.wizardPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage3.Location = new System.Drawing.Point(0, 0);
            this.wizardPage3.Margin = new System.Windows.Forms.Padding(0);
            this.wizardPage3.Name = "wizardPage3";
            this.wizardPage3.ShowCancel = false;
            this.wizardPage3.Size = new System.Drawing.Size(553, 239);
            this.wizardPage3.TabIndex = 2;
            this.wizardPage3.Text = "GameTDB";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::OpenWiiManager.Properties.Resources.gametdb_logo_small;
            this.pictureBox2.Location = new System.Drawing.Point(469, 222);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(84, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(553, 166);
            this.label1.TabIndex = 0;
            this.label1.Text = "In the next step, Open Wii Manager will connect to the internet to download the l" +
    "atest version of the GameTDB database file. This file provides metadata and exte" +
    "ned information of Wii games.";
            // 
            // wizardPage4
            // 
            this.wizardPage4.AllowBack = false;
            this.wizardPage4.AllowCancel = false;
            this.wizardPage4.AllowNext = false;
            this.wizardPage4.Controls.Add(this.pictureBox1);
            this.wizardPage4.Controls.Add(this.progressBar1);
            this.wizardPage4.Controls.Add(this.label2);
            this.wizardPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage4.Location = new System.Drawing.Point(0, 0);
            this.wizardPage4.Margin = new System.Windows.Forms.Padding(0);
            this.wizardPage4.Name = "wizardPage4";
            this.wizardPage4.ShowCancel = false;
            this.wizardPage4.ShowNext = false;
            this.wizardPage4.Size = new System.Drawing.Size(553, 239);
            this.wizardPage4.TabIndex = 3;
            this.wizardPage4.Text = "GameTDB";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::OpenWiiManager.Properties.Resources.gametdb_logo_small;
            this.pictureBox1.Location = new System.Drawing.Point(469, 220);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(84, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(94, 46);
            this.progressBar1.MarqueeAnimationSpeed = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(364, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Downloading GameTDB database...";
            // 
            // wizardPage5
            // 
            this.wizardPage5.AllowBack = false;
            this.wizardPage5.AllowCancel = false;
            this.wizardPage5.Controls.Add(this.label3);
            this.wizardPage5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardPage5.Location = new System.Drawing.Point(0, 0);
            this.wizardPage5.Margin = new System.Windows.Forms.Padding(0);
            this.wizardPage5.Name = "wizardPage5";
            this.wizardPage5.ShowCancel = false;
            this.wizardPage5.Size = new System.Drawing.Size(553, 239);
            this.wizardPage5.TabIndex = 4;
            this.wizardPage5.Text = "You are good to go!";
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OobeWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(600, 393);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OobeWizard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wizardPage1.ResumeLayout(false);
            this.wizardPage1.PerformLayout();
            this.wizardPage2.ResumeLayout(false);
            this.wizardPage2.PerformLayout();
            this.themedTableLayoutPanel1.ResumeLayout(false);
            this.themedTableLayoutPanel1.PerformLayout();
            this.wizardPage3.ResumeLayout(false);
            this.wizardPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.wizardPage4.ResumeLayout(false);
            this.wizardPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.wizardPage5.ResumeLayout(false);
            this.wizardPage5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage wizardPage1;
        private System.Windows.Forms.Label themedLabel1;
        private AeroWizard.WizardPage wizardPage2;
        private System.Windows.Forms.Label themedLabel2;
        private System.Windows.Forms.TableLayoutPanel themedTableLayoutPanel1;
        private System.Windows.Forms.Button browseFolderButton;
        private System.Windows.Forms.Label pathLabel;
        private Ookii.Dialogs.WinForms.VistaFolderBrowserDialog vistaFolderBrowserDialog1;
        private AeroWizard.WizardPage wizardPage3;
        private Label label1;
        private AeroWizard.WizardPage wizardPage4;
        private Label label2;
        private ProgressBar progressBar1;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private System.Windows.Forms.Timer timer1;
        private AeroWizard.WizardPage wizardPage5;
        private Label label3;
    }
}