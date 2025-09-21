using System.Drawing;
using CrossSharp.Utils.Input;

namespace CrossSharp.Utils;

public partial class Control
{
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }

    void SubscribeToInputEvents()
    {
        InputHandler.MouseMoved += OnMouseMoved;
    }

    internal virtual void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = GetScreenBounds().Contains(e.X, e.Y);
    }

    protected virtual void RaiseOnSizeChanged(Size newSize)
    {
        OnSizeChanged?.Invoke(this, newSize);
    }

    protected virtual void RaiseOnLocationChanged(Point newLocation)
    {
        OnLocationChanged?.Invoke(this, newLocation);
    }
}
