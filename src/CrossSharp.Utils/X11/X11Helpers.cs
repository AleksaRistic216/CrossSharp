using System.Runtime.InteropServices;

namespace CrossSharp.Utils.X11;

static class X11Helpers
{
    const string Libx11 = "libX11.so.6";

    [DllImport(Libx11)]
    internal static extern IntPtr XOpenDisplay(IntPtr display);

    [DllImport(Libx11)]
    internal static extern void XMoveWindow(IntPtr display, uint window, int x, int y);

    [DllImport(Libx11)]
    internal static extern IntPtr XDefaultRootWindow(IntPtr display);

    [DllImport(Libx11)]
    internal static extern void XFlush(IntPtr display);

    [DllImport(Libx11)]
    internal static extern void XGetWindowAttributes(
        IntPtr display,
        uint window,
        out XWindowAttributes attributes
    );

    [DllImport(Libx11)]
    internal static extern void XChangeWindowAttributes(
        IntPtr display,
        IntPtr window,
        ulong valuemask,
        ref XSetWindowAttributes attributes
    );

    [DllImport(Libx11)]
    internal static extern IntPtr XInternAtom(
        IntPtr display,
        string atom_name,
        bool only_if_exists
    );

    [DllImport(Libx11)]
    internal static extern void XChangeProperty(
        IntPtr display,
        IntPtr w,
        IntPtr property,
        IntPtr type,
        int format,
        int mode,
        ref IntPtr data,
        int nelements
    );
}
