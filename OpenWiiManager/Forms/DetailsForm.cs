using OpenWiiManager.Services;
using OpenWiiManager.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace OpenWiiManager.Forms
{
    public partial class DetailsForm : Form
    {
        public string IsoFileName { get; set; }
        public string GameId { get; set; }

        private XElement? GameTDBEntry;

        string? hashCrc, hashMd5, hashSha1;

        public DetailsForm()
        {
            InitializeComponent();

            Shown += DetailsForm_Shown;
        }

        private void DetailsForm_Shown(object? sender, EventArgs e)
        {
            progressBar1.Maximum = int.MaxValue;
            progressBar2.Maximum = int.MaxValue;
            progressBar3.Maximum = int.MaxValue;
            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();

            var total = new FileInfo(IsoFileName).Length;

            Task.WhenAll(
                IOUtil.GetFileCRC32Async(IsoFileName, new Progress<long>(p =>
                {
                    Invoke(() =>
                    {
                        progressBar1.Value = (int)(p / (decimal)total * int.MaxValue);
                    });
                })).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        textBox1.Text = t.Result;
                        hashCrc = t.Result;
                        progressBar1.Hide();
                        textBox1.Show();
                    });
                }),
                IOUtil.GetFileMD5Async(IsoFileName, new Progress<long>(p =>
                {
                    Invoke(() =>
                    {
                        progressBar2.Value = (int)(p / (decimal)total * int.MaxValue);
                    });
                })).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        textBox2.Text = t.Result;
                        hashMd5 = t.Result;
                        progressBar2.Hide();
                        textBox2.Show();
                    });
                }),
                IOUtil.GetFileSHA1Async(IsoFileName, new Progress<long>(p =>
                {
                    Invoke(() =>
                    {
                        progressBar3.Value = (int)(p / (decimal)total * int.MaxValue);
                    });
                })).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        textBox3.Text = t.Result;
                        hashSha1 = t.Result;
                        progressBar3.Hide();
                        textBox3.Show();
                    });
                }),
                GameTdbSingleton.Instance.LookupWiiTitleInfoAsync(GameId).ContinueWith(t =>
                {
                    GameTDBEntry = t.Result;
                })
            ).ContinueWith(t =>
            {
                var romElement = GameTDBEntry?.Element("rom");
                if (romElement != null)
                {
                    var expectedHashCrc = romElement.Attribute("crc")?.Value;
                    var expectedHashMd5 = romElement.Attribute("md5")?.Value;
                    var expectedHashSha1 = romElement.Attribute("sha1")?.Value;

                    if (expectedHashCrc == hashCrc) pictureBox1.Image = Properties.Resources.Tick;
                    else pictureBox1.Image = Properties.Resources.Error;

                    if (expectedHashMd5 == hashMd5) pictureBox2.Image = Properties.Resources.Tick;
                    else pictureBox2.Image = Properties.Resources.Error;
                    
                    if (expectedHashSha1 == hashSha1) pictureBox3.Image = Properties.Resources.Tick;
                    else pictureBox3.Image = Properties.Resources.Error;
                } else
                {
                    pictureBox1.Image =
                    pictureBox2.Image =
                    pictureBox3.Image = Properties.Resources.Help_3;
                }
            });
        }
    }
}
