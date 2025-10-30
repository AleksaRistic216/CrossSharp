using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract class FormBase<T> : IForm
    where T : class, IFormFactory
{
    protected readonly IForm Implementation = Services.GetSingleton<T>().Create();
    public object Parent
    {
        get => Implementation.Parent;
        set { Implementation.Parent = value; }
    }
    public IControlsContainer Controls => Implementation.Controls;
    public IntPtr Handle => Implementation.Handle;
    public ITitleBar? TitleBar => Implementation.TitleBar;
    public EventHandler? TitleChanged
    {
        get => Implementation.TitleChanged;
        set { Implementation.TitleChanged = value; }
    }
    public IApplication AppInstance => Implementation.AppInstance;
    public bool UseNativeTitleBar
    {
        get => Implementation.UseNativeTitleBar;
        set { Implementation.UseNativeTitleBar = value; }
    }
    public bool Visible
    {
        get => Implementation.Visible;
        set { Implementation.Visible = value; }
    }

    public EventHandler? Shown
    {
        get => Implementation.Shown;
        set { Implementation.Shown = value; }
    }
    public EventHandler? OnClose
    {
        get => Implementation.OnClose;
        set { Implementation.OnClose = value; }
    }

    readonly int _minimumWidth = 300;
    public int Width
    {
        get => Implementation.Width;
        set { Implementation.Width = Math.Max(_minimumWidth, value); }
    }
    readonly int _minimumHeight = 250;
    public int Height
    {
        get => Implementation.Height;
        set { Implementation.Height = Math.Max(_minimumHeight, value); }
    }
    public EventHandler<Size>? SizeChanged
    {
        get => Implementation.SizeChanged;
        set { Implementation.SizeChanged = value; }
    }
    public Point Location
    {
        get => Implementation.Location;
        set { Implementation.Location = value; }
    }
    public EventHandler<Point>? LocationChanged
    {
        get => Implementation.LocationChanged;
        set { Implementation.LocationChanged = value; }
    }
    public string Title
    {
        get => Implementation.Title;
        set { Implementation.Title = value; }
    }

    public void Minimize() => Implementation.Minimize();

    public void Maximize() => Implementation.Maximize();

    public void Restore() => Implementation.Restore();

    public void Show() => Implementation.Show();

    public void Redraw() => Implementation.Redraw();

    public WindowState State
    {
        get => Implementation.State;
        set { Implementation.State = value; }
    }

    public void Close() => Implementation.Close();

    public void Dispose() => Implementation.Dispose();

    public void Initialize() => Implementation.Initialize();

    public virtual void Invalidate() => Implementation.Invalidate();

    public void SuspendLayout() => Implementation.SuspendLayout();

    public void ResumeLayout() => Implementation.ResumeLayout();

    public void Draw(ref IGraphics graphics) => Implementation.Draw(ref graphics);

    public int BorderWidth
    {
        get => Implementation.BorderWidth;
        set { Implementation.BorderWidth = value; }
    }
    public ColorRgba BorderColor
    {
        get => Implementation.BorderColor;
        set { Implementation.BorderColor = value; }
    }

    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set { Implementation.BackgroundColor = value; }
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set { Implementation.BackgroundColorChanged = value; }
    }

    public void LimitClip(ref IGraphics g) => Implementation.LimitClip(ref g);

    public bool IsMouseOver
    {
        get => Implementation.IsMouseOver;
        set { Implementation.IsMouseOver = value; }
    }
}
