using System.Drawing;

namespace CrossSharp.Ui.Linux;

partial class FormSDL
{
    public EventHandler<Point>? LocationChanged { get; set; }
    public EventHandler? OnShow { get; set; }
    public EventHandler? OnClose { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }

    public EventHandler<Size>? SizeChanged { get; set; }

    void OnSizeChangedInternal()
    {
        Invalidate();
        RaiseSizeChanged();
    }

    void RaiseSizeChanged() => SizeChanged?.Invoke(this, new Size(Width, Height));

    public EventHandler? OnTitleChanged { get; set; }

    void OnTitleChangedInternal()
    {
        InvalidateTitle();
        OnTitleChanged?.Invoke(this, EventArgs.Empty);
    }

    void RaiseLocationChanged() => LocationChanged?.Invoke(this, new Point(Location.X, Location.Y));
}
