using System.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils;

public partial class ControlBase
{
    public EventHandler<Size>? SizeChanged { get; set; }
    public EventHandler<Point>? LocationChanged { get; set; }

    void SubscribeToInputEvents()
    {
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (this is IFocusable f)
            f.IsFocused = IsMouseOver;

        if (IsMouseOver && this is IClickable c)
            c.OnClick?.Invoke(this, e);
    }

    internal virtual void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this.GetScreenBounds(), new Point(e.X, e.Y));
    }

    protected virtual void RaiseOnSizeChanged(Size newSize)
    {
        SizeChanged?.Invoke(this, newSize);
    }

    protected virtual void RaiseOnLocationChanged(Point newLocation)
    {
        LocationChanged?.Invoke(this, newLocation);
    }
}
