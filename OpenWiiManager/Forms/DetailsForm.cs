using OpenWiiManager.Core;
using OpenWiiManager.Language.Extensions;
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

        Task? fetchDatabaseTask;

        CancellationTokenSource ctsHashes = new();
        CancellationToken ctHashes;

        static readonly Dictionary<string, string> peripheralNames = new()
        {
            { "wiimote", "Wii Remote™" },
            { "nunchuk", "Nunchuk" },
            { "gamecube", "GameCube™ Controller" },
            { "motionplus", "Wii MotionPlus" },
            { "balanceboard", "Wii Balance Board™" },
            { "classiccontroller", "Wii Classic Controller/Classic Controller Pro" },
            { "wheel", "Wii Wheel™" },
            { "zapper", "Wii Zapper" },
            { "drums", "Drums" },
            { "guitar", "Guitar" },
            { "microphone", "Microphone" },
            { "wiispeak", "Wii Speak™" },
            { "3dglasses", "3D Glasses" },
            { "mii", "Mii" },
            { "dancepad", "Dance Pad" },
            { "nintendods", "Nintendo DS™" },
            { "keyboard", "Keyboard" },
            { "udraw", "uDraw GameTablet™" },
        };

        public DetailsForm()
        {
            InitializeComponent();

            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged;

#if !DEBUG
            tabControl1.TabPages.Remove(artworkTabPage);
#endif

            ctHashes = ctsHashes.Token;

            Load += DetailsForm_Load;
            Shown += DetailsForm_Shown;

            button1.Click += Button1_Click;
        }

        private void DetailsForm_Load(object? sender, EventArgs e)
        {
            CenterToParent();
        }

        private void Button1_Click(object? sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            ctsHashes.Cancel();
        }

        private void DetailsForm_Shown(object? sender, EventArgs e)
        {
            FetchDatabaseEntry();
        }

        private void TabControl1_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == hashesTabPage)
                CalculateHashes();
        }

        public void AddGeneralProperty(string name, string? text)
        {
            var label = new Label()
            {
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            var textBox = new TextBox()
            {
                BackColor = SystemColors.Window,
                ForeColor = SystemColors.WindowText,
                BorderStyle = BorderStyle.None,
                Text = text ?? "",
                ReadOnly = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            var i = Math.Max(generalTableLayoutPanel.RowStyles.Count, generalTableLayoutPanel.RowCount) - 1;
            Debug.WriteLine($"Add property {name} as row index {i}");
            generalTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.AutoSize));
            generalTableLayoutPanel.Controls.Add(label, 0, i);
            generalTableLayoutPanel.Controls.Add(textBox, 1, i);
        }

        public void AddGeneralPropertyMonospaced(string name, string? text)
        {
            var label = new Label()
            {
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            var textBox = new TextBox()
            {
                BackColor = SystemColors.Window,
                ForeColor = SystemColors.WindowText,
                BorderStyle = BorderStyle.None,
                Text = text ?? "",
                ReadOnly = true,
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Consolas", 10, FontStyle.Regular)
            };
            var i = Math.Max(generalTableLayoutPanel.RowStyles.Count, generalTableLayoutPanel.RowCount) - 1;
            Debug.WriteLine($"Add property {name} as row index {i}");
            generalTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.AutoSize));
            generalTableLayoutPanel.Controls.Add(label, 0, i);
            generalTableLayoutPanel.Controls.Add(textBox, 1, i);
        }

        public void AddTagListProperty(string name, IEnumerable<string> items)
        {
            var label = new Label()
            {
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            var flpanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Location = Point.Empty,
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 1, 3, 1)
            };
            flpanel.Controls.AddRange(items.Select(itm => new Label()
            {
                Text = itm,
                //BackColor = SystemColors.Control,
                //ForeColor = SystemColors.ControlText,
                BackColor = Color.FromArgb(128, 0, 209),
                ForeColor = Color.White,
                AutoSize = true,
                //BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(0, 2, 4, 2)
            }).ToArray());
            var i = Math.Max(generalTableLayoutPanel.RowStyles.Count, generalTableLayoutPanel.RowCount) - 1;
            Debug.WriteLine($"Add tag list property {name} as row index {i}");
            generalTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.AutoSize));
            generalTableLayoutPanel.Controls.Add(label, 0, i);
            generalTableLayoutPanel.Controls.Add(flpanel, 1, i);
        }
        public void AddRatingsProperty(string name, XElement? ratingElem)
        {
            var label = new Label()
            {
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            var flpanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Location = Point.Empty,
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 1, 3, 1)
            };
            var ratingText = "n/a";
            var ratingDescElems = ratingElem?.Elements("descriptor");
            if (ratingElem != null)
                ratingText = $"{ratingElem?.Attribute("type")?.Value} {ratingElem?.Attribute("value")?.Value}";
            var ratingType = ratingElem?.Attribute("type")?.Value.ToLower();
            var ratingValue = ratingElem?.Attribute("value")?.Value.ToLower();

            if (ratingType == null || ratingValue == null)
            {
                Debug.WriteLine("[WARN] Rating did not have required attributes");
            }
            else
            {
                var img = Properties.Pictograms.ResourceManager.GetObject(ratingType + "_" + ratingValue) as Image;
                var pbox = new PictureBox()
                {
                    SizeMode = PictureBoxSizeMode.AutoSize,
                    Size = new Size(32, 32),
                    Margin = new Padding(0, 2, 4, 2)
                };
                pbox.Image = img ?? pbox.ErrorImage;
                globalToolTip.SetToolTip(pbox, ratingText);
                flpanel.Controls.Add(pbox);
            }

            if (ratingType == "pegi" && ratingDescElems?.Any() == true)
            {
                flpanel.Controls.AddRange(ratingDescElems.Select(e =>
                {
                    var img = Properties.Pictograms.ResourceManager.GetObject(ratingType + "_d_" + e?.Value?.ToLower()) as Image;
                    var pbox = new PictureBox()
                    {
                        SizeMode = PictureBoxSizeMode.AutoSize,
                        Size = new Size(32, 32),
                        Margin = new Padding(0, 2, 4, 2)
                    };
                    pbox.Image = img ?? pbox.ErrorImage;
                    globalToolTip.SetToolTip(pbox, e?.Value?.ToTitleCase() ?? "???");
                    return pbox;
                }).Where(v => v != null).ToArray());
            }

            var i = Math.Max(generalTableLayoutPanel.RowStyles.Count, generalTableLayoutPanel.RowCount) - 1;
            Debug.WriteLine($"Add ratings property {name} as row index {i}");
            generalTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.AutoSize));
            generalTableLayoutPanel.Controls.Add(label, 0, i);
            generalTableLayoutPanel.Controls.Add(flpanel, 1, i);
        }

        public void AddPeripheralsProperty(string name, IEnumerable<(string, bool)> peripherals)
        {
            var label = new Label()
            {
                Text = name,
                AutoSize = true,
                Anchor = AnchorStyles.Left
            };
            var flpanel = new FlowLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = true,
                Location = Point.Empty,
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 1, 3, 1)
            };
            flpanel.Controls.AddRange(peripherals.Select(itm =>
            {
                if (itm.Item1 == null)
                {
                    Debug.WriteLine("[WARN] itm.Item1 == null");
                    return null;
                }
                var img = Properties.Pictograms.ResourceManager.GetObject("wii_peripheral_" + itm.Item1) as Image;
                var pbox = new PictureBox()
                {
                    Size = new Size(24, 24),
                    Margin = new Padding(0, 2, 4, 2)
                };
                pbox.Image = img ?? pbox.ErrorImage;
                globalToolTip.SetToolTip(pbox, (itm.Item2 == true ? "Requires " : "Supports ") + (peripheralNames.ContainsKey(itm.Item1) ? peripheralNames[itm.Item1] : itm.Item1.ToTitleCase()));
                return pbox;
            }).Where(x => x != null).ToArray());
            var i = Math.Max(generalTableLayoutPanel.RowStyles.Count, generalTableLayoutPanel.RowCount) - 1;
            Debug.WriteLine($"Add peripheral property {name} as row index {i}");
            generalTableLayoutPanel.RowStyles.Insert(i, new RowStyle(SizeType.AutoSize));
            generalTableLayoutPanel.Controls.Add(label, 0, i);
            generalTableLayoutPanel.Controls.Add(flpanel, 1, i);
        }

        private void FetchDatabaseEntry()
        {
            if (fetchDatabaseTask != null) return;

            fetchDatabaseTask = GameTdbSingleton.Instance.LookupWiiTitleInfoAsync(GameId).ContinueWith(t =>
            {
                if (t.Result == null)
                {
                    MessageBox.Show("Database lookup failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                GameTDBEntry = t.Result;
                Invoke(() =>
                {
                    PopulateGeneralPage();
                });
            });
        }

        private void PopulateGeneralPage()
        {
            if (GameTDBEntry == null)
            {
                MessageBox.Show("Database entry was null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ratingElem = GameTDBEntry?.Element("rating");

            var dateElem = GameTDBEntry?.Element("date");

            var peripherals = GameTDBEntry?.Element("input")?.Elements("control")?.Select(e => (e?.Attribute("type")?.Value, e?.Attribute("required")?.Value == "true"));

            generalTableLayoutPanel.SuspendLayout();
            AddGeneralPropertyMonospaced("ID", GameTDBEntry?.Element("id")?.Value);
            AddGeneralProperty("Title", GameTDBEntry?.Attribute("name")?.Value);
            AddGeneralProperty("Region", GameTDBEntry?.Element("region")?.Value);
            AddTagListProperty("Language(s)", (GameTDBEntry?.Element("languages")?.Value ?? "").Split(","));
            AddGeneralProperty("Developer", GameTDBEntry?.Element("developer")?.Value);
            AddGeneralProperty("Publisher", GameTDBEntry?.Element("publisher")?.Value);
            AddGeneralProperty("Release date", $"{dateElem?.Attribute("year")?.Value}-{dateElem?.Attribute("month")?.Value?.PadLeft(2, '0')}-{dateElem?.Attribute("day")?.Value?.PadLeft(2, '0')}");
            AddTagListProperty("Genre(s)", (GameTDBEntry?.Element("genre")?.Value ?? "").Split(",").Select(s => s.ToTitleCase()).Select(s => HandleGenreSpecialCases(s)));
            //AddGeneralProperty("Rating", ratingText);
            AddRatingsProperty("Rating", ratingElem);
            AddGeneralProperty("Player count", GameTDBEntry?.Element("input")?.Attribute("players")?.Value);
            AddPeripheralsProperty("Required peripherals", peripherals?.Where(e => e.Item2)!);
            AddPeripheralsProperty("Optional peripherals", peripherals?.Where(e => !e.Item2)!);
            generalTableLayoutPanel.ResumeLayout(true);
        }

        private string HandleGenreSpecialCases(string s)
        {
            return s
                .Replace("3d ", "3D ")
            ;
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
                    if (!IsDisposed)
                        Invoke(() =>
                        {
                            progressBar1.Value = (int)(p / (decimal)total * int.MaxValue);
                        });
                }), ctHashes).ContinueWith(t =>
                {
                    if (!IsDisposed)
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
                    if (!IsDisposed)
                        Invoke(() =>
                        {
                            progressBar2.Value = (int)(p / (decimal)total * int.MaxValue);
                        });
                }), ctHashes).ContinueWith(t =>
                {
                    if (!IsDisposed)
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

                    if (!IsDisposed)
                        Invoke(() =>
                        {
                            progressBar3.Value = (int)(p / (decimal)total * int.MaxValue);
                        });
                }), ctHashes).ContinueWith(t =>
                {
                    if (!IsDisposed)
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
                fetchDatabaseTask ?? Task.CompletedTask
            ).ContinueWith(t =>
            {
                if (IsDisposed)
                    return;

                var fileType = Path.GetExtension(IsoFileName).ToLowerInvariant();

                if (fileType != ".iso")
                {
                    pictureBox1.Hide();
                    pictureBox2.Hide();
                    pictureBox3.Hide();
                    return;
                }

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

        private async Task<string> GetCRC32Async(string gameId, string isoFileName, IProgress<long> progress, CancellationToken cancellationToken = default)
        {
            var hash = HashCache.GetCrc32(gameId);
            if (hash == null)
                hash = await IOUtil.GetFileCRC32Async(isoFileName, progress, cancellationToken);
            HashCache.CacheCrc32(gameId, hash);
            return hash;
        }

        private async Task<string> GetMD5Async(string gameId, string isoFileName, IProgress<long> progress, CancellationToken cancellationToken = default)
        {
            var hash = HashCache.GetMD5(gameId);
            if (hash == null)
                hash = await IOUtil.GetFileMD5Async(isoFileName, progress, cancellationToken);
            HashCache.CacheMD5(gameId, hash);
            return hash;
        }

        private async Task<string> GetSHA1Async(string gameId, string isoFileName, IProgress<long> progress, CancellationToken cancellationToken = default)
        {
            var hash = HashCache.GetSHA1(gameId);
            if (hash == null)
                hash = await IOUtil.GetFileSHA1Async(isoFileName, progress, cancellationToken);
            HashCache.CacheSHA1(gameId, hash);
            return hash;
        }
    }
}
