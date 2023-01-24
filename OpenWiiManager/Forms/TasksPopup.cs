using OpenWiiManager.Controls;
using OpenWiiManager.Tools;
using OpenWiiManager.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWiiManager.Forms
{
    public partial class TasksPopup : PopupWindow
    {
        private struct OperationItem
        {
            public string Message;
            public bool Completed;

            public override string ToString()
            {
                return Message;
            }
        }

        private List<MainForm.BackgroundOperation> _completedOperations = new();

        private ObservableCollection<MainForm.BackgroundOperation>? _operationsList = null;
        public ObservableCollection<MainForm.BackgroundOperation>? OperationsList
        {
            get => _operationsList;
            set
            {
                if (_operationsList != null)
                    _operationsList.CollectionChanged -= _operationsList_CollectionChanged;
                _operationsList = value;
                _completedOperations.Clear();
                if (_operationsList != null)
                    _operationsList.CollectionChanged += _operationsList_CollectionChanged;
            }
        }

        private void _operationsList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
                _completedOperations.AddRange(e.OldItems.OfType<MainForm.BackgroundOperation>());

            UpdateList();
        }

        private void UpdateList()
        {
            if (IsHandleCreated)
                Invoke(() =>
                {
                    //FIXME
                    var scroll = listBox1.AutoScrollOffset;
                    listBox1.BeginUpdate();
                    listBox1.Items.Clear();
                    foreach (var op in _completedOperations)
                        if (op?.Message != null)
                            listBox1.Items.Add(new OperationItem() { Message = op.Message, Completed = true });
                    if (_operationsList != null)
                        foreach (var op in _operationsList)
                            if (op?.Message != null)
                                listBox1.Items.Add(new OperationItem() { Message = op.Message, Completed = false });
                    listBox1.EndUpdate();
                    listBox1.AutoScrollOffset = scroll;
                });
        }

        public TasksPopup()
        {
            InitializeComponent();

            listBox1.HandleCreated += ListBox1_HandleCreated;
        }

        private void ListBox1_HandleCreated(object? sender, EventArgs e)
        {
            User32.SendMessage(listBox1.Handle, Constants.WM_CHANGEUISTATE, InteropUtil.MakeLong(Constants.UIS_SET, Constants.UISF_HIDEFOCUS), 0);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (!Visible)
                if (_operationsList?.Count < 1)
                {
                    _completedOperations.Clear();
                    UpdateList();
                }
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index >= 0)
            {
                var item = (OperationItem)listBox1.Items[e.Index];
                var img = item.Completed ? Properties.Resources.Tick : Properties.Resources.Clock;

                e.Graphics.DrawImage(img, new Rectangle(
                    e.Bounds.X,
                    e.Bounds.Y + (e.Bounds.Height - img.Height) / 2,
                    img.Width,
                    img.Height
                ));
                TextRenderer.DrawText(e.Graphics, item.Message, e.Font, new Rectangle(
                    e.Bounds.X + img.Width + 4,
                    e.Bounds.Y,
                    e.Bounds.Width - img.Width - 4,
                    e.Bounds.Height
                ), e.ForeColor, e.BackColor, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
