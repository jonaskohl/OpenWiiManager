using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Controls
{
    public class TextBoxWithHeight : TextBox
    {
        public new bool AutoSize
        {
            get => base.AutoSize;
            set => base.AutoSize = value;
        }
    }
}
