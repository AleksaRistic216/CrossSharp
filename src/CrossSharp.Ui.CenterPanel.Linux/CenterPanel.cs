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

    public override void DrawShadows(ref Graphics g)
    {
        if (_child is null)
            return;
        _child!.DrawShadows(ref g);
    }

    public override void DrawBackground(ref Graphics g)
    {
        g.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
        if (_child is null)
            return;
        _child!.DrawBackground(ref g);
    }

    public override void DrawBorders(ref Graphics g)
    {
        if (_child is null)
            return;
        _child!.DrawBorders(ref g);
    }

    public override void DrawContent(ref Graphics g)
    {
        if (_child is null)
            return;
        _child!.DrawContent(ref g);
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
