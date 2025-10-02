using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public partial class CenterPanel : Utils.Linux.ControlBase, ICenterPanel
{
    public override void Invalidate()
    {
        base.Invalidate();
        InvalidateChildLocation();
    }

    void InvalidateChildLocation()
    {
        if (_child is null)
            return;
        var childSize = (_child as ICenterPanelChild)!.GetSize();
        var x = Location.X + Width / 2 - childSize.Width / 2;
        var y = Location.Y + Height / 2 - childSize.Height / 2;
        _child.Location = new Point(x, y);
    }

    public override void DrawShadows(Graphics g)
    {
        if (_child is null)
            return;
        _child!.DrawShadows(g);
    }

    public override void DrawBackground(Graphics g)
    {
        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
        if (_child is null)
            return;
        _child!.DrawBackground(g);
    }

    public override void DrawBorders(Graphics g)
    {
        if (_child is null)
            return;
        _child!.DrawBorders(g);
    }

    public override void DrawContent(Graphics g)
    {
        if (_child is null)
            return;
        _child!.DrawContent(g);
    }

    void ValidateChild(IControl? widget)
    {
        if (widget is null)
            return;
        if (widget.Parent is not null)
            throw new InvalidOperationException("Widget already has a parent");
        if (widget is not ICenterPanelChild)
            throw new InvalidOperationException("Widget must implement ICenterPanelChild");
    }
}
