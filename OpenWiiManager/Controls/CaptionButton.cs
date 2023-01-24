using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace OpenWiiManager.Controls
{
    public class CaptionButton : CustomButtonBase
    {
        [Serializable]
        public enum CaptionButtonType
        {
            Close,
            Maximize,
            Minimize,
            Restore,
            Help
        }

        const string VS_CLASSNAME = "WINDOW";
        const int WP_MINBUTTON = 15;
        const int WP_MAXBUTTON = 17;
        const int WP_CLOSEBUTTON = 18;
        const int WP_RESTOREBUTTON = 21;
        const int WP_HELPBUTTON = 23;
        const int STATE_NORMAL = 1;
        const int STATE_HOT = 2;
        const int STATE_PUSHED = 3;
        const int STATE_DISABLED = 4;


        private CaptionButtonType _type = CaptionButtonType.Close;
        [Category("Appearance"), DefaultValue(CaptionButtonType.Close)]
        public CaptionButtonType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(CaptionButtonType), value))
                    throw new ArgumentException(value.ToString() + " is not valid for type " + nameof(CaptionButtonType));
                _type = value;
                Invalidate();
            }
        }

        VisualStyleRenderer renderer = new VisualStyleRenderer(VS_CLASSNAME, WP_CLOSEBUTTON, STATE_NORMAL);
        
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool CanFocus => false;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool TabStop => false;

        public CaptionButton()
        {
            SetStyle(ControlStyles.Selectable, false);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            SetupRenderer();
            renderer.DrawParentBackground(pevent.Graphics, ClientRectangle, this);
            renderer.DrawBackground(pevent.Graphics, ClientRectangle);
        }

        private void SetupRenderer()
        {
            var part = _type switch
            {
                CaptionButtonType.Close => WP_CLOSEBUTTON,
                CaptionButtonType.Restore => WP_RESTOREBUTTON,
                CaptionButtonType.Maximize => WP_MAXBUTTON,
                CaptionButtonType.Minimize => WP_MINBUTTON,
                CaptionButtonType.Help => WP_HELPBUTTON,
                _ => throw new UnreachableException()
            };

            int state;
            if (!Enabled)
                state = STATE_DISABLED;
            else if (IsMouseDown)
                state = STATE_PUSHED;
            else if (IsMouseOver)
                state = STATE_HOT;
            else
                state = STATE_NORMAL;

            renderer.SetParameters(VS_CLASSNAME, part, state);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            SetupRenderer();
            using var g = Graphics.FromHwnd(Handle);
            return renderer.GetPartSize(g, ThemeSizeType.Draw);
        }
    }
}
