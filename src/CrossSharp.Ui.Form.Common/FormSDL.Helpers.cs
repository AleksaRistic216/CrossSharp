using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

partial class FormSDL
{
    static IntPtr CreateWindow(string title, int width, int height)
    {
        return CreateWindowCore(title, width, height); // Idea is to somehow handle creation from different thread
    }

    static IntPtr CreateWindowCore(string title, int width, int height)
    {
        var appConfig = Services.GetSingleton<IApplicationConfiguration>();
        uint flags = SDLWindowFlags.HIDDEN | SDLWindowFlags.RESIZABLE;
        if (appConfig.HighDpi)
            flags |= SDLWindowFlags.ALLOW_HIGHDPI;

        // Set to OPENGL, later can be changed to VULKAN or METAL or DIRECT3D based on configuration and platform
        flags |= SDLWindowFlags.OPENGL;

        var window = SDLHelpers.SDL_CreateWindow(
            title,
            SDLWindowPosition.CENTERED,
            SDLWindowPosition.CENTERED,
            width,
            height,
            flags
        );
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
