using OpenWiiManager.Core;
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

        bool hashCalculationStarted = false;

        public DetailsForm()
        {
            InitializeComponent();

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged; ;
        }

        private void TabControl1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == hashesTabPage)
                CalculateHashes();
        }

        private void CalculateHashes()
        {
            if (hashCalculationStarted) return;

            hashCalculationStarted = true;

            pictureBox1.Image =
            pictureBox2.Image =
            pictureBox3.Image = Properties.Resources.Clock;

            progressBar1.Maximum = int.MaxValue;
            progressBar2.Maximum = int.MaxValue;
            progressBar3.Maximum = int.MaxValue;

            textBox1.Hide();
            textBox2.Hide();
            textBox3.Hide();

            globalToolTip.SetToolTip(pictureBox1, "Calculating CRC32...");
            globalToolTip.SetToolTip(pictureBox2, "Calculating MD5...");
            globalToolTip.SetToolTip(pictureBox3, "Calculating SHA1...");

            var total = new FileInfo(IsoFileName).Length;

            Task.WhenAll(
                GetCRC32Async(GameId, IsoFileName, new Progress<long>(p =>
                {
                    Invoke(() =>
                    {
                        progressBar1.Value = (int)(p / (decimal)total * int.MaxValue);
                    });
                })).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        var hash = t.Result;
                        if (ApplicationConfigurationSingleton.Instance.UpperCaseHashes)
                            hash = hash.ToUpperInvariant();
                        textBox1.Text = hash;
                        hashCrc = hash;
                        progressBar1.Hide();
                        textBox1.Show();

                        globalToolTip.SetToolTip(pictureBox1, "Waiting for other hashes and/or game lookup to finish...");
                    });
                }),
                GetMD5Async(GameId, IsoFileName, new Progress<long>(p =>
                {
                    Invoke(() =>
                    {
                        progressBar2.Value = (int)(p / (decimal)total * int.MaxValue);
                    });
                })).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        var hash = t.Result;
                        if (ApplicationConfigurationSingleton.Instance.UpperCaseHashes)
                            hash = hash.ToUpperInvariant();
                        textBox2.Text = hash;
                        hashMd5 = hash;
                        progressBar2.Hide();
                        textBox2.Show();

                        globalToolTip.SetToolTip(pictureBox2, "Waiting for other hashes and/or game lookup to finish...");
                    });
                }),
                GetSHA1Async(GameId, IsoFileName, new Progress<long>(p =>
                {
                    Invoke(() =>
                    {
                        progressBar3.Value = (int)(p / (decimal)total * int.MaxValue);
                    });
                })).ContinueWith(t =>
                {
                    Invoke(() =>
                    {
                        var hash = t.Result;
                        if (ApplicationConfigurationSingleton.Instance.UpperCaseHashes)
                            hash = hash.ToUpperInvariant();
                        textBox3.Text = hash;
                        hashSha1 = hash;
                        progressBar3.Hide();
                        textBox3.Show();

                        globalToolTip.SetToolTip(pictureBox3, "Waiting for other hashes and/or game lookup to finish...");
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

                    if (expectedHashCrc == null)
                    {
                        pictureBox1.Image = Properties.Resources.Help_3;
                        globalToolTip.SetToolTip(pictureBox1, "Database does not contain a CRC32 hash for this title");
                    }
                    else if (expectedHashCrc.Equals(hashCrc, StringComparison.InvariantCultureIgnoreCase) == true)
                    {
                        pictureBox1.Image = Properties.Resources.Tick;
                        globalToolTip.SetToolTip(pictureBox1, "CRC32 matches database");
                    }
                    else
                    {
                        pictureBox1.Image = Properties.Resources.Error;
                        globalToolTip.SetToolTip(pictureBox1, "CRC32 does not match database");
                    }

                    if (expectedHashMd5 == null)
                    {
                        pictureBox2.Image = Properties.Resources.Help_3;
                        globalToolTip.SetToolTip(pictureBox2, "Database does not contain a CRC32 hash for this title");
                    }
                    else if (expectedHashMd5.Equals(hashMd5, StringComparison.InvariantCultureIgnoreCase) == true)
                    {
                        pictureBox2.Image = Properties.Resources.Tick;
                        globalToolTip.SetToolTip(pictureBox2, "MD5 matches database");
                    }
                    else
                    {
                        pictureBox2.Image = Properties.Resources.Error;
                        globalToolTip.SetToolTip(pictureBox2, "MD5 does not match database");
                    }

                    if (expectedHashSha1 == null)
                    {
                        pictureBox3.Image = Properties.Resources.Help_3;
                        globalToolTip.SetToolTip(pictureBox3, "Database does not contain a CRC32 hash for this title");
                    }
                    else if (expectedHashSha1.Equals(hashSha1, StringComparison.InvariantCultureIgnoreCase) == true)
                    {
                        pictureBox3.Image = Properties.Resources.Tick;
                        globalToolTip.SetToolTip(pictureBox3, "SHA1 matches database");
                    }
                    else
                    {
                        pictureBox3.Image = Properties.Resources.Error;
                        globalToolTip.SetToolTip(pictureBox3, "SHA1 does not match database");
                    }
                }
                else
                {
                    pictureBox1.Image =
                    pictureBox2.Image =
                    pictureBox3.Image = Properties.Resources.Help_3;

                    globalToolTip.SetToolTip(pictureBox1, "Database does not contain a hash for this game");
                    globalToolTip.SetToolTip(pictureBox2, "Database does not contain a hash for this game");
                    globalToolTip.SetToolTip(pictureBox3, "Database does not contain a hash for this game");
                }
            });
        }

        private async Task<string> GetCRC32Async(string gameId, string isoFileName, IProgress<long> progress)
        {
            var hash = HashCache.GetCrc32(gameId);
            if (hash == null)
                hash = await IOUtil.GetFileCRC32Async(isoFileName, progress);
            HashCache.CacheCrc32(gameId, hash);
            return hash;
        }

        private async Task<string> GetMD5Async(string gameId, string isoFileName, IProgress<long> progress)
        {
            var hash = HashCache.GetMD5(gameId);
            if (hash == null)
                hash = await IOUtil.GetFileMD5Async(isoFileName, progress);
            HashCache.CacheMD5(gameId, hash);
            return hash;
        }

        private async Task<string> GetSHA1Async(string gameId, string isoFileName, IProgress<long> progress)
        {
            var hash = HashCache.GetSHA1(gameId);
            if (hash == null)
                hash = await IOUtil.GetFileSHA1Async(isoFileName, progress);
            HashCache.CacheSHA1(gameId, hash);
            return hash;
        }
    }
}
