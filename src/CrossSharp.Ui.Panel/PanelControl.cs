using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class PanelControl : IGtkWidget, IBackgroundColorProvider
{
    readonly IPanelControl _impl = ServicesPool.GetSingleton<IPanelControlFactory>().Create();

    public IntPtr Handle
    {
        get => _impl.Handle;
        set => _impl.Handle = value;
    }
    public IntPtr ParentHandle
    {
        get => _impl.ParentHandle;
        set => _impl.ParentHandle = value;
    }
    public object Parent
    {
        get => _impl.Parent;
        set => _impl.Parent = value;
    }
    public int ZIndex
    {
        get => _impl.ZIndex;
        set => _impl.ZIndex = value;
    }
    public bool Visible
    {
        get => _impl.Visible;
        set => _impl.Visible = value;
    }

    public void Initialize() => _impl.Initialize();

    public void Invalidate() => _impl.Invalidate();

    public void Show() => _impl.Show();

    public int Width
    {
        get => _impl.Width;
        set => _impl.Width = value;
    }
    public int Height
    {
        get => _impl.Height;
        set => _impl.Height = value;
    }
    public EventHandler<Size>? OnSizeChanged
    {
        get => _impl.OnSizeChanged;
        set => _impl.OnSizeChanged = value;
    }
    public Point Location
    {
        get => _impl.Location;
        set => _impl.Location = value;
    }
    public EventHandler<Point>? OnLocationChanged
    {
        get => _impl.OnLocationChanged;
        set => _impl.OnLocationChanged = value;
    }

    public void DrawShadows(Graphics g) => _impl.DrawShadows(g);

    public void DrawBackground(Graphics g) => _impl.DrawBackground(g);

    public void DrawBorders(Graphics g) => _impl.DrawBorders(g);

    public void DrawContent(Graphics g) => _impl.DrawContent(g);

    public ColorRgba BackgroundColor
    {
        get => _impl.BackgroundColor;
        set => _impl.BackgroundColor = value;
    }

    public void Dispose() => _impl.Dispose();
}
