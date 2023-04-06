using OpenWiiManager.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWiiManager.Tools
{
    public static class InteropUtil
    {
        private delegate IntPtr WndProcDelegate(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        public delegate IntPtr WndProcDelegatePublic(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam, ref bool breakMessageChain);
        public static int HiWord(int number)
        {
            if ((number & 0x80000000) == 0x80000000)
                return (number >> 16);
            else
                return (number >> 16) & 0xffff;
        }

        public static int LoWord(int number)
        {
            return number & 0xffff;
        }

        public static int MakeLong(int LoWord, int HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        public static uint MakeLong(uint LoWord, uint HiWord)
        {
            return (HiWord << 16) | (LoWord & 0xffff);
        }

        public static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return new IntPtr((HiWord << 16) | (LoWord & 0xffff));
        }

        public static int RemoveSubclass(IWin32Window win, IntPtr hOldWndProc)
        {
            var result = -1;
            if (win != null)
                result = User32.SetWindowLong(win.Handle, Constants.GWL_WNDPROC, hOldWndProc);
            return result;
        }

        public static int AddSubclass(IWin32Window win, WndProcDelegatePublic newWndProc, out IntPtr hOldWndProc)
        {
            var result = -1;
            if (win != null && newWndProc != null)
            {
                hOldWndProc = User32.GetWindowLong(win.Handle, Constants.GWL_WNDPROC);
                var hOldWndProc2 = hOldWndProc;
                WndProcDelegate wndProcWrapper = (IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam) =>
                {
                    var @break = false;
                    var procRes = IntPtr.Zero;

                    if (msg == Constants.WM_DESTROY)
                    {
                        RemoveSubclass(win, hOldWndProc2);
                    }
                    else
                    {
                        try
                        {
                            procRes = newWndProc(hWnd, msg, wParam, lParam, ref @break);
                        }
                        catch { }
                    }

                    if (!@break)
                        return User32.CallWindowProc(hOldWndProc2, hWnd, msg, wParam, lParam);
                    else
                        return procRes;
                };
                result = User32.SetWindowLong(win.Handle, Constants.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(wndProcWrapper));
            }
            else
                hOldWndProc = IntPtr.Zero;
            return result;
        }

        /// <summary>A SafeHandle to track DC handles.</summary>
		public class SafeDCHandle : SafeHandle
        {
            /// <summary>A null handle.</summary>
            public static readonly SafeDCHandle Null = new SafeDCHandle(IntPtr.Zero);

            private readonly IDeviceContext idc;

            /// <summary>Initializes a new instance of the <see cref="SafeDCHandle"/> class.</summary>
            /// <param name="hDC">The handle to the DC.</param>
            /// <param name="ownsHandle">
            /// <see langword="true"/> to have the native handle released when this safe handle is disposed or finalized; <see
            /// langword="false"/> otherwise.
            /// </param>
            public SafeDCHandle(IntPtr hDC, bool ownsHandle = true)
                : base(IntPtr.Zero, ownsHandle) => SetHandle(hDC);

            /// <summary>Initializes a new instance of the <see cref="SafeDCHandle"/> class.</summary>
            /// <param name="dc">An <see cref="IDeviceContext"/> instance.</param>
            public SafeDCHandle(IDeviceContext dc)
                : base(IntPtr.Zero, true)
            {
                if (dc == null)
                {
                    throw new ArgumentNullException(nameof(dc));
                }

                idc = dc;
                SetHandle(dc.GetHdc());
            }

            public static SafeDCHandle ScreenCompatibleDCHandle => new SafeDCHandle(Gdi32.CreateCompatibleDC(IntPtr.Zero));

            /// <inheritdoc/>
            public override bool IsInvalid => handle == IntPtr.Zero;

            /// <summary>Performs an implicit conversion from <see cref="Graphics"/> to <see cref="SafeDCHandle"/>.</summary>
            /// <param name="graphics">The <see cref="Graphics"/> instance.</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator SafeDCHandle(Graphics graphics) => new SafeDCHandle(graphics);

            public SafeDCHandle GetCompatibleDCHandle() => new SafeDCHandle(Gdi32.CreateCompatibleDC(handle));

            /// <inheritdoc/>
            protected override bool ReleaseHandle()
            {
                if (idc != null)
                {
                    idc.ReleaseHdc();
                    return true;
                }

                return Gdi32.DeleteDC(handle);
            }
        }

        public class SafeDCObjectHandle : SafeHandle
        {
            private readonly SafeDCHandle hDC;
            private readonly IntPtr hOld;

            public SafeDCObjectHandle(SafeDCHandle hdc, IntPtr hObj) : base(IntPtr.Zero, true)
            {
                if (hdc == null || hdc.IsInvalid) return;
                hDC = hdc;
                hOld = Gdi32.SelectObject(hdc, hObj);
                SetHandle(hObj);
            }

            public override bool IsInvalid => handle == IntPtr.Zero;

            protected override bool ReleaseHandle()
            {
                Gdi32.SelectObject(hDC, hOld);
                return Gdi32.DeleteObject(handle);
            }
        }
    }
}
