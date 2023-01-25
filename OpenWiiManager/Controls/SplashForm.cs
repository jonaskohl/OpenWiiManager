using OpenWiiManager.Language.Extensions;
using OpenWiiManager.Win32;
using OpenWiiManager.Win32.Structures;
using System.Drawing.Imaging;
using System.Windows.Forms.VisualStyles;

namespace OpenWiiManager.Controls
{
    public class SplashForm : Form
    {
        private float _opacity = 1.0f;
        public Bitmap BackgroundBitmap;

        public new float Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                _opacity = value;
                SelectBitmap(BackgroundBitmap);
            }
        }

        public SplashForm(Bitmap bitmap)
        {
            // Window settings   
            this.TopMost = true;
            //this.ShowInTaskbar = false;
            this.Size = bitmap.Size;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // Must be called before setting bitmap   
            this.BackgroundBitmap = bitmap;
            this.SelectBitmap(BackgroundBitmap);
            this.BackColor = Color.Red;
        }

        // Sets the current bitmap
        public void SelectBitmap(Bitmap bitmap)
        {
            // Does this bitmap contain an alpha channel?   
            if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
            {
                throw new ApplicationException("The bitmap must be 32bpp with alpha-channel.");
            }
            // Get device contexts   
            IntPtr screenDc = User32.GetDC(IntPtr.Zero);
            IntPtr memDc = Gdi32.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr hOldBitmap = IntPtr.Zero;
            try
            {
                // Get handle to the new bitmap and select it into the current device context      
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                hOldBitmap = Gdi32.SelectObject(memDc, hBitmap);
                // Set parameters for layered window update      
                SIZE newSize = new SIZE(bitmap.Width, bitmap.Height);
                // Size window to match bitmap      
                POINT sourceLocation = new POINT(0, 0);
                POINT newLocation = new POINT(this.Left, this.Top);
                // Same as this window      
                BLENDFUNCTION blend = new BLENDFUNCTION();
                blend.BlendOp = Constants.AC_SRC_OVER;
                // Only works with a 32bpp bitmap      
                blend.BlendFlags = 0; // Always 0   
                blend.SourceConstantAlpha = (byte)(Opacity * 255); // Set to 255 for per-pixel alpha values
                blend.AlphaFormat = Constants.AC_SRC_ALPHA;
                // Only works when the bitmap contains an alpha channel      
                // Update the window      
                User32.UpdateLayeredWindow(Handle, screenDc, ref newLocation, ref newSize, memDc, ref sourceLocation, 0, ref blend, Constants.ULW_ALPHA);
            }
            finally
            {
                // Release device context      
                User32.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    Gdi32.SelectObject(memDc, hOldBitmap);
                    Gdi32.DeleteObject(hBitmap);
                    // Remove bitmap resources      
                }
                Gdi32.DeleteDC(memDc);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Add the layered extended style (WS_EX_LAYERED) to this window      
                CreateParams createParams = base.CreateParams;
                if (!this.IsDesignMode())
                    createParams.ExStyle |= Constants.WS_EX_LAYERED;
                return createParams;
            }
        }

        // Let Windows drag this window for us (thinks its hitting the title bar of the window)
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == Constants.WM_NCHITTEST)
            {
                message.Result = IntPtr.Zero;
            }
            else if (message.Msg == Constants.WM_PAINT)
                SelectBitmap(BackgroundBitmap);
            else
            {
                base.WndProc(ref message);
            }
        }
    }
}