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
    IForm Form => (IForm)Parent!;

    CancellationTokenSource? _formDragCancellationTokenSource;

    // ReSharper disable once NotAccessedField.Local
    Task? _formDragTask;
    Point? _formDragDestination;
    int CoreFps => Services.GetSingleton<IApplicationConfiguration>().CoreFps;
    DateTime? _lastFormDragTime;
    const int MOVEMENT_THRESHOLD = 1;
    Point? _mouseDownMousePosition;
    Point? _mouseDownFormPosition;
    int _deltaX;
    int _deltaY;

    internal FormSDLTitleBar()
    {
        InputHandler.MouseMoved += OnMouseMoved;
        InputHandler.MousePressed += OnMousePressed;
        InputHandler.MouseDragged += OnMouseDragged;
        InputHandler.MouseReleased += OnMouseReleased;
    }

    void StartMovingForm()
    {
        _formDragCancellationTokenSource = new CancellationTokenSource();
        _formDragTask = Task.Run(
            () =>
            {
                while (!_formDragCancellationTokenSource.IsCancellationRequested)
                {
                    if (_formDragDestination is null)
                        continue;
                    int targetDelay = (int)(1000f / CoreFps);
                    int timeSinceLastDrag = _lastFormDragTime is null
                        ? int.MaxValue
                        : (int)(DateTime.Now - _lastFormDragTime.Value).TotalMilliseconds;
                    if (timeSinceLastDrag < targetDelay)
                        continue;
                    _lastFormDragTime = DateTime.Now;
                    Form.Move(_formDragDestination.Value);
                }
            },
            _formDragCancellationTokenSource.Token
        );
    }

    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        _formDragCancellationTokenSource?.Cancel();
        _mouseDownMousePosition = null;
        _mouseDownFormPosition = null;
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        _mouseDownMousePosition = new Point(e.X, e.Y);
        _mouseDownFormPosition = Form.Location;
        StartMovingForm();
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (!IsMouseOver || _mouseDownMousePosition is null || _mouseDownFormPosition is null)
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

    public override void Dispose()
    {
        base.Dispose();
        InputHandler.MouseMoved -= OnMouseMoved;
        InputHandler.MousePressed -= OnMousePressed;
        InputHandler.MouseDragged -= OnMouseDragged;
        InputHandler.MouseReleased -= OnMouseReleased;
        _formDragCancellationTokenSource?.Cancel();
        _formDragTask = null;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
    }
}
