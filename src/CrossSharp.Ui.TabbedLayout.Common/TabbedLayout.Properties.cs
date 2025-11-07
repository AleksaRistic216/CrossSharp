using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class TabbedLayout
{
    readonly List<IControl> _controls = [];
    ITheme Theme => Services.GetSingleton<ITheme>();
    DynamicControlsController _controlsController = null!;
    IControlsContainer _tabs = null!;
    IStackedLayout _header = null!;
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; } = ColorRgba.Transparent;
    Point _location;
    public Point Location
    {
        get => _location;
        set
        {
            if (_location == value)
                return;
            _location = value;
            OnLocationChangedInternal();
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
            OnSizeChangedInternal();
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
            OnSizeChangedInternal();
        }
    }
    public object? Parent { get; set; }
    public bool IsMouseOver { get; set; }

    int _marginTop;
    public int MarginTop
    {
        get => _marginTop;
        set
        {
            if (_marginTop == value)
                return;
            _marginTop = value;
            OnMarginChangedInternal();
        }
    }
    int _marginBottom;
    public int MarginBottom
    {
        get => _marginBottom;
        set
        {
            if (_marginBottom == value)
                return;
            _marginBottom = value;
            OnMarginChangedInternal();
        }
    }
    int _marginLeft;
    public int MarginLeft
    {
        get => _marginLeft;
        set
        {
            if (_marginLeft == value)
                return;
            _marginLeft = value;
            OnMarginChangedInternal();
        }
    }
    int _marginRight;
    public int MarginRight
    {
        get => _marginRight;
        set
        {
            if (_marginRight == value)
                return;
            _marginRight = value;
            OnMarginChangedInternal();
        }
    }
    public int Index { get; set; }
    public int DockIndex { get; set; }
    public DockStyle Dock { get; set; }
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (Equals(_backgroundColor, value))
                return;
            _backgroundColor = value;
            OnBackgroundColorChangedInternal();
        }
    }
    bool _visible = true;
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
    int _headerItemsSpacing;
    public int HeaderItemsSpacing
    {
        get => _headerItemsSpacing;
        set
        {
            if (_headerItemsSpacing == value)
                return;
            _headerItemsSpacing = value;
            OnHeaderItemsSpacingChanged();
        }
    }
    Padding _headerPadding;
    public Padding HeaderPadding
    {
        get => _headerPadding;
        set
        {
            if (
                _headerPadding.Left == value.Left
                && _headerPadding.Right == value.Right
                && _headerPadding.Top == value.Top
                && _headerPadding.Bottom == value.Bottom
            )
                return;
            _headerPadding = value;
            OnHeaderPaddingChanged();
        }
    }
    int _headerHeight = 30;
    public int HeaderHeight
    {
        get => _headerHeight;
        set
        {
            if (_headerHeight == value)
                return;
            _headerHeight = value;
            OnHeaderHeightChanged();
        }
    }
    public int CornerRadius { get; set; }
}
