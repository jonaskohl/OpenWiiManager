using OpenWiiManager.Win32.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32
{
    internal static class UxTheme
    {
        [DllImport("uxtheme.dll")]
        public extern static int SetWindowThemeAttribute(IntPtr hWnd, int wtype, ref WtaOptions attributes, uint size);

        //[DllImport("uxtheme.dll")]
        //public extern static int GetThemeMargins(IntPtr hTheme, IntPtr hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, ref MARGIN pMargins);

        [DllImport("uxtheme.dll")]
        public extern static int GetThemeMargins(IntPtr hTheme, SafeHandle hdc, int iPartId, int iStateId, int iPropId, IntPtr rect, out RECT pMargins);

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
    }
}
