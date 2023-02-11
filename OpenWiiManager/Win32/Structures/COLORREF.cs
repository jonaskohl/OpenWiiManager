using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct COLORREF
    {
        public byte Blue;
        public byte Green;
        public byte Red;
        public byte Alpha;
    }
}
