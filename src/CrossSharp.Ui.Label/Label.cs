using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Label : ILabel
{
    readonly ILabel _impl = ServicesPool.GetSingleton<ILabelFactory>().Create();

    public void Dispose() => _impl.Dispose();

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

    public EventHandler<EventArgs>? OnTextChanged
    {
        get => _impl.OnTextChanged;
        set => _impl.OnTextChanged = value;
    }
    public string Text
    {
        get => _impl.Text;
        set => _impl.Text = value;
    }
    public string FontFamily
    {
        get => _impl.FontFamily;
        set => _impl.FontFamily = value;
    }
    public int FontSize
    {
        get => _impl.FontSize;
        set => _impl.FontSize = value;
    }
}
