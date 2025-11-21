using System.Drawing;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Utils;

public partial class ControlBase
{
    Point _location = new Point(0, 0);
    int _width = 0;
    int _height = 0;
    bool _visible = true;

    public ColorRgba ForegroundColor { get; set; } = ColorRgba.Transparent;
    public EventHandler? LayoutChanged { get; set; }

    protected IInputHandler InputHandler { get; set; }

    public bool IsMouseOver { get; set; }
    int _borderWidth { get; set; }
    public int BorderWidth
    {
        get => _borderWidth;
        set
        {
            if (_borderWidth == value)
                return;
            _borderWidth = value;
            Invalidate();
        }
    }
    ColorRgba _borderColor { get; set; } = ColorRgba.Transparent;
    public ColorRgba BorderColor
    {
        get => _borderColor;
        set
        {
            if (_borderColor == value)
                return;
            _borderColor = value;
            Invalidate();
        }
    }
    public IntPtr ParentHandle { get; set; }
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
        }
    }
    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;
            _width = value;
            RaiseOnSizeChanged(new Size(Width, Height));
        }
    }
    public int Height
    {
        get => _height;
        set
        {
            _height = value;
            RaiseOnSizeChanged(new Size(Width, Height));
        }
    }
    public Point Location
    {
        get => _location;
        set
        {
            if (_location == value)
                return;
            _location = value;
            RaiseOnLocationChanged(value);
        }
    }
    public int ZIndex { get; set; } = 0;

    private Margin _margin = Margin.Zero;
    public Margin Margin
    {
        get => _margin;
        set
        {
            if (_margin.Equals(value))
                return;
            _margin = value;
            OnMarginChanged(this, System.EventArgs.Empty);
        }
    }
}
