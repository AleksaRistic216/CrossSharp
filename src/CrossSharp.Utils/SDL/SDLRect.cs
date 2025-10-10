using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
public struct SDLRect
{
    public int x;
    public int y;
    public int w;
    public int h;
}
