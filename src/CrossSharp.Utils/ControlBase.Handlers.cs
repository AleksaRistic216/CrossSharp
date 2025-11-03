using System.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

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
        if (!MouseHelpers.IsEventForThisForm(this))
            return;
        if (this is IFocusable f)
            f.IsFocused = IsMouseOver;
        if (IsMouseOver && this is IClickable c)
        {
            MainThreadDispatcher.Invoke(() =>
            {
                c.Click?.Invoke(this, e);
            });
        }
    }

    internal virtual void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
    }

    protected virtual void RaiseOnSizeChanged(Size newSize)
    {
        SizeChanged?.Invoke(this, newSize);
    }

    protected virtual void RaiseOnLocationChanged(Point newLocation)
    {
        LocationChanged?.Invoke(this, newLocation);
    }

    public EventHandler? MarginChanged { get; set; }

    void RaiseMarginChanged()
    {
        MarginChanged?.Invoke(this, System.EventArgs.Empty);
    }

    void OnMarginChangedInternal(object? sender, System.EventArgs e)
    {
        RaiseMarginChanged();
    }

    public EventHandler? Disposing { get; set; }

    void RaiseDisposing()
    {
        Disposing?.Invoke(this, System.EventArgs.Empty);
    }

    void OnDisposingInternal()
    {
        SizeChanged = null;
        LocationChanged = null;
        InputHandler.MouseMoved -= OnMouseMoved;
        RaiseDisposing();
    }
}
