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
    public class BodyTitleLabel : Label
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

        public BodyTitleLabel()
        {
            if (VisualStyleRenderer.IsSupported)
                _renderer = new VisualStyleRenderer("TEXTSTYLE", 3, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // TODO Transparency
            e.Graphics.Clear(BackColor);

            if (VisualStyleRenderer.IsSupported)
                _renderer?.DrawText(e.Graphics, ClientRectangle, Text, !Enabled, DrawingUtil.GetTextFormatFlags(this));
            else
                using (var f = new Font(DrawingUtil.FontStack("Segoe UI", "Trebuchet MS"), 9, FontStyle.Bold))
                    TextRenderer.DrawText(e.Graphics, Text, f, ClientRectangle, SystemColors.WindowText, BackColor, DrawingUtil.GetTextFormatFlags(this));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            using var g = Graphics.FromHwnd(Handle);
            if (VisualStyleRenderer.IsSupported)
                return _renderer!.GetTextExtent(g, new Rectangle(Point.Empty, proposedSize), Text, DrawingUtil.GetTextFormatFlags(this)).Size;
            else
                using (var f = new Font(DrawingUtil.FontStack("Segoe UI", "Trebuchet MS"), 9, FontStyle.Bold))
                    return TextRenderer.MeasureText(g, Text, f, proposedSize, DrawingUtil.GetTextFormatFlags(this));
        }
    }
}
