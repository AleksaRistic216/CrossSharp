using System.Collections;
using System.Drawing;
using System.Numerics;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Linux;

class StackedLayout : IStackedLayout
{
    readonly List<IControl> _controls = [];

    public int DockIndex { get; set; }
    public DockPosition Dock { get; set; }
    public int ItemsSpacing { get; set; } = 0;
    public Direction Direction { get; set; } = Direction.Vertical;
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public Point Location { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public object Parent { get; set; }
    public bool Visible { get; set; }

    public void LimitClip(ref IGraphics g) { }

    public void Initialize() { }

    public void Invalidate()
    {
        this.PerformDocking();
        if (Direction == Direction.Vertical)
            InvalidateStackVertical();
        else
            InvalidateStackHorizontal();
        foreach (IControl control in _controls)
            control.Invalidate();
    }

    void InvalidateStackVertical()
    {
        var currentY = Padding.Top;
        foreach (var c in _controls)
        {
            c.Location = new Point(Location.X + Padding.Left, currentY);
            c.Width = Width - Padding.Horizontal;
            currentY += c.Height + ItemsSpacing;
        }
    }

    void InvalidateStackHorizontal()
    {
        var currentX = Padding.Left;
        foreach (var c in _controls)
        {
            c.Location = new Point(currentX, Location.Y + Padding.Top);
            c.Height = Height - Padding.Vertical;
            currentX += c.Width + ItemsSpacing;
        }
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls)
    {
        foreach (IControl control in controls)
            control.Parent = this;
        _controls.AddRange(controls);
        Invalidate();
    }

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Draw(ref IGraphics graphics)
    {
        graphics.SetClip(new Rectangle(Location, new Size(Width, Height)));
        graphics.SetOffset(0, 0);
        DrawBackground(ref graphics);
        foreach (var c in _controls)
            c.Draw(ref graphics);
    }

    void DrawBackground(ref IGraphics graphics)
    {
        graphics.FillRectangle(Location.X, Location.Y, Width, Height, BackgroundColor);
    }

    public IForm? GetForm() => ControlsHelpers.GetForm(this);

    public void Dispose()
    {
        foreach (var c in _controls)
            c.Dispose();
        _controls.Clear();
    }

    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public EventHandler? OnBackgroundColorChange { get; set; }
    public bool IsMouseOver { get; set; }
    public Padding Padding { get; set; } = Padding.Zero;
}
