using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager
{
    public class PopupWindow : Form
    {
        const int WS_POPUP = unchecked((int)0x80000000);
        const int WS_BORDER = 0x00800000;
        const int WS_CAPTION = 0x00C00000;
        const int WS_MAXIMIZE = 0x01000000;
        const int WS_MINIMIZE = 0x20000000;
        const int WS_THICKFRAME = 0x00040000;
        const int WS_EX_DLGMODALFRAME = 0x00000001;
        const int WS_EX_CLIENTEDGE = 0x00000200;
        const int WS_EX_STATICEDGE = 0x00020000;
        const int CS_DROPSHADOW = 0x00020000;

        private bool _isDesignMode;
        protected bool IsDesignMode => _isDesignMode || (Site != null && Site.DesignMode);

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool MaximizeBox { get => false; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool MinimizeBox { get => false; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool HelpButton { get => false; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool ShowIcon { get => false; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool ShowInTaskbar { get => false; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new bool ControlBox { get => false; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Bindable(false)]
        [Browsable(false)]
        public new FormBorderStyle FormBorderStyle { get => FormBorderStyle.None; }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool HideOnDeactivate { get; set; } = false;

        public PopupWindow()
        {
            base.MaximizeBox = this.MaximizeBox;
            base.MinimizeBox = this.MinimizeBox;
            base.HelpButton = this.HelpButton;
            base.ShowIcon = this.ShowIcon;
            base.ShowInTaskbar = this.ShowInTaskbar;
            base.ControlBox = this.ControlBox;
            base.FormBorderStyle = this.FormBorderStyle;

            _isDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle &= ~(WS_EX_CLIENTEDGE | WS_EX_DLGMODALFRAME | WS_EX_STATICEDGE);
                if (!IsDesignMode)
                {
                    cp.Style = WS_POPUP | WS_BORDER;
                    cp.ClassStyle |= CS_DROPSHADOW;
                }
                else
                {
                    cp.Style &= ~(WS_CAPTION | WS_MAXIMIZE | WS_MINIMIZE | WS_BORDER | WS_THICKFRAME);
                }
                return cp;
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (!IsDesignMode && HideOnDeactivate)
                Hide();
        }
    }
}
