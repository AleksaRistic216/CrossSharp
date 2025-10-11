using System.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Utils;

public partial class ControlBase
{
    public EventHandler<Size>? OnSizeChanged { get; set; }
    public EventHandler<Point>? OnLocationChanged { get; set; }

    void SubscribeToInputEvents()
    {
        InputHandler.MouseMoved += OnMouseMoved;
    }

    internal virtual void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        var screenBounds = this.GetScreenBounds();
        IsMouseOver = screenBounds.Contains(e.X, e.Y);
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
