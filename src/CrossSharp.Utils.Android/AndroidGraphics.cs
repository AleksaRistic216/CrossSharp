using Android.Graphics;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Color = Android.Graphics.Color;
using Paint = Android.Graphics.Paint;
using Path = Android.Graphics.Path;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace CrossSharp.Utils.Android;

/// <summary>
/// Content-based cache key for images that avoids hash collisions.
/// Uses image dimensions plus sampled pixel data for fast, reliable identification.
/// </summary>
internal readonly struct ImageCacheKey : IEquatable<ImageCacheKey>
{
    private readonly int _width;
    private readonly int _height;
    private readonly int _contentHash;

    public ImageCacheKey(Image<Rgba32> image)
    {
        _width = image.Width;
        _height = image.Height;
        _contentHash = ComputeContentHash(image);
    }

    private static int ComputeContentHash(Image<Rgba32> image)
    {
        // Sample pixels at corners and center for fast content hashing
        // This catches most differences while being much faster than hashing all pixels
        unchecked
        {
            int hash = 17;
            int w = image.Width;
            int h = image.Height;

            hash = hash * 31 + GetPixelValue(image, 0, 0);
            hash = hash * 31 + GetPixelValue(image, w - 1, 0);
            hash = hash * 31 + GetPixelValue(image, 0, h - 1);
            hash = hash * 31 + GetPixelValue(image, w - 1, h - 1);
            hash = hash * 31 + GetPixelValue(image, w / 2, h / 2);

            // Add a few more samples for larger images to reduce collision probability
            if (w > 10 && h > 10)
            {
                hash = hash * 31 + GetPixelValue(image, w / 4, h / 4);
                hash = hash * 31 + GetPixelValue(image, 3 * w / 4, 3 * h / 4);
            }

            return hash;
        }
    }

    private static int GetPixelValue(Image<Rgba32> image, int x, int y)
    {
        var pixel = image[x, y];
        return (pixel.R << 24) | (pixel.G << 16) | (pixel.B << 8) | pixel.A;
    }

    public bool Equals(ImageCacheKey other) =>
        _width == other._width && _height == other._height && _contentHash == other._contentHash;

    public override bool Equals(object? obj) => obj is ImageCacheKey other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(_width, _height, _contentHash);
}

public class AndroidGraphics : IGraphics
{
    /// <summary>
    /// Scale factor for converting CrossSharp font sizes to Android text sizes.
    /// Android uses different density units that require scaling.
    /// </summary>
    private const float AndroidFontScaleFactor = 2.5f;

    /// <summary>
    /// Maximum number of bitmaps to cache for image rendering performance.
    /// </summary>
    private const int MaxBitmapCacheSize = 50;

    /// <summary>
    /// Lock object for thread-safe bitmap cache operations.
    /// </summary>
    private static readonly object CacheLock = new();

    /// <summary>
    /// Static bitmap cache shared across AndroidGraphics instances.
    /// Uses content-based key for reliable cache hits.
    /// </summary>
    private static readonly Dictionary<ImageCacheKey, Bitmap> BitmapCache = new();
    private static readonly LinkedList<ImageCacheKey> BitmapCacheOrder = new();

    private readonly Canvas _canvas;
    private readonly Paint _paint;
    private readonly Paint _textPaint;
    private ClipState _clipState = ClipState.Empty;
    private System.Drawing.Point _offset = System.Drawing.Point.Empty;
    private readonly Stack<int> _clipSaveStack = new();

    public AndroidGraphics(Canvas canvas)
    {
        _canvas = canvas;
        _paint = new Paint { AntiAlias = true };
        _textPaint = new Paint { AntiAlias = true };
    }

    public void Render()
    {
        // No-op on Android - Canvas is immediately rendered
    }

    public void DrawRectangle(
        int x,
        int y,
        int width,
        int height,
        ColorRgba borderColor,
        int borderWidth,
        int roundedCornersRadius
    )
    {
        if (width <= 0 || height <= 0 || borderWidth <= 0 || borderColor.A == 0)
            return;

        x += _offset.X;
        y += _offset.Y;

        _paint.SetStyle(Paint.Style.Stroke);
        _paint.StrokeWidth = borderWidth;
        _paint.Color = ToAndroidColor(borderColor);

        if (roundedCornersRadius > 0)
        {
            var rect = new RectF(x, y, x + width, y + height);
            _canvas.DrawRoundRect(rect, roundedCornersRadius, roundedCornersRadius, _paint);
        }
        else
        {
            _canvas.DrawRect(x, y, x + width, y + height, _paint);
        }
    }

    public void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (width <= 0 || height <= 0 || fillColor.A == 0)
            return;

        x += _offset.X;
        y += _offset.Y;

        _paint.SetStyle(Paint.Style.Fill);
        _paint.Color = ToAndroidColor(fillColor);

        if (_clipState.CornerRadius > 0)
        {
            var rect = new RectF(x, y, x + width, y + height);
            _canvas.DrawRoundRect(rect, _clipState.CornerRadius, _clipState.CornerRadius, _paint);
        }
        else
        {
            _canvas.DrawRect(x, y, x + width, y + height, _paint);
        }
    }

    public void DrawText(string text, int x, int y, FontFamily fontFamily, int fontSize, ColorRgba textColor)
    {
        if (string.IsNullOrWhiteSpace(text) || fontSize <= 0 || textColor.A == 0)
            return;

        x += _offset.X;
        y += _offset.Y;

        _textPaint.TextSize = fontSize * AndroidFontScaleFactor; // Scale factor for Android
        _textPaint.Color = ToAndroidColor(textColor);

        // Set typeface based on font family
        var typeface = GetTypeface(fontFamily);
        _textPaint.SetTypeface(typeface);

        // Android draws text from baseline, adjust y position
        var metrics = _textPaint.GetFontMetrics();
        float baselineOffset = metrics != null ? -metrics.Top : fontSize;

        _canvas.DrawText(text, x, y + baselineOffset, _textPaint);
    }

    public Size MeasureText(string text, FontFamily fontFamily, int fontSize)
    {
        if (string.IsNullOrWhiteSpace(text) || fontSize <= 0)
            return Size.Empty;

        _textPaint.TextSize = fontSize * AndroidFontScaleFactor;
        var typeface = GetTypeface(fontFamily);
        _textPaint.SetTypeface(typeface);

        float width = _textPaint.MeasureText(text);
        var metrics = _textPaint.GetFontMetrics();
        float height = metrics != null ? metrics.Bottom - metrics.Top : fontSize;

        return new Size((int)width, (int)height);
    }

    public void DrawImage(Image<Rgba32> image, Rectangle rect)
    {
        int width = image.Width;
        int height = image.Height;
        var cacheKey = new ImageCacheKey(image);

        Bitmap? bitmap;
        lock (CacheLock)
        {
            // Check cache with proper locking
            if (BitmapCache.TryGetValue(cacheKey, out bitmap) && !bitmap.IsRecycled)
            {
                // Move to end of LRU list (mark as recently used)
                BitmapCacheOrder.Remove(cacheKey);
                BitmapCacheOrder.AddLast(cacheKey);
            }
            else
            {
                // Create new bitmap outside of lock scope concern - but keep lock
                // to ensure cache consistency
                bitmap = ConvertToBitmap(image, width, height);
                if (bitmap == null)
                    return;

                // Remove old entry if it exists (recycled bitmap case)
                if (BitmapCache.ContainsKey(cacheKey))
                {
                    BitmapCache.Remove(cacheKey);
                    BitmapCacheOrder.Remove(cacheKey);
                }

                // Add to cache
                BitmapCache[cacheKey] = bitmap;
                BitmapCacheOrder.AddLast(cacheKey);

                // Evict oldest entries if cache is full
                while (BitmapCache.Count > MaxBitmapCacheSize && BitmapCacheOrder.First != null)
                {
                    var oldKey = BitmapCacheOrder.First.Value;
                    BitmapCacheOrder.RemoveFirst();

                    if (BitmapCache.TryGetValue(oldKey, out var oldBitmap))
                    {
                        BitmapCache.Remove(oldKey);
                        oldBitmap.Recycle();
                    }
                }
            }
        }

        var srcRect = new Rect(0, 0, width, height);
        var dstRect = new Rect(
            rect.X + _offset.X,
            rect.Y + _offset.Y,
            rect.X + _offset.X + rect.Width,
            rect.Y + _offset.Y + rect.Height
        );

        _canvas.DrawBitmap(bitmap, srcRect, dstRect, _paint);
    }

    private static Bitmap? ConvertToBitmap(Image<Rgba32> image, int width, int height)
    {
        var bitmap = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888!);
        if (bitmap == null)
            return null;

        var pixels = new int[width * height];
        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < height; y++)
            {
                var row = accessor.GetRowSpan(y);
                for (int x = 0; x < width; x++)
                {
                    var pixel = row[x];
                    pixels[y * width + x] = Color.Argb(pixel.A, pixel.R, pixel.G, pixel.B);
                }
            }
        });
        bitmap.SetPixels(pixels, 0, width, 0, 0, width, height);

        return bitmap;
    }

    public void SetOffset(System.Drawing.Point offset)
    {
        _offset = offset;
    }

    public System.Drawing.Point GetOffset() => _offset;

    public void SetClip(ClipState state)
    {
        // Restore previous clip state if we had one
        if (_clipSaveStack.Count > 0)
        {
            _canvas.RestoreToCount(_clipSaveStack.Pop());
        }

        _clipState = state;

        // Apply new clip if not empty
        if (state.Bounds.Width > 0 && state.Bounds.Height > 0)
        {
            // Save canvas state before applying clip
            _clipSaveStack.Push(_canvas.Save());

            var clipRect = new RectF(
                state.Bounds.X + _offset.X,
                state.Bounds.Y + _offset.Y,
                state.Bounds.X + _offset.X + state.Bounds.Width,
                state.Bounds.Y + _offset.Y + state.Bounds.Height
            );

            if (state.CornerRadius > 0)
            {
                var path = new Path();
                path.AddRoundRect(clipRect, state.CornerRadius, state.CornerRadius, Path.Direction.Cw!);
                _canvas.ClipPath(path);
            }
            else
            {
                _canvas.ClipRect(clipRect);
            }
        }
    }

    public ClipState GetClipState() => _clipState;

    public void Dispose()
    {
        // Restore all saved canvas states
        while (_clipSaveStack.Count > 0)
        {
            _canvas.RestoreToCount(_clipSaveStack.Pop());
        }

        _paint.Dispose();
        _textPaint.Dispose();
    }

    /// <summary>
    /// Clears the static bitmap cache. Call this when memory is low or the app is paused.
    /// </summary>
    public static void ClearBitmapCache()
    {
        lock (CacheLock)
        {
            foreach (var bitmap in BitmapCache.Values)
            {
                bitmap.Recycle();
            }

            BitmapCache.Clear();
            BitmapCacheOrder.Clear();
        }
    }

    private static Color ToAndroidColor(ColorRgba color)
    {
        return Color.Argb(color.AByte, color.RByte, color.GByte, color.BByte);
    }

    private static Typeface? GetTypeface(FontFamily fontFamily)
    {
        return fontFamily switch
        {
            FontFamily.Default => Typeface.SansSerif,
            FontFamily.DejaVuSans => Typeface.SansSerif,
            FontFamily.Corbel => Typeface.SansSerif,
            _ => Typeface.Default,
        };
    }
}
