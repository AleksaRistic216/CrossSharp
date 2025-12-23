using System.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Input;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Utils.Helpers;

/// <summary>
/// Handles scrollbar click, drag, and wheel interactions for scrollable controls.
/// Provides throttling, deadzone detection, and async updates for smooth scrolling.
/// </summary>
internal sealed class ScrollbarInteractionHandler<T> : IDisposable
    where T : IScrollable, ISizeProvider, ILocationProvider, IChild, IControl
{
    readonly IInputHandler _inputHandler;
    readonly T _scrollable;
    readonly Func<Rectangle> _getViewport;
    readonly Action<Rectangle> _setViewport;
    readonly Action _onScrolled;
    readonly Func<bool> _getIsMouseOver;
    readonly Action<bool> _setIsMouseOver;

    ScrollableHelpers.ScrollDragController? _dragController;
    bool _isSubscribed;

    public ScrollbarInteractionHandler(
        IInputHandler inputHandler,
        T scrollable,
        Func<Rectangle> getViewport,
        Action<Rectangle> setViewport,
        Action onScrolled,
        Func<bool> getIsMouseOver,
        Action<bool> setIsMouseOver
    )
    {
        _inputHandler = inputHandler;
        _scrollable = scrollable;
        _getViewport = getViewport;
        _setViewport = setViewport;
        _onScrolled = onScrolled;
        _getIsMouseOver = getIsMouseOver;
        _setIsMouseOver = setIsMouseOver;
    }

    public void Subscribe()
    {
        if (_isSubscribed)
            return;
        _inputHandler.MouseMoved += OnMouseMoved;
        _inputHandler.MousePressed += OnMousePressed;
        _inputHandler.MouseDragged += OnMouseDragged;
        _inputHandler.MouseReleased += OnMouseReleased;
        _inputHandler.MouseWheel += OnMouseWheel;
        _isSubscribed = true;
    }

    public void Unsubscribe()
    {
        if (!_isSubscribed)
            return;
        _inputHandler.MouseMoved -= OnMouseMoved;
        _inputHandler.MousePressed -= OnMousePressed;
        _inputHandler.MouseDragged -= OnMouseDragged;
        _inputHandler.MouseReleased -= OnMouseReleased;
        _inputHandler.MouseWheel -= OnMouseWheel;
        _isSubscribed = false;
    }

    public void Dispose()
    {
        Unsubscribe();
        _dragController = null;
    }

    void OnMouseMoved(object? sender, MouseInputArgs e)
    {
        _setIsMouseOver(MouseHelpers.IsMouseOver(_scrollable, new Point(e.X, e.Y)));
    }

    void OnMousePressed(object? sender, MouseInputArgs e)
    {
        if (!_getIsMouseOver())
            return;
        if (e.Button != MouseButton.Left)
            return;

        var mousePoint = new Point(e.X, e.Y);
        var verticalTrack = ScrollableHelpers.GetVerticalScrollTrackRect(_scrollable);
        var horizontalTrack = ScrollableHelpers.GetHorizontalScrollTrackRect(_scrollable);

        if (!verticalTrack.IsEmpty && verticalTrack.Contains(mousePoint))
        {
            var state = ScrollableHelpers.BeginVerticalDrag(_scrollable);
            _dragController = new ScrollableHelpers.ScrollDragController(
                state,
                isVertical: true,
                _getViewport,
                _setViewport,
                _onScrolled
            );
            _dragController.Update(e.Y);
        }
        else if (!horizontalTrack.IsEmpty && horizontalTrack.Contains(mousePoint))
        {
            var state = ScrollableHelpers.BeginHorizontalDrag(_scrollable);
            _dragController = new ScrollableHelpers.ScrollDragController(
                state,
                isVertical: false,
                _getViewport,
                _setViewport,
                _onScrolled
            );
            _dragController.Update(e.X);
        }
    }

    void OnMouseDragged(object? sender, MouseInputArgs e)
    {
        if (_dragController is null)
            return;
        _dragController.Update(_dragController.IsVertical ? e.Y : e.X);
    }

    void OnMouseReleased(object? sender, MouseInputArgs e)
    {
        _dragController = null;
    }

    void OnMouseWheel(object? sender, MouseWheelInputArgs e)
    {
        if (!_getIsMouseOver())
            return;

        var rotation = e.Rotation;
        rotation /= 10;
        if (Math.Abs(rotation) <= 0)
            return;

        var viewPort = _getViewport();
        var scrollMode = _scrollable.Scrollable;

        if (scrollMode == ScrollableMode.Vertical)
        {
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, _scrollable, ref viewPort);
        }
        else if (scrollMode == ScrollableMode.Horizontal)
        {
            ScrollableHelpers.Scroll(Orientation.Horizontal, rotation, _scrollable, ref viewPort);
        }
        else if (scrollMode == ScrollableMode.Both)
        {
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, _scrollable, ref viewPort);
        }

        _setViewport(viewPort);
        _onScrolled();
    }
}
