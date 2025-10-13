using System.Drawing;

namespace CrossSharp.Ui.Linux;

partial class FormSDL
{
    public EventHandler<Point>? OnLocationChanged { get; set; }
    public EventHandler? OnShow { get; set; }
    public EventHandler? OnClose { get; set; }
    public EventHandler? OnBackgroundColorChange { get; set; }

    public EventHandler<Size>? OnSizeChanged { get; set; }

    void OnSizeChangedInternal()
    {
        Invalidate();
        RaiseSizeChanged();
    }

    void RaiseSizeChanged() => OnSizeChanged?.Invoke(this, new Size(Width, Height));

    public EventHandler? OnTitleChanged { get; set; }

    void OnTitleChangedInternal()
    {
        InvalidateTitle();
        OnTitleChanged?.Invoke(this, EventArgs.Empty);
    }

    void RaiseLocationChanged() =>
        OnLocationChanged?.Invoke(this, new Point(Location.X, Location.Y));
}
