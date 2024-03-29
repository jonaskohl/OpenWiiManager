﻿using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OpenWiiManager.Language.Extensions
{
    public static class ControlExtensions
    {
        public static bool IsDesignMode(this Control control)
        {
            return control.Site != null && control.Site.DesignMode;
        }

        public static int GetScrollPosition(this ListBox listBox)
        {
            var info = new SCROLLINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.fMask = Constants.SIF_POS;
            var result = User32.GetScrollInfo(listBox.Handle, Constants.SB_VERT, ref info);
            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return info.nPos;
        }

        public static void SetScrollPosition(this ListBox listBox, int position)
        {
            var info = new SCROLLINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.fMask = Constants.SIF_POS;
            info.nPos = position;
            User32.SetScrollInfo(listBox.Handle, Constants.SB_VERT, ref info, true);
        }

        public static void DisableCloseButton(this IWin32Window window)
        {
            User32.EnableMenuItem(User32.GetSystemMenu(window.Handle, false), Constants.SC_CLOSE, 1);
        }

        public static void EnableCloseButton(this IWin32Window window)
        {
            User32.EnableMenuItem(User32.GetSystemMenu(window.Handle, false), Constants.SC_CLOSE, 0);
        }
    }
}
