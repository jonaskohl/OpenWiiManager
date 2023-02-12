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
        private Image? _fallbackImage = null;
        private bool _isLoading = false;
        private string? _error = null;
        private Uri? _url;
        private Bitmap? _cachedBitmap;

        public Image? FallbackImage
        {
            get => _fallbackImage;
            set
            {
                _fallbackImage = value;
                Invalidate();
            }
        }

        public string? URL
        {
            get => _url?.ToString();
            set
            {
                SetUrlInternal(value);
                ApplyUrlInternal();
            }
        }

        private Task<bool> ApplyUrlInternal()
        {
            Debug.WriteLine("Apply URL");
            if (_url != null)
                return Invoke(() => TryDownloadBitmap());
            else
            {
                Debug.WriteLine("URL=null");
                Image = null;
                _error = null;
                _cachedBitmap?.Dispose();
                _cachedBitmap = null;
                Invalidate();
                return Task.Run(() => false);
            }
        }

        private void SetUrlInternal(string? value)
        {
            _url = value == null ? null : new Uri(value);
        }

        public Task<bool> SetUrlAsync(string url)
        {
            SetUrlInternal(url);
            return ApplyUrlInternal();
        }

        private Task<bool> TryDownloadBitmap()
        {
            Image = null;
            _isLoading = true;
            _error = null;
            Invalidate();
            var task = Task.Run(async () =>
            {
                try
                {
                    if (_url == null)
                        return false;

                    var res = await _url.GetAsync();
                    if (res.StatusCode < 200 || res.StatusCode >= 400)
                    {
                        _error = $"HTTP {res.StatusCode} {res.ResponseMessage}";
                        return false;
                    }

                    try
                    {
                        using var stream = await res.GetStreamAsync();
                        var bmp = new Bitmap(stream);
                        _cachedBitmap?.Dispose();
                        _cachedBitmap = bmp;
                        _error = null;
                        Image = _cachedBitmap;
                        return true;
                    }
                    catch (ArgumentException ex)
                    {
                        _cachedBitmap?.Dispose();
                        Image = null;
                        Debug.WriteLine($"ERROR: {ex.GetType().Name}: {ex.Message}");
                        _error = $"{ex.GetType().Name}: {ex.Message}";
                        return false;
                    }
                }
                catch (FlurlHttpException flex)
                {
                    _cachedBitmap?.Dispose();
                    Image = null;
                    Debug.WriteLine($"ERROR: {flex.GetType().Name}: {flex.Message}");
                    _error = $"{flex.GetType().Name}: {flex.Message}";
                    return false;
                }
            });

            task.ContinueWith(t =>
            {
                Invoke(() =>
                {
                    _isLoading = false;
                    Invalidate();
                });
            });

            return task;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            if (_error != null)
            {
                if (_fallbackImage != null)
                    pe.Graphics.DrawImage(_fallbackImage, ClientRectangle);
                else
                    TextRenderer.DrawText(pe.Graphics, _error, Font, ClientRectangle, Color.Red, BackColor, TextFormatFlags.WordBreak | TextFormatFlags.TextBoxControl);
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
