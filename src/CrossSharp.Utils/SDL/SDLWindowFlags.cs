namespace CrossSharp.Utils.SDL;

static class SDLWindowFlags
{
    internal const uint FULLSCREEN = 0x00000001;
    internal const uint OPENGL = 0x00000002;
    internal const uint SHOWN = 0x00000004;
    internal const uint HIDDEN = 0x00000008;
    internal const uint BORDERLESS = 0x00000010;
    internal const uint RESIZABLE = 0x00000020;
    internal const uint MINIMIZED = 0x00000040;
    internal const uint MAXIMIZED = 0x00000080;
    internal const uint INPUT_GRABBED = 0x00000100;
    internal const uint INPUT_FOCUS = 0x00000200;
    internal const uint MOUSE_FOCUS = 0x00000400;
    internal const uint FULLSCREEN_DESKTOP = (FULLSCREEN | 0x00001000);
    internal const uint FOREIGN = 0x00000800;
    internal const uint ALLOW_HIGHDPI = 0x00002000;
    internal const uint MOUSE_CAPTURE = 0x00004000;
    internal const uint ALWAYS_ON_TOP = 0x00008000;
    internal const uint SKIP_TASKBAR = 0x00010000;
    internal const uint UTILITY = 0x00020000;
    internal const uint TOOLTIP = 0x00040000;
    internal const uint POPUP_MENU = 0x00080000;
    internal const uint VULKAN = 0x10000000;
    internal const uint METAL = 0x20000000;
}
