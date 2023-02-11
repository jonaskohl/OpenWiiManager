using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    [Flags()]
    internal enum Wtnca : uint
    {
        NoDrawCaption = 0x1,
        NoDrawIcon = 0x2,
        NoSysMenu = 0x4,
        NoMirrorHelp = 0x8
    }
}
