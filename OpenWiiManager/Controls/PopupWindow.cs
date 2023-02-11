using OpenWiiManager.Win32;
using OpenWiiManager.Language.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Controls
{
    public class PopupWindow : Form
    {
        //    private bool _isDesignMode;
        //    protected bool IsDesignMode => _isDesignMode || (Site != null && Site.DesignMode);

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

            //_isDesignMode = LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle &= ~(Constants.WS_EX_CLIENTEDGE | Constants.WS_EX_DLGMODALFRAME | Constants.WS_EX_STATICEDGE);
                if (!this.IsDesignMode())
                {
                    cp.Style = unchecked((int)(Constants.WS_POPUP | Constants.WS_BORDER));
                    cp.ClassStyle |= Constants.CS_DROPSHADOW | Constants.CS_SAVEBITS;
                }
                else
                {
                    cp.Style &= ~unchecked((int)(Constants.WS_CAPTION | Constants.WS_MAXIMIZE | Constants.WS_MINIMIZE | Constants.WS_BORDER | Constants.WS_THICKFRAME));
                }
                return cp;
            }
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            if (!this.IsDesignMode() && HideOnDeactivate)
                Hide();
        }
    }
}
