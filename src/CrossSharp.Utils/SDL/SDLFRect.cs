using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
public struct SDLFRect
{
    public float x;
    public float y;
    public float w;
    public float h;
}
