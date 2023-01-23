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

namespace OpenWiiManager
{
    public partial class TasksPopup : PopupWindow
    {
        private ObservableCollection<MainForm.BackgroundOperation>? _operationsList = null;
        public ObservableCollection<MainForm.BackgroundOperation>? OperationsList
        {
            get => _operationsList;
            set
            {
                if (_operationsList != null)
                    _operationsList.CollectionChanged -= _operationsList_CollectionChanged;
                _operationsList = value;
                if (_operationsList != null)
                    _operationsList.CollectionChanged += _operationsList_CollectionChanged;
            }
        }

        private void _operationsList_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            if (_operationsList != null)
                foreach (var op in _operationsList)
                    if (op?.Message != null)
                        listBox1.Items.Add(op.Message);
            listBox1.EndUpdate();
        }

        public TasksPopup()
        {
            InitializeComponent();
        }
    }
}
