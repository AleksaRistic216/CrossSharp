using System.Drawing;
using CrossSharp.Utils;

namespace CrossSharp.Ui.Linux;

partial class FlowLayout
{
    public EventHandler<Point>? LocationChanged { get; set; }

    void OnLocationChanged(Point newLocation)
    {
        Invalidate();
        RaiseLocationChanged(newLocation);
    }

    void RaiseLocationChanged(Point newLocation) => LocationChanged?.Invoke(this, newLocation);

    public EventHandler<Size>? SizeChanged { get; set; }

    void OnSizeChanged(Size newSize)
    {
        Invalidate();
        RaiseSizeChanged(newSize);
    }

    void RaiseSizeChanged(Size newSize) => SizeChanged?.Invoke(this, newSize);

    public EventHandler? BackgroundColorChanged { get; set; }

    void OnBackgroundColorChanged()
    {
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);

    public EventHandler? ItemsSpacingChanged { get; set; }

    void OnItemsSpacingChanged()
    {
        Invalidate();
        RaiseItemsSpacingChanged();
    }

    void RaiseItemsSpacingChanged() => ItemsSpacingChanged?.Invoke(this, EventArgs.Empty);

    public EventHandler? JustifyContentChanged { get; set; }

    void OnJustifyContentChanged()
    {
        Invalidate();
        RaiseJustifyContentChanged();
    }

    void RaiseJustifyContentChanged() => JustifyContentChanged?.Invoke(this, EventArgs.Empty);

    public EventHandler? MarginChanged { get; set; }

    void RaiseMarginChanged() => MarginChanged?.Invoke(this, EventArgs.Empty);

    void OnMarginChangedInternal() => RaiseMarginChanged();

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
