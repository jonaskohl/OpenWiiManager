using Ookii.Dialogs.WinForms;
using OpenWiiManager.Language.Extensions;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace OpenWiiManager
{
    public partial class MainForm : Form
    {
        class BackgroundOperation
        {
            public string? Message { get; init; }
            public Task? Operation { get; init; }

            public bool IsFinished => Operation?.IsCompleted == true;
        }

        readonly List<BackgroundOperation> backgroundOperations = new();

        public MainForm()
        {
            InitializeComponent();

            if (ApplicationState.MainWindowSplitDistance != default)
                splitter1.SplitPosition = ApplicationState.MainWindowSplitDistance;

            if (ApplicationState.MainWindowSize != default)
                ClientSize = ApplicationState.MainWindowSize;

            ResizeEnd += MainForm_ResizeEnd;
            splitter1.SplitterMoved += Splitter1_SplitterMoved;

            Shown += MainForm_Shown;
        }

        private void Splitter1_SplitterMoved(object? sender, SplitterEventArgs e)
        {
            ApplicationState.MainWindowSplitDistance = splitter1.SplitPosition;
        }

        private void MainForm_ResizeEnd(object? sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                ApplicationState.MainWindowSize = ClientSize;
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
                    backgroundOperationLabel.Visible = false;
                    backgroundOperationLabel.Text = "";
                    backgroundOperationProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
                }
                else
                {
                    backgroundOperationProgressBar.Visible = true;
                    backgroundOperationLabel.Visible = true;
                    backgroundOperationLabel.Text = backgroundOperations.Count > 1 ? $"{backgroundOperations.Count} tasks are running" : backgroundOperations.First().Message;
                    backgroundOperationProgressBar.Value += 1;
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
            DownloadDbIfNecessary().ContinueWith(t =>
            {
                t.ThrowIfFaulted();
            });
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
    }
}