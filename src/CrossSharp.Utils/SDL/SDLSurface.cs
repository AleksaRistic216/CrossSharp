using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
public struct SDLSurface
{
    public uint flags;
    public IntPtr format;
    public int w,
        h;
    public int pitch;
    public IntPtr pixels;
    public IntPtr userdata;
    public int locked;
    public IntPtr lock_data;
    public SDLRect clip_rect;
    public IntPtr map;
    public int refcount;
}
