using System.Drawing;
using System.Runtime.InteropServices;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Utils.Drawing;

public class SDLGraphics : IGraphics
{
    IntPtr _renderer;
    IFontFamilyMap _fontFamilyMap = Services.GetSingleton<IFontFamilyMap>();

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderClear(IntPtr renderer);

    [DllImport(SDLHelpers.LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int SDL_RenderFillRect(IntPtr renderer, ref SDLRect rect);

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern int TTF_Init();

    [DllImport(SDLHelpers.TTF_LIB, CallingConvention = CallingConvention.Cdecl)]
    static extern IntPtr TTF_OpenFont(string file, int ptsize);

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

    // Set draw color to red
    public SDLGraphics(IntPtr renderer)
    {
        _renderer = renderer;
    }

    public void DrawRectangle(
        int x,
        int y,
        int width,
        int height,
        ColorRgba borderColor,
        float borderWidth
    ) { }

    public void FillRectangle(int x, int y, int width, int height, ColorRgba fillColor)
    {
        if (width <= 0 || height <= 0)
            return;
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
        float fontSize,
        ColorRgba textColor
    )
    {
        TTF_Init();

        var fontPath = _fontFamilyMap.GetFontFamilyPath(fontFamily);
        IntPtr font = TTF_OpenFont(fontPath, (int)fontSize);
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
            w = w,
            h = h,
        };

        SDL_RenderCopy(_renderer, texture, IntPtr.Zero, ref dstRect);

        SDL_FreeSurface(surface);
        SDL_DestroyTexture(texture);
    }

    public void SetClip(Rectangle rectangle) { }

    public void ResetOffset() { }

    public void SetOffset(int locationX, int locationY) { }

    public void SetClip(Rectangle rectangle, int roundedCornersRadius) { }

    public void Dispose() { }
}
