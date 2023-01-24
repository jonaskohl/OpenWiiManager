using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Language.Extensions
{
    public static class DrawingExtensions
    {
        public static Rectangle AccountForPadding(this Rectangle rect, Padding padding)
        {
            return new Rectangle(
                rect.X + padding.Left,
                rect.Y + padding.Top,
                rect.Width - padding.Horizontal,
                rect.Height - padding.Vertical
            );
        }
    }
}
