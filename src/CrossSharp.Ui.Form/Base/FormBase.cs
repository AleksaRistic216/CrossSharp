using System.Collections;
using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Base;

public abstract class FormBase : IForm
{
    readonly IForm _formImpl = Services.GetSingleton<IFormFactory>().Create();
    public object Parent { get; set; } = null!;
    public IControlsContainer Controls => _formImpl.Controls;
    public IntPtr Handle => _formImpl.Handle;
    public ITitleBar? TitleBar => _formImpl.TitleBar;
    public IApplication AppInstance => _formImpl.AppInstance;
    public bool UseNativeTitleBar
    {
        get => _formImpl.UseNativeTitleBar;
        set { _formImpl.UseNativeTitleBar = value; }
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

    readonly int _minimumWidth = 300;
    public int Width
    {
        get => _formImpl.Width;
        set { _formImpl.Width = Math.Max(_minimumWidth, value); }
    }
    readonly int _minimumHeight = 250;
    public int Height
    {
        get => _formImpl.Height;
        set { _formImpl.Height = Math.Max(_minimumHeight, value); }
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

    public void Minimize() => _formImpl.Minimize();

    public void Maximize() => _formImpl.Maximize();

    public void Restore() => _formImpl.Restore();

    public void Show() => _formImpl.Show();

    public WindowState State
    {
        get => _formImpl.State;
        set { _formImpl.State = value; }
    }

    public void Close() => _formImpl.Close();

    public void Dispose() => _formImpl.Dispose();

    public void Initialize() => _formImpl.Initialize();

    public void Invalidate() => _formImpl.Invalidate();

    public void SuspendLayout() => _formImpl.SuspendLayout();

    public void ResumeLayout() => _formImpl.ResumeLayout();

    public void Draw(ref IGraphics graphics) => _formImpl.Draw(ref graphics);

    public int BorderWidth
    {
        get => _formImpl.BorderWidth;
        set { _formImpl.BorderWidth = value; }
    }
    public ColorRgba BorderColor
    {
        get => _formImpl.BorderColor;
        set { _formImpl.BorderColor = value; }
    }

    public ColorRgba BackgroundColor
    {
        get => _formImpl.BackgroundColor;
        set { _formImpl.BackgroundColor = value; }
    }

    public void LimitClip(ref IGraphics g) => _formImpl.LimitClip(ref g);
}
