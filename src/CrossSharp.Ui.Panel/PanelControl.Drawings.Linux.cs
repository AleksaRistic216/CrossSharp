using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
namespace CrossSharp.Ui;

public partial class PanelControl {
    protected override void DrawShadowsLinux(Graphics g) { }
    protected override void DrawBackgroundLinux(Graphics g) { 
        if(g.ContextHandle == IntPtr.Zero) return;
        GtkHelpers.cairo_set_source_rgba(g.ContextHandle, BackgroundColor.R, BackgroundColor.G, BackgroundColor.B, BackgroundColor.A);
        GtkHelpers.cairo_rectangle(g.ContextHandle, Location.X, Location.Y, Width, Height);
        GtkHelpers.cairo_fill(g.ContextHandle);
    }
    protected override void DrawBordersLinux(Graphics g) { }
    protected override void DrawContentLinux(Graphics g) { }
}