using System.Runtime.InteropServices;
namespace CrossSharp.Utils.Gtk;

[StructLayout(LayoutKind.Sequential)]
public struct GtkRgba {
    public double red;
    public double green;
    public double blue;
    public double alpha;
}