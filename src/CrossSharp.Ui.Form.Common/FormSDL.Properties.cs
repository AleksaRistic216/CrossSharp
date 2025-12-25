using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class FormSDL
{
    #region Window Handles

    public IntPtr Handle { get; private set; }
    public IntPtr Renderer { get; private set; }
    public IntPtr ParentHandle { get; set; }
    public IntPtr DisplayHandle { get; set; }
    public IntPtr WindowSurfaceHandle { get; set; }

    uint _windowId;
    public uint WindowId
    {
        get
        {
            if (_windowId == 0)
                _windowId = SDLHelpers.SDL_GetWindowID(Handle);
            return _windowId;
        }
    }

    #endregion

    #region Layout Properties

    public object? Parent { get; set; }
    public IControlsContainer Controls { get; private set; } = null!;
    public Point Location { get; set; }
    public int Column { get; set; }
    public int Row { get; set; }
    public int ZIndex { get; set; }
    public int Index { get; set; }

    #endregion

    #region Size Properties

    int _width = 800;
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

    int _height = 600;
    public int Height
    {
        get => _height - (_titleBar?.Height ?? 0);
        set
        {
            if (_height == value)
                return;
            _height = value;
            OnSizeChangedInternal();
        }
    }

    #endregion

    #region Margin Properties

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

    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }

    #endregion

    #region Appearance Properties

    public ColorRgba BackgroundColor { get; set; } = ColorRgba.Transparent;
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; } = ColorRgba.Transparent;

    #endregion

    #region State Properties

    string _title = string.Empty;
    public string Title
    {
        get => _title;
        set
        {
            if (_title == value)
                return;
            _title = value;
            OnTitleChangedInternal();
        }
    }

    WindowState _state = WindowState.Normal;
    public WindowState State
    {
        get => _state;
        set
        {
            if (_state == value)
                return;
            _state = value;
            OnStateChanged();
        }
    }

    bool _visible;
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
            OnVisibleChanged();
        }
    }

    public bool IsMouseOver { get; set; }

    #endregion

    #region Internal Components

    FormSDLTitleBar? _titleBar;
    FormSDLHitTestHandler? _hitTestHandler;

    #endregion
}
