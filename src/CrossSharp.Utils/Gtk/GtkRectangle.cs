using System.Runtime.InteropServices;
namespace CrossSharp.Utils.Gtk;

[StructLayout(LayoutKind.Sequential)]
public struct GtkRectangle
{
    public int x;
    public int y;
    public int width;
    public int height;
}