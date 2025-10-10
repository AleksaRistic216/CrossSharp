using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
public struct SDLRendererInfo
{
    public IntPtr name; // UTF-8 string
    public uint flags;
    public uint num_texture_formats;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public uint[] texture_formats;
    public int max_texture_width;
    public int max_texture_height;
}
