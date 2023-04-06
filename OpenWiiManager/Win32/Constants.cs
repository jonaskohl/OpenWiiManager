using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Win32
{
    internal static class Constants
    {
        #region <WinUser.h>
        #region Window messages
        public const int WM_DESTROY = 0x0002;
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_PAINT = 0x000F;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_SHOWWINDOW = 0x0018;
        public const int WM_NCHITTEST = 0x0084;
        public const int WM_CHANGEUISTATE = 0x0127;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_USER = 0x0400;
        public const int WM_CLOSE = 0x0010;
        public const int WM_PARENTNOTIFY = 0x0210;
        #endregion

        #region WM_ACTIVATE state values
        public const int WA_INACTIVE = 0;
        public const int WA_ACTIVE = 1;
        public const int WA_CLICKACTIVE = 2;
        #endregion

        #region Window styles
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_BORDER = 0x00800000;
        public const uint WS_CAPTION = 0x00C00000;
        public const uint WS_MAXIMIZE = 0x01000000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_THICKFRAME = 0x00040000;
        #endregion

        #region Extended window styles
        public const int WS_EX_DLGMODALFRAME = 0x00000001;
        public const int WS_EX_TOPMOST = 0x00000008;
        public const int WS_EX_CLIENTEDGE = 0x00000200;
        public const int WS_EX_STATICEDGE = 0x00020000;
        public const int WS_EX_LAYERED = 0x00080000;
        #endregion

        #region Class Styles
        public const int CS_SAVEBITS = 0x0800;
        public const int CS_DROPSHADOW = 0x00020000;
        #endregion

        #region UI state constants
        public const uint UIS_SET = 1;
        public const uint UISF_HIDEFOCUS = 0x1;
        #endregion

        #region Scroll Bar constants
        public const int SB_HORZ = 0;
        public const int SB_VERT = 1;
        public const int SB_CTL = 2;
        public const int SB_BOTH = 3;
        #endregion

        #region Scroll Bar Messages
        public const int SIF_RANGE = 0x0001;
        public const int SIF_PAGE = 0x0002;
        public const int SIF_POS = 0x0004;
        public const int SIF_DISABLENOSCROLL = 0x0008;
        public const int SIF_TRACKPOS = 0x0010;
        public const int SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS);
        #endregion

        #region System Menu Command Values
        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_NEXTWINDOW = 0xF040;
        public const int SC_PREVWINDOW = 0xF050;
        public const int SC_CLOSE = 0xF060;
        public const int SC_VSCROLL = 0xF070;
        public const int SC_HSCROLL = 0xF080;
        public const int SC_MOUSEMENU = 0xF090;
        public const int SC_KEYMENU = 0xF100;
        public const int SC_ARRANGE = 0xF110;
        public const int SC_RESTORE = 0xF120;
        public const int SC_TASKLIST = 0xF130;
        public const int SC_SCREENSAVE = 0xF140;
        public const int SC_HOTKEY = 0xF150;
        public const int SC_DEFAULT = 0xF160;
        public const int SC_MONITORPOWER = 0xF170;
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_SEPARATOR = 0xF00F;
        #endregion

        #region WM_NCHITTEST and MOUSEHOOKSTRUCT Mouse Position Codes
        public const int HTERROR = (-2);
        public const int HTTRANSPARENT = (-1);
        public const int HTNOWHERE = 0;
        public const int HTCLIENT = 1;
        public const int HTCAPTION = 2;
        public const int HTSYSMENU = 3;
        public const int HTGROWBOX = 4;
        public const int HTSIZE = HTGROWBOX;
        public const int HTMENU = 5;
        public const int HTHSCROLL = 6;
        public const int HTVSCROLL = 7;
        public const int HTMINBUTTON = 8;
        public const int HTMAXBUTTON = 9;
        public const int HTLEFT = 10;
        public const int HTRIGHT = 11;
        public const int HTTOP = 12;
        public const int HTTOPLEFT = 13;
        public const int HTTOPRIGHT = 14;
        public const int HTBOTTOM = 15;
        public const int HTBOTTOMLEFT = 16;
        public const int HTBOTTOMRIGHT = 17;
        public const int HTBORDER = 18;
        public const int HTREDUCE = HTMINBUTTON;
        public const int HTZOOM = HTMAXBUTTON;
        public const int HTSIZEFIRST = HTLEFT;
        public const int HTSIZELAST = HTBOTTOMRIGHT;
        public const int HTOBJECT = 19;
        public const int HTCLOSE = 20;
        public const int HTHELP = 21;
        #endregion

        #region UpdateLayeredWindow flags
        public const int ULW_COLORKEY = 0x00000001;
        public const int ULW_ALPHA = 0x00000002;
        public const int ULW_OPAQUE = 0x00000004;
        public const int ULW_EX_NORESIZE = 0x00000008;
        #endregion

        #region ShowWindow() Commands
        public const int SW_SHOW = 5;
        #endregion

        #region CreateWindow
        public const int CW_USEDEFAULT = unchecked((int)0x80000000);
        #endregion

        #region Window field offsets for GetWindowLong()
        public const int GWL_WNDPROC = (-4);
        public const int GWL_HINSTANCE = (-6);
        public const int GWL_HWNDPARENT = (-8);
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);
        public const int GWL_USERDATA = (-21);
        public const int GWL_ID = (-12);
        #endregion

        #region SetWindowPos Flags;
        public const int SWP_NOSIZE = 0x0001;
        public const int SWP_NOMOVE = 0x0002;
        public const int SWP_NOZORDER = 0x0004;
        public const int SWP_NOREDRAW = 0x0008;
        public const int SWP_NOACTIVATE = 0x0010;
        public const int SWP_FRAMECHANGED = 0x0020;
        public const int SWP_SHOWWINDOW = 0x0040;
        public const int SWP_HIDEWINDOW = 0x0080;
        public const int SWP_NOCOPYBITS = 0x0100;
        public const int SWP_NOOWNERZORDER = 0x0200;
        public const int SWP_NOSENDCHANGING = 0x0400;
        public const int SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const int SWP_NOREPOSITION = SWP_NOOWNERZORDER;
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        #endregion

        #region Edit Control Messages
        public const int EM_SETMARGINS = 0x00D3;
        public const int EM_SETCUEBANNER = 0x1501;
        #endregion

        #region Edit Control Notification Codes
        public const int EC_LEFTMARGIN = 0x0001;
        public const int EC_RIGHTMARGIN = 0x0002;
        public const int EC_USEFONTINFO = 0xffff;
        #endregion
        #endregion

        #region <wingdi.h>
        #region alpha format flags
        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;
        #endregion
        #endregion

        #region <shellapi.h>
        #region Begin ShellExecuteEx and family
        public const uint SEE_MASK_INVOKEIDLIST = 12;
        #endregion

        #region Image lists
        public const int SHIL_LARGE = 0x0;
        public const int SHIL_SMALL = 0x1;
        public const int SHIL_EXTRALARGE = 0x2;
        public const int SHIL_SYSSMALL = 0x3;
        public const int SHIL_JUMBO = 0x4;
        public const int SHIL_LAST = 0x4;
        #endregion
        #endregion

        #region <CommCtrl.h>
        #region Ranges for controll message IDs
        public const int LVM_FIRST = 0x1000;
        public const int HDM_FIRST = 0x1200;
        #endregion

        #region LISTVIEW CONTROL
        public const int LVM_GETHEADER = LVM_FIRST + 31;
        #endregion

        #region HEADER CONTROL
        public const int HDM_GETITEM = HDM_FIRST + 11;
        public const int HDM_SETITEM = HDM_FIRST + 12;
        #endregion

        #region TOOLTIPS CONTROL
        public const string TOOLTIPS_CLASS = "tooltips_class32";
        public const int TTS_ALWAYSTIP = 0x01;
        public const int TTS_NOPREFIX = 0x02;
        public const int TTS_NOANIMATE = 0x10;
        public const int TTS_NOFADE = 0x20;
        public const int TTS_BALLOON = 0x40;
        public const int TTS_CLOSE = 0x80;
        public const int TTF_IDISHWND = 0x0001;
        public const int TTF_CENTERTIP = 0x0002;
        public const int TTF_RTLREADING = 0x0004;
        public const int TTF_SUBCLASS = 0x0010;
        public const int TTF_TRACK = 0x0020;
        public const int TTF_ABSOLUTE = 0x0080;
        public const int TTF_TRANSPARENT = 0x0100;
        public const int TTF_PARSELINKS = 0x1000;
        public const int TTF_DI_SETITEM = 0x8000;
        public const int TTM_TRACKACTIVATE = (WM_USER + 17);
        public const int TTM_TRACKPOSITION = (WM_USER + 18);
        public const int TTM_SETMAXTIPWIDTH = (WM_USER + 24);
        public const int TTM_SETTITLE = (WM_USER + 33);
        public const int TTM_ADDTOOL = (WM_USER + 50);
        public const int TTM_DELTOOL = (WM_USER + 51);
        public const int TTM_GETTOOLINFO = (WM_USER + 53);
        public const int TTM_SETTOOLINFO = (WM_USER + 54);
        public const int TTM_UPDATETIPTEXT = (WM_USER + 57);
        #endregion

        #region IMAGE APIS
        public const int ILD_TRANSPARENT = 0x00000001;
        public const int ILD_IMAGE = 0x00000020;
        #endregion
        #endregion

        #region Misc (GUIDS etc.)
        public const string IID_IImageList = @"{46EB5926-582E-4017-9FDF-E8998DAA0950}";
        public const string IID_IImageList2 = @"{192B9D83-50FC-457B-90A0-2B82A8B5DAE1}";
        #endregion
    }
}
