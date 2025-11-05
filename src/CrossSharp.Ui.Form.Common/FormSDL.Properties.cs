using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

partial class FormSDL
{
    public IntPtr Handle { get; private set; }

    public IntPtr Renderer { get; private set; }
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
    public IControlsContainer Controls { get; private set; }
    public ColorRgba BackgroundColor { get; set; }
    public int Column { get; set; }
    public int Row { get; set; }
    public IntPtr DisplayHandle { get; set; }
    public IntPtr WindowSurfaceHandle { get; set; }
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
    public IApplication AppInstance { get; }
    public WindowState State { get; set; }
    public ITitleBar? TitleBar { get; }
    public IntPtr ParentHandle { get; set; }
    public object Parent { get; set; }
    public int BorderWidth { get; set; }
    public ColorRgba BorderColor { get; set; }
    public Point Location { get; set; }
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
        get => _height;
        set
        {
            if (_height == value)
                return;
            _height = value;
            OnSizeChangedInternal();
        }
    }
    public int ZIndex { get; set; }
    bool _visible = false;
    public bool Visible
    {
        get => _visible;
        set
        {
            if (_visible == value)
                return;
            _visible = value;
            OnVisibleChangedInternal();
        }
    }
    public int Index { get; set; }

    public bool IsMouseOver { get; set; }
}
