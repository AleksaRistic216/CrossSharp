namespace CrossSharp.Utils.SDL;

static class SDLWindowFlags
{
    internal const ulong FULLSCREEN = 0x0000000000000001;
    internal const ulong OPENGL = 0x0000000000000002;
    internal const ulong OCCLUDED = 0x0000000000000004;
    internal const ulong HIDDEN = 0x0000000000000008;
    internal const ulong BORDERLESS = 0x0000000000000010;
    internal const ulong RESIZABLE = 0x0000000000000020;
    internal const ulong MINIMIZED = 0x0000000000000040;
    internal const ulong MAXIMIZED = 0x0000000000000080;
    internal const ulong MOUSE_GRABBED = 0x0000000000000100;
    internal const ulong INPUT_FOCUS = 0x0000000000000200;
    internal const ulong MOUSE_FOCUS = 0x0000000000000400;
    internal const ulong EXTERNAL = 0x0000000000000800;
    internal const ulong MODAL = 0x0000000000001000;
    internal const ulong HIGH_PIXEL_DENSITY = 0x0000000000002000;
    internal const ulong MOUSE_CAPTURE = 0x0000000000004000;
    internal const ulong MOUSE_RELATIVE_MODE = 0x0000000000008000;
    internal const ulong ALWAYS_ON_TOP = 0x0000000000010000;
    internal const ulong UTILITY = 0x0000000000020000;
    internal const ulong TOOLTIP = 0x0000000000040000;
    internal const ulong POPUP_MENU = 0x0000000000080000;
    internal const ulong KEYBOARD_GRABBED = 0x0000000000100000;
    internal const ulong VULKAN = 0x0000000010000000;
    internal const ulong METAL = 0x0000000020000000;
    internal const ulong TRANSPARENT = 0x0000000040000000;
    internal const ulong NOT_FOCUSABLE = 0x0000000080000000;
}
