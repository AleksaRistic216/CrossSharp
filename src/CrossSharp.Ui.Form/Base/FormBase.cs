using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Base;

public abstract class FormBase : IForm
{
    readonly IForm _formImpl = PlatformHelpers.GetCurrentPlatform() switch
    {
        CrossPlatformType.Windows => throw new NotImplementedException(),
        CrossPlatformType.Linux => new FormLinux.FormLinux(),
        CrossPlatformType.MacOs => throw new NotImplementedException(),
        _ => throw new NotSupportedException("Current platform is not supported"),
    };
    public IntPtr Handle
    {
        get => _formImpl.Handle;
        set { _formImpl.Handle = value; }
    }
    public IntPtr ParentHandle
    {
        get => _formImpl.ParentHandle;
        set { _formImpl.ParentHandle = value; }
    }
    public IApplication AppInstance => _formImpl.AppInstance;
    public IControlsContainer Controls => _formImpl.Controls;
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

    public void Dispose() => _formImpl.Dispose();

    public void Initialize() => _formImpl.Initialize();

    public void Invalidate() => _formImpl.Invalidate();

    public void Show() => _formImpl.Show();
}
