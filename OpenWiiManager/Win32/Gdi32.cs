using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32
{
    internal static class Gdi32
    {
        [DllImport("Gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDc);

        [DllImport("Gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hGdiObj);

        [DllImport("Gdi32.dll")]
        public static extern IntPtr SelectObject(SafeHandle hDc, IntPtr hGdiObj);

        [DllImport("Gdi32.dll")]
        public static extern bool DeleteObject(IntPtr ho);

        [DllImport("Gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDc);
    }
}
