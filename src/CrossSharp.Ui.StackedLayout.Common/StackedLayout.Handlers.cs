using System.Drawing;
using CrossSharp.Utils.Helpers;

namespace CrossSharp.Ui.Common;

partial class StackedLayout
{
    ScrollbarInteractionHandler<StackedLayout>? _scrollbarHandler;

    void InitializeScrollbarHandler()
    {
        _scrollbarHandler = new ScrollbarInteractionHandler<StackedLayout>(
            _inputHandler,
            this,
            () => _viewPort,
            vp => _viewPort = vp,
            OnScrolled,
            () => IsMouseOver,
            v => IsMouseOver = v
        );
        _scrollbarHandler.Subscribe();
    }

    void DisposeScrollbarHandler()
    {
        _scrollbarHandler?.Dispose();
        _scrollbarHandler = null;
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
        DisposeScrollbarHandler();
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
