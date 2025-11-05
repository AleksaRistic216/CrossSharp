using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

class StaticLayout : IStaticLayout
{
    readonly List<IControl> _controls = [];

    public int DockIndex { get; set; }

    public EventHandler? Disposing { get; set; }
    public int Index { get; set; }
    public DockStyle Dock { get; set; }
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; } = ColorRgba.Transparent;

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
    public object? Parent { get; set; }
    bool _visible = true;

    protected StaticLayout()
    {
        PerformTheme();
    }

    public void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().LayoutBackgroundColor;
        this.SetMargin(Services.GetSingleton<ITheme>().DefaultLayoutItemSpacing);
        foreach (var control in _controls)
            control.PerformTheme();
    }

    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
            Invalidate();
        }
    }

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

    IEnumerator IEnumerable.GetEnumerator() => _controls.GetEnumerator();

    public void Add(params IControl[] controls)
    {
        foreach (var c in controls)
            c.Parent = this;
        _controls.AddRange(controls);
        Invalidate();
    }

    public void Remove(params IControl[] controls) => _controls.RemoveAll(controls.Contains);

    public void Clear() => _controls.Clear();

    public void Draw(ref IGraphics graphics)
    {
        var clientBounds = this.GetClientBounds();
        graphics.SetOffset(clientBounds.X, clientBounds.Y);
        graphics.SetClip(clientBounds, 0);
        DrawBackground(ref graphics);
        foreach (var c in _controls.ToArray())
            c.Draw(ref graphics);
        // ScrollableHelpers.DrawScrollBar(ref graphics, this);
    }

    void DrawBackground(ref IGraphics graphics)
    {
        graphics.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    public void Dispose() => OnDisposingInternal();

    void RaiseDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

    void OnDisposingInternal()
    {
        foreach (var c in _controls)
            c.Dispose();
        _controls.Clear();
        RaiseDisposing();
    }

    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public EventHandler? BackgroundColorChanged { get; set; }
    public bool IsMouseOver { get; set; } = false;
    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    public EventHandler? MarginChanged { get; set; }
}
