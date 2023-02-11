using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct HDITEM
    {
        public Mask mask;
        public int cxy;
        [MarshalAs(UnmanagedType.LPTStr)] public string pszText;
        public IntPtr hbm;
        public int cchTextMax;
        public Format fmt;
        public IntPtr lParam;
        public int iImage;
        public int iOrder;
        public uint type;
        public IntPtr pvFilter;
        public uint state;

        [Flags]
        public enum Mask
        {
            Format = 0x4,       // HDI_FORMAT
        };

        [Flags]
        public enum Format
        {
            SortDown = 0x200,   // HDF_SORTDOWN
            SortUp = 0x400,     // HDF_SORTUP
        };
    };
}
