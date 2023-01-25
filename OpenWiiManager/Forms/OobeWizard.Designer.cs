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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OobeWizard));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.wizardPage1 = new AeroWizard.WizardPage();
            this.themedLabel1 = new System.Windows.Forms.Label();
            this.wizardPage2 = new AeroWizard.WizardPage();
            this.themedTableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.browseFolderButton = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.themedLabel2 = new System.Windows.Forms.Label();
            this.vistaFolderBrowserDialog1 = new Ookii.Dialogs.WinForms.VistaFolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wizardPage1.SuspendLayout();
            this.wizardPage2.SuspendLayout();
            this.themedTableLayoutPanel1.SuspendLayout();
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
            this.wizardControl1.NextButtonText = "&Continue";
            this.wizardControl1.Pages.Add(this.wizardPage1);
            this.wizardControl1.Pages.Add(this.wizardPage2);
            this.wizardControl1.Size = new System.Drawing.Size(600, 393);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Text = "wizardControl1";
            this.wizardControl1.Title = "Open Wii Manager";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
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
            this.themedTableLayoutPanel1.Size = new System.Drawing.Size(553, 36);
            this.themedTableLayoutPanel1.TabIndex = 2;
            // 
            // browseFolderButton
            // 
            this.browseFolderButton.Location = new System.Drawing.Point(520, 3);
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
    }
}