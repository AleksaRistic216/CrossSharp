using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
class FormSDLTitleBar : StackedLayout, IMouseTargetable
{
    IInputHandler InputHandler => Services.GetSingleton<IInputHandler>();
    Point _dragStartMousePosition = Point.Empty;
    Point _dragStartWindowPosition = Point.Empty;
    bool _isDragging = false;
    DateTime _lastFormLocationUpdate = DateTime.MinValue;
    static readonly TimeSpan _formLocationUpdateInterval = TimeSpan.FromMilliseconds(
        1000 / Math.Max(1, Math.Min(30, Services.GetSingleton<IApplicationConfiguration>().CoreFps))
    );
    const int MOVEMENT_THRESHOLD = 1;

    internal FormSDLTitleBar()
    {
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;
        InputHandler.MouseDragged += OnMouseDragged;
        InputHandler.MouseReleased += OnMouseReleased;
    }

    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        _isDragging = false;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver)
            return;
        if (Parent is not IFormSDL form)
            return;
        _isDragging = true;
        _dragStartMousePosition = new Point(e.X, e.Y);
        _dragStartWindowPosition = form.Location;
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (Parent is not IFormSDL form)
            return;
        if (!IsMouseOver)
        {
            _isDragging = false;
            return;
        }
        if (!_isDragging)
        {
            int deltaX = Math.Abs(e.X - form.Location.X);
            int deltaY = Math.Abs(e.Y - form.Location.Y);
            if (deltaX < MOVEMENT_THRESHOLD && deltaY < MOVEMENT_THRESHOLD)
                return;
            _isDragging = true;
            _dragStartMousePosition = new Point(e.X, e.Y);
            _dragStartWindowPosition = form.Location;
        }

        // Throttle form movement updates
        var now = DateTime.UtcNow;
        if (now - _lastFormLocationUpdate < _formLocationUpdateInterval)
            return;
        _lastFormLocationUpdate = now;

        var moveDeltaX = e.X - _dragStartMousePosition.X;
        var moveDeltaY = e.Y - _dragStartMousePosition.Y;
        form.Move(new Point(_dragStartWindowPosition.X + moveDeltaX, _dragStartWindowPosition.Y + moveDeltaY));
    }

    public override void Dispose()
    {
        base.Dispose();
        InputHandler.MouseMoved -= OnMouseMoved;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
    }
}
