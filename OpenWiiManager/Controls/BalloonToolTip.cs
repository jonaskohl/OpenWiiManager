using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.ComponentModel;
using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;

namespace OpenWiiManager.Controls
{
    /// <remarks>This classes implements the balloon tooltip.
    /// http://stackoverflow.com/questions/2028466
    /// I hated Microsoft WinForms QA department after I had to develop my own version of the tooltip class,
    /// just to workaround a bug, that would only add ~5 lines of code into the system.windows.forms.dll when fixed properly.</remarks>
    public class BalloonToolTip : IDisposable
    {
        #region Managed wrapper over some tooltip messages.
        // The most useful part of the wrappers in this section is their documentation.

        /// <summary>Send TTM_SETTITLE message to the tooltip.
        /// TODO [very low]: implement the custom icon, if needed. </summary>
        /// <param name="_wndTooltip">HWND of the tooltip</param>
        /// <param name="_icon">Standard icon</param>
        /// <param name="_title">The title string</param>
        internal static int SetTitle(IntPtr _wndTooltip, ToolTipIcon _icon, string _title)
        {
            var tempptr = IntPtr.Zero;
            try
            {
                tempptr = Marshal.StringToHGlobalUni(_title);
                return User32.SendMessage(_wndTooltip, Constants.TTM_SETTITLE, (int)_icon, tempptr);
            }
            finally
            {
                if (IntPtr.Zero != tempptr)
                {
                    Marshal.FreeHGlobal(tempptr);
                    tempptr = IntPtr.Zero;
                }
            }
        }

        /// <summary>Send a message that wants LPTOOLINFO as the lParam</summary>
        /// <param name="_wndTooltip">HWND of the tooltip.</param>
        /// <param name="_msg">window message to send</param>
        /// <param name="_wParam">wParam value</param>
        /// <param name="_ti">TOOLINFO structure that goes to the lParam field. The cbSize field must be set.</param>
        internal static int SendToolInfoMessage(IntPtr _wndTooltip, int _msg, int _wParam, TOOLINFO _ti)
        {
            var tempptr = IntPtr.Zero;
            try
            {
                tempptr = Marshal.AllocHGlobal(_ti.cbSize);
                Marshal.StructureToPtr(_ti, tempptr, false);

                return User32.SendMessage(_wndTooltip, _msg, _wParam, tempptr);
            }
            finally
            {
                if (IntPtr.Zero != tempptr)
                {
                    Marshal.FreeHGlobal(tempptr);
                    tempptr = IntPtr.Zero;
                }
            }
        }

        /// <summary>Registers a tool with a ToolTip control</summary>
        /// <param name="_wndTooltip">HWND of the tooltip</param>
        /// <param name="_ti">TOOLINFO structure containing information that the ToolTip control needs to display text for the tool.</param>
        /// <returns>Returns true if successful.</returns>
        internal static bool AddTool(IntPtr _wndTooltip, TOOLINFO _ti)
        {
            int res = SendToolInfoMessage(_wndTooltip, Constants.TTM_ADDTOOL, 0, _ti);
            return Convert.ToBoolean(res);
        }

        /// <summary>Registers a tool with a ToolTip control</summary>
        /// <param name="_wndTooltip">HWND of the tooltip</param>
        /// <param name="_ti">TOOLINFO structure containing information that the ToolTip control needs to display text for the tool.</param>
        internal static void DelTool(IntPtr _wndTooltip, TOOLINFO _ti)
        {
            SendToolInfoMessage(_wndTooltip, Constants.TTM_DELTOOL, 0, _ti);
        }

        internal static void UpdateTipText(IntPtr _wndTooltip, TOOLINFO _ti)
        {
            SendToolInfoMessage(_wndTooltip, Constants.TTM_UPDATETIPTEXT, 0, _ti);
        }

        /// <summary>Activates or deactivates a tracking ToolTip</summary>
        /// <param name="_wndTooltip">HWND of the tooltip</param>
        /// <param name="bActivate">Value specifying whether tracking is being activated or deactivated</param>
        /// <param name="_ti">Pointer to a TOOLINFO structure that identifies the tool to which this message applies</param>
        internal static void TrackActivate(IntPtr _wndTooltip, bool bActivate, TOOLINFO _ti)
        {
            SendToolInfoMessage(_wndTooltip, Constants.TTM_TRACKACTIVATE, Convert.ToInt32(bActivate), _ti);
        }

        /// <summary>returns (LPARAM) MAKELONG( pt.X, pt.Y )</summary>
        internal static IntPtr makeLParam(Point pt)
        {
            int res = (pt.X & 0xFFFF);
            res |= ((pt.Y & 0xFFFF) << 16);
            return new IntPtr(res);
        }

        /// <summary>Sets the position of a tracking ToolTip</summary>
        /// <param name="_wndTooltip">HWND of the tooltip.</param>
        /// <param name="pt">The point at which the tracking ToolTip will be displayed, in screen coordinates.</param>
        internal static void TrackPosition(IntPtr _wndTooltip, Point pt)
        {
            User32.SendMessage(_wndTooltip, Constants.TTM_TRACKPOSITION, 0, makeLParam(pt));
        }

        /// <summary>Sets the maximum width for a ToolTip window</summary>
        /// <param name="_wndTooltip">HWND of the tooltip.</param>
        /// <param name="pxWidth">Maximum ToolTip window width, or -1 to allow any width</param>
        /// <returns>the previous maximum ToolTip width</returns>
        internal static int SetMaxTipWidth(IntPtr _wndTooltip, int pxWidth)
        {
            return User32.SendMessage(_wndTooltip, Constants.TTM_SETMAXTIPWIDTH, 0, new IntPtr(pxWidth));
        }

        #endregion

        /// <summary>Sets the information that a ToolTip control maintains for a tool (not currently used).</summary>
        /// <param name="act"></param>
        internal void AlterToolInfo(Action<TOOLINFO> act)
        {
            var tempptr = IntPtr.Zero;
            try
            {
                tempptr = Marshal.AllocHGlobal(m_toolinfo.cbSize);
                Marshal.StructureToPtr(m_toolinfo, tempptr, false);

                User32.SendMessage(m_wndToolTip, Constants.TTM_GETTOOLINFO, 0, tempptr);

                m_toolinfo = (TOOLINFO)Marshal.PtrToStructure(tempptr, typeof(TOOLINFO));

                act(m_toolinfo);

                Marshal.StructureToPtr(m_toolinfo, tempptr, false);

                User32.SendMessage(m_wndToolTip, Constants.TTM_SETTOOLINFO, 0, tempptr);
            }
            finally
            {
                Marshal.FreeHGlobal(tempptr);
            }
        }

        readonly Control m_ownerControl;

        // The ToolTip's HWND
        IntPtr m_wndToolTip = IntPtr.Zero;

        /// <summary>The maximum width for a ToolTip window.
        /// If a ToolTip string exceeds the maximum width, the control breaks the text into multiple lines.</summary>
        int m_pxMaxWidth = 200;

        TOOLINFO m_toolinfo = new TOOLINFO();

        public BalloonToolTip(Control owner)
        {
            m_ownerControl = owner;

            // See http://msdn.microsoft.com/en-us/library/bb760252(VS.85).aspx#tooltip_sample_rect
            m_toolinfo.cbSize = Marshal.SizeOf(typeof(TOOLINFO));
            m_toolinfo.uFlags = Constants.TTF_TRANSPARENT | Constants.TTF_TRACK;
            m_toolinfo.hwnd = m_ownerControl.Handle;
            m_toolinfo.rect = m_ownerControl.Bounds;
        }

        public bool ControlIsAlive()
        {
            return IntPtr.Zero != m_wndToolTip;
        }

        /// <summary>Throws an exception if there's no alive WIN32 window.</summary>
        void VerifyControlIsAlive()
        {
            if (!ControlIsAlive())
                throw new ApplicationException("The control is not created, or is already destroyed.");
        }

        /// <summary>Create the balloon window.</summary>
        public void Create()
        {
            if (IntPtr.Zero != m_wndToolTip)
                Destroy();

            // Create the tooltip control.
            m_wndToolTip = User32.CreateWindowEx(Constants.WS_EX_TOPMOST, Constants.TOOLTIPS_CLASS,
                string.Empty,
                Constants.WS_POPUP | Constants.TTS_BALLOON | Constants.TTS_NOPREFIX | Constants.TTS_ALWAYSTIP,
                Constants.CW_USEDEFAULT, Constants.CW_USEDEFAULT, Constants.CW_USEDEFAULT, Constants.CW_USEDEFAULT,
                m_ownerControl.Handle, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);

            if (IntPtr.Zero == m_wndToolTip)
                throw new Win32Exception();

            if (!User32.SetWindowPos(m_wndToolTip, Constants.HWND_TOPMOST,
                        0, 0, 0, 0,
                        Constants.SWP_NOMOVE | Constants.SWP_NOSIZE | Constants.SWP_NOACTIVATE))
                throw new Win32Exception();

            SetMaxTipWidth(m_wndToolTip, m_pxMaxWidth);
        }

        bool m_bVisible = false;

        /// <summary>return true if the balloon is currently visible.</summary>
        public bool bVisible
        {
            get
            {
                if (IntPtr.Zero == m_wndToolTip)
                    return false;
                return m_bVisible;
            }
        }

        /// <summary>Balloon title.
        /// The balloon will only use the new value on the next Show() operation.</summary>
        public string strTitle;

        /// <summary>Balloon title icon.
        /// The balloon will only use the new value on the next Show() operation.</summary>
        public ToolTipIcon icon;

        /// <summary>Balloon title icon.
        /// The new value is updated immediately.</summary>
        public string strText
        {
            get { return m_toolinfo.lpszText; }
            set
            {
                m_toolinfo.lpszText = value;
                if (bVisible)
                    UpdateTipText(m_wndToolTip, m_toolinfo);
            }
        }

        /// <summary>Show the balloon.</summary>
        /// <param name="pt">The balloon stem position, in the owner's client coordinates.</param>
        public void Show(Point pt)
        {
            VerifyControlIsAlive();

            if (m_bVisible) Hide();

            // http://www.deez.info/sengelha/2008/06/12/balloon-tooltips/
            if (!AddTool(m_wndToolTip, m_toolinfo))
                throw new ApplicationException("Unable to register the tooltip");

            SetTitle(m_wndToolTip, icon, strTitle);

            TrackPosition(m_wndToolTip, m_ownerControl.PointToScreen(pt));

            TrackActivate(m_wndToolTip, true, m_toolinfo);

            m_bVisible = true;
        }

        public void Track(Point pt)
        {
            VerifyControlIsAlive();

            if (!m_bVisible) return;

            TrackPosition(m_wndToolTip, pt);
        }

        /// <summary>Hide the balloon if it's visible.
        /// If the balloon was previously hidden, this method does nothing.</summary>
        public void Hide()
        {
            VerifyControlIsAlive();

            if (!m_bVisible) return;

            TrackActivate(m_wndToolTip, false, m_toolinfo);

            DelTool(m_wndToolTip, m_toolinfo);

            m_bVisible = false;
        }

        /// <summary>Destroy the balloon.</summary>
        public void Destroy()
        {
            if (IntPtr.Zero == m_wndToolTip)
                return;
            if (m_bVisible) Hide();

            User32.DestroyWindow(m_wndToolTip);
            m_wndToolTip = IntPtr.Zero;
        }

        void IDisposable.Dispose()
        {
            Destroy();
        }
    }
}