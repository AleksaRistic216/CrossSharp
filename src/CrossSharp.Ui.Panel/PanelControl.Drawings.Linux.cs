using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;

namespace CrossSharp.Ui;

public partial class PanelControl
{
    protected override void DrawShadowsLinux(Graphics g) { }

    protected override void DrawBackgroundLinux(Graphics g)
    {
        if (g.ContextHandle == IntPtr.Zero)
            return;

        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
    }

    protected override void DrawBordersLinux(Graphics g) { }

    protected override void DrawContentLinux(Graphics g) { }
}
