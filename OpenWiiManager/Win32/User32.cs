using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32
{
    internal static class User32
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint msg, uint wParam, uint lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetScrollInfo(IntPtr hWnd, int nBar, ref SCROLLINFO lpScrollInfo);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetScrollInfo(IntPtr hWnd, int nBar, ref SCROLLINFO lpScrollInfo, bool redraw);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint itemId, uint uEnable);

        [DllImport("user32.dll")]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref POINT pptDst, ref SIZE psize, IntPtr hdcSrc, ref POINT pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDc);
    }
}
