using System.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Linux;

partial class StackedLayout
{
    void SubscribeToInputHandlerEvents()
    {
        _inputHandler.MouseWheel += InputHandlerOnMouseWheel;
        _inputHandler.MouseMoved += OnMouseMoved;
    }

    void UnsubscribeFromInputHandlerEvents()
    {
        _inputHandler.MouseWheel -= InputHandlerOnMouseWheel;
        _inputHandler.MouseMoved -= OnMouseMoved;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        IsMouseOver = MouseHelpers.IsMouseOver(this, new Point(e.X, e.Y));
    }

    void InputHandlerOnMouseWheel(object? sender, MouseWheelInputArgs e)
    {
        if (!IsMouseOver)
            return;

        var rotation = e.Rotation;
        rotation /= 10;
        if (Math.Abs(rotation) <= 0)
            return;
        if (Scrollable == ScrollableMode.Vertical)
            ScrollableHelpers.Scroll(Direction.Vertical, rotation, this, ref _viewPort);
        else if (Scrollable == ScrollableMode.Horizontal)
            ScrollableHelpers.Scroll(Direction.Horizontal, rotation, this, ref _viewPort);
        else if (Scrollable == ScrollableMode.Both)
        {
            // TODO: implement both direction scrolling using mouse and shift key
        }
    }

    public EventHandler<Point>? LocationChanged { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }
    public EventHandler? Disposing { get; set; }

    void RaiseDisposing()
    {
        Disposing?.Invoke(this, System.EventArgs.Empty);
    }

    void OnDisposeInternal()
    {
        foreach (var c in _controls)
            c.Dispose();
        _controls.Clear();
        UnsubscribeFromInputHandlerEvents();
        RaiseDisposing();
    }
}
