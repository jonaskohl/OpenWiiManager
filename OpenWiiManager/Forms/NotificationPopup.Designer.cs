namespace OpenWiiManager.Forms
{
    partial class NotificationPopup
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
            this.listBoxEx1 = new OpenWiiManager.Controls.ListBoxEx();
            this.SuspendLayout();
            // 
            // listBoxEx1
            // 
            this.listBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxEx1.EmptyText = "You have no notifications";
            this.listBoxEx1.FormattingEnabled = true;
            this.listBoxEx1.IntegralHeight = false;
            this.listBoxEx1.ItemHeight = 15;
            this.listBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.listBoxEx1.Name = "listBoxEx1";
            this.listBoxEx1.Size = new System.Drawing.Size(480, 320);
            this.listBoxEx1.TabIndex = 0;
            // 
            // NotificationPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.listBoxEx1);
            this.HideOnDeactivate = true;
            this.Name = "NotificationPopup";
            this.Text = "NotificationPopup";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ListBoxEx listBoxEx1;
    }
}