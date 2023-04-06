using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Controls
{
    public class TextBoxWithHeight : TextBox
    {
        [
            Browsable(true),
            EditorBrowsable(EditorBrowsableState.Always),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
            Category("Layout"),
            Description("Specifies whether a control will automatically size itself to fit its contents.")
        ]
        public override bool AutoSize
        {
            get => base.AutoSize;
            set => base.AutoSize = value;
        }
    }
}
