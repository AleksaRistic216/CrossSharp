using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

static class SDLHelpers
{
#if WINDOWS
    internal const string LIB = "runtimes/win-x64/native/SDL2.dll";
    internal const string TTF_LIB = "runtimes/win-x64/native/SDL2_ttf.dll";
#else
    internal const string LIB = "runtimes/linux-x64/native/libSDL2-2.0.so.0";
    internal const string TTF_LIB = "runtimes/linux-x64/native/libSDL2_ttf-2.0.so.0";
#endif

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SDL_Init(uint flags);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_GetError();

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_CreateWindow(string title, int x, int y, int w, int h, uint flags);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_SetWindowTitle(IntPtr window, string title);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_SetWindowPosition(IntPtr window, int x, int y);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_ShowWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_GetWindowPosition(IntPtr window, out int x, out int y);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_GetWindowSize(IntPtr window, out int w, out int h);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_RenderPresent(IntPtr renderer);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SDL_UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_DestroyWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_MinimizeWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_MaximizeWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_RestoreWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_DestroyRenderer(IntPtr renderer);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_GetWindowWMInfo(IntPtr window, ref SDL_SysWMinfo info);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint SDL_GetWindowFlags(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint SDL_GetWindowID(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_Quit();

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_PollEvent(out SDL_Event sdlEvent);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_Delay(uint ms);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_CreateRenderer(IntPtr window, int index, SDLRenderFlags flags);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern int SDL_GetRendererInfo(IntPtr renderer, ref SDLRendererInfo info);

    internal const uint SDL_INIT_VIDEO = 0x00000020;
}
