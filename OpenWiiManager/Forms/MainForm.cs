//#define FEATURE__SHA1_ISO

using Flurl.Util;
using Microsoft.VisualBasic.Devices;
using OpenWiiManager.Controls;
using OpenWiiManager.Controls.Data;
using OpenWiiManager.Core;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Services;
using OpenWiiManager.Tools;
using OpenWiiManager.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace OpenWiiManager.Forms
{
    public partial class MainForm : Form
    {
        private TasksPopup tasksPopup;
        private NotificationPopup notificationPopup;
        private bool allowTasksPopupClose = false;
        private bool allowNotificationPopupClose = false;
        private Font IdColumnFont = new Font("Consolas", 10);
        private ListViewColumnSorter sorter;
        private BalloonToolTip btt;
        private CancellationTokenSource gameSelectCancellationTokenSource = new();
        private CancellationToken gameSelectCancellationToken;

        public class BackgroundOperation
        {
            public string? Message { get; init; }
            public Task? Operation { get; init; }

            public bool IsFinished => Operation?.IsCompleted == true;
        }

        readonly ObservableCollection<BackgroundOperation> backgroundOperations = new();

#if FEATURE__SHA1_ISO
        int __hashColumnIndex = -1;
#endif

        public MainForm()
        {
            InitializeComponent();

            tasksPopup = new();
            tasksPopup.OperationsList = backgroundOperations;
            tasksPopup.FormClosing += TasksPopup_FormClosing;

            ToolStripManager.Renderer = new AeroToolStripRenderer(ToolBarTheme.Toolbar);

            notificationPopup = new();
            notificationPopup.FormClosing += NotificationPopup_FormClosing;

            ApplicationStateSingleton.EnsureInstance();
            ApplicationConfigurationSingleton.EnsureInstance();

            if (ModifierKeys == Keys.Shift && MessageBox.Show("Do you wish to erase the application state file?", "Open Wii Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ApplicationStateSingleton.Instance.Reset();
            else if (ModifierKeys == (Keys.Control | Keys.Shift) && MessageBox.Show("Do you wish to erase the application configuration file?", "Open Wii Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                ApplicationConfigurationSingleton.Instance.Reset();

            if (ApplicationStateSingleton.Instance.MainWindowSplitDistance != default)
                splitter1.SplitPosition = ApplicationStateSingleton.Instance.MainWindowSplitDistance;

            if (ApplicationStateSingleton.Instance.MainWindowSize != default)
                ClientSize = ApplicationStateSingleton.Instance.MainWindowSize;

            ResizeEnd += MainForm_ResizeEnd;
            splitter1.SplitterMoved += Splitter1_SplitterMoved;

            listView1.HandleCreated += ListView1_HandleCreated;
            listView1.ListViewItemSorter = sorter = new();
            listView1.SetSortIcon(0, SortOrder.Ascending);
            sorter.Order = SortOrder.Ascending;
            sorter.SortColumn = 0;

#if FEATURE__SHA1_ISO
            __hashColumnIndex = listView1.Columns.Add("Hash (SHA1)").Index;
#endif

#if !DEBUG
            debugShowBalloonToolStripMenuItem.Visible = false;
            notificationsButton.Visible = false;
#endif

            gameSelectCancellationToken = gameSelectCancellationTokenSource.Token;

            Shown += MainForm_Shown;
            statusStrip1.HandleCreated += StatusStrip1_HandleCreated;
            searchToolStripTextBox.TextBox.HandleCreated += TextBox_HandleCreated;

            showFilesInExplorerToolStripMenuItem.Image = ShellUtil.GetIconAsImage(Environment.ExpandEnvironmentVariables(@"%systemroot%\explorer.exe"), ShellUtil.ShellIconSize.Small);
        }

        private void SearchToolStripTextBox_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Properties.Resources.Zoom, new Rectangle(2, (searchToolStripTextBox.Height - 16) / 2, 16, 16));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.F))
            {
                msg.Result = IntPtr.Zero;
                searchToolStripTextBox.Focus();
                searchToolStripTextBox.SelectAll();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void TextBox_HandleCreated(object? sender, EventArgs e)
        {
            User32.SendMessage(searchToolStripTextBox.TextBox.Handle, Constants.EM_SETCUEBANNER, 0, "Search... (Ctrl+F)");
            //User32.SendMessage(searchToolStripTextBox.TextBox.Handle, Constants.EM_SETMARGINS, Constants.EC_LEFTMARGIN, 30);

            /*
            Kernel32.SetLastError(0);
            var result = InteropUtil.AddSubclass(searchToolStripTextBox.TextBox, (IntPtr hWnd, uint msg, IntPtr lParam, IntPtr wParam, ref bool @break) =>
            {
                Debug.WriteLine("Window message 0x" + msg.ToString("X8"));

                if (msg == Constants.WM_ACTIVATE)
                {
                    @break = true;
                    return IntPtr.Zero;
                }

                return IntPtr.Zero;
            }, out IntPtr textBoxOldWndProc);

            if (result == 0)
            {
                MessageBox.Show("Error with HRESULT 0x" + Marshal.GetLastWin32Error().ToString("X8") + " occurred while calling AddSubclass", "Win32 error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                Debug.WriteLine("Subclass result: " + result);
            */
        }

        private void MainForm_LocationChanged(object? sender, EventArgs e)
        {
            btt.Track(statusStrip1.PointToScreen(notificationsButton.Bounds.Location));
        }

        private void StatusStrip1_HandleCreated(object? sender, EventArgs e)
        {

        }

        private void ListView1_HandleCreated(object? sender, EventArgs e)
        {
            UxTheme.SetWindowTheme(listView1.Handle, "Explorer", null);
            User32.SendMessage(listView1.Handle, Constants.WM_CHANGEUISTATE, InteropUtil.MakeLong(Constants.UIS_SET, Constants.UISF_HIDEFOCUS), 0);
        }

        private void NotificationPopup_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = !allowNotificationPopupClose;
                notificationPopup.Hide();
            }
        }

        private void TasksPopup_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = !allowTasksPopupClose;
                tasksPopup.Hide();
            }
        }

        private void Splitter1_SplitterMoved(object? sender, SplitterEventArgs e)
        {
            ApplicationStateSingleton.Instance.MainWindowSplitDistance = splitter1.SplitPosition;
        }

        private void MainForm_ResizeEnd(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                ApplicationStateSingleton.Instance.MainWindowSize = ClientSize;
        }

        private void AddAndAutoRemoveBackgroundOperation(BackgroundOperation op)
        {
            AddBackgroundOperation(op);
            op.Operation?.ContinueWith(_ =>
            {
                RemoveBackgroundOperation(op);
            });
        }

        private void AddBackgroundOperation(BackgroundOperation op)
        {
            backgroundOperations.Add(op);

            Invoke(() =>
            {
                backgroundOperationLabel.Visible = true;
                backgroundOperationProgressBar.Visible = true;
                backgroundTaskPopupButton.Image = Properties.Resources.TaskBusy;
                if (backgroundOperations.Count > 1)
                {
                    var max = Math.Max(backgroundOperationProgressBar.Maximum, backgroundOperations.Count);
                    backgroundOperationProgressBar.Maximum = max;
                    backgroundOperationProgressBar.Value = Math.Max(0, max - backgroundOperations.Count);

                    backgroundOperationLabel.Text = $"{backgroundOperations.Count} tasks are running";
                    backgroundOperationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
                }
                else
                {
                    backgroundOperationLabel.Text = op.Message;
                    backgroundOperationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                    backgroundOperationProgressBar.Maximum = 1;
                    backgroundOperationProgressBar.Value = 1;
                }
            });
        }

        private void RemoveBackgroundOperation(BackgroundOperation op)
        {
            if (backgroundOperations.Contains(op))
                backgroundOperations.Remove(op);

            Invoke(() =>
            {
                if (backgroundOperations.Count < 1)
                {
                    backgroundOperationProgressBar.Value = 0;
                    backgroundOperationProgressBar.Maximum = 0;
                    backgroundOperationProgressBar.Visible = false;
                    backgroundTaskPopupButton.Image = Properties.Resources.Task;
                    //backgroundOperationLabel.Text = "Idle";
                    backgroundOperationLabel.Text = "";
                    backgroundOperationLabel.Visible = false;
                    backgroundOperationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                }
                else
                {
                    backgroundTaskPopupButton.Image = Properties.Resources.TaskBusy;
                    backgroundOperationProgressBar.Visible = true;
                    backgroundOperationLabel.Visible = true;
                    backgroundOperationLabel.Text = backgroundOperations.Count > 1 ? $"{backgroundOperations.Count} tasks are running" : backgroundOperations.FirstOrDefault()?.Message ?? "";
                    backgroundOperationProgressBar.Value = Math.Min(backgroundOperationProgressBar.Value + 1, backgroundOperationProgressBar.Maximum);
                    backgroundOperationProgressBar.Style = backgroundOperations.Count > 1 ? System.Windows.Forms.ProgressBarStyle.Continuous : System.Windows.Forms.ProgressBarStyle.Marquee;
                }

            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && backgroundOperations.Count > 0)
            {
                var res = MessageBox.Show("The following background tasks are still pending:\n\n" + string.Join("\n", backgroundOperations.Select(o => o?.Message ?? "<<NULLMESSAGE>>")) + "\n\nPress [Cancel] to return to the application.\nPress [Try again] to check if there are still tasks pending.\nPress [Continue] to force-quit the application. Note: This may lead to data corruption.", "", MessageBoxButtons.CancelTryContinue, MessageBoxIcon.Exclamation);
                if (res == DialogResult.Continue)
                    e.Cancel = false;
                else if (res == DialogResult.TryAgain)
                {
                    e.Cancel = true;
                    Close();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private void IndeterminateBackgroundOperation(string text, Task operation)
        {
            AddAndAutoRemoveBackgroundOperation(new BackgroundOperation()
            {
                Message = text,
                Operation = operation
            });
            operation.ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                    throw t.Exception;
            });
        }

        private void IndeterminateBackgroundOperation<TResult>(string text, Task<TResult> operation)
        {
            AddAndAutoRemoveBackgroundOperation(new BackgroundOperation()
            {
                Message = text,
                Operation = operation
            });
            operation.ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                    throw t.Exception;
            });
        }
        private async Task IndeterminateBackgroundOperationAsync(string text, Task operation)
        {
            AddAndAutoRemoveBackgroundOperation(new BackgroundOperation()
            {
                Message = text,
                Operation = operation
            });
            await operation;
        }

        private async Task<TResult> IndeterminateBackgroundOperationAsync<TResult>(string text, Task<TResult> operation)
        {
            AddAndAutoRemoveBackgroundOperation(new BackgroundOperation()
            {
                Message = text,
                Operation = operation
            });
            return await operation;
        }

        private void MainForm_Shown(object? sender, EventArgs e)
        {

            tasksPopup.Opacity = 0;
            tasksPopup.Show();
            tasksPopup.Hide();
            tasksPopup.Opacity = 1;
            notificationPopup.Opacity = 0;
            notificationPopup.Show();
            notificationPopup.Hide();
            notificationPopup.Opacity = 1;

            //for (var i = 0; i < 20; ++i) IndeterminateBackgroundOperation("Test " + i, Task.Delay(1000 + i * 100)); // Debug

            CheckForOOBE();
            Task.WhenAll(
                CheckForDatabaseUpdate(),
                ScanISOs()
            ).ContinueWith(_ =>
            {
                GC.Collect();
            });

            //notificationToolTip.Show("Lorem ipsum dolor sit amet", statusStrip1, notificationsButton.Bounds.Location);
            //UIUtil.ShowBalloon(statusStrip1, "Lorem ipsum dolor sit amet");
        }

        private Task ScanISOs()
        {
            refreshToolStripMenuItem.Enabled = false;
            listView1.Items.Clear();
            return IndeterminateBackgroundOperationAsync("Scanning for games", Task.Run(async () =>
            {
                if (string.IsNullOrWhiteSpace(ApplicationConfigurationSingleton.Instance.IsoPath) || !Directory.Exists(ApplicationConfigurationSingleton.Instance.IsoPath))
                    return;

                foreach (var file in
                           Directory.EnumerateFiles(ApplicationConfigurationSingleton.Instance.IsoPath, "*.iso")
                    .Union(Directory.EnumerateFiles(ApplicationConfigurationSingleton.Instance.IsoPath, "*.rvz"))
                    //.Union(Directory.EnumerateFiles(ApplicationConfigurationSingleton.Instance.IsoPath, "*.nkit"))
                    .Union(Directory.EnumerateFiles(ApplicationConfigurationSingleton.Instance.IsoPath, "*.wbfs"))
                )
                {
                    var meta = await GetGameFileMeta(file);

                    if (meta == null)
                        continue;

                    Debug.WriteLine(file);
                    listView1.Invoke(() =>
                    {
                        var basename = Path.GetFileName(file);
                        var item = new ListViewItem(basename); // subitems[0]
                        item.UseItemStyleForSubItems = false;
                        var idSubitem = new ListViewItem.ListViewSubItem(item, meta.GameId);
                        idSubitem.Font = IdColumnFont;
                        item.SubItems.Add(idSubitem); // subitems[1]
                        item.SubItems.Add(meta.Title); // subitems[2]
                        item.ImageIndex = 4;
                        if (meta.Region == GameRegion._UNKNOWN_)
                            item.SubItems.Add($"Unknown (0x{meta.__rawRegionByteValue:X2} '{(char)meta.__rawRegionByteValue}')"); // subitems[3]
                        else
                            item.SubItems.Add(meta.Region.ToString2()); // subitems[4]
                        listView1.Items.Add(item);
                        item.SubItems.Add(""); // Developer // subitems[5]
                        item.SubItems.Add(""); // Publisher // subitems[6]
                        item.SubItems.Add(""); // Languages // subitems[7]
                        item.SubItems.Add(""); // Date // subitems[8]
#if FEATURE__SHA1_ISO
                        item.SubItems.Add(""); // Hash
#endif
                        item.Tag = file;

                        IndeterminateBackgroundOperationAsync($"Scanning database for file {basename}", Task.Run(() => GetDeferredWiiTDBInfo(meta.GameId, item, file))).ContinueWith(t =>
                        {
                            t.ThrowIfFaulted();
                            Invoke(() => listView1.Sort());
                        });
                    });
                }
            }).ContinueWith(async t =>
            {
#if FEATURE__SHA1_ISO
                await listView1.Items.OfType<ListViewItem>().ParallelForEachAsync(async itm =>
                {
                    var file = itm.Tag?.ToString() ?? "";
                    await IndeterminateBackgroundOperationAsync($"Calculating hash of {Path.GetFileName(file)}", GetDeferredFileHash(itm, file));
                }, maxDegreeOfParallelism: 1);
#endif

                Invoke(() =>
                {
                    refreshToolStripMenuItem.Enabled = true;
                });
                t.ThrowIfFaulted();
            })).ContinueWith(_ =>
            {
                Invoke(() =>
                {
                    var source = new AutoCompleteStringCollection();
                    foreach (ListViewItem item in listView1.Items)
                        foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                            source.Add(subitem.Text);

                    foreach (ColumnHeader header in listView1.Columns)
                    {
                        Debug.WriteLine("Header: " + header.Text);
                        source.Add($"{{{header.Text}");
                    }

                    searchToolStripTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    searchToolStripTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    searchToolStripTextBox.AutoCompleteCustomSource = source;
                });
            });
        }

        public enum GameRegion
        {
            Germany,
            USA,
            France,
            Italy,
            Japan,
            Korea,
            PAL,
            Russia,
            Spain,
            Taiwan,
            Australia,
            _UNKNOWN_,
        }

        private readonly Dictionary<byte, GameRegion> regionMapping = new()
        {
            { (byte)'D', GameRegion.Germany },
            { (byte)'E', GameRegion.USA },
            { (byte)'F', GameRegion.France },
            { (byte)'I', GameRegion.Italy },
            { (byte)'J', GameRegion.Japan },
            { (byte)'K', GameRegion.Korea },
            { (byte)'P', GameRegion.PAL },
            { (byte)'R', GameRegion.Russia },
            { (byte)'S', GameRegion.Spain },
            { (byte)'T', GameRegion.Taiwan },
            { (byte)'U', GameRegion.Australia },
        };

        class IsoMeta
        {
            internal byte @__rawRegionByteValue;

            public GameRegion Region;
            public string GameId;
            public string Title;
            public byte Version;
        }

        const uint WII_MAGICWORD_ONWII = 0xA39E1C5D; // Wii magic word is this value for Wii games (would be 0 for GameCube games)
        const uint GAMECUBE_MAGICWORD_ONWII = 0x00000000; // GameCube magic word is all zeroes for Wii games (would be non-0 for GameCube games)
        const uint SYSTEM_MAGICWORD = 0x8E1AF8C3;
        const long ISO_LENGTH = 0x118240000;


        private async Task<IsoMeta?> GetGameFileMeta(string file)
        {
            var ext = new FileInfo(file).Extension.ToLower();
            return ext switch
            {
                ".iso" => await GetIsoMeta(file),
                ".wbfs" => await GetWbfsMeta(file),
                ".rvz" => await GetRvzMeta(file),
                _ => null
            };
        }

        private Task<IsoMeta?> GetRvzMeta(string file)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(file))
                    return null;

                var info = new FileInfo(file);

                using var hFile = File.Open(file, FileMode.Open);
                using var breader = new BinaryReader(hFile);
                var rvzBytes = breader.ReadBytes(3);
                if (rvzBytes.All(b => b == 0)) // All null bytes. Never the case on an RVZ file
                    return null;
                hFile.Seek(0x58, SeekOrigin.Begin);
                var idBytes = breader.ReadBytes(6);
                if (idBytes.All(b => b == 0)) // All null bytes. Never the case on an RVZ file
                    return null;
                hFile.Seek(0x5F, SeekOrigin.Begin);
                var version = breader.ReadByte();
                hFile.Seek(0x78, SeekOrigin.Begin);
                var titleBytes = breader.ReadBytes(0x40);
                var idString = Encoding.ASCII.GetString(idBytes);
                var titleString = Encoding.ASCII.GetString(titleBytes);
                var regionByte = idBytes[3];
                GameRegion region;
                if (!regionMapping.TryGetValue(regionByte, out region))
                    region = GameRegion._UNKNOWN_;
                return new IsoMeta()
                {
                    @__rawRegionByteValue = regionByte,
                    GameId = idString,
                    Region = region,
                    Title = titleString,
                    Version = version,
                };
            });
        }

        private Task<IsoMeta?> GetWbfsMeta(string file)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(file))
                    return null;

                var info = new FileInfo(file);

                using var hFile = File.Open(file, FileMode.Open);
                using var breader = new BinaryReader(hFile);
                var wbfsBytes = breader.ReadBytes(4);
                if (wbfsBytes.All(b => b == 0)) // All null bytes. Never the case on an WBFS file
                    return null;
                hFile.Seek(0x200, SeekOrigin.Begin);
                var idBytes = breader.ReadBytes(6);
                if (idBytes.All(b => b == 0)) // All null bytes. Never the case on an WBFS file
                    return null;
                hFile.Seek(0x207, SeekOrigin.Begin);
                var version = breader.ReadByte();
                hFile.Seek(0x220, SeekOrigin.Begin);
                var titleBytes = breader.ReadBytes(0x40);
                var idString = Encoding.ASCII.GetString(idBytes);
                var titleString = Encoding.ASCII.GetString(titleBytes);
                var regionByte = idBytes[3];
                GameRegion region;
                if (!regionMapping.TryGetValue(regionByte, out region))
                    region = GameRegion._UNKNOWN_;
                return new IsoMeta()
                {
                    @__rawRegionByteValue = regionByte,
                    GameId = idString,
                    Region = region,
                    Title = titleString,
                    Version = version,
                };
            });
        }

        private Task<IsoMeta?> GetIsoMeta(string file)
        {
            return Task.Run(() =>
            {
                if (!File.Exists(file))
                    return null;

                var info = new FileInfo(file);
                if (info.Length != ISO_LENGTH)
                    return null;

                using var hFile = File.Open(file, FileMode.Open);
                using var breader = new BinaryReader(hFile);
                var idBytes = breader.ReadBytes(6);
                if (idBytes.All(b => b == 0)) // All null bytes. Never the case on an actual Wii game
                    return null;
                hFile.Seek(0x18, SeekOrigin.Begin);
                var wiiMagicWord = breader.ReadUInt32();
                if (wiiMagicWord != WII_MAGICWORD_ONWII) // Check if Wii magic word matches. See https://wiibrew.org/wiki/Wii_disc#Header
                    return null;
                var gcMagicWord = breader.ReadUInt32();
                if (gcMagicWord != GAMECUBE_MAGICWORD_ONWII) // Check if GameCube magic word matches
                    return null;
                hFile.Seek(0x4FFFC, SeekOrigin.Begin);
                var sysMagicWord = breader.ReadUInt32();
                if (sysMagicWord != SYSTEM_MAGICWORD)
                    return null;
                hFile.Seek(0x20, SeekOrigin.Begin);
                var titleBytes = breader.ReadBytes(0x40);
                var idString = Encoding.ASCII.GetString(idBytes);
                var titleString = Encoding.ASCII.GetString(titleBytes);
                var regionByte = idBytes[3];
                hFile.Seek(7, SeekOrigin.Begin);
                var version = breader.ReadByte();
                GameRegion region;
                if (!regionMapping.TryGetValue(regionByte, out region))
                    region = GameRegion._UNKNOWN_;
                return new IsoMeta()
                {
                    @__rawRegionByteValue = regionByte,
                    GameId = idString,
                    Region = region,
                    Title = titleString,
                    Version = version,
                };
            });
        }

        private async Task GetDeferredWiiTDBInfo(string id, ListViewItem item, string filename)
        {
            var info = await GameTdbSingleton.Instance.LookupWiiTitleInfoAsync(id);
            Invoke(() =>
            {
                var region = info?.Element("region")?.Value;
                var name = info?.Attribute("name")?.Value;
                var developer = info?.Element("developer")?.Value;
                var publisher = info?.Element("publisher")?.Value;
                var languages = info?.Element("languages")?.Value;
                var tagDate = info?.Element("date");
                var dateYear = tagDate?.Attribute("year")?.Value;
                var dateMonth = tagDate?.Attribute("month")?.Value;
                var dateDay = tagDate?.Attribute("day")?.Value;

                var xdoc = new XDocument(
                    new XElement("owmgame", new XAttribute("format", "1"), new XAttribute("id", id),
                        info
                    )
                );

                //try
                //{
                //    File.WriteAllText(filename + ":owminfo", xdoc.ToString(), Encoding.UTF8);
                //}
                //catch
                //{
                //    var fname = filename + ".INFO.xog";
                //    if (File.Exists(fname))
                //        File.SetAttributes(fname, FileAttributes.Normal);
                //    File.WriteAllText(fname, xdoc.ToString(), Encoding.UTF8);
                //    File.SetAttributes(fname, FileAttributes.Hidden);
                //}

                if (region != null)
                    item.SubItems[3].Text = region;
                if (name != null)
                    item.SubItems[2].Text = name;
                if (developer != null)
                    item.SubItems[4].Text = developer;
                if (publisher != null)
                    item.SubItems[5].Text = publisher;
                if (languages != null)
                    item.SubItems[6].Text = languages;
                if (dateYear != null && dateMonth != null && dateDay != null)
                    item.SubItems[7].Text = $"{dateYear}-{dateMonth.PadLeft(2, '0')}-{dateDay.PadLeft(2, '0')}";

                if (region == null && name == null && developer == null && publisher == null && languages == null && dateYear == null && dateMonth == null && dateDay == null)
                {
                    item.ImageIndex = 3;
                }
                else if (region == null || name == null || developer == null || publisher == null || languages == null || dateYear == null || dateMonth == null || dateDay == null)
                {
                    item.ImageIndex = 1;
                }
                else
                {
                    item.ImageIndex = 0;
                }
            });
        }

#if FEATURE__SHA1_ISO
        private async Task GetDeferredFileHash(ListViewItem item, string filename)
        {
            Color prevColor = default;
            Font? prevFont = null;
            Invoke(() =>
            {
                prevColor = item.SubItems[__hashColumnIndex].ForeColor;
                prevFont = item.SubItems[__hashColumnIndex].Font;

                item.SubItems[__hashColumnIndex].Text = "Calculating...";
                item.SubItems[__hashColumnIndex].ForeColor = SystemColors.GrayText;
                item.SubItems[__hashColumnIndex].Font = new Font(item.SubItems[8].Font, FontStyle.Italic);
            });
            var total = new FileInfo(filename).Length;
            var hash = await IOUtil.GetFileSHA1Async(filename, new Progress<long>(l =>
            {
                item.SubItems[__hashColumnIndex].Text = $"Calculating... ({Math.Round(l / (decimal)total * 100l, 1)}%)";
            }));
            Invoke(() =>
            {
                item.SubItems[__hashColumnIndex].Text = hash;
                item.SubItems[__hashColumnIndex].ForeColor = prevColor;
                var newFont = item.SubItems[__hashColumnIndex].Font;
                item.SubItems[__hashColumnIndex].Font = prevFont;
                newFont.Dispose();
            });
        }
#endif

        private void CheckForOOBE()
        {
            if (!ApplicationStateSingleton.Instance.IsFirstRun && ApplicationConfigurationSingleton.Instance.SettingsFileExists)
                return;

            using var w = new OobeWizard();
            if (w.ShowDialog(this) == DialogResult.OK)
            {
                ApplicationStateSingleton.Instance.IsFirstRun = false;
                ApplicationConfigurationSingleton.Instance.IsoPath = w.IsoPath;
            }
            else
            {
                Close();
                return;
            }

            //MessageBox.Show(ApplicationConfigurationSingleton.Instance.IsoPath);
        }

        private Task CheckForDatabaseUpdate()
        {
            return DownloadDbIfNecessary().ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception?.InnerException is InvalidOperationException)
                    return;
                t.ThrowIfFaulted();
            });
        }

        private void PurgeDatabase()
        {
            if (MessageBox.Show("Purge database?", "OpenWiiManager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (File.Exists(ApplicationEnviornment.GameDatabaseFilePath))
                    File.Delete(ApplicationEnviornment.GameDatabaseFilePath);
                ApplicationStateSingleton.Instance.LastFeedUpdate = DateTimeOffset.MinValue;
                GameTdbSingleton.Instance.UnloadDatabase();
                MessageBox.Show("Database purged! Please use 'Check for updates' to re-download the database!", "OpenWiiManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task DownloadDbIfNecessary()
        {
            var needsUpdate = await IndeterminateBackgroundOperationAsync("Checking for WiiTDB database update", Task.Run(async () => await GameTdbSingleton.Instance.NeedsUpdate()));
            if (needsUpdate)
            {
                await IndeterminateBackgroundOperationAsync("Downloading WiiTDB database", Task.Run(async () => await GameTdbSingleton.Instance.DownloadDatabase()));
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var f = new AboutForm();
            f.ShowDialog(this);
        }

        private void backgroundOperationLabel_Click(object sender, EventArgs e)
        {
            ShowTasksPopup();
        }

        private void ShowTasksPopup()
        {
            tasksPopup.Location = PointToScreen(new Point(
                0,
                ClientSize.Height - tasksPopup.Height - statusStrip1.Height
            ));
            tasksPopup.Show(this);
        }

        private void ShowNotificationPopup()
        {
            notificationPopup.Location = PointToScreen(new Point(
                ClientSize.Width - notificationPopup.Width,
                ClientSize.Height - notificationPopup.Height - statusStrip1.Height
            ));
            notificationPopup.Show(this);
        }

        private void backgroundTaskPopupButton_ButtonClick(object sender, EventArgs e)
        {
            ShowTasksPopup();
        }

        private void notificationsButton_ButtonClick(object sender, EventArgs e)
        {
            ShowNotificationPopup();
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForDatabaseUpdate();
        }

        private void purgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PurgeDatabase();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var f = new SettingsForm();
            f.ShowDialog(this);
        }

        public void InitializeWork()
        {
            //Thread.Sleep(2000);
        }

        public void PostInitialize()
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanISOs();
        }

        private void DeselectGame()
        {
            gameSelectCancellationTokenSource.Cancel();
            ResetCancellationTokenSourceAndToken(ref gameSelectCancellationTokenSource, ref gameSelectCancellationToken);
            webPictureBox1.URL = null;
            webPictureBox2.URL = null;
            textBox1.Text = "";
        }

        private void SelectGameWithId(string? gameId)
        {
            if (gameId == null)
            {
                DeselectGame();
                return;
            }

            GameTdbSingleton.Instance.LookupWiiTitleInfoAsync(gameId).ContinueWith(t =>
            {
                var langs = t.Result?.Element("languages")?.Value?.Split(',');
                if (langs == null || langs.Length < 1)
                {
                    langs = new[] { "EN" };
                }

                Task.Run(async () =>
                {
                    gameSelectCancellationToken.ThrowIfCancellationRequested();

                    var langsCopy = new Stack<string>((string[])langs.Clone());
                    if (t.Result?.Element("region")?.Value == "NTSC-U")
                        langsCopy.Push("US");
                    if (!langsCopy.Contains("EN"))
                        langsCopy.Push("EN");
                    while (langsCopy.Count > 0)
                    {
                        gameSelectCancellationToken.ThrowIfCancellationRequested();
                        var language = langsCopy.Pop();
                        var coverUrl = $"https://art.gametdb.com/wii/cover3D/{language}/{gameId}.png";
                        Debug.WriteLine($"[COVER] Trying {coverUrl}");
                        if (await webPictureBox1.SetUrlAsync(coverUrl))
                        {
                            Debug.WriteLine($"[COVER] Found to be working");
                            break;
                        }
                    }
                    Debug.WriteLine($"[COVER] Done trying");
                });

                Task.Run(async () =>
                {
                    gameSelectCancellationToken.ThrowIfCancellationRequested();
                    var langsCopy = new Stack<string>((string[])langs.Clone());
                    if (t.Result?.Element("region")?.Value == "NTSC-U")
                        langsCopy.Push("US");
                    if (!langsCopy.Contains("EN"))
                        langsCopy.Push("EN");
                    while (langsCopy.Count > 0)
                    {
                        gameSelectCancellationToken.ThrowIfCancellationRequested();
                        var language = langsCopy.Pop();
                        var discUrl = $"https://art.gametdb.com/wii/disc/{language}/{gameId}.png";
                        Debug.WriteLine($"[DISC] Trying {discUrl}");
                        if (await webPictureBox2.SetUrlAsync(discUrl))
                        {
                            Debug.WriteLine($"[DISC] Found to be working");
                            break;
                        }
                    }
                    Debug.WriteLine($"[DISC] Done trying");
                });

                var lang = langs?.FirstOrDefault() ?? "EN";
                var localeNodes = t.Result?.Elements("locale");

                var firstLoc = localeNodes?.FirstOrDefault(n => n.Attribute("lang")?.Value == lang)?.Element("synopsis")?.Value;
                var secondLoc = localeNodes?.FirstOrDefault()?.Element("synopsis")?.Value;

                var synopsis = (firstLoc ?? secondLoc ?? "").Trim().Replace("\r\n", "\n").Replace("\n", "\r\n");
                Invoke(() => textBox1.Text = synopsis);
            });
        }

        private void ResetCancellationTokenSourceAndToken(ref CancellationTokenSource source, ref CancellationToken token)
        {
            if (!source.TryReset())
            {
                source.Dispose();
                source = new();
                token = source.Token;
            }
        }

        private void forceGarbageCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Debug.WriteLine(listView1.SelectedItems.Count);
            if (listView1.SelectedItems.Count != 1)
                DeselectGame();
            else
                SelectGameWithId(listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault()?.SubItems[1]?.Text);

            playGameUsingDolphinToolStripMenuItem.Enabled = IsDolphinConfigured() && listView1.SelectedItems.Count == 1;
            detailsToolStripMenuItem.Enabled = listView1.SelectedItems.Count == 1;
        }

        private void gameContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
                e.Cancel = true;
        }

        private void viewGameOnGameTDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems.OfType<ListViewItem>();

            if (items.Count() > 1)
            {
                if (MessageBox.Show("There are multiple games selected. Do you want to open the GameTDB page for each of them?", "Caution", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var ids = items.Select(i => i.SubItems[1].Text);
                    foreach (var id in ids)
                        OpenWiiTdb(id);
                }
            }
            else
            {
                var id = items.FirstOrDefault()?.SubItems[1]?.Text;
                if (id != null)
                    OpenWiiTdb(id);
            }
        }

        private void OpenWiiTdb(string? id)
        {
            Process.Start(new ProcessStartInfo($"https://www.gametdb.com/Wii/{HttpUtility.UrlEncode(id)}") { UseShellExecute = true });
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sorter.SortColumn)
            {
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listView1.SetSortIcon(sorter.SortColumn, sorter.Order);
            listView1.Sort();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var files = listView1.SelectedItems.OfType<ListViewItem>().Select(i => i.Tag?.ToString()).Where(i => i != null).Select(i => i ?? "").ToArray();
            ShellUtil.ShowFileProperties(files);
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var items = listView1.SelectedItems.OfType<ListViewItem>();

            if (items.Count() == 1)
            {
                /*using */
                var f = new DetailsForm();
                // TODO
                f.IsoFileName = items.First().Tag.ToString();
                f.GameId = items.First().SubItems[1].Text;
                //f.ShowDialog(this);
                f.Show(this);
            }
        }

        private void debugShowBalloonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btt == null)
            {
                btt = new(this);
                btt.strTitle = "Title text";
                btt.strText = "Lorem ipsum dolor sit amet";
                btt.icon = ToolTipIcon.Info;
                btt.Create();
                LocationChanged += MainForm_LocationChanged;
                Resize += MainForm_LocationChanged;
            }

            btt.Show(notificationsButton.Bounds.Location);
        }

        private void playGameUsingDolphinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var file = listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault()?.Tag?.ToString();
            if (file != null)
                PlayGameInDolphin(file);
        }

        private void PlayGameInDolphin(string file)
        {
            if (!IsDolphinConfigured())
                return;

            Process.Start(new ProcessStartInfo()
            {
                FileName = ApplicationConfigurationSingleton.Instance.DolphinPath,
                Arguments = ApplicationConfigurationSingleton.Instance.DolphinArgs?.Replace("%1", file)
            });
        }

        private bool IsDolphinConfigured()
        {
            return !string.IsNullOrEmpty(ApplicationConfigurationSingleton.Instance.DolphinPath) && File.Exists(ApplicationConfigurationSingleton.Instance.DolphinPath);
        }

        private void showFilesInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var files = listView1.SelectedItems.OfType<ListViewItem>().Select(i => i.Tag?.ToString()).Where(i => i != null).Select(i => i ?? "").ToArray();
            ShellUtil.ShowFilesInExplorer(files);
        }

        private void expandColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            foreach (ColumnHeader col in listView1.Columns)
                col.Width = -1;
            listView1.EndUpdate();
        }

        private void shrinkColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.BeginUpdate();
            var totalWidth = listView1.Columns.OfType<ColumnHeader>().Sum(c => c.Width);
            var targetWidth = listView1.ClientSize.Width;
            var factor = targetWidth / (double)totalWidth;
            foreach (ColumnHeader col in listView1.Columns)
                col.Width = (int)(col.Width * factor);
            listView1.EndUpdate();
        }

        private void searchToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            searchDelayTimer.Stop();
            searchDelayTimer.Start();
        }

        private void searchDelayTimer_Tick(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void PerformSearch()
        {
            searchDelayTimer.Stop();
            var query = searchToolStripTextBox.TextBox.Text;
            Debug.WriteLine($"Raw query (as entered) is {query}");
            var parsedQuery = SearchQuery.Parse(query);
            var totalTokenCount = parsedQuery.TokenCount;
            var emptyQuery = string.IsNullOrWhiteSpace(query);
            //return;
            UseWaitCursor = true;
            searchToolStripTextBox.Enabled = false;
            Application.DoEvents();
            listView1.BeginUpdate();
            foreach (ListViewItem item in listView1.Items)
            {
                if (emptyQuery)
                {
                    foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                    {
                        subitem.ForeColor = listView1.ForeColor;
                        subitem.BackColor = listView1.BackColor;
                    }
                }
                else
                {
                    var isVisible = false;
                    Dictionary<string, int> matchedTokens = new();
                    List<ListViewItem.ListViewSubItem> matchedItems = new();
                    foreach (var token in parsedQuery.Tokens)
                        matchedTokens[token ?? ""] = 0;
                    for (var i = 0; i < item.SubItems.Count; ++i)
                    {
                        var subitem = item.SubItems[i];
                        var column = listView1.Columns[i];
                        var siMatchedTokens = SubItemMatchesQuery(subitem, column, parsedQuery);
                        var didMatch = siMatchedTokens.Length > 0;

                        foreach (var token in siMatchedTokens)
                        {
                            if (!matchedTokens.ContainsKey(token))
                                matchedTokens[token] = 0;
                            ++matchedTokens[token];
                        }

                        if (didMatch)
                        {
                            Debug.WriteLine("Subitem " + subitem.Text + " matches query " + query);
                            matchedItems.Add(subitem);
                        }
                        else
                        {
                            Debug.WriteLine($"Subitem {subitem.Text} does not match query {query}");
                        }
                    }

                    Debug.WriteLine("----------------");
                    Debug.WriteLine("Result:");
                    foreach (var i in matchedTokens)
                        Debug.WriteLine($"{i.Key}: {i.Value}");
                    Debug.WriteLine("----------------");

                    isVisible = matchedTokens.All(v => v.Value > 0);

                    foreach (ListViewItem.ListViewSubItem subitem in item.SubItems)
                    {
                        subitem.ForeColor = isVisible ? Color.Black : SystemColors.GrayText;
                        subitem.BackColor = isVisible ? (
                            matchedItems.Contains(subitem) ?
                            Color.Yellow : Color.FromArgb(-64)
                        ) : listView1.BackColor;
                    }
                }
            }
            searchToolStripTextBox.Enabled = true;
            searchToolStripTextBox.Focus();
            listView1.EndUpdate();
            UseWaitCursor = false;
        }

        private enum ExpressionComparatorType
        {
            Contains,
            Equals,
            EqualsExact,
            StartsWith,
            EndsWith,
            Greater,
            GreaterOrEqual,
            Less,
            LessOrEqual
        }

        private struct ComplexExpression
        {
            public string Key { get; private set; }
            public string Value { get; private set; }
            public ExpressionComparatorType Comparator { get; private set; }
            public bool Negate { get; private set; }

            public string OriginalToken { get; private set; }

            public static ComplexExpression Parse(string expr)
            {
                var oToken = expr;

                if (!expr.StartsWith("{") && !expr.EndsWith("}"))
                    throw new FormatException("Invalid format");

                expr = expr.Substring(1, expr.Length - 2);

                var negate = false;
                if (expr.StartsWith("!"))
                {
                    negate = true;
                    expr = expr.Substring(1);
                }

                string key, value, comp;

                var match = Regex.Match(expr, @"^(.+?)(\*=|==|#=|\^=|\$=|>:|>=|<:|<=)(.+)$");

                if (!match.Success)
                    throw new FormatException("Invalid format");

                key = match.Groups[1].Value;
                value = match.Groups[3].Value;
                comp = match.Groups[2].Value;

                var ctype = comp switch
                {
                    "*=" => ExpressionComparatorType.Contains,
                    "==" => ExpressionComparatorType.Equals,
                    "#=" => ExpressionComparatorType.EqualsExact,
                    "^=" => ExpressionComparatorType.StartsWith,
                    "$=" => ExpressionComparatorType.EndsWith,
                    ">:" => ExpressionComparatorType.Greater,
                    ">=" => ExpressionComparatorType.GreaterOrEqual,
                    "<:" => ExpressionComparatorType.Less,
                    "<=" => ExpressionComparatorType.LessOrEqual,
                    _ => throw new UnreachableException()
                };

                return new()
                {
                    Key = key,
                    Value = value,
                    Comparator = ctype,
                    Negate = negate,
                    OriginalToken = oToken
                };
            }

            public override string ToString()
            {
                return $"ComplexExpression{{{Key} {(Negate ? "Not " : "")}{Comparator} {Value}}}";
            }

            public bool Matches(string compKey, string compValue)
            {
                if (compKey != Key)
                    return false;

                switch (Comparator)
                {
                    case ExpressionComparatorType.Contains:
                        return Negate ^ compValue.Contains(Value, StringComparison.InvariantCultureIgnoreCase);
                    case ExpressionComparatorType.Equals:
                        return Negate ^ compValue.Equals(Value, StringComparison.InvariantCultureIgnoreCase);
                    case ExpressionComparatorType.EqualsExact:
                        return Negate ^ compValue.Equals(Value, StringComparison.InvariantCulture);
                    case ExpressionComparatorType.StartsWith:
                        return Negate ^ compValue.StartsWith(Value, StringComparison.InvariantCulture);
                    case ExpressionComparatorType.EndsWith:
                        return Negate ^ compValue.EndsWith(Value, StringComparison.InvariantCulture);
                    case ExpressionComparatorType.Greater:
                        return Negate ^ (string.Compare(compValue, Value, StringComparison.InvariantCultureIgnoreCase) > 0);
                    case ExpressionComparatorType.GreaterOrEqual:
                        return Negate ^ (string.Compare(compValue, Value, StringComparison.InvariantCultureIgnoreCase) >= 0);
                    case ExpressionComparatorType.Less:
                        return Negate ^ (string.Compare(compValue, Value, StringComparison.InvariantCultureIgnoreCase) < 0);
                    case ExpressionComparatorType.LessOrEqual:
                        return Negate ^ (string.Compare(compValue, Value, StringComparison.InvariantCultureIgnoreCase) <= 0);
                    default:
                        throw new UnreachableException();
                }
            }
        }

        private class SearchQuery
        {
            public readonly List<ComplexExpression> ComplexExpessions = new();
            public readonly List<string> Keywords = new();
            public readonly List<string> Tokens = new();
            public int TokenCount => Tokens.Count;

            private void Load(string query)
            {
                if (string.IsNullOrWhiteSpace(query))
                    return;

                StringBuilder currentTokenBuffer = new();
                bool isInsideExpression = false;
                for (var i = 0; i < query.Length; ++i)
                {
                    var cchar = query[i];
                    if (cchar == '{' && !isInsideExpression)
                    {
                        if (currentTokenBuffer.Length > 0)
                        {
                            Debug.WriteLine($"Adding text [{currentTokenBuffer}] to token list");
                            Tokens.Add(currentTokenBuffer.ToString());
                            currentTokenBuffer.Clear();
                        }
                        currentTokenBuffer.Append(cchar);
                        isInsideExpression = true;
                    }
                    else if (cchar == '}' && isInsideExpression)
                    {
                        currentTokenBuffer.Append(cchar);
                        isInsideExpression = false;
                        Debug.WriteLine($"Adding text [{currentTokenBuffer}] to token list");
                        Tokens.Add(currentTokenBuffer.ToString());
                        currentTokenBuffer.Clear();
                    }
                    else if (cchar == ' ' && !isInsideExpression)
                    {
                        if (currentTokenBuffer.Length > 0)
                        {
                            Debug.WriteLine($"Adding text [{currentTokenBuffer}] to token list");
                            Tokens.Add(currentTokenBuffer.ToString());
                            currentTokenBuffer.Clear();
                        }
                    }
                    else
                    {
                        currentTokenBuffer.Append(cchar);
                    }
                }
                if (currentTokenBuffer.Length > 0)
                {
                    Debug.WriteLine($"Adding text [{currentTokenBuffer}] to token list");
                    Tokens.Add(currentTokenBuffer.ToString());
                }

                Debug.WriteLine($"Token list({Tokens.Count}): {string.Join("||", Tokens.ToArray())}");

                foreach (var word in Tokens)
                {
                    // TODO More expressions
                    if (word.StartsWith("{") && word.EndsWith("}"))
                    {
                        try
                        {
                            ComplexExpessions.Add(ComplexExpression.Parse(word));
                        }
                        catch (FormatException)
                        {
                            Keywords.Add(word);
                        }
                    }
                    else
                    {
                        Debug.WriteLine($"Item {word}");
                        Keywords.Add(word);
                    }
                }
            }

            public static SearchQuery Parse(string query)
            {
                var instance = new SearchQuery();
                instance.Load(query);
                return instance;
            }
        }

        private string[] SubItemMatchesQuery(ListViewItem.ListViewSubItem subitem, ColumnHeader column, SearchQuery query)
        {
            List<string> matchedTokens = new();

            foreach (var kword in query.Keywords)
                if (subitem.Text.Contains(kword, StringComparison.InvariantCultureIgnoreCase))
                    matchedTokens.Add(kword);

            foreach (var expr in query.ComplexExpessions)
            {
                Debug.WriteLine($"Testing expression {expr} on {column.Text}.{subitem.Text}");
                if (expr.Matches(column.Text, subitem.Text))
                    matchedTokens.Add(expr.OriginalToken);
            }

            return matchedTokens.ToArray();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var sd = new SaveFileDialog()
            {
                Filter = "CSV file|*.csv|HTML|*.html"
            };
            if (sd.ShowDialog() != DialogResult.OK) return;
            switch (sd.FilterIndex)
            {
                case 1:
                    File.WriteAllText(sd.FileName, GetListCSVString());
                    break;
                case 2:
                    File.WriteAllText(sd.FileName, GetListHTMLString());
                    break;
                default:
                    throw new UnreachableException();
            }
        }

        private string[][] GetListCells()
        {
            return listView1.Items.OfType<ListViewItem>()?.Select(i => i.SubItems.OfType<ListViewItem.ListViewSubItem>().Select(s => s.Text).ToArray()).ToArray();
        }

        private string GetListCSVString()
        {
            const string CellSeparator = ",";

            var items = GetListCells();

            var rowsEscaped = new string[items.Length];
            for (var j = 0; j < items.Length; ++j)
            {
                var row = items[j];
                var rowItemsEscaped = new string[row.Length];
                for (var i = 0; i < row.Length; ++i)
                {
                    var cell = row[i];
                    if (
                        cell.Contains(CellSeparator) ||
                        cell.Contains("\"") ||
                        cell.Contains("\n") ||
                        cell.Contains("\r") ||
                        cell.Contains("\\") ||
                        cell.Contains("'")
                    )
                        rowItemsEscaped[i] = "\"" + cell.Replace("\"", "\"\"") + "\"";
                    else
                        rowItemsEscaped[i] = cell;
                }
                rowsEscaped[j] = string.Join(CellSeparator, rowItemsEscaped);
            }

            return string.Join("\r\n", rowsEscaped);
        }

        private string GetListHTMLString()
        {
            const string ns = "http://www.w3.org/1999/xhtml";
            var items = GetListCells();
            var enc = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            using var stream = new MemoryStream();
            using var xml = XmlWriter.Create(stream, new XmlWriterSettings()
            {
                Encoding = enc,
                OmitXmlDeclaration = true,
                Indent = true
            });
            var username = WindowsIdentity.GetCurrent().Name;
            username = username.Substring(username.IndexOf("\\") + 1);
            xml.WriteDocType("html", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", null);
            xml.WriteStartElement("html", ns);
            xml.WriteAttributeString("lang", "en");
            xml.WriteStartElement("head");
            xml.WriteStartElement("meta");
            xml.WriteAttributeString("http-equiv", "Content-Type");
            xml.WriteAttributeString("content", "text/html; charset=UTF-8");
            xml.WriteEndElement(); // meta
            xml.WriteStartElement("meta");
            xml.WriteAttributeString("http-equiv", "X-UA-Compatible");
            xml.WriteAttributeString("content", "IE=edge");
            xml.WriteEndElement(); // meta
            xml.WriteStartElement("meta");
            xml.WriteAttributeString("name", "generator");
            xml.WriteAttributeString("content", Application.ProductName + " " + Application.ProductVersion);
            xml.WriteEndElement(); // meta
            xml.WriteStartElement("meta");
            xml.WriteAttributeString("name", "date.created");
            xml.WriteAttributeString("content", DateTime.Now.ToString("yyyy-MM-dd"));
            xml.WriteEndElement(); // meta
            xml.WriteStartElement("meta");
            xml.WriteAttributeString("name", "author");
            xml.WriteAttributeString("content", username);
            xml.WriteEndElement(); // meta
            xml.WriteElementString("title", "Games - Open Wii Manager");
            xml.WriteStartElement("link");
            xml.WriteAttributeString("rel", "icon");
            xml.WriteAttributeString("type", "image/png");
            xml.WriteAttributeString("href", "https://service.jonaskohl.de/assets/de/jonaskohl/owm/_images/_iconsm.png");
            xml.WriteEndElement(); // link
            xml.WriteStartElement("style");
            xml.WriteAttributeString("type", "text/css");
            xml.WriteString(@"
body, th, td {
    font: 10pt sans-serif;
}
table {
    border-collapse: collapse;
}
thead {
    background: #ddd;
}
th {
    font-weight: bold;
}
th, td {
    padding: 4px;
    border: 1px solid gray;
}
tr.even {
    background: #eee;
}
.sorting-icon {
    color: gray;
}
".Trim());
            xml.WriteEndElement(); // style
            xml.WriteEndElement(); // head
            xml.WriteStartElement("body");
            xml.WriteStartElement("p");
            xml.WriteStartElement("a");
            xml.WriteAttributeString("href", ApplicationEnviornment.WebUrl);
            xml.WriteStartElement("img");
            xml.WriteAttributeString("src", "https://service.jonaskohl.de/assets/de/jonaskohl/owm/_images/_logo.png");
            xml.WriteAttributeString("alt", "Open Wii Manager");
            xml.WriteAttributeString("width", "256");
            xml.WriteAttributeString("height", "41");
            xml.WriteEndElement(); // img
            xml.WriteEndElement(); // a
            xml.WriteEndElement(); // p
            xml.WriteStartElement("p");
            xml.WriteString($"Exported on {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            xml.WriteEndElement(); // p
            xml.WriteStartElement("table");
            xml.WriteStartElement("thead");
            xml.WriteStartElement("tr");
            foreach (ColumnHeader col in listView1.Columns)
            {
                xml.WriteStartElement("th");
                xml.WriteString(col.Text);
                if (col.Index == sorter.SortColumn)
                {
                    xml.WriteStartElement("span");
                    xml.WriteAttributeString("class", "sorting-icon");
                    xml.WriteString(sorter.Order == SortOrder.Descending ? "\u25BC" : "\u25B2");
                    xml.WriteEndElement(); // span
                }
                xml.WriteEndElement(); // th
            }
            xml.WriteEndElement(); // tr
            xml.WriteEndElement(); // thead
            xml.WriteStartElement("tbody");
            for (var i = 0; i < items.Length; ++i)
            {
                var row = items[i];
                xml.WriteStartElement("tr");
                xml.WriteAttributeString("class", i % 2 == 0 ? "odd" : "even");
                foreach (var cell in row)
                    xml.WriteElementString("td", cell);
                xml.WriteEndElement(); // tr
            }
            xml.WriteEndElement(); // tbody
            xml.WriteEndElement(); // table
            xml.WriteElementString("hr", null);
            xml.WriteStartElement("p");
            xml.WriteStartElement("img");
            xml.WriteAttributeString("src", "http://www.w3.org/Icons/valid-xhtml10");
            xml.WriteAttributeString("alt", "Valid XHTML 1.0 Strict");
            xml.WriteAttributeString("width", "88");
            xml.WriteAttributeString("height", "31");
            xml.WriteEndElement(); // img
            xml.WriteEndElement(); // p
            xml.WriteEndElement(); // body
            xml.WriteEndElement(); // html
            xml.Flush();
            return enc.GetString(stream.ToArray());
        }
    }

    static class _GameRegionExtensions_
    {
        public static string ToString2(this MainForm.GameRegion v)
        {
            return Regex.Replace(v.ToString(), "__0([0-9a-f]{2})0__", m =>
            {
                var hexCharCode = m.Groups[1].Value;
                return ((char)int.Parse(hexCharCode, NumberStyles.HexNumber, CultureInfo.InvariantCulture)).ToString();
            });
        }
    }
}