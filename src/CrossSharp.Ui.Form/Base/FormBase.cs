using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Base;

public abstract class FormBase : IForm
{
    readonly IForm _formImpl = Services.GetSingleton<IFormFactory>().Create();
    public object Parent { get; set; } = null!;
    public ITitleBar TitleBar => _formImpl.TitleBar;
    public IApplication AppInstance => _formImpl.AppInstance;
    public IControlsContainer Controls => _formImpl.Controls;
    public IntPtr DisplayHandle
    {
        get => _formImpl.DisplayHandle;
        set { _formImpl.DisplayHandle = value; }
    }
    public IntPtr WindowSurfaceHandle
    {
        get => _formImpl.WindowSurfaceHandle;
        set { _formImpl.WindowSurfaceHandle = value; }
    }
    public IntPtr Handle
    {
        get => ((IControl)_formImpl).Handle;
        set { ((IControl)_formImpl).Handle = value; }
    }
    public IntPtr ParentHandle
    {
        get => _formImpl.ParentHandle;
        set { _formImpl.ParentHandle = value; }
    }
    public int ZIndex
    {
        get => _formImpl.ZIndex;
        set { _formImpl.ZIndex = value; }
    }
    public bool Visible
    {
        get => _formImpl.Visible;
        set { _formImpl.Visible = value; }
    }

    public EventHandler? OnShow
    {
        get => _formImpl.OnShow;
        set { _formImpl.OnShow = value; }
    }
    public EventHandler? OnClose
    {
        get => _formImpl.OnClose;
        set { _formImpl.OnClose = value; }
    }

    public int Width
    {
        get => _formImpl.Width;
        set { _formImpl.Width = value; }
    }
    public int Height
    {
        get => _formImpl.Height;
        set { _formImpl.Height = value; }
    }
    public EventHandler<Size>? OnSizeChanged
    {
        get => _formImpl.OnSizeChanged;
        set { _formImpl.OnSizeChanged = value; }
    }
    public Point Location
    {
        get => _formImpl.Location;
        set { _formImpl.Location = value; }
    }
    public EventHandler<Point>? OnLocationChanged
    {
        get => _formImpl.OnLocationChanged;
        set { _formImpl.OnLocationChanged = value; }
    }
    public string Title
    {
        get => _formImpl.Title;
        set { _formImpl.Title = value; }
    }

    public void PerformLayout() => _formImpl.PerformLayout();

    public void Close() => _formImpl.Close();

    public void Dispose() => _formImpl.Dispose();

    public void Initialize() => _formImpl.Initialize();

    public void Invalidate() => _formImpl.Invalidate();

    public void Show() => _formImpl.Show();

    public ColorRgba BackgroundColor
    {
        get => _formImpl.BackgroundColor;
        set { _formImpl.BackgroundColor = value; }
    }
}
