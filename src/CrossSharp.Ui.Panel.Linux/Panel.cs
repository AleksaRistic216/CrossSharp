using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class Panel : Utils.Linux.ControlBase, IPanel
{
    public override void DrawShadows(ref Graphics g) { }

    public override void DrawBackground(ref Graphics g)
    {
        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
    }

    public override void DrawContent(ref Graphics g) { }

    Size ICenterPanelChild.GetSize()
    {
        return new Size(Width, Height);
    }
}
