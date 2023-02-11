using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32
{
    internal class Shell32
    {
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);
    }
}
