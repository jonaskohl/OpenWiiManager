using OpenWiiManager.Tools;
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
    public class NavButton : CustomButtonBase
    {
        [Serializable]
        public enum NavButtonType
        {
            Back,
            Forward,
            Menu
        }

        private NavButtonType _type = NavButtonType.Back;
        [Category("Appearance"), DefaultValue(NavButtonType.Back)]
        public NavButtonType Type
        {
            get => _type;
            set
            {
                if (!Enum.IsDefined(typeof(NavButtonType), value))
                    throw new ArgumentException(value.ToString() + " is not valid for type " + nameof(NavButtonType));
                _type = value;
                Invalidate();
            }
        }

        const string VS_CLASSNAME = "NAVIGATION";
        const int NAV_BACKBUTTON = 1;
        const int NAV_FORWARDBUTTON = 2;
        const int NAV_MENUBUTTON = 3;
        const int STATE_NORMAL = 1;
        const int STATE_HOT = 2;
        const int STATE_PRESSED = 3;
        const int STATE_DISABLED = 4;

        VisualStyleRenderer renderer = new VisualStyleRenderer(VS_CLASSNAME, NAV_BACKBUTTON, STATE_NORMAL);

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

        public NavButton()
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
                NavButtonType.Back => NAV_BACKBUTTON,
                NavButtonType.Forward => NAV_FORWARDBUTTON,
                NavButtonType.Menu => NAV_MENUBUTTON,
                _ => throw new UnreachableException()
            };

            int state;
            if (!Enabled)
                state = STATE_DISABLED;
            else if (IsMouseDown)
                state = STATE_PRESSED;
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
