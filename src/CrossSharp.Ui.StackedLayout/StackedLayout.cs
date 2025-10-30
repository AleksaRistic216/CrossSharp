using System.Collections;
using System.Drawing;
using System.Numerics;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui;

public class StackedLayout()
    : CrossWrapper<IStackedLayout>(Services.GetSingleton<IStackedLayoutFactory>().Create()),
        IStackedLayout
{
    IStackedLayout _impl => GetImplementation();
    public int ItemsSpacing
    {
        get => _impl.ItemsSpacing;
        set => _impl.ItemsSpacing = value;
    }
    public Direction Direction
    {
        get => _impl.Direction;
        set => _impl.Direction = value;
    }

    public int DockIndex
    {
        get => _impl.DockIndex;
        set => _impl.DockIndex = value;
    }
    public DockStyle Dock
    {
        get => _impl.Dock;
        set => _impl.Dock = value;
    }

    public IEnumerator<IControl> GetEnumerator() => _impl.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls) => _impl.Add(controls);

    public void Remove(params IControl[] controls) => _impl.Remove(controls);

    public void Dispose() => _impl.Dispose();

    public int BorderWidth
    {
        get => _impl.BorderWidth;
        set => _impl.BorderWidth = value;
    }
    public ColorRgba BorderColor
    {
        get => _impl.BorderColor;
        set => _impl.BorderColor = value;
    }

    public void LimitClip(ref IGraphics g) => _impl.LimitClip(ref g);

    public Point Location
    {
        get => _impl.Location;
        set => _impl.Location = value;
    }
    public EventHandler<Point>? LocationChanged
    {
        get => _impl.LocationChanged;
        set => _impl.LocationChanged = value;
    }
    public int Width
    {
        get => _impl.Width;
        set => _impl.Width = value;
    }
    public int Height
    {
        get => _impl.Height;
        set => _impl.Height = value;
    }
    public EventHandler<Size>? SizeChanged
    {
        get => _impl.SizeChanged;
        set => _impl.SizeChanged = value;
    }
    public object Parent
    {
        get => _impl.Parent;
        set => _impl.Parent = value;
    }
    public bool Visible
    {
        get => _impl.Visible;
        set => _impl.Visible = value;
    }

    public virtual void Invalidate() => _impl.Invalidate();

    public virtual void SuspendLayout() => _impl.SuspendLayout();

    public virtual void ResumeLayout() => _impl.ResumeLayout();

    public void Draw(ref IGraphics graphics) => _impl.Draw(ref graphics);

    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set => _impl.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => _impl.BackgroundColorChanged;
        set => _impl.BackgroundColorChanged = value;
    }
    public bool IsMouseOver
    {
        get => _impl.IsMouseOver;
        set => _impl.IsMouseOver = value;
    }
    public Padding Padding
    {
        get => _impl.Padding;
        set => _impl.Padding = value;
    }
    public ScrollableMode Scrollable
    {
        get => _impl.Scrollable;
        set => _impl.Scrollable = value;
    }
    public Rectangle Viewport
    {
        get => _impl.Viewport;
        set => _impl.Viewport = value;
    }
    public Rectangle ContentBounds
    {
        get => _impl.ContentBounds;
        set => _impl.ContentBounds = value;
    }
}
