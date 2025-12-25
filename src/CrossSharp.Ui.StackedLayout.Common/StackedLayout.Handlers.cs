using System.Drawing;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class StackedLayout
{
    ScrollbarInteractionHandler<StackedLayout>? _scrollbarHandler;
    ReorderInteractionHandler<StackedLayout>? _reorderHandler;

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

    void InitializeReorderHandler()
    {
        if (_reorderHandler is not null)
            return;
        _reorderHandler = new ReorderInteractionHandler<StackedLayout>(
            _inputHandler,
            this,
            () => IsMouseOver,
            UpdateDragState,
            OnReorderCompleted
        );
        _reorderHandler.Subscribe();
    }

    void DisposeReorderHandler()
    {
        _reorderHandler?.Dispose();
        _reorderHandler = null;
    }

    void UpdateDragState(IControl? draggedControl, int dropTargetIndex)
    {
        DraggedControl = draggedControl;
        DropTargetIndex = dropTargetIndex;
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
        DisposeReorderHandler();
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

    public EventHandler<ControlsReorderedEventArgs>? ControlsReordered { get; set; }

    void RaiseControlsReordered(IControl control, int oldIndex, int newIndex)
    {
        ControlsReordered?.Invoke(this, new ControlsReorderedEventArgs(control, oldIndex, newIndex));
    }

    void OnReorderEnabledChanged()
    {
        if (ReorderEnabled)
            InitializeReorderHandler();
        else
            DisposeReorderHandler();
        Invalidate();
    }

    void OnReorderCompleted(IControl control, int newIndex)
    {
        var oldIndex = control.Index;
        if (oldIndex == newIndex)
            return;

        // Reorder the controls by updating their Index values
        var controls = _controls.Where(c => c.Visible).OrderBy(c => c.Index).ToList();

        if (newIndex > oldIndex)
        {
            // Moving down: shift controls between oldIndex and newIndex up
            foreach (var c in controls.Where(c => c.Index > oldIndex && c.Index <= newIndex))
            {
                c.Index--;
            }
            control.Index = newIndex;
        }
        else
        {
            // Moving up: shift controls between newIndex and oldIndex down
            foreach (var c in controls.Where(c => c.Index >= newIndex && c.Index < oldIndex))
            {
                c.Index++;
            }
            control.Index = newIndex;
        }

        Invalidate();
        RaiseControlsReordered(control, oldIndex, newIndex);
    }
}
