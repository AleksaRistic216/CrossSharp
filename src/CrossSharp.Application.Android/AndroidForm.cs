using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Application.Android;

/// <summary>
/// Android-specific Form implementation.
/// On Android, the Activity is the window, so this class provides a thin
/// wrapper that implements IForm while delegating to the Activity lifecycle.
/// </summary>
internal class AndroidForm : IForm
{
    private readonly IControlsContainer _controls;
    private string _title = "CrossSharp App";
    private bool _visible = true;
    private WindowState _state = WindowState.Normal;
    private int _width = 1080;
    private int _height = 1920;
    private Point _location = Point.Empty;
    private ColorRgba _backgroundColor = new(255, 255, 255, 255);
    private ColorRgba _borderColor = ColorRgba.Transparent;
    private Margin _margin = Margin.Zero;

    public AndroidForm()
    {
        // Create the root controls container using the factory
        _controls = new StaticLayout();
    }

    public void Initialize()
    {
        // Initialization complete
    }

    public object? Parent { get; set; }
    public IControlsContainer Controls => _controls;
    public IntPtr Handle => IntPtr.Zero; // Android doesn't use Win32 handles
    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            TitleChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public ITitleBar? TitleBar => null; // Android handles title bar via Activity
    public EventHandler? TitleChanged { get; set; }

    public void PerformTheme()
    {
        ThemePerformed?.Invoke(this, EventArgs.Empty);
    }

    public EventHandler? ThemePerformed { get; set; }

    public bool Visible
    {
        get => _visible;
        set => _visible = value;
    }

    public EventHandler? Shown { get; set; }
    public EventHandler? OnClose { get; set; }

    public int Width
    {
        get => _width;
        set
        {
            _width = value;
            SizeChanged?.Invoke(this, new Size(_width, _height));
        }
    }

    public int Height
    {
        get => _height;
        set
        {
            _height = value;
            SizeChanged?.Invoke(this, new Size(_width, _height));
        }
    }

    public EventHandler<Size>? SizeChanged { get; set; }

    public Point Location
    {
        get => _location;
        set
        {
            _location = value;
            LocationChanged?.Invoke(this, _location);
        }
    }

    public EventHandler<Point>? LocationChanged { get; set; }

    public void Minimize()
    {
        // On Android, minimize would move app to background
        // This would typically be handled by the Activity
    }

    public void Maximize()
    {
        // Android apps are typically full screen
    }

    public void Restore()
    {
        // No-op on Android
    }

    public void Show()
    {
        _visible = true;
        Shown?.Invoke(this, EventArgs.Empty);
    }

    public void Redraw()
    {
        Invalidate();
    }

    public WindowState State
    {
        get => _state;
        set
        {
            _state = value;
            StateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public EventHandler? StateChanged { get; set; }

    public void Move(Point location)
    {
        Location = location;
    }

    public void Close()
    {
        OnClose?.Invoke(this, EventArgs.Empty);
    }

    public void Dispose()
    {
        Disposing?.Invoke(this, EventArgs.Empty);
        (_controls as IDisposable)?.Dispose();
    }

    public void Invalidate()
    {
        Invalidated?.Invoke(this, EventArgs.Empty);
    }

    public EventHandler? Invalidated { get; set; }

    public void Draw(ref IGraphics graphics)
    {
        // The controls will be drawn by the CrossSharpView
    }

    public EventHandler? Disposing { get; set; }

    public int Index { get; set; }

    public bool NoClip { get; set; }

    public int BorderWidth { get; set; }

    public ColorRgba BorderColor
    {
        get => _borderColor;
        set => _borderColor = value;
    }

    public ColorRgba BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            BackgroundColorChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public EventHandler? BackgroundColorChanged { get; set; }

    public bool IsMouseOver { get; set; }

    public Margin Margin
    {
        get => _margin;
        set
        {
            _margin = value;
            MarginChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public EventHandler? MarginChanged { get; set; }
}
