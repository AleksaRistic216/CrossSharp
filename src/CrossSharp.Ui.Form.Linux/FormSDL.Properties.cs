using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Linux;

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
    public IControlsContainer Controls { get; }
    public ColorRgba BackgroundColor { get; set; } =
        Services.GetSingleton<ITheme>().BackgroundColor;
    public int Column { get; set; }
    public int Row { get; set; }
    public bool UseNativeTitleBar { get; set; }
    public IntPtr DisplayHandle { get; set; }
    public IntPtr WindowSurfaceHandle { get; set; }
    public string Title { get; set; }
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
    public bool Visible { get; set; }

    public bool IsMouseOver { get; set; }
}
