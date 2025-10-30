using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class FlowLayout
{
    IInputHandler _inputHandler;
    public object Parent { get; set; }
    public bool IsMouseOver { get; set; }
    public bool Visible { get; set; }
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }
    Point _location;
    public Point Location
    {
        get => _location;
        set
        {
            if (_location == value)
                return;
            _location = value;
            OnLocationChanged(new Point(_location.X, _location.Y));
        }
    }
    int _width;
    public int Width
    {
        get => _width;
        set
        {
            if (_width == value)
                return;
            _width = value;
            OnSizeChanged(new Size(_width, _height));
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
            OnSizeChanged(new Size(_width, _height));
        }
    }
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor.Equals(value))
                return;
            _backgroundColor = value;
            OnBackgroundColorChanged();
        }
    }
    int _itemsSpacing = 4;
    public int ItemsSpacing
    {
        get => _itemsSpacing;
        set
        {
            if (_itemsSpacing == value)
                return;
            _itemsSpacing = value;
            OnItemsSpacingChanged();
        }
    }
    FlowAlignment _justifyContentHorizontal = FlowAlignment.Center;
    public FlowAlignment JustifyContentHorizontal
    {
        get => _justifyContentHorizontal;
        set
        {
            if (_justifyContentHorizontal == value)
                return;
            _justifyContentHorizontal = value;
            OnJustifyContentChanged();
        }
    }
    FlowAlignment _justifyContentVertical = FlowAlignment.Center;
    public FlowAlignment JustifyContentVertical
    {
        get => _justifyContentVertical;
        set
        {
            if (_justifyContentVertical == value)
                return;
            _justifyContentVertical = value;
            OnJustifyContentChanged();
        }
    }
    ScrollableMode _scrollable = ScrollableMode.Vertical;
    public ScrollableMode Scrollable
    {
        get => _scrollable;
        set
        {
            if (_scrollable == value)
                return;
            if (_scrollable != ScrollableMode.None && value != ScrollableMode.Vertical)
                throw new NotSupportedException(
                    "FlowLayout only supports Vertical or None scrollable modes."
                );
            _scrollable = value;
            Invalidate();
        }
    }
    Rectangle _viewPort = Rectangle.Empty;
    public Rectangle Viewport
    {
        get => _viewPort;
        set
        {
            if (_viewPort.Equals(value))
                return;
            _viewPort = value;
            Invalidate();
        }
    }
    public Rectangle ContentBounds { get; set; }
    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
}
