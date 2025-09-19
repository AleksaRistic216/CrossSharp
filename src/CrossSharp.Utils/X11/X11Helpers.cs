using System.Runtime.InteropServices;

namespace CrossSharp.Utils.X11;

static class X11Helpers
{
    const string Libx11 = "libX11.so.6";

    [DllImport(Libx11)]
    public static extern IntPtr XOpenDisplay(IntPtr display);

    [DllImport(Libx11)]
    public static extern void XMoveWindow(IntPtr display, uint window, int x, int y);

    [DllImport(Libx11)]
    public static extern IntPtr XDefaultRootWindow(IntPtr display);

    [DllImport(Libx11)]
    public static extern void XFlush(IntPtr display);
}
