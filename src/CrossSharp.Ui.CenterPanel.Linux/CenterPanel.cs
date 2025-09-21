using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class CenterPanel : GtkWidget, ICenterPanel
{
    public override void Invalidate() { }

    public override void DrawShadows(Graphics g)
    {
        if (_child is null)
            return;
        _child.Location = Location;
        _child!.DrawShadows(g);
    }

    public override void DrawBackground(Graphics g)
    {
        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
        if (_child is null)
            return;
        _child.Location = Location;
        _child!.DrawBackground(g);
    }

    public override void DrawBorders(Graphics g)
    {
        if (_child is null)
            return;
        _child.Location = Location;
        _child!.DrawBorders(g);
    }

    public override void DrawContent(Graphics g)
    {
        if (_child is null)
            return;
        _child.Location = Location;
        _child!.DrawContent(g);
    }

    void ValidateChild(IGtkWidget? widget)
    {
        if (widget is null)
            return;
        if (widget.Parent is not null)
            throw new InvalidOperationException("Widget already has a parent");
        if (widget is not ICenterPanelChild)
            throw new InvalidOperationException("Widget must implement ICenterPanelChild");

        // NEXT THING: Implement ICenterPanelChild into all widgets
    }
}
