namespace CrossSharp.Utils.X11;

public struct XSetWindowAttributes
{
    public IntPtr background_pixmap; // Pixmap
    public ulong background_pixel;
    public IntPtr border_pixmap; // Pixmap
    public ulong border_pixel;
    public int bit_gravity;
    public int win_gravity;
    public int backing_store;
    public ulong backing_planes;
    public ulong backing_pixel;
    public bool save_under;
    public long event_mask;
    public long do_not_propagate_mask;
    public bool override_redirect;
    public IntPtr colormap; // Colormap
    public IntPtr cursor; // Cursor
}
