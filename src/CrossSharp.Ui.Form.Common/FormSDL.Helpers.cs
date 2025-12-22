using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

partial class FormSDL
{
    static IntPtr CreateWindow(string title, int width, int height) => CreateWindowCore(title, width, height); // Idea is to somehow handle creation from different thread

    static IntPtr CreateWindowCore(string title, int width, int height)
    {
        var appConfig = Services.GetSingleton<IApplicationConfiguration>();
        ulong flags = SDLWindowFlags.HIDDEN | SDLWindowFlags.RESIZABLE;
        if (appConfig.HighDpi)
            flags |= SDLWindowFlags.HIGH_PIXEL_DENSITY;

        if (Services.GetSingleton<IApplicationConfiguration>().FormsStyle is FormStyle.CrossSharp)
            flags |= SDLWindowFlags.BORDERLESS;

        // Set to OPENGL, later can be changed to VULKAN or METAL or DIRECT3D based on configuration and platform
        flags |= SDLWindowFlags.OPENGL;

        // SDL3: window position is set after creation
        var window = SDLHelpers.SDL_CreateWindow(title, width, height, flags);
        if (window != IntPtr.Zero)
            SDLHelpers.SDL_SetWindowPosition(window, SDLWindowPosition.CENTERED, SDLWindowPosition.CENTERED);
        return window;
    }

    void DestroyWindow()
    {
        if (Handle == IntPtr.Zero)
            return;
        SDLHelpers.SDL_DestroyRenderer(Renderer);
        SDLHelpers.SDL_DestroyWindow(Handle);
    }

    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
}
