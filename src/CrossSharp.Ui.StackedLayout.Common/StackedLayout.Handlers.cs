using System.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Input;

namespace CrossSharp.Ui.Common;

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
            ScrollableHelpers.Scroll(Orientation.Vertical, rotation, this, ref _viewPort);
        else if (Scrollable == ScrollableMode.Horizontal)
            ScrollableHelpers.Scroll(Orientation.Horizontal, rotation, this, ref _viewPort);
        else if (Scrollable == ScrollableMode.Both)
        {
            // TODO: implement both direction scrolling using mouse and shift key
        }
        OnScrolled();
    }

    public EventHandler<Point>? LocationChanged { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }

    void RaiseSizeChanged(Size newSize)
    {
        SizeChanged?.Invoke(this, newSize);
    }

    void OnSizeChanged(Size newSize)
    {
        Invalidate();
        RaiseSizeChanged(newSize);
    }

    public EventHandler? BackgroundColorChanged { get; set; }
    public EventHandler? ThemePerformed { get; set; }

    void RaiseThemePerformed()
    {
        ThemePerformed?.Invoke(this, System.EventArgs.Empty);
    }

    void OnThemePerformed()
    {
        RaiseThemePerformed();
    }

    public EventHandler? Invalidated { get; set; }

    void RaiseInvalidated()
    {
        Invalidated?.Invoke(this, System.EventArgs.Empty);
    }

    void OnInvalidated()
    {
        RaiseInvalidated();
    }

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

    public EventHandler? OrientationChanged { get; set; }

    void RaiseOrientationChanged()
    {
        OrientationChanged?.Invoke(this, System.EventArgs.Empty);
    }

    void OnOrientationChanged()
    {
        Invalidate();
        RaiseOrientationChanged();
    }

    public EventHandler? Scrolled { get; set; }

    void RaiseScrolled()
    {
        Scrolled?.Invoke(this, System.EventArgs.Empty);
    }

    void OnScrolled()
    {
        RaiseScrolled();
    }

    public EventHandler? MarginChanged { get; set; }

    void RaiseMarginChanged()
    {
        MarginChanged?.Invoke(this, System.EventArgs.Empty);
    }

    void OnMarginChanged()
    {
        Invalidate();
        RaiseMarginChanged();
    }
}
