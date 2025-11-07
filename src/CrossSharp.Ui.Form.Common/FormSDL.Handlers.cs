using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

partial class FormSDL
{
    public EventHandler<Point>? LocationChanged { get; set; }
    public EventHandler? Shown { get; set; }
    public EventHandler? OnClose { get; set; }
    public EventHandler? StateChanged { get; set; }

    void RaiseStateChanged() => StateChanged?.Invoke(this, EventArgs.Empty);

    void OnStateChanged() => RaiseStateChanged();

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

    public EventHandler? MarginChanged { get; set; }

    void RaiseMarginChanged() => MarginChanged?.Invoke(this, EventArgs.Empty);

    void OnMarginChangedInternal() => RaiseMarginChanged();

    public EventHandler? ThemePerformed { get; set; }

    void RaiseThemePerformed() => ThemePerformed?.Invoke(this, EventArgs.Empty);

    void OnThemePerformed() => RaiseThemePerformed();

    public EventHandler? Disposing { get; set; }

    void RaiseDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

    void OnDisposingInternal()
    {
        Services.GetSingleton<IApplication>().Tick += OnTickDispose;
        Services.GetSingleton<IApplication>().Forms.Remove(this);
        RaiseDisposing();
    }
}
