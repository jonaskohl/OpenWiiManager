using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static OpenWiiManager.Win32.UxTheme;

namespace OpenWiiManager.Win32.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct WtaOptions
    {
        public Wtnca Flags;
        public Wtnca Mask;
    }
}
