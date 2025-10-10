using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Linux;

partial class FormSDL
{
    static IntPtr CreateWindow()
    {
        var appConfig = Services.GetSingleton<IApplicationConfiguration>();
        var flags = SDLWindowFlags.SHOWN | SDLWindowFlags.RESIZABLE;
        if (appConfig.HighDpi)
            flags |= SDLWindowFlags.ALLOW_HIGHDPI;

        // Set to OPENGL, later can be changed to VULKAN or METAL or DIRECT3D based on configuration and platform
        flags |= SDLWindowFlags.OPENGL;

        var window = SDLHelpers.SDL_CreateWindow(
            "SDL2 Window",
            SDLWindowPosition.CENTERED,
            SDLWindowPosition.CENTERED,
            800,
            600,
            flags
        );
        return window;
    }
}
