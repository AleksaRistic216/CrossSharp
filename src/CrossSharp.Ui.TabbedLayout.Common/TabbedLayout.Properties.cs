using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class TabbedLayout
{
    readonly List<IControl> _controls = [];
    ITheme Theme => Services.GetSingleton<ITheme>();
    DynamicControlsController _controlsController;
    IControlsContainer _tabs;
    IStackedLayout _header;
    int headerHeight = 30;
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
    ColorRgba _backgroundColor = Services.GetSingleton<ITheme>().BackgroundColor;
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
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
}
