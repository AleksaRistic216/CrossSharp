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

    Margin _margin = Margin.Zero;
    public Margin Margin
    {
        get => _margin;
        set
        {
            if (_margin == value)
                return;
            _margin = value;
            OnMarginChanged();
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
