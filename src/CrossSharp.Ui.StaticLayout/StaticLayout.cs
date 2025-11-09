using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class StaticLayout()
    : CrossWrapper<IStaticLayout>(Services.GetSingleton<IStaticLayoutFactory>().Create()),
        IControlsContainer
{
    IStaticLayout _impl => GetImplementation();

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

    public void Clear() => _impl.Clear();

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
    public object? Parent
    {
        get => _impl.Parent;
        set { _impl.Parent = value; }
    }

    public void PerformTheme() => _impl.PerformTheme();

    public EventHandler? ThemePerformed
    {
        get => _impl.ThemePerformed;
        set => _impl.ThemePerformed = value;
    }

    public bool Visible
    {
        get => _impl.Visible;
        set => _impl.Visible = value;
    }

    public void Invalidate() => _impl.Invalidate();

    public void Draw(ref IGraphics graphics) => _impl.Draw(ref graphics);

    public EventHandler? Disposing
    {
        get => _impl.Disposing;
        set => _impl.Disposing = value;
    }
    public int Index
    {
        get => _impl.Index;
        set => _impl.Index = value;
    }

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
    public int MarginTop
    {
        get => _impl.MarginTop;
        set => _impl.MarginTop = value;
    }
    public int MarginBottom
    {
        get => _impl.MarginBottom;
        set => _impl.MarginBottom = value;
    }
    public int MarginLeft
    {
        get => _impl.MarginLeft;
        set => _impl.MarginLeft = value;
    }
    public int MarginRight
    {
        get => _impl.MarginRight;
        set => _impl.MarginRight = value;
    }
    public EventHandler? MarginChanged
    {
        get => _impl.MarginChanged;
        set => _impl.MarginChanged = value;
    }
    public int CornerRadius
    {
        get => _impl.CornerRadius;
        set => _impl.CornerRadius = value;
    }
}
