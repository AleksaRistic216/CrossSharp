using System.Runtime.InteropServices;

namespace CrossSharp.Utils.X11;

[StructLayout(LayoutKind.Sequential)]
public struct XWindowAttributes
{
    public int x;
    public int y;
    public int width;
    public int height;
    public int border_width;
    public int depth;
    public IntPtr visual;
    public uint root;
    public int @class;
    public int bit_gravity;
    public int win_gravity;
    public int backing_store;
    public ulong backing_planes;
    public ulong backing_pixel;

    [MarshalAs(UnmanagedType.I1)]
    public bool save_under;
    public uint colormap;

    [MarshalAs(UnmanagedType.I1)]
    public bool map_installed;
    public int map_state;
    public long all_event_masks;
    public long your_event_mask;
    public long do_not_propagate_mask;

    [MarshalAs(UnmanagedType.I1)]
    public bool override_redirect;
    public IntPtr screen;
}
