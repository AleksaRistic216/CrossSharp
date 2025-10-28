using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class StaticLayout : IStaticLayout
{
    readonly List<IControl> _controls = [];

    public int DockIndex { get; set; }
    public DockPosition Dock { get; set; }
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }

    public Point Location { get; set; }
    public EventHandler<Point>? LocationChanged { get; set; }
    int _width;

    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;
            _width = value;
            SizeChanged?.Invoke(this, new Size(_width, _height));
            Invalidate();
        }
    }
    int _height;
    public int Height
    {
        get => _height;
        set
        {
            if (_height == value)
                return;
            _height = value;
            SizeChanged?.Invoke(this, new Size(_width, _height));
            Invalidate();
        }
    }
    public EventHandler<Size>? SizeChanged { get; set; }
    public object Parent { get; set; }
    public bool Visible { get; set; }

    public void LimitClip(ref IGraphics g) { }

    public void Initialize() { }

    public void Invalidate()
    {
        this.PerformDocking();
        foreach (var control in _controls)
            control.Invalidate();
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public IEnumerator<IControl> GetEnumerator() => _controls.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public void Add(params IControl[] controls)
    {
        foreach (var c in controls)
            c.Parent = this;
        _controls.AddRange(controls);
        Invalidate();
    }

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Draw(ref IGraphics graphics)
    {
        foreach (var c in _controls.ToArray())
            c.Draw(ref graphics);
    }

    public void Dispose()
    {
        foreach (var c in _controls)
            c.Dispose();
        _controls.Clear();
    }

    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public EventHandler? BackgroundColorChanged { get; set; }
    public bool IsMouseOver { get; set; } = false;
}
