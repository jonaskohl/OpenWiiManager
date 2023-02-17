using OpenWiiManager.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace OpenWiiManager.Controls
{
    public class ToolbarButton : CustomButtonBase
    {
        private bool _checked;
        [Category("Appearance")]
        public bool Checked
        {
            get => _checked;
            set
            {
                _checked = value;
                Invalidate();
            }
        }

        const string VS_CLASSNAME = "TOOLBAR";
        const int TP_BUTTON = 1;
        const int TS_NORMAL = 1;
        const int TS_HOT = 2;
        const int TS_PRESSED = 3;
        const int TS_DISABLED = 4;
        const int TS_CHECKED = 5;
        const int TS_HOTCHECKED = 6;
        const int TS_NEARHOT = 7;
        const int TS_OTHERSIDEHOT = 8;

        private VisualStyleRenderer? renderer = VisualStyleRenderer.IsSupported ? new(VS_CLASSNAME, TP_BUTTON, TS_NORMAL) : default;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (VisualStyleRenderer.IsSupported)
            {
                int state;
                if (!Enabled)
                    state = TS_DISABLED;
                else if (IsMouseDown)
                    state = TS_PRESSED;
                else if (Checked && IsMouseOver)
                    state = TS_HOTCHECKED;
                else if (!Checked && IsMouseOver)
                    state = TS_HOT;
                else if (Checked)
                    state = TS_CHECKED;
                else
                    state = TS_NORMAL;

                renderer?.SetParameters(VS_CLASSNAME, TP_BUTTON, state);
                renderer?.DrawParentBackground(e.Graphics, ClientRectangle, this);
                renderer?.DrawBackground(e.Graphics, ClientRectangle);
                var color = renderer?.GetColor(ColorProperty.TextColor) ?? ForeColor;
                TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, color, DrawingUtil.GetTextFormatFlags(this));
                if (Image != null)
                {
                    var imBounds = DrawingUtil.GetImageBounds(this, new Padding(2)) ?? new Rectangle(Point.Empty, Image.Size);
                    if (Enabled)
                        renderer?.DrawImage(e.Graphics, imBounds, Image);
                    else
                        ControlPaint.DrawImageDisabled(e.Graphics, Image, imBounds.X, imBounds.Y, Color.Transparent);
                }

                if (Focused && ShowFocusCues)
                    ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(4, 4, Width - 8, Height - 8));
            }
            else
            {
                e.Graphics.Clear(BackColor);
                ButtonState state;
                if (!Enabled)
                    state = ButtonState.Inactive;
                else if (IsMouseDown)
                    state = ButtonState.Pushed;
                else if (Checked)
                    state = ButtonState.Checked;
                else
                    state = ButtonState.Normal;
                ControlPaint.DrawButton(e.Graphics, ClientRectangle, state);
                TextRenderer.DrawText(e.Graphics, Text, Font, ClientRectangle, Enabled ? ForeColor : SystemColors.GrayText, DrawingUtil.GetTextFormatFlags(this));
                if (Image != null)
                {
                    var imBounds = DrawingUtil.GetImageBounds(this, new Padding(2)) ?? new Rectangle(Point.Empty, Image.Size);
                    if (Enabled)
                        e.Graphics.DrawImage(Image, imBounds);
                    else
                        ControlPaint.DrawImageDisabled(e.Graphics, Image, imBounds.X, imBounds.Y, BackColor);
                }

                if (Focused && ShowFocusCues)
                    ControlPaint.DrawFocusRectangle(e.Graphics, new Rectangle(4, 4, Width - 8, Height - 8));
            }
        }
    }
}
