using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class Panel : GtkWidget, IPanel
{
    public override void Invalidate() { }

    public override void DrawShadows(Graphics g) { }

    public override void DrawBackground(Graphics g)
    {
        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
    }

    public override void DrawBorders(Graphics g) { }

    public override void DrawContent(Graphics g) { }

    Size ICenterPanelChild.GetSize()
    {
        return new Size(Width, Height);
    }
}
