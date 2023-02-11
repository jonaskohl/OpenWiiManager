using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Tools
{
    public static class ShellUtil
    {
        public static bool ShowFileProperties(string filename)
        {
            SHELLEXECUTEINFO info = new();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = filename;
            info.nShow = Constants.SW_SHOW;
            info.fMask = Constants.SEE_MASK_INVOKEIDLIST;
            return Shell32.ShellExecuteEx(ref info);
        }
    }
}
