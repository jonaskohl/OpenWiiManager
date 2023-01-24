using OpenWiiManager.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace OpenWiiManager.Controls
{
    public class InstructionLabel : Label
    {
        private VisualStyleRenderer? _renderer;

#pragma warning disable CA1822
#pragma warning disable CS8603
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new Color ForeColor => Color.Empty;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new Font Font => null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new FlatStyle FlatStyle => FlatStyle.Standard;
#pragma warning restore CA1822
#pragma warning restore CS8603

        public InstructionLabel()
        {
            if (VisualStyleRenderer.IsSupported)
                _renderer = new VisualStyleRenderer("TEXTSTYLE", 1, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // TODO Transparency
            e.Graphics.Clear(BackColor);

            if (VisualStyleRenderer.IsSupported)
                _renderer?.DrawText(e.Graphics, ClientRectangle, Text, !Enabled, DrawingUtil.GetTextFormatFlags(this));
            else
                using (var f = new Font(DrawingUtil.FontStack("Segoe UI", "Trebuchet MS"), 12))
                    TextRenderer.DrawText(e.Graphics, Text, f, ClientRectangle, Color.FromArgb(255, 0, 51, 153), BackColor, DrawingUtil.GetTextFormatFlags(this));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            using var g = Graphics.FromHwnd(Handle);
            if (VisualStyleRenderer.IsSupported)
                return _renderer!.GetTextExtent(g, new Rectangle(Point.Empty, proposedSize), Text, DrawingUtil.GetTextFormatFlags(this)).Size;
            else
                using (var f = new Font(DrawingUtil.FontStack("Segoe UI", "Trebuchet MS"), 12))
                    return TextRenderer.MeasureText(g, Text, f, proposedSize, DrawingUtil.GetTextFormatFlags(this));
        }
    }
}
