using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    internal class SIZE
    {
        public int cx;
        public int cy;

        public SIZE(int cx, int cy)
        {
            this.cx = cx;
            this.cy = cy;
        }
    }
}
