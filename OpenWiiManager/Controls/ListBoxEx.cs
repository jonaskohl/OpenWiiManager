using OpenWiiManager.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Controls
{
    public class ListBoxEx : ListBox
    {
        [Category("Property Changed")]
        public event EventHandler? EmptyTextChanged;

        protected bool isEmptyTextVisible => Items.Count < 1 && !string.IsNullOrWhiteSpace(emptyText);

        protected string emptyText = "";

        [Category("Appearance")]
        [DefaultValue("")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string EmptyText
        {
            get => emptyText;
            set
            {
                emptyText = value;
                OnEmptyTextChanged();
            }
        }

        protected virtual void OnEmptyTextChanged()
        {
            Invalidate();
            EmptyTextChanged?.Invoke(this, EventArgs.Empty);
        }

        public ListBoxEx()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == Constants.WM_PAINT)
            {
                OnWmPaint(ref m);
            }
            else if (m.Msg == Constants.WM_ERASEBKGND && isEmptyTextVisible)
            {
                ClearBackground();
            }
        }

        protected virtual void OnWmPaint(ref Message m)
        {
            if (isEmptyTextVisible)
                DrawText();
        }

        protected void DrawText()
        {
            using var g = GetGraphics();
            TextRenderer.DrawText(g, EmptyText, Font, ClientRectangle, SystemColors.GrayText, BackColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        protected void ClearBackground()
        {
            using var g = GetGraphics();
            g.Clear(BackColor);
        }

        protected Graphics GetGraphics()
        {
            return Graphics.FromHwnd(Handle);
        }
    }
}
