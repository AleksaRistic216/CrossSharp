using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

/// <summary>
/// SDL hit test result values for SDL_SetWindowHitTest callback.
/// </summary>
enum SDLHitTestResult
{
    SDL_HITTEST_NORMAL = 0,
    SDL_HITTEST_DRAGGABLE = 1,
    SDL_HITTEST_RESIZE_TOPLEFT = 2,
    SDL_HITTEST_RESIZE_TOP = 3,
    SDL_HITTEST_RESIZE_TOPRIGHT = 4,
    SDL_HITTEST_RESIZE_RIGHT = 5,
    SDL_HITTEST_RESIZE_BOTTOMRIGHT = 6,
    SDL_HITTEST_RESIZE_BOTTOM = 7,
    SDL_HITTEST_RESIZE_BOTTOMLEFT = 8,
    SDL_HITTEST_RESIZE_LEFT = 9,
}

/// <summary>
/// SDL point structure for hit test callback.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
struct SDL_Point
{
    public int x;
    public int y;
}

/// <summary>
/// Delegate for SDL_SetWindowHitTest callback.
/// </summary>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
delegate SDLHitTestResult SDL_HitTest(IntPtr window, ref SDL_Point area, IntPtr data);

static class SDLHelpers
{
#if WINDOWS
    internal const string LIB = "runtimes/win-x64/native/SDL3.dll";
    internal const string TTF_LIB = "runtimes/win-x64/native/SDL3_ttf.dll";
#else
    internal const string LIB = "runtimes/linux-x64/native/libSDL3.so.0";
    internal const string TTF_LIB = "runtimes/linux-x64/native/libSDL3_ttf.so.0";
#endif

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_Init(SDLInitFlags flags);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_GetError();

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_CreateWindow(string title, int w, int h, ulong flags);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_SetWindowTitle(IntPtr window, string title);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_SetWindowPosition(IntPtr window, int x, int y);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_ShowWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_GetWindowPosition(IntPtr window, out int x, out int y);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_GetWindowSize(IntPtr window, out int w, out int h);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_RenderPresent(IntPtr renderer);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_DestroyWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_MinimizeWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_MaximizeWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_RestoreWindow(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_DestroyRenderer(IntPtr renderer);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern ulong SDL_GetWindowFlags(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern uint SDL_GetWindowID(IntPtr window);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_Quit();

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_PollEvent(out SDL_Event sdlEvent);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern void SDL_Delay(uint ms);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern IntPtr SDL_CreateRenderer(IntPtr window, string? name);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_SetRenderVSync(IntPtr renderer, int vsync);

    [DllImport(LIB, CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_SetWindowHitTest(IntPtr window, SDL_HitTest? callback, IntPtr callbackData);
}
