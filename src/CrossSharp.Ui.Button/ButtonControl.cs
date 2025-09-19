using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public partial class ButtonControl : GtkWidget, IClickable
{
    public ButtonControl()
    {
        SubscribeToInputs();
    }

    public override void Invalidate()
    {
        Redraw();
    }

    protected override void DrawShadowsLinux(Graphics g) { }

    protected override void DrawShadowsWindows(Graphics g) { }

    protected override void DrawShadowsMacOs(Graphics g) { }

    protected override void DrawBackgroundLinux(Graphics g)
    {
        if (g.ContextHandle == IntPtr.Zero)
            return;

        g.FillRectangle(Location.X, Location.Y, Width, Height, Color.Black);
    }

    protected override void DrawBackgroundWindows(Graphics g) { }

    protected override void DrawBackgroundMacOs(Graphics g) { }

    protected override void DrawBordersLinux(Graphics g) { }

    protected override void DrawBordersWindows(Graphics g) { }

    protected override void DrawBordersMacOs(Graphics g) { }

    protected override void DrawContentLinux(Graphics g) { }

    protected override void DrawContentWindows(Graphics g) { }

    protected override void DrawContentMacOs(Graphics g) { }
}
