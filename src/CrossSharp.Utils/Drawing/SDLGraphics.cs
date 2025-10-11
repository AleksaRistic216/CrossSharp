using System.Drawing;
using System.Runtime.InteropServices;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils.Drawing;

public class SDLGraphics : IGraphics
{
    const int FONT_SCALE = 2; // This scale is used to improve text rendering quality by loading font at higher size and scaling down
    static IntPtr _renderer;
    IFontFamilyMap _fontFamilyMap = Services.GetSingleton<IFontFamilyMap>();

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderClear(IntPtr renderer);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderSetClipRect(IntPtr renderer, ref SDLRect rect);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderFillRect(IntPtr renderer, ref SDLRect rect);

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
    static extern void SDL_FreeSurface(IntPtr surface);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern void SDL_DestroyTexture(IntPtr texture);

    public SDLGraphics(IntPtr renderer)
    {
        _renderer = renderer;
    }

    public void ForceRender()
    {
        SDLHelpers.SDL_RenderPresent(_renderer);
    }

    public void DrawRectangle(
        int x,
        int y,
        int width,
        int height,
        ColorRgba borderColor,
        float borderWidth
    )
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (width <= 0 || height <= 0)
            return;
        x += offsetX;
        y += offsetY;
        SDL_SetRenderDrawColor(
            _renderer,
            borderColor.RByte,
            borderColor.GByte,
            borderColor.BByte,
            borderColor.AByte
        );
        var rect = new SDLRect
        {
            x = x,
            y = y,
            w = width,
            h = height,
        };
        SDL_RenderDrawRect(_renderer, ref rect);
    }

    public void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        if (width <= 0 || height <= 0)
            return;
        if (fillColor.A == 0)
            return;
        x += offsetX;
        y += offsetY;
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
        x += offsetX;
        y += offsetY;
        TTF_Init();

        var fontPath = _fontFamilyMap.GetFontFamilyPath(fontFamily);
        IntPtr font = TTF_OpenFont(fontPath, fontSize * FONT_SCALE);
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
        TTF_CloseFont(font);
        if (surface == IntPtr.Zero)
            return;

        IntPtr texture = SDL_CreateTextureFromSurface(_renderer, surface);
        if (texture == IntPtr.Zero)
        {
            SDL_FreeSurface(surface);
            return;
        }

        SDL_QueryTexture(texture, IntPtr.Zero, IntPtr.Zero, out int w, out int h);
        var dstRect = new SDLRect
        {
            x = x,
            y = y,
            w = w / FONT_SCALE,
            h = h / FONT_SCALE,
        };

        SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref dstRect);

        SDL_FreeSurface(surface);
        SDL_DestroyTexture(texture);
    }

    public Size MeasureText(string text, FontFamily fontFamily, int fontSize)
    {
        if (_renderer == IntPtr.Zero)
            throw new NullReferenceException(nameof(_renderer));
        TTF_Init();

        var fontPath = _fontFamilyMap.GetFontFamilyPath(fontFamily);
        IntPtr font = TTF_OpenFont(fontPath, fontSize * FONT_SCALE);
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
        return new Size(w / FONT_SCALE, h / FONT_SCALE);
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

    int offsetX = 0;
    int offsetY = 0;

    public void SetOffset(int x, int y)
    {
        if (x == offsetX && y == offsetY)
            return;
        offsetX = x;
        offsetY = y;
    }

    public void ResetOffset()
    {
        offsetX = 0;
        offsetY = 0;
    }

    public void SetClip(Rectangle rectangle, int roundedCornersRadius) { }

    public void Dispose()
    {
        _renderer = IntPtr.Zero;
    }
}
