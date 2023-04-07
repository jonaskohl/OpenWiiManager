using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Media;
using OpenWiiManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWiiManager.Forms
{
    public partial class OobeWizard : Form
    {
        public string IsoPath => vistaFolderBrowserDialog1.SelectedPath;
        public OobeWizard()
        {
            InitializeComponent();

            browseFolderButton.Image = StockIcons.GetStockIconAsImage(StockIcons.SHSTOCKICONID.SIID_FOLDEROPEN, new Size(16, 16), StockIcons.IconSize.Small);

            wizardControl1.SelectedPageChanged += WizardControl1_SelectedPageChanged;
        }

        private void WizardControl1_SelectedPageChanged(object? sender, EventArgs e)
        {
            if (wizardControl1.SelectedPage == wizardPage4)
            {
                label2.Text = "Checking GameTDB version information... This might take a moment...";
                _ = Task.Run(async () =>
                {
                    await GameTdbSingleton.Instance.NeedsUpdate();

                    Invoke(() =>
                    {
                        label2.Text = "Connecting to GameTDB...";
                    });

                    await GameTdbSingleton.Instance.DownloadDatabase(new Progress<(byte, long, long)>(rep =>
                    {
                        Invoke(() =>
                        {
                            Debug.WriteLine($"Progress: 0x{rep.Item1:X2} - {rep.Item2} / {rep.Item3}");

                            if (rep.Item1 == 0x00)
                                label2.Text = "Downloading GameTDB database file...";
                            else
                                label2.Text = "Extracting GameTDB database file...";

                            if (rep.Item3 >= 0)
                            {
                                progressBar1.Maximum = (int)rep.Item3;
                                progressBar1.Value = (int)rep.Item2;
                                progressBar1.Style = ProgressBarStyle.Continuous;
                            }
                            else
                            {
                                progressBar1.Value = 0;
                                progressBar1.Style = ProgressBarStyle.Marquee;
                            }
                        });
                    }));

                    Invoke(() =>
                    {
                        wizardControl1.NextPage(wizardPage5);
                    });
                }).ContinueWith(t =>
                {
                    t.ThrowIfFaulted();
                });
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.DisableCloseButton();
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (vistaFolderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                pathLabel.Text = vistaFolderBrowserDialog1.SelectedPath;
                pathLabel.Font = pathLabel.Parent.Font;
                wizardPage2.AllowNext = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://www.gametdb.com/") { UseShellExecute = true });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            wizardControl1.NextPage(wizardPage5);
        }
    }
}
