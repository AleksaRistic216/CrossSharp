using System.Drawing;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public abstract class CrossControl<T>(T implementation) : IControl
    where T : IControl
{
    protected T Implementation = implementation;

    public void Dispose() => Implementation.Dispose();

    public int BorderWidth
    {
        get => Implementation.BorderWidth;
        set => Implementation.BorderWidth = value;
    }
    public ColorRgba BorderColor
    {
        get => Implementation.BorderColor;
        set => Implementation.BorderColor = value;
    }

    public void LimitClip(ref IGraphics g) => Implementation.LimitClip(ref g);

    public Point Location
    {
        get => Implementation.Location;
        set => Implementation.Location = value;
    }
    public EventHandler<Point>? OnLocationChanged
    {
        get => Implementation.OnLocationChanged;
        set => Implementation.OnLocationChanged = value;
    }
    public int Width
    {
        get => Implementation.Width;
        set => Implementation.Width = value;
    }
    public int Height
    {
        get => Implementation.Height;
        set => Implementation.Height = value;
    }
    public EventHandler<Size>? OnSizeChanged
    {
        get => Implementation.OnSizeChanged;
        set => Implementation.OnSizeChanged = value;
    }
    public object Parent
    {
        get => Implementation.Parent;
        set => Implementation.Parent = value;
    }
    public bool Visible
    {
        get => Implementation.Visible;
        set => Implementation.Visible = value;
    }

    public void Initialize() => Implementation.Initialize();

    public void Invalidate() => Implementation.Invalidate();

    public void SuspendLayout() => Implementation.SuspendLayout();

    public void ResumeLayout() => Implementation.ResumeLayout();

    public void Draw(ref IGraphics graphics) => Implementation.Draw(ref graphics);

    public IForm? GetForm() => Implementation.GetForm();
}
