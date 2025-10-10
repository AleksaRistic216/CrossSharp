using System.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract class Control<T>(T impl) : IControl
    where T : IControl // TODO: This should implementUtils.Linux.ControlBase, not gtkUtils.Linux.ControlBase
{
    T _impl = impl;

    public void Dispose() => _impl.Dispose();

    public bool Visible
    {
        get => _impl.Visible;
        set => _impl.Visible = value;
    }

    public void Initialize() => _impl.Initialize();

    public void Invalidate() => _impl.Invalidate();

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

    public void Draw(ref IGraphics graphics) => _impl.Draw(ref graphics);

    public void LimitClip(ref IGraphics g) => _impl.LimitClip(ref g);

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
