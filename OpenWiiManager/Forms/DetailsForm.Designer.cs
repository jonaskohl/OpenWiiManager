namespace OpenWiiManager.Forms
{
    partial class DetailsForm
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
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            tabControl1 = new TabControl();
            generalTabPage = new TabPage();
            panel1 = new Panel();
            generalTableLayoutPanel = new TableLayoutPanel();
            artworkTabPage = new TabPage();
            hashesTabPage = new TabPage();
            tableLayoutPanel2 = new TableLayoutPanel();
            progressBar3 = new ProgressBar();
            textBox3 = new TextBox();
            progressBar2 = new ProgressBar();
            textBox2 = new TextBox();
            progressBar1 = new ProgressBar();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button1 = new Button();
            globalToolTip = new ToolTip(components);
            tableLayoutPanel1.SuspendLayout();
            tabControl1.SuspendLayout();
            generalTabPage.SuspendLayout();
            panel1.SuspendLayout();
            hashesTabPage.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tabControl1, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutPanel1.Size = new Size(401, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(generalTabPage);
            tabControl1.Controls.Add(artworkTabPage);
            tabControl1.Controls.Add(hashesTabPage);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(3, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(395, 409);
            tabControl1.TabIndex = 0;
            // 
            // generalTabPage
            // 
            generalTabPage.BackColor = SystemColors.Window;
            generalTabPage.Controls.Add(panel1);
            generalTabPage.ForeColor = SystemColors.WindowText;
            generalTabPage.Location = new Point(4, 24);
            generalTabPage.Name = "generalTabPage";
            generalTabPage.Padding = new Padding(3);
            generalTabPage.Size = new Size(387, 381);
            generalTabPage.TabIndex = 0;
            generalTabPage.Text = "General";
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(generalTableLayoutPanel);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Margin = new Padding(0);
            panel1.Name = "panel1";
            panel1.Size = new Size(381, 375);
            panel1.TabIndex = 1;
            // 
            // generalTableLayoutPanel
            // 
            generalTableLayoutPanel.AutoSize = true;
            generalTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            generalTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            generalTableLayoutPanel.ColumnCount = 2;
            generalTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            generalTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            generalTableLayoutPanel.Dock = DockStyle.Top;
            generalTableLayoutPanel.Location = new Point(0, 0);
            generalTableLayoutPanel.Name = "generalTableLayoutPanel";
            generalTableLayoutPanel.RowCount = 1;
            generalTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            generalTableLayoutPanel.Size = new Size(381, 2);
            generalTableLayoutPanel.TabIndex = 0;
            // 
            // artworkTabPage
            // 
            artworkTabPage.Location = new Point(4, 24);
            artworkTabPage.Name = "artworkTabPage";
            artworkTabPage.Padding = new Padding(3);
            artworkTabPage.Size = new Size(387, 381);
            artworkTabPage.TabIndex = 2;
            artworkTabPage.Text = "Artwork";
            artworkTabPage.UseVisualStyleBackColor = true;
            // 
            // hashesTabPage
            // 
            hashesTabPage.Controls.Add(tableLayoutPanel2);
            hashesTabPage.Location = new Point(4, 24);
            hashesTabPage.Name = "hashesTabPage";
            hashesTabPage.Padding = new Padding(3);
            hashesTabPage.Size = new Size(387, 381);
            hashesTabPage.TabIndex = 1;
            hashesTabPage.Text = "Hashes";
            hashesTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(progressBar3, 0, 5);
            tableLayoutPanel2.Controls.Add(textBox3, 0, 4);
            tableLayoutPanel2.Controls.Add(progressBar2, 0, 3);
            tableLayoutPanel2.Controls.Add(textBox2, 0, 2);
            tableLayoutPanel2.Controls.Add(progressBar1, 0, 1);
            tableLayoutPanel2.Controls.Add(textBox1, 0, 0);
            tableLayoutPanel2.Controls.Add(pictureBox1, 1, 0);
            tableLayoutPanel2.Controls.Add(pictureBox2, 1, 2);
            tableLayoutPanel2.Controls.Add(pictureBox3, 1, 4);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(381, 375);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // progressBar3
            // 
            progressBar3.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            progressBar3.Location = new Point(3, 118);
            progressBar3.Name = "progressBar3";
            progressBar3.Size = new Size(368, 8);
            progressBar3.Style = ProgressBarStyle.Continuous;
            progressBar3.TabIndex = 5;
            // 
            // textBox3
            // 
            textBox3.Dock = DockStyle.Fill;
            textBox3.Location = new Point(3, 89);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(368, 23);
            textBox3.TabIndex = 4;
            // 
            // progressBar2
            // 
            progressBar2.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            progressBar2.Location = new Point(3, 75);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(368, 8);
            progressBar2.Style = ProgressBarStyle.Continuous;
            progressBar2.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Dock = DockStyle.Fill;
            textBox2.Location = new Point(3, 46);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(368, 23);
            textBox2.TabIndex = 2;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(3, 32);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(368, 8);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(3, 3);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(368, 23);
            textBox1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.Location = new Point(377, 21);
            pictureBox1.Name = "pictureBox1";
            tableLayoutPanel2.SetRowSpan(pictureBox1, 2);
            pictureBox1.Size = new Size(1, 1);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.None;
            pictureBox2.Location = new Point(377, 64);
            pictureBox2.Name = "pictureBox2";
            tableLayoutPanel2.SetRowSpan(pictureBox2, 2);
            pictureBox2.Size = new Size(1, 1);
            pictureBox2.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.Anchor = AnchorStyles.None;
            pictureBox3.Location = new Point(377, 107);
            pictureBox3.Name = "pictureBox3";
            tableLayoutPanel2.SetRowSpan(pictureBox3, 2);
            pictureBox3.Size = new Size(1, 1);
            pictureBox3.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Right;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Location = new Point(317, 418);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(81, 29);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Right;
            button1.DialogResult = DialogResult.OK;
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "&OK";
            button1.UseVisualStyleBackColor = true;
            // 
            // DetailsForm
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = button1;
            ClientSize = new Size(401, 450);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DetailsForm";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Details";
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tabControl1.ResumeLayout(false);
            generalTabPage.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            hashesTabPage.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TabControl tabControl1;
        private TabPage generalTabPage;
        private TabPage hashesTabPage;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox textBox1;
        private ProgressBar progressBar1;
        private TextBox textBox3;
        private ProgressBar progressBar3;
        private TextBox textBox2;
        private ProgressBar progressBar2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private ToolTip globalToolTip;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button1;
        private TabPage artworkTabPage;
        private TableLayoutPanel generalTableLayoutPanel;
        private Panel panel1;
    }
}