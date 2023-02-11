using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public class MARGIN
    {
        public static readonly MARGIN DefaultMargin = new MARGIN(-1);

        public readonly int cxLeftWidth;
        public readonly int cxRightWidth;
        public readonly int cyTopHeight;
        public readonly int cyBottomheight;

        public MARGIN(int all)
        {
            cxLeftWidth = all;
            cxRightWidth = all;
            cyTopHeight = all;
            cyBottomheight = all;
        }

        public MARGIN(int leftWidth, int topHeight, int rightWidth, int bottomHeight)
        {
            cxLeftWidth = leftWidth;
            cxRightWidth = rightWidth;
            cyTopHeight = topHeight;
            cyBottomheight = bottomHeight;
        }
    }
}
