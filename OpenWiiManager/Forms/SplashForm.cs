using OpenWiiManager.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenWiiManager.Forms
{
    public partial class SplashForm : Form
    {
        string fullVer;

        private static Dictionary<char, Bitmap> charImages = new()
        {
            { '0', Properties.Resources.digit0 },
            { '1', Properties.Resources.digit1 },
            { '2', Properties.Resources.digit2 },
            { '3', Properties.Resources.digit3 },
            { '4', Properties.Resources.digit4 },
            { '5', Properties.Resources.digit5 },
            { '6', Properties.Resources.digit6 },
            { '7', Properties.Resources.digit7 },
            { '8', Properties.Resources.digit8 },
            { '9', Properties.Resources.digit9 },
            { '.', Properties.Resources.dot },
            { '-', Properties.Resources.dash },
            { 'a', Properties.Resources.a },
            { 'b', Properties.Resources.b },
            { 'c', Properties.Resources.c },
            { 'd', Properties.Resources.d },
            { 'e', Properties.Resources.e },
            { 'f', Properties.Resources.f },
            { 'g', Properties.Resources.g },
            { 'h', Properties.Resources.h },
            { 'i', Properties.Resources.i },
            { 'j', Properties.Resources.j },
            { 'k', Properties.Resources.k },
            { 'l', Properties.Resources.l },
            { 'm', Properties.Resources.m },
            { 'n', Properties.Resources.n },
            { 'o', Properties.Resources.o },
            { 'p', Properties.Resources.p },
            { 'q', Properties.Resources.q },
            { 'r', Properties.Resources.r },
            { 's', Properties.Resources.s },
            { 't', Properties.Resources.t },
            { 'u', Properties.Resources.u },
            { 'v', Properties.Resources.v },
            { 'w', Properties.Resources.w },
            { 'x', Properties.Resources.x },
            { 'y', Properties.Resources.y },
            { 'z', Properties.Resources.z },
        };

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.Style &= ~unchecked((int)Constants.WS_MAXIMIZE);
                return cp;
            }
        }

        public SplashForm()
        {
            InitializeComponent();

            fullVer = Application.ProductVersion;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawDigits(e.Graphics);
        }
        
        private void DrawDigits(Graphics g)
        {
            const int xstart = 58;
            const int ystart = 302;
            const int kerning = 1;

            var xpos = xstart;

            for (var i = 0; i < fullVer.Length; ++i)
            {
                var curChar = fullVer[i];
                Image charBmp;
                if (!charImages.ContainsKey(curChar))
                {
                    charBmp = Properties.Resources.fallback;
                }
                else
                {
                    charBmp = charImages[curChar];
                }

                g.DrawImage(charBmp, new Rectangle(
                    xpos, ystart,
                    charBmp.Width, charBmp.Height
                ));
                xpos += charBmp.Width + kerning;
            }
        }
    }
}
