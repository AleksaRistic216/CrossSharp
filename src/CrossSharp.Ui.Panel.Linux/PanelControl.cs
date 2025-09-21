using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class PanelControl : GtkWidget, IPanelControl
{
    public override void Invalidate() { }

    public override void DrawShadows(Graphics g) { }

    public override void DrawBackground(Graphics g)
    {
        if (g.ContextHandle == IntPtr.Zero)
            return;

        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
    }

    public override void DrawBorders(Graphics g) { }

    public override void DrawContent(Graphics g) { }
}
