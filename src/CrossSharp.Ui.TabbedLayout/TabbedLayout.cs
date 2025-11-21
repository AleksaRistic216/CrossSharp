using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui;

public class TabbedLayout()
    : CrossWrapper<ITabbedLayout>(Services.GetSingleton<ITabbedLayoutFactory>().Create()),
        ITabbedLayout
{
    ITabbedLayout _impl => GetImplementation();

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
        set => _impl.Parent = value;
    }
    public bool IsMouseOver
    {
        get => _impl.IsMouseOver;
        set => _impl.IsMouseOver = value;
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

    public EventHandler? Invalidated
    {
        get => _impl.Invalidated;
        set => _impl.Invalidated = value;
    }

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

    public int HeaderItemsSpacing
    {
        get => _impl.HeaderItemsSpacing;
        set => _impl.HeaderItemsSpacing = value;
    }
    public EventHandler? HeaderItemsSpacingChanged
    {
        get => _impl.HeaderItemsSpacingChanged;
        set => _impl.HeaderItemsSpacingChanged = value;
    }
    public Padding HeaderPadding
    {
        get => _impl.HeaderPadding;
        set => _impl.HeaderPadding = value;
    }
    public EventHandler? HeaderPaddingChanged
    {
        get => _impl.HeaderPaddingChanged;
        set => _impl.HeaderPaddingChanged = value;
    }
    public int HeaderHeight
    {
        get => _impl.HeaderHeight;
        set => _impl.HeaderHeight = value;
    }
    public EventHandler? HeaderHeightChanged
    {
        get => _impl.HeaderHeightChanged;
        set => _impl.HeaderHeightChanged = value;
    }
    public EventHandler? CurrentTabChanged
    {
        get => _impl.CurrentTabChanged;
        set => _impl.CurrentTabChanged = value;
    }

    public void AddTab(string title, Type content) => _impl.AddTab(title, content);

    public IButton CreateTabButton() => _impl.CreateTabButton();

    public void RemoveTab(string title) => _impl.RemoveTab(title);

    public void SelectTab(string title) => _impl.SelectTab(title);

    public IEnumerator<IControl> GetEnumerator() => _impl.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _impl.GetEnumerator();

    public void Add(params IControl[] controls) => _impl.Add(controls);

    public void Remove(params IControl[] controls) => _impl.Remove(controls);

    public void Clear() => _impl.Clear();

    public Margin Margin
    {
        get => _impl.Margin;
        set => _impl.Margin = value;
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
