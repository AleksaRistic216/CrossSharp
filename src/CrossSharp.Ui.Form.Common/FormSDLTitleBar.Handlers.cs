using System.Drawing;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar
{
    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        _formDragCancellationTokenSource?.Cancel();
        _mouseDownMousePosition = null;
        _mouseDownFormPosition = null;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver)
            return;
        var mousePoint = new Point(e.X, e.Y);
        if (!IsWithinDraggableBounds(mousePoint))
            return;
        _mouseDownMousePosition = mousePoint;
        _mouseDownFormPosition = Form.Location;
        StartMovingForm();
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver || _mouseDownMousePosition is null || _mouseDownFormPosition is null)
            return;
        var mousePoint = new Point(e.X, e.Y);
        if (!IsWithinDraggableBounds(mousePoint))
            return;
        var dx = e.X - _mouseDownMousePosition.Value.X;
        var dy = e.Y - _mouseDownMousePosition.Value.Y;
        if (Math.Abs(dx - _deltaX) < MOVEMENT_THRESHOLD && Math.Abs(dy - _deltaY) < MOVEMENT_THRESHOLD)
            return;
        _deltaX = dx;
        _deltaY = dy;
        var newLocation = new Point(_mouseDownFormPosition.Value.X + _deltaX, _mouseDownFormPosition.Value.Y + _deltaY);
        _formDragDestination = newLocation;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
    }
}
