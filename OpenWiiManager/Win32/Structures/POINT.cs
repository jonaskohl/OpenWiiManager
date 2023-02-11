using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT
    {
        public int x;
        public int y;

        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
