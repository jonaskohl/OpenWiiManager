namespace OpenWiiManager.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.toolbarButton1 = new OpenWiiManager.Controls.ToolbarButton();
            this.toolbarButton2 = new OpenWiiManager.Controls.ToolbarButton();
            this.toolbarButton3 = new OpenWiiManager.Controls.ToolbarButton();
            this.toolbarButton4 = new OpenWiiManager.Controls.ToolbarButton();
            this.toolbarButton5 = new OpenWiiManager.Controls.ToolbarButton();
            this.toolbarButton6 = new OpenWiiManager.Controls.ToolbarButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9, 9);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 339);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.okButton);
            this.flowLayoutPanel1.Controls.Add(this.cancelButton);
            this.flowLayoutPanel1.Controls.Add(this.applyButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(343, 306);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(288, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // okButton
            // 
            this.okButton.Image = global::OpenWiiManager.Properties.Resources.Tick;
            this.okButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.okButton.Location = new System.Drawing.Point(3, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(90, 24);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "&OK";
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Image = global::OpenWiiManager.Properties.Resources.Close_Button;
            this.cancelButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cancelButton.Location = new System.Drawing.Point(99, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(90, 24);
            this.cancelButton.TabIndex = 0;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // applyButton
            // 
            this.applyButton.Enabled = false;
            this.applyButton.Image = global::OpenWiiManager.Properties.Resources.Save;
            this.applyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.applyButton.Location = new System.Drawing.Point(195, 3);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(90, 24);
            this.applyButton.TabIndex = 0;
            this.applyButton.Text = "&Apply";
            this.applyButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanel2.BackColor = System.Drawing.SystemColors.Window;
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.toolbarButton1);
            this.flowLayoutPanel2.Controls.Add(this.toolbarButton2);
            this.flowLayoutPanel2.Controls.Add(this.toolbarButton3);
            this.flowLayoutPanel2.Controls.Add(this.toolbarButton4);
            this.flowLayoutPanel2.Controls.Add(this.toolbarButton5);
            this.flowLayoutPanel2.Controls.Add(this.toolbarButton6);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(3);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(132, 297);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // toolbarButton1
            // 
            this.toolbarButton1.Checked = true;
            this.toolbarButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolbarButton1.Image = global::OpenWiiManager.Properties.Resources.Tools;
            this.toolbarButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolbarButton1.Location = new System.Drawing.Point(6, 3);
            this.toolbarButton1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.toolbarButton1.Name = "toolbarButton1";
            this.toolbarButton1.Size = new System.Drawing.Size(120, 23);
            this.toolbarButton1.TabIndex = 0;
            this.toolbarButton1.Text = "&General";
            this.toolbarButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolbarButton1.UseVisualStyleBackColor = true;
            this.toolbarButton1.Click += new System.EventHandler(this.toolbarButton1_Click);
            // 
            // toolbarButton2
            // 
            this.toolbarButton2.Checked = false;
            this.toolbarButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolbarButton2.Image = global::OpenWiiManager.Properties.Resources.Database;
            this.toolbarButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolbarButton2.Location = new System.Drawing.Point(6, 29);
            this.toolbarButton2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.toolbarButton2.Name = "toolbarButton2";
            this.toolbarButton2.Size = new System.Drawing.Size(120, 23);
            this.toolbarButton2.TabIndex = 0;
            this.toolbarButton2.Text = "&Database";
            this.toolbarButton2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolbarButton2.UseVisualStyleBackColor = true;
            this.toolbarButton2.Click += new System.EventHandler(this.toolbarButton1_Click);
            // 
            // toolbarButton3
            // 
            this.toolbarButton3.Checked = false;
            this.toolbarButton3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolbarButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolbarButton3.Location = new System.Drawing.Point(6, 55);
            this.toolbarButton3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.toolbarButton3.Name = "toolbarButton3";
            this.toolbarButton3.Size = new System.Drawing.Size(120, 23);
            this.toolbarButton3.TabIndex = 0;
            this.toolbarButton3.Text = "Test 1";
            this.toolbarButton3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolbarButton3.UseVisualStyleBackColor = true;
            this.toolbarButton3.Click += new System.EventHandler(this.toolbarButton1_Click);
            // 
            // toolbarButton4
            // 
            this.toolbarButton4.Checked = false;
            this.toolbarButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolbarButton4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolbarButton4.Location = new System.Drawing.Point(6, 81);
            this.toolbarButton4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.toolbarButton4.Name = "toolbarButton4";
            this.toolbarButton4.Size = new System.Drawing.Size(120, 23);
            this.toolbarButton4.TabIndex = 0;
            this.toolbarButton4.Text = "Test 2";
            this.toolbarButton4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolbarButton4.UseVisualStyleBackColor = true;
            this.toolbarButton4.Click += new System.EventHandler(this.toolbarButton1_Click);
            // 
            // toolbarButton5
            // 
            this.toolbarButton5.Checked = false;
            this.toolbarButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolbarButton5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolbarButton5.Location = new System.Drawing.Point(6, 107);
            this.toolbarButton5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.toolbarButton5.Name = "toolbarButton5";
            this.toolbarButton5.Size = new System.Drawing.Size(120, 23);
            this.toolbarButton5.TabIndex = 0;
            this.toolbarButton5.Text = "Test 3";
            this.toolbarButton5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolbarButton5.UseVisualStyleBackColor = true;
            this.toolbarButton5.Click += new System.EventHandler(this.toolbarButton1_Click);
            // 
            // toolbarButton6
            // 
            this.toolbarButton6.Checked = false;
            this.toolbarButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toolbarButton6.Enabled = false;
            this.toolbarButton6.Image = global::OpenWiiManager.Properties.Resources.Save;
            this.toolbarButton6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolbarButton6.Location = new System.Drawing.Point(6, 133);
            this.toolbarButton6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.toolbarButton6.Name = "toolbarButton6";
            this.toolbarButton6.Size = new System.Drawing.Size(120, 23);
            this.toolbarButton6.TabIndex = 0;
            this.toolbarButton6.Text = "Test 4";
            this.toolbarButton6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolbarButton6.UseVisualStyleBackColor = true;
            this.toolbarButton6.Click += new System.EventHandler(this.toolbarButton1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(141, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 297);
            this.panel1.TabIndex = 2;
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.applyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(652, 357);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button okButton;
        private Button cancelButton;
        private Button applyButton;
        private FlowLayoutPanel flowLayoutPanel2;
        private Controls.ToolbarButton toolbarButton1;
        private Controls.ToolbarButton toolbarButton2;
        private Controls.ToolbarButton toolbarButton3;
        private Controls.ToolbarButton toolbarButton4;
        private Controls.ToolbarButton toolbarButton5;
        private Controls.ToolbarButton toolbarButton6;
        private Panel panel1;
    }
}