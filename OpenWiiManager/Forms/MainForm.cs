using OpenWiiManager.Controls;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace OpenWiiManager.Forms
{
    public partial class MainForm : Form
    {
        private TasksPopup tasksPopup;
        private NotificationPopup notificationPopup;
        private bool allowTasksPopupClose = false;
        private bool allowNotificationPopupClose = false;
        private Font IdColumnFont = new Font("Consolas", 10);

        public class BackgroundOperation
        {
            public string? Message { get; init; }
            public Task? Operation { get; init; }

            public bool IsFinished => Operation?.IsCompleted == true;
        }

        readonly ObservableCollection<BackgroundOperation> backgroundOperations = new();

        public MainForm()
        {
            InitializeComponent();

            tasksPopup = new();
            tasksPopup.OperationsList = backgroundOperations;
            tasksPopup.FormClosing += TasksPopup_FormClosing;

            ToolStripManager.Renderer = new AeroToolStripRenderer(ToolBarTheme.Toolbar);

            notificationPopup = new();
            notificationPopup.FormClosing += NotificationPopup_FormClosing;

            if (ApplicationStateSingleton.Instance.MainWindowSplitDistance != default)
                splitter1.SplitPosition = ApplicationStateSingleton.Instance.MainWindowSplitDistance;

            if (ApplicationStateSingleton.Instance.MainWindowSize != default)
                ClientSize = ApplicationStateSingleton.Instance.MainWindowSize;

            ResizeEnd += MainForm_ResizeEnd;
            splitter1.SplitterMoved += Splitter1_SplitterMoved;

            listView1.HandleCreated += ListView1_HandleCreated;

            Shown += MainForm_Shown;
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
        }

        private Task ScanISOs()
        {
            refreshToolStripMenuItem.Enabled = false;
            listView1.Items.Clear();
            return IndeterminateBackgroundOperationAsync("Scanning for games", Task.Run(async () =>
            {
                foreach (var file in Directory.EnumerateFiles(ApplicationConfigurationSingleton.Instance.IsoPath, "*.iso"))
                {
                    var meta = await GetIsoMeta(file);
                    Debug.WriteLine(file);
                    listView1.Invoke(() =>
                    {
                        var basename = Path.GetFileName(file);
                        var item = new ListViewItem(basename);
                        item.UseItemStyleForSubItems = false;
                        var idSubitem = new ListViewItem.ListViewSubItem(item, meta.GameId);
                        idSubitem.Font = IdColumnFont;
                        item.SubItems.Add(idSubitem);
                        item.SubItems.Add(meta.Title);
                        if (meta.Region == GameRegion._UNKNOWN_)
                            item.SubItems.Add($"Unknown (0x{meta.__rawRegionByteValue:X2} '{(char)meta.__rawRegionByteValue}')");
                        else
                            item.SubItems.Add(meta.Region.ToString2());
                        listView1.Items.Add(item);
                        item.SubItems.Add(""); // Developer
                        item.SubItems.Add(""); // Publisher
                        item.SubItems.Add(""); // Languages
                        item.SubItems.Add(""); // Date

                        IndeterminateBackgroundOperation($"Scanning database for file {basename}", Task.Run(() => GetDeferredWiiTDBInfo(meta.GameId, item)));
                    });
                }
            }).ContinueWith(t =>
            {
                t.ThrowIfFaulted();
                Invoke(() =>
                {
                    refreshToolStripMenuItem.Enabled = true;
                });
            }));
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

        struct IsoMeta
        {
            internal byte @__rawRegionByteValue;

            public GameRegion Region;
            public string GameId;
            public string Title;
            public byte Version;
        }

        private Task<IsoMeta> GetIsoMeta(string file)
        {
            return Task.Run(() =>
            {
                using var hFile = File.Open(file, FileMode.Open);
                using var breader = new BinaryReader(hFile);
                var idBytes = breader.ReadBytes(6);
                hFile.Seek(0x20, SeekOrigin.Begin);
                var titleBytes = breader.ReadBytes(64);
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

        private async Task GetDeferredWiiTDBInfo(string id, ListViewItem item)
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
            });
        }

        private void CheckForOOBE()
        {
            if (!ApplicationStateSingleton.Instance.IsFirstRun)
                return;

            //var page = new TaskDialogPage()
            //{
            //    Heading = "Welcome to Open Wii Manager!",
            //    Caption = "Welcome to Open Wii Manager!",
            //    Text = "This seems to be the first "
            //};
            //System.Windows.Forms.TaskDialog.ShowDialog(page);

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

            MessageBox.Show(ApplicationConfigurationSingleton.Instance.IsoPath);
        }

        private Task CheckForDatabaseUpdate()
        {
            return DownloadDbIfNecessary().ContinueWith(t =>
            {
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
            Thread.Sleep(2000);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScanISOs();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
        }

        private void DeselectGame()
        {
            webPictureBox1.URL = null;
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
                var language = t.Result?.Element("languages")?.Value?.Split(',').FirstOrDefault() ?? "EN";
                var coverUrl = $"https://art.gametdb.com/wii/cover3D/{language}/{gameId}.png";
                webPictureBox1.URL = coverUrl;
                var localeNodes = t.Result?.Elements("locale");

                var firstLoc = localeNodes?.FirstOrDefault(n => n.Attribute("lang")?.Value == language)?.Element("synopsis")?.Value;
                var secondLoc = localeNodes?.FirstOrDefault()?.Element("synopsis")?.Value;

                var synopsis = (firstLoc ?? secondLoc ?? "").Trim().Replace("\r\n", "\n").Replace("\n", "\r\n");
                Invoke(() => textBox1.Text = synopsis);
            });
        }

        private void forceGarbageCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1)
                DeselectGame();
            else
                SelectGameWithId(listView1.SelectedItems.OfType<ListViewItem>().FirstOrDefault()?.SubItems[1]?.Text);
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