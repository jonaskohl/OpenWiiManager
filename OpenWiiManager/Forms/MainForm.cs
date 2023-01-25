using Ookii.Dialogs.WinForms;
using OpenWiiManager.Core;
using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace OpenWiiManager.Forms
{
    public partial class MainForm : Form
    {
        private TasksPopup tasksPopup;
        private NotificationPopup notificationPopup;
        private bool allowTasksPopupClose = false;
        private bool allowNotificationPopupClose = false;

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

            notificationPopup = new();
            notificationPopup.FormClosing += NotificationPopup_FormClosing;

            if (ApplicationStateSingleton.Instance.MainWindowSplitDistance != default)
                splitter1.SplitPosition = ApplicationStateSingleton.Instance.MainWindowSplitDistance;

            if (ApplicationStateSingleton.Instance.MainWindowSize != default)
                ClientSize = ApplicationStateSingleton.Instance.MainWindowSize;

            ResizeEnd += MainForm_ResizeEnd;
            splitter1.SplitterMoved += Splitter1_SplitterMoved;

            Shown += MainForm_Shown;
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
                    backgroundOperationLabel.Text = backgroundOperations.Count > 1 ? $"{backgroundOperations.Count} tasks are running" : backgroundOperations.First().Message;
                    backgroundOperationProgressBar.Value = Math.Min(backgroundOperationProgressBar.Value + 1, backgroundOperationProgressBar.Maximum);
                    backgroundOperationProgressBar.Style = backgroundOperations.Count > 1 ? System.Windows.Forms.ProgressBarStyle.Continuous : System.Windows.Forms.ProgressBarStyle.Marquee;
                }

            });
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && backgroundOperations.Count > 0)
            {
                var res = MessageBox.Show("The following background tasks are still pending:\n\n" + string.Join("\n", backgroundOperations.Select(o => o.Message)) + "\n\nPress [Cancel] to return to the application.\nPress [Try again] to check if there are still tasks pending.\nPress [Continue] to force-quit the application. Note: This may lead to data corruption.", "", MessageBoxButtons.CancelTryContinue, MessageBoxIcon.Exclamation);
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

            CheckForDatabaseUpdate();
        }

        private void CheckForOOBE()
        {
            if (!ApplicationStateSingleton.Instance.IsFirstRun)
                return;

            var page = new TaskDialogPage()
            {
                Heading = "test123"
            };
            System.Windows.Forms.TaskDialog.ShowDialog(page);

            //ApplicationStateSingleton.Instance.IsFirstRun = false;
        }

        private void CheckForDatabaseUpdate()
        {
            DownloadDbIfNecessary().ContinueWith(t =>
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
                MessageBox.Show("Database purged! Please use 'Check for updates' to re-download the database!", "OpenWiiManager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task DownloadDbIfNecessary()
        {
            var needsUpdate = await IndeterminateBackgroundOperationAsync("Checking for database update", Task.Run(async () => await GameTdb.NeedsUpdate()));
            if (needsUpdate)
            {
                await IndeterminateBackgroundOperationAsync("Downloading database", Task.Run(async () => await GameTdb.DownloadDatabase()));
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
    }
}