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
        Version ver;

        private static readonly Bitmap[] digitImages = new[]
        {
            Properties.Resources.digit0,
            Properties.Resources.digit1,
            Properties.Resources.digit2,
            Properties.Resources.digit3,
            Properties.Resources.digit4,
            Properties.Resources.digit5,
            Properties.Resources.digit6,
            Properties.Resources.digit7,
            Properties.Resources.digit8,
            Properties.Resources.digit9,
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

            ver = Version.Parse(Application.ProductVersion.Substring(0, Application.ProductVersion.IndexOf("-")));
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

            int[] verParts = new[] { ver.Major, ver.Minor, ver.Build, ver.Revision };
            List<int> numbers = new();
            foreach (var p in verParts)
            {
                if (p < 0)
                    break;
                numbers.Add(p);
            }

            var xpos = xstart;

            for (var i = 0; i < numbers.Count; ++i)
            {
                foreach (var d in GetDigitsOf(numbers[i]))
                {
                    var bmp = digitImages[d];
                    g.DrawImage(bmp, new Rectangle(
                        xpos, ystart,
                        bmp.Width, bmp.Height
                    ));
                    xpos += bmp.Width + kerning;
                }
                if (i < numbers.Count - 1)
                {
                    g.DrawImage(Properties.Resources.dot, new Rectangle(
                        xpos, ystart,
                        Properties.Resources.dot.Width, Properties.Resources.dot.Height
                    ));
                    xpos += Properties.Resources.dot.Width + kerning;
                }
            }
        }

        private int[] GetDigitsOf(int num)
        {
            var numDigits = num == 0 ? 1 : (int)Math.Floor(Math.Log10(num) + 1);
            int[] digits = new int[numDigits];
            int c = 0;
            while (num > 0)
            {
                var d = num % 10;
                digits[numDigits - 1 - c] = d;
                num /= 10;
                ++c;
            }
            return digits;
        }
    }
}
