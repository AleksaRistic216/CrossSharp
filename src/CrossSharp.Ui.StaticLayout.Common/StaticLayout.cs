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
        OnThemePerformed();
    }

    public EventHandler? ThemePerformed { get; set; }

    void RaiseThemePerformed() => ThemePerformed?.Invoke(this, EventArgs.Empty);

    void OnThemePerformed()
    {
        RaiseThemePerformed();
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

    public void PrepareClipAndOffset(ref IGraphics g)
    {
        // I think problem is here. I should apply scroll if this is child of scrollable
        var clientBounds = this.GetClientBounds();
        g.SetOffset(clientBounds.X, clientBounds.Y);
        g.SetClip(clientBounds, CornerRadius);
    }

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
        PrepareClipAndOffset(ref graphics);
        DrawBackground(ref graphics);
        foreach (var c in _controls.ToArray())
            c.Draw(ref graphics);
        PrepareClipAndOffset(ref graphics);
        // ScrollableHelpers.DrawScrollBar(ref graphics, this);
        DrawBorders(ref graphics);
    }

    void DrawBackground(ref IGraphics graphics)
    {
        graphics.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    void DrawBorders(ref IGraphics graphics)
    {
        if (BorderWidth <= 0)
            return;
        if (Equals(BorderColor, ColorRgba.Transparent))
            return;
        if (Width <= 0 || Height <= 0)
            return;

        var cornersRadius = (this as IRoundedCorners)?.CornerRadius ?? 0;
        graphics.DrawRectangle(0, 0, Width, Height, BorderColor, BorderWidth, cornersRadius);
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
    public int CornerRadius { get; set; }
}
