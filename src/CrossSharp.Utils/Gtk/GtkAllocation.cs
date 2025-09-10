using System.Runtime.InteropServices;

namespace CrossSharp.Utils.Gtk;

[StructLayout(LayoutKind.Sequential)]
public struct GtkAllocation
{
    public int X;
    public int Y;
    public int Width;
    public int Height;
}
