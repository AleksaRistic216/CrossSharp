using System.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
sealed partial class FormSDLTitleBar
{
    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver)
            return;
        var mousePoint = new Point(e.X, e.Y);

        var now = DateTime.UtcNow;
        if (IsDoubleClick(mousePoint, now))
        {
            _lastClickTime = null;
            _lastClickPosition = null;
            ToggleMaximize();
            return;
        }

        _lastClickTime = now;
        _lastClickPosition = mousePoint;
    }

    bool IsDoubleClick(Point currentPoint, DateTime now)
    {
        if (_lastClickTime is null || _lastClickPosition is null)
            return false;

        var timeSinceLastClick = (now - _lastClickTime.Value).TotalMilliseconds;
        if (timeSinceLastClick > DOUBLE_CLICK_THRESHOLD_MS)
            return false;

        var dx = Math.Abs(currentPoint.X - _lastClickPosition.Value.X);
        var dy = Math.Abs(currentPoint.Y - _lastClickPosition.Value.Y);
        return dx <= DOUBLE_CLICK_DISTANCE_THRESHOLD && dy <= DOUBLE_CLICK_DISTANCE_THRESHOLD;
    }

    void ToggleMaximize()
    {
        if (Form.State == WindowState.Maximized)
            Form.Restore();
        else
            Form.Maximize();
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
    }
}
