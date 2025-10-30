using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

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
    public bool IsMouseOver
    {
        get => _impl.IsMouseOver;
        set => _impl.IsMouseOver = value;
    }
    public bool Visible
    {
        get => _impl.Visible;
        set => _impl.Visible = value;
    }

    public void Invalidate() => _impl.Invalidate();

    public void SuspendLayout() => _impl.SuspendLayout();

    public void ResumeLayout() => _impl.ResumeLayout();

    public void Draw(ref IGraphics graphics) => _impl.Draw(ref graphics);

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

    public void AddTab(string title, Type content) => _impl.AddTab(title, content);

    public void AddTabButton(string text, Action onClick) => _impl.AddTabButton(text, onClick);

    public void RemoveTab(string title) => _impl.RemoveTab(title);

    public void SelectTab(string title) => _impl.SelectTab(title);

    public IEnumerator<IControl> GetEnumerator() => _impl.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _impl.GetEnumerator();

    public void Add(params IControl[] controls) => _impl.Add(controls);

    public void Remove(params IControl[] controls) => _impl.Remove(controls);
}
