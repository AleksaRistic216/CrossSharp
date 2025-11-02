using System.Runtime.InteropServices;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace CrossSharp.Utils.Drawing;

class SDLGraphics : IGraphics
{
    static SDLGraphics()
    {
        TTF_Init();
    }

    readonly ITheme _theme;

    const int FONT_SCALE = 2; // This scale is used to improve text rendering quality by loading font at higher size and scaling down
    IntPtr _renderer;
    IFontFamilyMap _fontFamilyMap = Services.GetSingleton<IFontFamilyMap>();

    int _offsetX;
    int _offsetY;
    int _clipRoundedCornerRadius;

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_CreateRGBSurfaceWithFormatFrom(
        IntPtr pixels,
        int width,
        int height,
        int depth,
        int pitch,
        SDLPixelFormat format
    );

    //  This doesn't exist in SDL2
    // [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    // static extern IntPtr SDL_CreateSurfaceFrom(
    //     int width,
    //     int height,
    //     SDLPixelFormat format,
    //     IntPtr pixels,
    //     int pitch
    // );

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderDrawPoint(IntPtr renderer, int x, int y);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderClear(IntPtr renderer);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderSetClipRect(IntPtr renderer, ref SDLRect rect);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderFillRect(IntPtr renderer, ref SDLRect rect);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderDrawRect(IntPtr renderer, ref SDLRect rect);

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int TTF_Init();

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr TTF_OpenFont(string file, int ptsize);

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr TTF_CloseFont(IntPtr font);

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr TTF_RenderUTF8_Blended(IntPtr font, string text, SDLColor color);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_QueryTexture(
        IntPtr texture,
        IntPtr format,
        IntPtr access,
        out int w,
        out int h
    );

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderCopy(
        IntPtr renderer,
        IntPtr texture,
        IntPtr srcRect,
        ref SDLRect dstRect
    );

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SDL_SetTextureBlendMode(IntPtr texture, SDLBlendMode blendMode);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderCopy(
        IntPtr renderer,
        IntPtr texture,
        IntPtr srcRect,
        IntPtr dstRect
    );

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern void SDL_FreeSurface(IntPtr surface);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern void SDL_DestroyTexture(IntPtr texture);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_CreateTexture(
        IntPtr renderer,
        uint format,
        int access,
        int w,
        int h
    );

    public SDLGraphics(IntPtr renderer)
    {
        _renderer = renderer;
        _theme = Services.GetSingleton<ITheme>();
    }

    public void Render()
    {
        SDLHelpers.SDL_RenderPresent(_renderer);
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
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (width <= 0 || height <= 0 || borderWidth <= 0 || borderColor.A == 0)
            return;

        if (_clipRoundedCornerRadius <= 0)
        {
            DrawRectangleWithoutMask(x, y, width, height, borderColor, borderWidth);
            return;
        }

        // Ensure border width is at least as large as corner radius to avoid artifacts
        // if (borderWidth > 1 && borderWidth < roundedCornersRadius)
        //     borderWidth = roundedCornersRadius;

        x += _offsetX;
        y += _offsetY;

        // Step 1: Create transparent target texture for border
        IntPtr targetTexture = SDL_CreateTexture(
            _renderer,
            (uint)SDLPixelFormat.ABGR8888,
            (int)SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, targetTexture);
        SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 0); // Fully transparent
        SDL_RenderClear(_renderer);
        SDL_SetRenderDrawColor(
            _renderer,
            borderColor.RByte,
            borderColor.GByte,
            borderColor.BByte,
            borderColor.AByte
        );

        if (roundedCornersRadius > 0)
            DrawRoundedRectBorder(0, 0, width, height, _clipRoundedCornerRadius, borderWidth);

        // Step 2: Create rounded mask texture
        IntPtr maskTexture = SDL_CreateTexture(
            _renderer,
            (uint)SDLPixelFormat.ABGR8888,
            (int)SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, maskTexture);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 0); // Transparent clear
        SDL_RenderClear(_renderer);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 255); // Opaque mask
        FillRoundedRectMask(0, 0, width, height, _clipRoundedCornerRadius);

        // Step 3: Composite masked border
        SDL_SetRenderTarget(_renderer, IntPtr.Zero);
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);

        var dstRect = new SDLRect
        {
            x = x,
            y = y,
            w = width,
            h = height,
        };

        SDL_RenderCopy(_renderer, maskTexture, IntPtr.Zero, ref dstRect);
        SDL_RenderCopy(_renderer, targetTexture, IntPtr.Zero, ref dstRect);

        SDL_DestroyTexture(maskTexture);
        SDL_DestroyTexture(targetTexture);
    }

    void DrawRectangleWithoutMask(
        int x,
        int y,
        int width,
        int height,
        ColorRgba borderColor,
        float borderWidth
    )
    {
        x += _offsetX;
        y += _offsetY;
        SDL_SetRenderDrawColor(
            _renderer,
            borderColor.RByte,
            borderColor.GByte,
            borderColor.BByte,
            borderColor.AByte
        );
        for (int i = 0; i < (int)borderWidth; i++)
        {
            var rect = new SDLRect
            {
                x = x + i,
                y = y + i,
                w = width - 2 * i,
                h = height - 2 * i,
            };
            SDL_RenderDrawRect(_renderer, ref rect);
        }
    }

    void DrawQuarterCircleArc(int cx, int cy, int radius, Corner corner)
    {
        if (radius <= 0)
            return;

        double step = Math.PI / (2 * radius);

        for (double angle = 0; angle <= Math.PI / 2; angle += step)
        {
            int x = (int)(radius * Math.Cos(angle));
            int y = (int)(radius * Math.Sin(angle));

            int drawX = corner switch
            {
                Corner.TopLeft => cx - x,
                Corner.TopRight => cx + x,
                Corner.BottomLeft => cx - x,
                Corner.BottomRight => cx + x,
                _ => cx,
            };

            int drawY = corner switch
            {
                Corner.TopLeft => cy - y,
                Corner.TopRight => cy - y,
                Corner.BottomLeft => cy + y,
                Corner.BottomRight => cy + y,
                _ => cy,
            };

            SDL_RenderDrawPoint(_renderer, drawX, drawY);
        }
    }

    void DrawRoundedRectBorder(int x, int y, int width, int height, int radius, int borderWidth)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (width <= 0 || height <= 0 || borderWidth <= 0 || radius < 0)
            return;
        int layers = Math.Max(1, borderWidth);
        for (int i = 0; i < layers; i++)
        {
            int inset = i;
            int r = Math.Max(0, radius - inset);
            int w = width - 2 * inset;
            int h = height - 2 * inset;
            int px = x + inset;
            int py = y + inset;

            // Straight edges
            SDL_RenderDrawLine(_renderer, px + r, py, px + w - r - 1, py); // Top
            SDL_RenderDrawLine(_renderer, px + r, py + h - 1, px + w - r - 1, py + h - 1); // Bottom
            SDL_RenderDrawLine(_renderer, px, py + r, px, py + h - r - 1); // Left
            SDL_RenderDrawLine(_renderer, px + w - 1, py + r, px + w - 1, py + h - r - 1); // Right

            if (borderWidth == 1)
            {
                DrawQuarterCircleArc(px + r, py + r, r, Corner.TopLeft);
                DrawQuarterCircleArc(px + w - r - 1, py + r, r, Corner.TopRight);
                DrawQuarterCircleArc(px + r, py + h - r - 1, r, Corner.BottomLeft);
                DrawQuarterCircleArc(px + w - r - 1, py + h - r - 1, r, Corner.BottomRight);
            }
            else
            {
                FillQuarterCircle(px + r, py + r, r, Corner.TopLeft, radius - borderWidth);
                FillQuarterCircle(px + w - r - 1, py + r, r, Corner.TopRight, radius - borderWidth);
                FillQuarterCircle(
                    px + r,
                    py + h - r - 1,
                    r,
                    Corner.BottomLeft,
                    radius - borderWidth
                );
                FillQuarterCircle(
                    px + w - r - 1,
                    py + h - r - 1,
                    r,
                    Corner.BottomRight,
                    radius - borderWidth
                );
            }
        }
    }

    void FillQuarterCircle(int cx, int cy, int radius, Corner corner, int skipFirst = 0)
    {
        if (radius <= 0 || skipFirst >= radius)
            return;

        switch (corner)
        {
            case Corner.TopLeft:
            case Corner.TopRight:
            case Corner.BottomLeft:
            case Corner.BottomRight:
                break;
            default:
                return;
        }

        for (int y = 0; y <= radius; y++)
        {
            double x = Math.Sqrt(radius * radius - y * y);
            double innerX = Math.Sqrt(skipFirst * skipFirst - y * y);

            if (y >= skipFirst || innerX < x) // Only draw if outside inner radius
            {
                int drawY = corner switch
                {
                    Corner.TopLeft => cy - y,
                    Corner.TopRight => cy - y,
                    Corner.BottomLeft => cy + y,
                    Corner.BottomRight => cy + y,
                    _ => cy,
                };

                int startX = corner switch
                {
                    Corner.TopLeft => cx - (int)x,
                    Corner.TopRight => cx + (int)innerX,
                    Corner.BottomLeft => cx - (int)x,
                    Corner.BottomRight => cx + (int)innerX,
                    _ => cx,
                };

                int endX = corner switch
                {
                    Corner.TopLeft => cx - (int)innerX,
                    Corner.TopRight => cx + (int)x,
                    Corner.BottomLeft => cx - (int)innerX,
                    Corner.BottomRight => cx + (int)x,
                    _ => cx,
                };

                SDL_RenderDrawLine(_renderer, startX, drawY, endX, drawY);
            }
        }
    }

    #region Fill rectangle
    public void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (_clipRoundedCornerRadius <= 0)
        {
            FillRectangleWithoutMask(x, y, width, height, fillColor);
            return;
        }
        // Step 1: Create target texture for filled rectangle
        IntPtr targetTexture = SDL_CreateTexture(
            _renderer,
            (uint)SDLPixelFormat.ABGR8888,
            (int)SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, targetTexture);
        SDL_SetRenderDrawColor(
            _renderer,
            fillColor.RByte,
            fillColor.GByte,
            fillColor.BByte,
            fillColor.AByte
        );
        SDL_RenderClear(_renderer);

        // Step 2: Create rounded corner mask texture
        IntPtr maskTexture = SDL_CreateTexture(
            _renderer,
            (uint)SDLPixelFormat.ABGR8888,
            (int)SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, maskTexture);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 0); // Transparent background
        SDL_RenderClear(_renderer);
        SDL_SetRenderDrawColor(_renderer, 255, 0, 255, 255); // Opaque mask

        // Fill rounded rectangle mask
        FillRoundedRectMask(x, y, width, height, _clipRoundedCornerRadius);

        // Step 3: Composite filled rectangle with mask
        SDL_SetRenderTarget(_renderer, IntPtr.Zero);
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Mod);
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);

        var dstRect = new SDLRect
        {
            x = x + _offsetX,
            y = y + _offsetY,
            w = width,
            h = height,
        };

        SDL_RenderCopy(_renderer, maskTexture, IntPtr.Zero, ref dstRect);
        SDL_RenderCopy(_renderer, targetTexture, IntPtr.Zero, ref dstRect);

        SDL_DestroyTexture(maskTexture);
        SDL_DestroyTexture(targetTexture);
    }

    void FillRectangleWithoutMask(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (width <= 0 || height <= 0)
            return;
        if (fillColor.A == 0)
            return;
        x += _offsetX;
        y += _offsetY;
        SDL_SetRenderDrawColor(
            _renderer,
            fillColor.RByte,
            fillColor.GByte,
            fillColor.BByte,
            fillColor.AByte
        );
        var rect = new SDLRect
        {
            x = x,
            y = y,
            w = width,
            h = height,
        };
        SDL_RenderFillRect(_renderer, ref rect);
    }

    void FillRoundedRectMask(int x, int y, int w, int h, int r)
    {
        // Fill center rectangle
        SDLRect rect1 = new SDLRect
        {
            x = x + r,
            y = y,
            w = w - 2 * r,
            h = h,
        };
        SDL_RenderFillRect(_renderer, ref rect1);
        SDLRect rect2 = new SDLRect
        {
            x = x,
            y = y + r,
            w = w,
            h = h - 2 * r,
        };
        SDL_RenderFillRect(_renderer, ref rect2);

        // Fill corners as circles
        FillCircle(x + r, y + r, r); // Top-left
        FillCircle(x + w - r - 1, y + r, r); // Top-right
        FillCircle(x + r, y + h - r - 1, r); // Bottom-left
        FillCircle(x + w - r - 1, y + h - r - 1, r); // Bottom-right
    }

    void FillCircle(int cx, int cy, int radius)
    {
        for (int w = 0; w < radius * 2; w++)
        {
            for (int h = 0; h < radius * 2; h++)
            {
                int dx = radius - w;
                int dy = radius - h;
                if ((dx * dx + dy * dy) <= (radius * radius))
                {
                    SDL_RenderDrawPoint(_renderer, cx + dx, cy + dy);
                }
            }
        }
    }
    #endregion

    #region Draw image
    public void DrawImage(Image<Rgba32> image, Rectangle rect)
    {
        // int width = image.Width;
        // int height = image.Height;
        //
        // // Get raw pixel memory
        // if (!image.DangerousTryGetSinglePixelMemory(out var pixelMemory))
        //     throw new InvalidOperationException("Unable to access pixel memory.");
        //
        // // Convert to byte span
        // var byteSpan = MemoryMarshal.AsBytes(pixelMemory.Span);
        // int byteCount = byteSpan.Length;
        //
        // // Allocate unmanaged buffer
        // IntPtr unmanagedBuffer = Marshal.AllocHGlobal(byteCount);
        // Marshal.Copy(byteSpan.ToArray(), 0, unmanagedBuffer, byteCount);
        //
        // // Create SDL surface from raw RGBA32 bytes
        // IntPtr surface = SDL_CreateRGBSurfaceWithFormatFrom(
        //     unmanagedBuffer,
        //     width,
        //     height,
        //     32,
        //     width * 4,
        //     SDLPixelFormat.ABGR8888
        // );
        // if (surface == IntPtr.Zero)
        // {
        //     Marshal.FreeHGlobal(unmanagedBuffer);
        //     throw new InvalidOperationException("Unable to create SDL surface from image.");
        // }
        //
        // // Create texture and render
        // IntPtr texture = SDL_CreateTextureFromSurface(_renderer, surface);
        // SDL_FreeSurface(surface);
        // Marshal.FreeHGlobal(unmanagedBuffer);
        //
        // SDLRect dstRect = new SDLRect
        // {
        //     x = rect.X + _offsetX,
        //     y = rect.Y + _offsetY,
        //     w = rect.Width,
        //     h = rect.Height,
        // };
        // SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref dstRect);
        // SDL_DestroyTexture(texture);
    }
    #endregion

    #region Draw text
    // fontPath, fontSize, pointer to TTF_Font
    static Dictionary<string, Dictionary<int, IntPtr>> _fontCache = new();

    IntPtr GetFont(string fontPath, int fontSize)
    {
        if (!_fontCache.TryGetValue(fontPath, out var fSizeMap))
        {
            fSizeMap = new Dictionary<int, IntPtr>();
            _fontCache[fontPath] = fSizeMap;
        }
        if (!fSizeMap.TryGetValue(fontSize, out var font))
        {
            font = TTF_OpenFont(fontPath, fontSize * FONT_SCALE);
            fSizeMap[fontSize] = font;
        }
        return font;
    }

    public void DrawText(
        string text,
        int x,
        int y,
        FontFamily fontFamily,
        int fontSize,
        ColorRgba textColor
    )
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (string.IsNullOrWhiteSpace(text) || fontSize <= 0 || textColor.A == 0)
            return;

        x += _offsetX;
        y += _offsetY;

        var font = GetFont(_fontFamilyMap.GetFontFamilyPath(fontFamily), fontSize);
        if (font == IntPtr.Zero)
            return;

        var color = new SDLColor
        {
            r = textColor.RByte,
            g = textColor.GByte,
            b = textColor.BByte,
            a = textColor.AByte,
        };

        IntPtr surface = TTF_RenderUTF8_Blended(font, text, color);
        // TTF_CloseFont(font); // I cache fonts, don't close here, do somewhere else (on app level is laziest but for performance should consider something else)
        if (surface == IntPtr.Zero)
            return;

        IntPtr textTexture = SDL_CreateTextureFromSurface(_renderer, surface);
        SDL_FreeSurface(surface);
        if (textTexture == IntPtr.Zero)
            return;

        SDL_QueryTexture(textTexture, IntPtr.Zero, IntPtr.Zero, out int w, out int h);
        int scaledW = w / FONT_SCALE;
        int scaledH = h / FONT_SCALE;

        var dstRect = new SDLRect
        {
            x = x,
            y = y,
            w = scaledW,
            h = scaledH,
        };

        if (_clipRoundedCornerRadius <= 0)
        {
            SDL_SetTextureBlendMode(textTexture, SDLBlendMode.Blend);
            SDL_RenderCopy(_renderer, textTexture, IntPtr.Zero, ref dstRect);
            SDL_DestroyTexture(textTexture);
            return;
        }

        // Step 1: Create transparent target texture
        IntPtr targetTexture = SDL_CreateTexture(
            _renderer,
            (uint)SDLPixelFormat.ABGR8888,
            (int)SDLTextureAccess.Target,
            scaledW,
            scaledH
        );
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, targetTexture);
        SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 0); // Fully transparent
        SDL_RenderClear(_renderer);
        SDL_SetTextureBlendMode(textTexture, SDLBlendMode.Blend);
        SDL_RenderCopy(_renderer, textTexture, IntPtr.Zero, IntPtr.Zero);

        // Step 2: Create rounded mask texture
        IntPtr maskTexture = SDL_CreateTexture(
            _renderer,
            (uint)SDLPixelFormat.ABGR8888,
            (int)SDLTextureAccess.Target,
            scaledW,
            scaledH
        );
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, maskTexture);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 0); // Transparent clear
        SDL_RenderClear(_renderer);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 255); // Opaque mask
        FillRoundedRectMask(0, 0, scaledW, scaledH, _clipRoundedCornerRadius);

        // Step 3: Composite masked text
        SDL_SetRenderTarget(_renderer, IntPtr.Zero);
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);

        SDL_RenderCopy(_renderer, maskTexture, IntPtr.Zero, ref dstRect);
        SDL_RenderCopy(_renderer, targetTexture, IntPtr.Zero, ref dstRect);

        SDL_DestroyTexture(maskTexture);
        SDL_DestroyTexture(targetTexture);
        SDL_DestroyTexture(textTexture);
    }

    #endregion

    public Size MeasureText(string text, FontFamily fontFamily, int fontSize)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));

        var fontPath = _fontFamilyMap.GetFontFamilyPath(fontFamily);
        IntPtr font = GetFont(fontPath, fontSize);
        if (font == IntPtr.Zero)
            return Size.Empty;

        var color = new SDLColor
        {
            r = 0,
            g = 0,
            b = 0,
            a = 1,
        };

        IntPtr surface = TTF_RenderUTF8_Blended(font, text, color);
        // TTF_CloseFont(font); // I cache fonts, don't close here, do somewhere else (on app level is laziest but for performance should consider something else)
        if (surface == IntPtr.Zero)
            return Size.Empty;

        IntPtr texture = SDL_CreateTextureFromSurface(_renderer, surface);
        if (texture == IntPtr.Zero)
        {
            SDL_FreeSurface(surface);
            return Size.Empty;
        }

        SDL_QueryTexture(texture, IntPtr.Zero, IntPtr.Zero, out int w, out int h);
        SDL_FreeSurface(surface);
        SDL_DestroyTexture(texture);
        return new Size(w / FONT_SCALE, h / FONT_SCALE);
    }

    public void SetOffset(int x, int y)
    {
        if (x == _offsetX && y == _offsetY)
            return;
        _offsetX = x;
        _offsetY = y;
    }

    public void ResetOffset()
    {
        _offsetX = 0;
        _offsetY = 0;
    }

    public void SetClip(Rectangle rectangle)
    {
        var rect = new SDLRect
        {
            x = rectangle.X,
            y = rectangle.Y,
            w = rectangle.Width,
            h = rectangle.Height,
        };
        SDL_RenderSetClipRect(_renderer, ref rect);
    }

    public void SetClip(Rectangle rectangle, int roundedCornersRadius)
    {
        SetClip(rectangle);
        _clipRoundedCornerRadius = roundedCornersRadius;
    }

    public void Dispose() { }
}
