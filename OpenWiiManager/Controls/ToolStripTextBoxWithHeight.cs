using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Controls
{
    public class ToolStripTextBoxWithHeight : ToolStripTextBox
    {
        public new bool AutoSize
        {
            get => base.AutoSize;
            set
            {
                base.AutoSize = value;
                RefreshAutoSize();
            }
        }

        public ToolStripTextBoxWithHeight() : base()
        {
            RefreshAutoSize();
        }

        public ToolStripTextBoxWithHeight(string name) : base(name)
        {
            RefreshAutoSize();
        }

        private void RefreshAutoSize()
        {
            typeof(TextBox).GetProperty("AutoSize")?.SetValue(Control, AutoSize);
        }
    }
}
