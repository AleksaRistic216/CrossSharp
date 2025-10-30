using System.Drawing;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Linux;

partial class FormSDL
{
    public EventHandler<Point>? LocationChanged { get; set; }
    public EventHandler? Shown { get; set; }
    public EventHandler? OnClose { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }

    public EventHandler<Size>? SizeChanged { get; set; }

    void OnSizeChangedInternal()
    {
        Invalidate();
        RaiseSizeChanged();
    }

    void RaiseSizeChanged() => SizeChanged?.Invoke(this, new Size(Width, Height));

    public EventHandler? TitleChanged { get; set; }

    void OnTitleChangedInternal()
    {
        InvalidateTitle();
        TitleChanged?.Invoke(this, EventArgs.Empty);
    }

    void RaiseLocationChanged() => LocationChanged?.Invoke(this, new Point(Location.X, Location.Y));

    void OnVisibleChangedInternal()
    {
        SDLHelpers.SDL_ShowWindow(Handle);
        if (Visible)
            Shown?.Invoke(this, EventArgs.Empty);
    }
}
