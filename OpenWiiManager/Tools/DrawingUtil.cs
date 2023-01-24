using OpenWiiManager.Controls;
using OpenWiiManager.Language.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Tools
{
    public static class DrawingUtil
    {
        public static TextFormatFlags GetTextFormatFlags(ContentAlignment align, bool autoEllipsis = false, bool useMnemonic = false)
        {
            var ff = TextFormatFlags.Default;

            if (!useMnemonic)
                ff |= TextFormatFlags.NoPrefix;
            if (align == System.Drawing.ContentAlignment.TopLeft)
                ff |= (TextFormatFlags.Left | TextFormatFlags.Top);
            else if (align == System.Drawing.ContentAlignment.TopCenter)
                ff |= (TextFormatFlags.HorizontalCenter | TextFormatFlags.Top);
            else if (align == System.Drawing.ContentAlignment.TopRight)
                ff |= (TextFormatFlags.Right | TextFormatFlags.Top);
            else if (align == System.Drawing.ContentAlignment.MiddleLeft)
                ff |= (TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            else if (align == System.Drawing.ContentAlignment.MiddleCenter)
                ff |= (TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            else if (align == System.Drawing.ContentAlignment.MiddleRight)
                ff |= (TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
            else if (align == System.Drawing.ContentAlignment.BottomLeft)
                ff |= (TextFormatFlags.Left | TextFormatFlags.Bottom);
            else if (align == System.Drawing.ContentAlignment.BottomCenter)
                ff |= (TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom);
            else if (align == System.Drawing.ContentAlignment.BottomRight)
                ff |= (TextFormatFlags.Right | TextFormatFlags.Bottom);

            if (autoEllipsis)
                ff |= TextFormatFlags.EndEllipsis;

            return ff;
        }

        public static TextFormatFlags GetTextFormatFlags(Label label)
        {
            return GetTextFormatFlags(label.TextAlign, label.AutoEllipsis, label.UseMnemonic);
        }

        public static TextFormatFlags GetTextFormatFlags(ButtonBase button)
        {
            return GetTextFormatFlags(button.TextAlign, button.AutoEllipsis, button.UseMnemonic);
        }

        public static Rectangle AlignRect(Rectangle bounds, Size inner, ContentAlignment alignment)
        {
            Point p;
            if (alignment == ContentAlignment.TopLeft)
                p = Point.Empty;
            else if (alignment == ContentAlignment.TopCenter)
                p = new Point((bounds.Width - inner.Width) / 2, 0);
            else if (alignment == ContentAlignment.TopRight)
                p = new Point(bounds.Width - inner.Width, 0);
            else if (alignment == ContentAlignment.MiddleLeft)
                p = new Point(0, (bounds.Height - inner.Height) / 2);
            else if (alignment == ContentAlignment.MiddleCenter)
                p = new Point((bounds.Width - inner.Width) / 2, (bounds.Height - inner.Height) / 2);
            else if (alignment == ContentAlignment.MiddleRight)
                p = new Point(bounds.Width - inner.Width, (bounds.Height - inner.Height) / 2);
            else if (alignment == ContentAlignment.BottomLeft)
                p = new Point(0, bounds.Height - inner.Height);
            else if (alignment == ContentAlignment.BottomCenter)
                p = new Point((bounds.Width - inner.Width) / 2, bounds.Height - inner.Height);
            else if (alignment == ContentAlignment.BottomRight)
                p = new Point(bounds.Width - inner.Width, bounds.Height - inner.Height);
            else
                throw new UnreachableException();

            p.Offset(bounds.Location);

            return new Rectangle(p, inner);
        }

        public static Rectangle? GetImageBounds(ButtonBase button, Padding? padding = null)
        {
            if (button.Image == null)
                return null;

            padding ??= button.Padding;

            if (padding == null)
                throw new UnreachableException();

            var outer = button.ClientRectangle.AccountForPadding(padding.Value);

            return AlignRect(outer, button.Image.Size, button.ImageAlign);
        }

        public static Size GetTextSize(Control control)
        {
            using var g = Graphics.FromHwnd(control.Handle);
            return TextRenderer.MeasureText(g, control.Text, control.Font);
        }
    }
}
