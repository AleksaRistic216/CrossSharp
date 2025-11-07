using System.Drawing;

namespace CrossSharp.Ui.Common;

partial class TabbedLayout
{
    public EventHandler<Point>? LocationChanged { get; set; }

    void RaiseLocationChanged() => LocationChanged?.Invoke(this, new Point(Location.X, Location.Y));

    void OnLocationChangedInternal()
    {
        Invalidate();
        RaiseLocationChanged();
    }

    public EventHandler<Size>? SizeChanged { get; set; }

    void RaiseSizeChanged() => SizeChanged?.Invoke(this, new Size(Width, Height));

    void OnSizeChangedInternal()
    {
        Invalidate();
        RaiseSizeChanged();
    }

    public EventHandler? MarginChanged { get; set; }

    void RaiseMarginChanged() => MarginChanged?.Invoke(this, EventArgs.Empty);

    void OnMarginChangedInternal()
    {
        Invalidate();
        RaiseMarginChanged();
    }

    public EventHandler? BackgroundColorChanged { get; set; }

    void RaiseBackgroundColorChanged() => BackgroundColorChanged?.Invoke(this, EventArgs.Empty);

    void OnBackgroundColorChangedInternal()
    {
        Invalidate();
        RaiseBackgroundColorChanged();
    }

    public EventHandler? HeaderItemsSpacingChanged { get; set; }

    void RaiseHeaderItemsSpacingChanged() => HeaderItemsSpacingChanged?.Invoke(this, EventArgs.Empty);

    void OnHeaderItemsSpacingChanged()
    {
        _header.ItemsSpacing = _headerItemsSpacing;
        Invalidate();
        RaiseHeaderItemsSpacingChanged();
    }

    public EventHandler? HeaderPaddingChanged { get; set; }

    void RaiseHeaderPaddingChanged() => HeaderPaddingChanged?.Invoke(this, EventArgs.Empty);

    void OnHeaderPaddingChanged()
    {
        _header.Padding = HeaderPadding;
        Invalidate();
        RaiseHeaderPaddingChanged();
    }

    public EventHandler? HeaderHeightChanged { get; set; }

    void RaiseHeaderHeightChanged() => HeaderHeightChanged?.Invoke(this, EventArgs.Empty);

    void OnHeaderHeightChanged()
    {
        _header.Height = _headerHeight;
        Invalidate();
        RaiseHeaderHeightChanged();
    }

    public EventHandler? CurrentTabChanged { get; set; }

    void RaiseCurrentTabChanged() => CurrentTabChanged?.Invoke(this, EventArgs.Empty);

    void OnCurrentTabChanged()
    {
        Invalidate();
        RaiseCurrentTabChanged();
    }

    public EventHandler? ThemePerformed { get; set; }

    void RaiseThemePerformed() => ThemePerformed?.Invoke(this, EventArgs.Empty);

    void OnThemePerformed() => RaiseThemePerformed();

    public EventHandler? Disposing { get; set; }

    void RaiseDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

    void OnDisposeInternal() => RaiseDisposing();
}
