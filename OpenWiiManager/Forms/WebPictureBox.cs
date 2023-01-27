using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWiiManager.Forms
{
    public class WebPictureBox : PictureBox
    {
        private bool _isLoading = false;
        private string? _error = null;
        private Uri? _url;
        private Bitmap _cachedBitmap;

        public string? URL
        {
            get => _url?.ToString();
            set
            {
                _url = value == null ? null : new Uri(value);
                if (_url != null)
                    Invoke(() => TryDownloadBitmap());
                else
                {
                    Image = null;
                    _cachedBitmap?.Dispose();
                    _cachedBitmap = null;
                    Invalidate();
                }
            }
        }

        private void TryDownloadBitmap()
        {
            Image = null;
            _isLoading = true;
            Invalidate();
            Task.Run(async () =>
            {
                try
                {
                    var res = await _url.GetAsync();
                    if (res.StatusCode < 200 || res.StatusCode >= 400)
                    {
                        _error = $"HTTP {res.StatusCode} {res.ResponseMessage}";
                        return;
                    }

                    try
                    {
                        using var stream = await res.GetStreamAsync();
                        var bmp = new Bitmap(stream);
                        _cachedBitmap?.Dispose();
                        _cachedBitmap = bmp;
                        Image = _cachedBitmap;
                    }
                    catch (ArgumentException ex)
                    {
                        _cachedBitmap?.Dispose();
                        Image = null;
                        Debug.WriteLine($"ERROR: {ex.GetType().Name}: {ex.Message}");
                        _error = $"{ex.GetType().Name}: {ex.Message}";
                    }
                } catch (FlurlHttpException flex)
                {
                    _cachedBitmap?.Dispose();
                    Image = null;
                    Debug.WriteLine($"ERROR: {flex.GetType().Name}: {flex.Message}");
                    _error = $"{flex.GetType().Name}: {flex.Message}";
                }
            }).ContinueWith(t =>
            {
                Invoke(() =>
                {
                    _isLoading = false;
                    Invalidate();
                });
            });
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (_error != null)
            {
                TextRenderer.DrawText(pe.Graphics, _error, Font, ClientRectangle, Color.Red, BackColor, TextFormatFlags.WordBreak);
            }
            else if (_isLoading)
            {
                var pos = Point.Empty;
                pos.X = (ClientRectangle.Width - Properties.Resources.Clock.Width) / 2;
                pos.Y = (ClientRectangle.Height - Properties.Resources.Clock.Height) / 2 - 20;
                pe.Graphics.DrawImage(Properties.Resources.Clock, new Rectangle(pos, Properties.Resources.Clock.Size));
                TextRenderer.DrawText(pe.Graphics, "Loading...", Font, ClientRectangle, SystemColors.GrayText, BackColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }
            else if (Image == null)
                ControlPaint.DrawBorder3D(pe.Graphics, ClientRectangle);
        }
    }
}
