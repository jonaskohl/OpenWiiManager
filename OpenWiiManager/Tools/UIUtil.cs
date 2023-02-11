using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Tools
{
    public static class UIUtil
    {
        public static void ShowBalloon(IWin32Window target, string text)
        {
            ArgumentNullException.ThrowIfNull(text, nameof(text));

            var g_hinst = User32.GetWindowLong(IntPtr.Zero, Constants.GWL_HINSTANCE);
            var hWnd = User32.CreateWindow(Constants.TOOLTIPS_CLASS, null, Constants.WS_POPUP | Constants.TTS_NOPREFIX | Constants.TTS_BALLOON, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, g_hinst, IntPtr.Zero);
            if (hWnd == IntPtr.Zero)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            var info = new TOOLINFO();
            info.cbSize = Marshal.SizeOf<TOOLINFO>();
            info.hwnd = target.Handle;
            info.uFlags = Constants.TTF_TRANSPARENT | Constants.TTF_CENTERTIP;
            info.uId = IntPtr.Zero;
            info.hinst = IntPtr.Zero;
            info.lpszText = text;
            if (!User32.GetClientRect(target.Handle, out info.rect))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            if (!User32.SendMessage(hWnd, Constants.TTM_ADDTOOL, 0, ref info))
                throw new Win32Exception(Marshal.GetLastWin32Error());
            if (User32.SendMessage(hWnd, Constants.WM_SHOWWINDOW, 1, 0) != 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}
