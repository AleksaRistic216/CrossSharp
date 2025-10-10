using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
struct SDL_SysWMinfo
{
    public SDL_version version;
    public SysWMType subsystem;
    public Info info;

    [StructLayout(LayoutKind.Explicit)]
    public struct Info
    {
        [FieldOffset(0)]
        public X11Info x11;
    }

    public enum SysWMType
    {
        SDL_SYSWM_UNKNOWN,
        SDL_SYSWM_X11,
        // other backends omitted
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct X11Info
    {
        public IntPtr display;
        public IntPtr window;
    }
}
