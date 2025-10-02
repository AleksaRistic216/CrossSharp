using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Gtk;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class Form
{
    GdkMonitor? _monitor;
    ColorRgba _backgroundColor = Services.GetSingleton<ITheme>().BackgroundColor;
    int _width = 800;
    int _lastNormalStateWidth = 800;
    int _height = 600;
    int _lastNormalStateHeight = 600;
    Point _location = new(100, 100);
    Point _lastNormalStateLocation = new Point(100, 100);
    string _title = "Form";
    bool _useNativeTitleBar = Services.GetSingleton<ITheme>().UseNativeTitleBar;
    bool _suspendLayout = false;
    public object Parent { get; set; } = null!;
    public bool UseNativeTitleBar
    {
        get => _useNativeTitleBar;
        set
        {
            if (_useNativeTitleBar == value)
                return;
            _useNativeTitleBar = value;
            RaiseOnNativeTitleBarChanged();
        }
    }
    public int ZIndex { get; set; }
    public IntPtr DisplayHandle { get; set; }
    public IntPtr WindowSurfaceHandle { get; set; }
    public string Title
    {
        get => _title;
        set
        {
            if (_title == value)
                return;
            _title = value;
            if (Handle != IntPtr.Zero)
                GtkHelpers.gtk_window_set_title(Handle, _title);
        }
    }
    public ITitleBar? TitleBar { get; private set; }
    public IApplication AppInstance { get; }
    public IControlsContainer Controls { get; private set; }
    WindowState _state = WindowState.Normal;
    public WindowState State
    {
        get => _state;
        set
        {
            if (_state == value)
                return;
            if (_state == WindowState.Normal)
            {
                _lastNormalStateHeight = Height;
                _lastNormalStateWidth = Width;
                _lastNormalStateLocation = Location;
            }
            _state = value;

            if (_monitor is null)
                return;
            if (_state == WindowState.Maximized)
            {
                var monitorGeometry = _monitor.GetGeometry();
                // -1 because when set to full size, its state changes to maximized which breaks a lot of things
                SuspendLayout();
                Location = new Point(monitorGeometry.x, monitorGeometry.y);
                Width = monitorGeometry.width - 1;
                Height = monitorGeometry.height - 1;
                ResumeLayout();
                return;
            }
            if (_state == WindowState.Normal)
            {
                SuspendLayout();
                Location = _lastNormalStateLocation;
                Width = _lastNormalStateWidth;
                Height = _lastNormalStateHeight;
                ResumeLayout();
                return;
            }
            if (_state == WindowState.Minimized)
            {
                GtkHelpers.gtk_window_minimize(Handle);
                return;
            }
        }
    }
    public IntPtr Handle { get; set; }
    public IntPtr ParentHandle { get; set; }
    public bool Visible { get; set; }
    public int BorderWidth
    {
        get => Controls.BorderWidth;
        set
        {
            if (Controls.BorderWidth == value)
                return;
            Controls.BorderWidth = value;
            Controls.Redraw();
        }
    }
    public ColorRgba BorderColor
    {
        get => Controls.BorderColor;
        set
        {
            if (Controls.BorderColor == value)
                return;
            Controls.BorderColor = value;
            Controls.Redraw();
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
            if (!_suspendLayout)
                PerformSize();
            RaiseOnSizeChanged(new Size(_width, _height));
        }
    }
    public int Height
    {
        get => _height;
        set
        {
            if (_height == value)
                return;
            _height = value;
            if (!_suspendLayout)
                PerformSize();
            RaiseOnSizeChanged(new Size(_width, _height));
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
            if (!_suspendLayout)
                PerformLocation();
            RaiseOnLocationChanged(_location);
        }
    }
    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            if (_backgroundColor == value)
                return;
            _backgroundColor = value;
            RaiseOnBackgroundColorChanged(_backgroundColor);
        }
    }
}
