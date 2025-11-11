using System.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract class CrossControl<T>(T implementation) : CrossWrapper<IControl>(implementation), IControl
    where T : IControl
{
    protected T Implementation = implementation;

    public override int GetHashCode() => Implementation?.GetHashCode() ?? 0;

    public void Dispose() => Implementation.Dispose();

    public int BorderWidth
    {
        get => Implementation.BorderWidth;
        set => Implementation.BorderWidth = value;
    }
    public ColorRgba BorderColor
    {
        get => Implementation.BorderColor;
        set => Implementation.BorderColor = value;
    }

    public Point Location
    {
        get => Implementation.Location;
        set => Implementation.Location = value;
    }
    public EventHandler<Point>? LocationChanged
    {
        get => Implementation.LocationChanged;
        set => Implementation.LocationChanged = value;
    }
    public int Width
    {
        get => Implementation.Width;
        set => Implementation.Width = value;
    }
    public int Height
    {
        get => Implementation.Height;
        set => Implementation.Height = value;
    }
    public EventHandler<Size>? SizeChanged
    {
        get => Implementation.SizeChanged;
        set => Implementation.SizeChanged = value;
    }
    public object? Parent
    {
        get => Implementation.Parent;
        set => Implementation.Parent = value;
    }

    public virtual void PerformTheme() => Implementation.PerformTheme();

    public EventHandler? ThemePerformed
    {
        get => Implementation.ThemePerformed;
        set => Implementation.ThemePerformed = value;
    }

    public bool Visible
    {
        get => Implementation.Visible;
        set => Implementation.Visible = value;
    }

    public void Invalidate() => Implementation.Invalidate();

    public EventHandler? Invalidated
    {
        get => Implementation.Invalidated;
        set { Implementation.Invalidated = value; }
    }

    public void Draw(ref IGraphics graphics) => Implementation.Draw(ref graphics);

    public EventHandler? Disposing
    {
        get => Implementation.Disposing;
        set => Implementation.Disposing = value;
    }
    public int Index
    {
        get => Implementation.Index;
        set => Implementation.Index = value;
    }

    public bool IsMouseOver
    {
        get => Implementation.IsMouseOver;
        set => Implementation.IsMouseOver = value;
    }
    public int MarginTop
    {
        get => Implementation.MarginTop;
        set => Implementation.MarginTop = value;
    }
    public int MarginBottom
    {
        get => Implementation.MarginBottom;
        set => Implementation.MarginBottom = value;
    }
    public int MarginLeft
    {
        get => Implementation.MarginLeft;
        set => Implementation.MarginLeft = value;
    }
    public int MarginRight
    {
        get => Implementation.MarginRight;
        set => Implementation.MarginRight = value;
    }
    public EventHandler? MarginChanged
    {
        get => Implementation.MarginChanged;
        set => Implementation.MarginChanged = value;
    }
}
