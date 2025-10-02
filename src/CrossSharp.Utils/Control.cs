using System.Drawing;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract class Control<T>(T impl) : IControl
    where T : IControl // TODO: This should implementUtils.Linux.ControlBase, not gtkUtils.Linux.ControlBase
{
    protected T _impl = impl;

    public void Dispose() => _impl.Dispose();

    public IntPtr Handle
    {
        get => _impl.Handle;
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

    public void Redraw() => _impl.Redraw();

    public void SuspendLayout() => _impl.SuspendLayout();

    public void ResumeLayout() => _impl.ResumeLayout();

    public int BorderWidth
    {
        get => _impl.BorderWidth;
        set => _impl.BorderWidth = value;
    }
    public ColorRgba BorderColor
    {
        get => _impl.BorderColor;
        set => _impl.BorderColor = value;
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

    public void LimitClip(ref Graphics g) => _impl.LimitClip(ref g);

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
}
