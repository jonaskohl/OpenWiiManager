using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct TOOLINFO
    {
        public int cbSize;
        public int uFlags;
        public IntPtr hwnd;
        public IntPtr uId;
        public RECT rect;
        public IntPtr hinst;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpszText;

        public IntPtr lParam;
    }
}
