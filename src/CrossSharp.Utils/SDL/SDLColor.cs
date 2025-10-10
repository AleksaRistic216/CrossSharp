using System.Runtime.InteropServices;

namespace CrossSharp.Utils.SDL;

[StructLayout(LayoutKind.Sequential)]
public struct SDLColor
{
    public byte r;
    public byte g;
    public byte b;
    public byte a;
}
