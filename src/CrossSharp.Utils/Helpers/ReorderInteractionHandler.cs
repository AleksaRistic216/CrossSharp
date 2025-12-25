using System.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

/// <summary>
/// Handles drag-and-drop reordering for controls within a StackedLayout.
/// Detects clicks on grabber areas and tracks drag state to reorder controls.
/// </summary>
internal sealed class ReorderInteractionHandler<T> : IDisposable
    where T : IStackedLayout, IControl
{
    readonly IInputHandler _inputHandler;
    readonly T _stackedLayout;
    readonly Func<bool> _getIsMouseOver;
    readonly Action<IControl?, int> _updateDragState;
    readonly Action<IControl, int> _onReorderCompleted;

    bool _isSubscribed;
    IControl? _draggedControl;
    Point _dragStartPosition;
    bool _isDragging;
    int _currentDropIndex = -1;

    public ReorderInteractionHandler(
        IInputHandler inputHandler,
        T stackedLayout,
        Func<bool> getIsMouseOver,
        Action<IControl?, int> updateDragState,
        Action<IControl, int> onReorderCompleted
    )
    {
        _inputHandler = inputHandler;
        _stackedLayout = stackedLayout;
        _getIsMouseOver = getIsMouseOver;
        _updateDragState = updateDragState;
        _onReorderCompleted = onReorderCompleted;
    }

    public void Subscribe()
    {
        if (_isSubscribed)
            return;
        _inputHandler.MousePressed += OnMousePressed;
        _inputHandler.MouseDragged += OnMouseDragged;
        _inputHandler.MouseReleased += OnMouseReleased;
        _isSubscribed = true;
    }

    public void Unsubscribe()
    {
        if (!_isSubscribed)
            return;
        _inputHandler.MousePressed -= OnMousePressed;
        _inputHandler.MouseDragged -= OnMouseDragged;
        _inputHandler.MouseReleased -= OnMouseReleased;
        _isSubscribed = false;
    }

    public void Dispose()
    {
        Unsubscribe();
        _draggedControl = null;
        _isDragging = false;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!_stackedLayout.ReorderEnabled)
            return;
        if (!_getIsMouseOver())
            return;
        if (e.Button != MouseButton.Left)
            return;

        var mousePoint = new Point(e.X, e.Y);

        // Check if we clicked on a grabber
        foreach (var control in _stackedLayout.Where(c => c.Visible).OrderBy(c => c.Index))
        {
            var grabberRect = GetGrabberScreenRect(control);
            if (grabberRect.Contains(mousePoint))
            {
                _draggedControl = control;
                _dragStartPosition = mousePoint;
                _isDragging = false;
                return;
            }
        }
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (_draggedControl is null)
            return;

        var mousePoint = new Point(e.X, e.Y);

        // Check if we've moved enough to start dragging
        if (!_isDragging)
        {
            var distance = Math.Abs(
                _stackedLayout.Orientation == Orientation.Vertical
                    ? mousePoint.Y - _dragStartPosition.Y
                    : mousePoint.X - _dragStartPosition.X
            );
            if (distance < 5)
                return;
            _isDragging = true;
            CursorHelper.SetGrabCursor();
        }

        // Calculate drop target index based on mouse position
        _currentDropIndex = CalculateDropIndex(mousePoint);
        _updateDragState(_draggedControl, _currentDropIndex);
    }

    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        if (_draggedControl is null)
            return;

        var control = _draggedControl;
        var dropIndex = _currentDropIndex;
        var wasDragging = _isDragging;

        // Clear drag state
        _draggedControl = null;
        _isDragging = false;
        _currentDropIndex = -1;
        _updateDragState(null, -1);

        // Reset cursor if we were dragging
        if (wasDragging)
            CursorHelper.ResetCursor();

        // Perform reorder if needed
        if (dropIndex >= 0 && dropIndex != control.Index)
        {
            _onReorderCompleted(control, dropIndex);
        }
    }

    /// <summary>
    /// Gets the screen rectangle for the grabber area of a control.
    /// </summary>
    internal Rectangle GetGrabberScreenRect(IControl control)
    {
        var controlScreenBounds = control.GetScreenBounds();
        var grabberSize = _stackedLayout.GrabberSize;

        if (_stackedLayout.Orientation == Orientation.Vertical)
        {
            // Grabber is on the left side
            return new Rectangle(
                controlScreenBounds.X - grabberSize,
                controlScreenBounds.Y,
                grabberSize,
                controlScreenBounds.Height
            );
        }
        else
        {
            // Grabber is above the control
            return new Rectangle(
                controlScreenBounds.X,
                controlScreenBounds.Y - grabberSize,
                controlScreenBounds.Width,
                grabberSize
            );
        }
    }

    /// <summary>
    /// Gets the local rectangle for the grabber area relative to the StackedLayout.
    /// </summary>
    internal Rectangle GetGrabberLocalRect(IControl control)
    {
        var grabberSize = _stackedLayout.GrabberSize;

        if (_stackedLayout.Orientation == Orientation.Vertical)
        {
            // Grabber is on the left side, at the control's Y position
            return new Rectangle(_stackedLayout.Padding.Left, control.Location.Y, grabberSize, control.Height);
        }
        else
        {
            // Grabber is above the control, at the control's X position
            return new Rectangle(control.Location.X, _stackedLayout.Padding.Top, control.Width, grabberSize);
        }
    }

    int CalculateDropIndex(Point screenMousePosition)
    {
        var controls = _stackedLayout.Where(c => c.Visible).OrderBy(c => c.Index).ToList();
        if (controls.Count == 0)
            return -1;

        if (_stackedLayout.Orientation == Orientation.Vertical)
        {
            // For vertical layout, check Y positions
            for (var i = 0; i < controls.Count; i++)
            {
                var control = controls[i];
                var controlScreenBounds = control.GetScreenBounds();
                var midY = controlScreenBounds.Y + controlScreenBounds.Height / 2;

                if (screenMousePosition.Y < midY)
                    return i; // Return list position, not Index
            }
            // If past all controls, return count (insert at end)
            return controls.Count;
        }
        else
        {
            // For horizontal layout, check X positions
            for (var i = 0; i < controls.Count; i++)
            {
                var control = controls[i];
                var controlScreenBounds = control.GetScreenBounds();
                var midX = controlScreenBounds.X + controlScreenBounds.Width / 2;

                if (screenMousePosition.X < midX)
                    return i; // Return list position, not Index
            }
            // If past all controls, return count (insert at end)
            return controls.Count;
        }
    }
}
