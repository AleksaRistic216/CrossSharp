using System.Reflection.PortableExecutable;
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

    const int FONT_SCALE = 2; // This scale is used to improve text rendering quality by loading font at higher size and scaling down
    IntPtr _renderer;
    IFontFamilyMap _fontFamilyMap = Services.GetSingleton<IFontFamilyMap>();
    ClipState _clipState = ClipState.Empty;
    System.Drawing.Point _offset = System.Drawing.Point.Empty;

    // SDL3: SDL_CreateSurfaceFrom replaces SDL_CreateRGBSurfaceWithFormatFrom with different parameter order
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_CreateSurfaceFrom(
        int width,
        int height,
        SDLPixelFormat format,
        IntPtr pixels,
        int pitch
    );

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    // SDL3: uses float coordinates
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderPoint(IntPtr renderer, float x, float y);

    // SDL3: uses float coordinates
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderLine(IntPtr renderer, float x1, float y1, float x2, float y2);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderClear(IntPtr renderer);

    // SDL3: renamed from SDL_RenderSetClipRect
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_SetRenderClipRect(IntPtr renderer, ref SDLRect rect);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_SetRenderClipRect(IntPtr renderer, IntPtr rect);

    // SDL3: uses SDL_FRect
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderFillRect(IntPtr renderer, ref SDLFRect rect);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_SetRenderTarget(IntPtr renderer, IntPtr texture);

    // SDL3: uses SDL_FRect
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderRect(IntPtr renderer, ref SDLFRect rect);

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool TTF_Init();

    // SDL3_ttf: ptsize is now float
    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr TTF_OpenFont(string file, float ptsize);

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern void TTF_CloseFont(IntPtr font);

    // SDL3_ttf: TTF_RenderText_Blended replaces TTF_RenderUTF8_Blended, with length parameter
    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr TTF_RenderText_Blended(IntPtr font, string text, nuint length, SDLColor color);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_GetTextureSize(IntPtr texture, out float w, out float h);

    // SDL3: SDL_RenderTexture replaces SDL_RenderCopy, uses SDL_FRect
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderTexture(IntPtr renderer, IntPtr texture, IntPtr srcRect, ref SDLFRect dstRect);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_SetTextureBlendMode(IntPtr texture, SDLBlendMode blendMode);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_SetRenderDrawBlendMode(IntPtr renderer, SDLBlendMode blendMode);

    // SDL3: SDL_RenderTexture replaces SDL_RenderCopy, uses SDL_FRect
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern bool SDL_RenderTexture(IntPtr renderer, IntPtr texture, IntPtr srcRect, IntPtr dstRect);

    // SDL3: SDL_DestroySurface replaces SDL_FreeSurface
    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern void SDL_DestroySurface(IntPtr surface);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern void SDL_DestroyTexture(IntPtr texture);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_CreateTexture(IntPtr renderer, SDLPixelFormat format, SDLTextureAccess access, int w, int h);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr SDL_GetRenderTarget(IntPtr renderer);

    public SDLGraphics(IntPtr renderer)
    {
        _renderer = renderer;
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

        if (_clipState.CornerRadius <= 0)
        {
            DrawRectangleWithoutMask(x, y, width, height, borderColor, borderWidth);
            return;
        }

        x += _offset.X;
        y += _offset.Y;

        // Step 1: Create transparent target texture for border
        IntPtr targetTexture = SDL_CreateTexture(
            _renderer,
            SDLPixelFormat.ABGR8888,
            SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, targetTexture);
        SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 0); // Fully transparent
        SDL_RenderClear(_renderer);

        if (roundedCornersRadius > 0)
            DrawRoundedRectBorder(0, 0, width, height, _clipState.CornerRadius, borderWidth, borderColor);

        // Step 2: Create rounded mask texture
        IntPtr maskTexture = SDL_CreateTexture(
            _renderer,
            SDLPixelFormat.ABGR8888,
            SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, maskTexture);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 0); // Transparent clear
        SDL_RenderClear(_renderer);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 255); // Opaque mask

        // Step 3: Composite masked border
        SDL_SetRenderTarget(_renderer, IntPtr.Zero);
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);

        var dstRect = new SDLFRect
        {
            x = x,
            y = y,
            w = width,
            h = height,
        };

        SDL_RenderTexture(_renderer, maskTexture, IntPtr.Zero, ref dstRect);
        SDL_RenderTexture(_renderer, targetTexture, IntPtr.Zero, ref dstRect);

        SDL_DestroyTexture(maskTexture);
        SDL_DestroyTexture(targetTexture);
    }

    void DrawRectangleWithoutMask(int x, int y, int width, int height, ColorRgba borderColor, float borderWidth)
    {
        x += _offset.X;
        y += _offset.Y;
        SDL_SetRenderDrawColor(_renderer, borderColor.RByte, borderColor.GByte, borderColor.BByte, borderColor.AByte);
        for (int i = 0; i < (int)borderWidth; i++)
        {
            var rect = new SDLFRect
            {
                x = x + i,
                y = y + i,
                w = width - 2 * i,
                h = height - 2 * i,
            };
            SDL_RenderRect(_renderer, ref rect);
        }
    }

    void DrawRoundedRectBorder(int x, int y, int width, int height, int radius, int borderWidth, ColorRgba borderColor)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (width <= 0 || height <= 0 || borderWidth <= 0 || radius < 0)
            return;
        SDL_SetRenderDrawColor(_renderer, borderColor.RByte, borderColor.GByte, borderColor.BByte, borderColor.AByte);
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
            SDL_RenderLine(_renderer, px + r, py, px + w - r - 1, py); // Top
            SDL_RenderLine(_renderer, px + r, py + h - 1, px + w - r - 1, py + h - 1); // Bottom
            SDL_RenderLine(_renderer, px, py + r, px, py + h - r - 1); // Left
            SDL_RenderLine(_renderer, px + w - 1, py + r, px + w - 1, py + h - r - 1); // Right
            FillQuarterCircle(px + r - 1, py + r - 1, r, Corner.TopLeft, borderColor, radius - borderWidth);
            FillQuarterCircle(px + w - r, py + r + 1, r, Corner.TopRight, borderColor, radius - borderWidth);
            FillQuarterCircle(px + r - 1, py + h - r, r, Corner.BottomLeft, borderColor, radius - borderWidth);
            FillQuarterCircle(px + w - r, py + h - r, r, Corner.BottomRight, borderColor, radius - borderWidth);
        }
    }

    enum FillQuarterCirclePointType
    {
        Solid,
        SeeThrough1,
        SeeThrough2,
    }

    // key: (Corner, radius, skipFirst)
    static readonly Dictionary<
        (Corner corner, int radius, int skipFirst),
        Dictionary<FillQuarterCirclePointType, List<Rectangle>>
    > _quarterCircleCache = new();

    void FillQuarterCircle(int cx, int cy, int radius, Corner corner, ColorRgba borderColor, int skipFirst = 0)
    {
        if (radius <= 0 || skipFirst >= radius)
            return;

        var seeThroughColor1 = new ColorRgba(borderColor.R, borderColor.G, borderColor.B, borderColor.A * 0.3f);
        var seeThroughColor2 = new ColorRgba(borderColor.R, borderColor.G, borderColor.B, borderColor.A * 0.15f);

        var key = (corner, radius, skipFirst);
        if (!_quarterCircleCache.TryGetValue(key, out var pixelsToDraw))
        {
            pixelsToDraw = new Dictionary<FillQuarterCirclePointType, List<Rectangle>>
            {
                [FillQuarterCirclePointType.Solid] = new List<Rectangle>(),
                [FillQuarterCirclePointType.SeeThrough1] = new List<Rectangle>(),
                [FillQuarterCirclePointType.SeeThrough2] = new List<Rectangle>(),
            };

            // build rectangles in a local coordinate system with center at (0,0)
            for (int y = 0; y <= radius; y++)
            {
                int ySq = y * y;

                int outerX = (int)Math.Floor(Math.Sqrt(radius * radius - ySq));
                int innerX =
                    (skipFirst > 0 && ySq < skipFirst * skipFirst)
                        ? (int)Math.Ceiling(Math.Sqrt(skipFirst * skipFirst - ySq))
                        : 0;

                int localY = corner switch
                {
                    Corner.TopLeft => -y,
                    Corner.TopRight => -y,
                    Corner.BottomLeft => y,
                    Corner.BottomRight => y,
                    _ => 0,
                };

                int startLocalX = corner switch
                {
                    Corner.TopLeft => -outerX,
                    Corner.TopRight => innerX,
                    Corner.BottomLeft => -outerX,
                    Corner.BottomRight => innerX,
                    _ => 0,
                };

                int endLocalX = corner switch
                {
                    Corner.TopLeft => -innerX,
                    Corner.TopRight => outerX,
                    Corner.BottomLeft => -innerX,
                    Corner.BottomRight => outerX,
                    _ => 0,
                };

                if (startLocalX > endLocalX)
                    continue;

                pixelsToDraw[FillQuarterCirclePointType.Solid]
                    .Add(new Rectangle(startLocalX, localY, endLocalX - startLocalX + 1, 1));

                pixelsToDraw[FillQuarterCirclePointType.SeeThrough1]
                    .Add(
                        new Rectangle(
                            corner switch
                            {
                                Corner.TopLeft => startLocalX - 1,
                                Corner.TopRight => endLocalX + 1,
                                Corner.BottomLeft => startLocalX - 1,
                                Corner.BottomRight => endLocalX + 1,
                                _ => startLocalX,
                            },
                            localY,
                            1,
                            1
                        )
                    );

                pixelsToDraw[FillQuarterCirclePointType.SeeThrough2]
                    .Add(
                        new Rectangle(
                            corner switch
                            {
                                Corner.TopLeft => startLocalX - 2,
                                Corner.TopRight => endLocalX + 2,
                                Corner.BottomLeft => startLocalX - 2,
                                Corner.BottomRight => endLocalX + 2,
                                _ => startLocalX,
                            },
                            localY,
                            1,
                            1
                        )
                    );
            }

            _quarterCircleCache[key] = pixelsToDraw;
        }

        foreach (var solidPixel in pixelsToDraw[FillQuarterCirclePointType.Solid])
        {
            SDL_SetRenderDrawColor(
                _renderer,
                borderColor.RByte,
                borderColor.GByte,
                borderColor.BByte,
                borderColor.AByte
            );
            var rect = new SDLFRect
            {
                x = cx + solidPixel.X,
                y = cy + solidPixel.Y,
                w = solidPixel.Width,
                h = solidPixel.Height,
            };
            SDL_RenderFillRect(_renderer, ref rect);
        }

        foreach (var seeThroughPixel in pixelsToDraw[FillQuarterCirclePointType.SeeThrough1])
        {
            SDL_SetRenderDrawColor(
                _renderer,
                seeThroughColor1.RByte,
                seeThroughColor1.GByte,
                seeThroughColor1.BByte,
                seeThroughColor1.AByte
            );
            var rect = new SDLFRect
            {
                x = cx + seeThroughPixel.X,
                y = cy + seeThroughPixel.Y,
                w = seeThroughPixel.Width,
                h = seeThroughPixel.Height,
            };
            SDL_RenderFillRect(_renderer, ref rect);
        }

        foreach (var seeThroughPixel in pixelsToDraw[FillQuarterCirclePointType.SeeThrough2])
        {
            SDL_SetRenderDrawColor(
                _renderer,
                seeThroughColor2.RByte,
                seeThroughColor2.GByte,
                seeThroughColor2.BByte,
                seeThroughColor2.AByte
            );
            var rect = new SDLFRect
            {
                x = cx + seeThroughPixel.X,
                y = cy + seeThroughPixel.Y,
                w = seeThroughPixel.Width,
                h = seeThroughPixel.Height,
            };
            SDL_RenderFillRect(_renderer, ref rect);
        }
    }

    #region Fill rectangle
    public void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (_clipState.CornerRadius <= 0)
        {
            FillRectangleWithoutMask(x, y, width, height, fillColor);
            return;
        }
        x += _offset.X;
        y += _offset.Y;

        // Step 1: Create transparent target texture for border
        IntPtr targetTexture = SDL_CreateTexture(
            _renderer,
            SDLPixelFormat.ABGR8888,
            SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, targetTexture);
        SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 0); // Fully transparent
        SDL_RenderClear(_renderer);

        // Step 2: Create rounded mask texture
        IntPtr maskTexture = SDL_CreateTexture(
            _renderer,
            SDLPixelFormat.ABGR8888,
            SDLTextureAccess.Target,
            width,
            height
        );
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, maskTexture);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 0); // Transparent clear
        SDL_RenderClear(_renderer);
        FillRoundedRectMask(0, 0, width, height, _clipState.CornerRadius, fillColor);

        // Step 3: Composite masked border
        SDL_SetRenderTarget(_renderer, IntPtr.Zero);
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);

        var dstRect = new SDLFRect
        {
            x = x,
            y = y,
            w = width,
            h = height,
        };

        SDL_RenderTexture(_renderer, maskTexture, IntPtr.Zero, ref dstRect);
        SDL_RenderTexture(_renderer, targetTexture, IntPtr.Zero, ref dstRect);

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
        x += _offset.X;
        y += _offset.Y;
        SDL_SetRenderDrawColor(_renderer, fillColor.RByte, fillColor.GByte, fillColor.BByte, fillColor.AByte);
        var rect = new SDLFRect
        {
            x = x,
            y = y,
            w = width,
            h = height,
        };
        SDL_RenderFillRect(_renderer, ref rect);
    }

    void FillRoundedRectMask(int x, int y, int w, int h, int r, ColorRgba fillColor)
    {
        SDL_SetRenderDrawColor(_renderer, fillColor.RByte, fillColor.GByte, fillColor.BByte, fillColor.AByte);
        // Fill center rectangle
        SDLFRect rect1 = new SDLFRect
        {
            x = x + r,
            y = y,
            w = w - 2 * r,
            h = h,
        };
        SDL_RenderFillRect(_renderer, ref rect1);
        SDLFRect rect2 = new SDLFRect
        {
            x = x,
            y = y + r,
            w = w,
            h = h - 2 * r,
        };
        SDL_RenderFillRect(_renderer, ref rect2);

        FillQuarterCircle(x + r, y + r, r, Corner.TopLeft, fillColor);
        FillQuarterCircle(x + w - r - 1, y + r, r, Corner.TopRight, fillColor);
        FillQuarterCircle(x + r, y + h - r - 1, r, Corner.BottomLeft, fillColor);
        FillQuarterCircle(x + w - r - 1, y + h - r - 1, r, Corner.BottomRight, fillColor);
    }
    #endregion

    #region Draw image
    public void DrawImage(Image<Rgba32> image, Rectangle rect)
    {
        int width = image.Width;
        int height = image.Height;

        // Get raw pixel memory
        if (!image.DangerousTryGetSinglePixelMemory(out var pixelMemory))
            throw new InvalidOperationException("Unable to access pixel memory.");

        // Convert to byte span
        var byteSpan = MemoryMarshal.AsBytes(pixelMemory.Span);
        int byteCount = byteSpan.Length;

        // Allocate unmanaged buffer
        IntPtr unmanagedBuffer = Marshal.AllocHGlobal(byteCount);
        Marshal.Copy(byteSpan.ToArray(), 0, unmanagedBuffer, byteCount);

        // Create SDL surface from raw RGBA32 bytes (SDL3: different parameter order)
        IntPtr surface = SDL_CreateSurfaceFrom(
            width,
            height,
            SDLPixelFormat.ABGR8888,
            unmanagedBuffer,
            width * 4
        );
        if (surface == IntPtr.Zero)
        {
            Marshal.FreeHGlobal(unmanagedBuffer);
            throw new InvalidOperationException("Unable to create SDL surface from image.");
        }

        // Create texture and render
        IntPtr texture = SDL_CreateTextureFromSurface(_renderer, surface);
        SDL_DestroySurface(surface);
        Marshal.FreeHGlobal(unmanagedBuffer);

        SDLFRect dstRect = new SDLFRect
        {
            x = rect.X + _offset.X,
            y = rect.Y + _offset.Y,
            w = rect.Width,
            h = rect.Height,
        };
        SDL_RenderTexture(_renderer, texture, IntPtr.Zero, ref dstRect);
        SDL_DestroyTexture(texture);
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

    public void DrawText(string text, int x, int y, FontFamily fontFamily, int fontSize, ColorRgba textColor)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (string.IsNullOrWhiteSpace(text) || fontSize <= 0 || textColor.A == 0)
            return;

        x += _offset.X;
        y += _offset.Y;

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

        // SDL3_ttf: TTF_RenderText_Blended with length parameter (0 for null-terminated)
        IntPtr surface = TTF_RenderText_Blended(font, text, 0, color);
        // TTF_CloseFont(font); // I cache fonts, don't close here, do somewhere else (on app level is laziest but for performance should consider something else)
        if (surface == IntPtr.Zero)
            return;

        IntPtr textTexture = SDL_CreateTextureFromSurface(_renderer, surface);
        SDL_DestroySurface(surface);
        if (textTexture == IntPtr.Zero)
            return;

        SDL_GetTextureSize(textTexture, out float w, out float h);
        int scaledW = (int)(w / FONT_SCALE);
        int scaledH = (int)(h / FONT_SCALE);

        var dstRect = new SDLFRect
        {
            x = x,
            y = y,
            w = scaledW,
            h = scaledH,
        };

        if (_clipState.CornerRadius <= 0)
        {
            SDL_SetTextureBlendMode(textTexture, SDLBlendMode.Blend);
            SDL_RenderTexture(_renderer, textTexture, IntPtr.Zero, ref dstRect);
            SDL_DestroyTexture(textTexture);
            return;
        }

        // Step 1: Create transparent target texture
        IntPtr targetTexture = SDL_CreateTexture(
            _renderer,
            SDLPixelFormat.ABGR8888,
            SDLTextureAccess.Target,
            scaledW,
            scaledH
        );
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, targetTexture);
        SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 0); // Fully transparent
        SDL_RenderClear(_renderer);
        SDL_SetTextureBlendMode(textTexture, SDLBlendMode.Blend);
        SDL_RenderTexture(_renderer, textTexture, IntPtr.Zero, IntPtr.Zero);

        // Step 2: Create rounded mask texture
        IntPtr maskTexture = SDL_CreateTexture(
            _renderer,
            SDLPixelFormat.ABGR8888,
            SDLTextureAccess.Target,
            scaledW,
            scaledH
        );
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetRenderTarget(_renderer, maskTexture);
        SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 0); // Transparent clear
        SDL_RenderClear(_renderer);
        // SDL_SetRenderDrawColor(_renderer, 255, 255, 255, 255); // Opaque mask
        // FillRoundedRectMask(0, 0, scaledW, scaledH, _clipRoundedCornerRadius);

        // Step 3: Composite masked text
        SDL_SetRenderTarget(_renderer, IntPtr.Zero);
        SDL_SetTextureBlendMode(maskTexture, SDLBlendMode.Blend);
        SDL_SetTextureBlendMode(targetTexture, SDLBlendMode.Blend);

        SDL_RenderTexture(_renderer, maskTexture, IntPtr.Zero, ref dstRect);
        SDL_RenderTexture(_renderer, targetTexture, IntPtr.Zero, ref dstRect);

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

        // SDL3_ttf: TTF_RenderText_Blended with length parameter (0 for null-terminated)
        IntPtr surface = TTF_RenderText_Blended(font, text, 0, color);
        // TTF_CloseFont(font); // I cache fonts, don't close here, do somewhere else (on app level is laziest but for performance should consider something else)
        if (surface == IntPtr.Zero)
            return Size.Empty;

        IntPtr texture = SDL_CreateTextureFromSurface(_renderer, surface);
        if (texture == IntPtr.Zero)
        {
            SDL_DestroySurface(surface);
            return Size.Empty;
        }

        SDL_GetTextureSize(texture, out float w, out float h);
        SDL_DestroySurface(surface);
        SDL_DestroyTexture(texture);
        return new Size((int)(w / FONT_SCALE), (int)(h / FONT_SCALE));
    }

    public void SetOffset(System.Drawing.Point offset)
    {
        _offset = offset;
    }

    public System.Drawing.Point GetOffset() => _offset;

    public void SetClip(ClipState state)
    {
        _clipState = state;
        var rect = new SDLRect
        {
            x = state.Bounds.X,
            y = state.Bounds.Y,
            w = state.Bounds.Width,
            h = state.Bounds.Height,
        };
        SDL_SetRenderClipRect(_renderer, ref rect);
    }

    public ClipState GetClipState() => _clipState;

    public void Dispose() { }
}
