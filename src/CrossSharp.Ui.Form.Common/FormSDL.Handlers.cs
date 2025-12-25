using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Common;

partial class FormSDL
{
    #region Events

    public EventHandler<Point>? LocationChanged { get; set; }
    public EventHandler? Shown { get; set; }
    public EventHandler? OnClose { get; set; }
    public EventHandler? StateChanged { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }
    public EventHandler? TitleChanged { get; set; }
    public EventHandler? MarginChanged { get; set; }
    public EventHandler? ThemePerformed { get; set; }
    public EventHandler? Invalidated { get; set; }
    public EventHandler? Disposing { get; set; }

    #endregion

    #region State Change Handlers

    void OnStateChanged() => RaiseStateChanged();

    void OnVisibleChanged()
    {
        SDLHelpers.SDL_ShowWindow(Handle);
        if (Visible)
            Shown?.Invoke(this, EventArgs.Empty);
        PerformTheme();
    }

    void OnMarginChanged() => RaiseMarginChanged();

    void OnThemePerformed() => RaiseThemePerformed();

    void OnInvalidated() => RaiseInvalidated();

    #endregion

    #region Internal Change Handlers

    void OnSizeChangedInternal()
    {
        Invalidate();
        RaiseSizeChanged();
    }

    void OnTitleChangedInternal()
    {
        InvalidateTitle();
        TitleChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnDisposingInternal()
    {
        Services.GetSingleton<IApplication>().Tick += OnTickDispose;
        Services.GetSingleton<IApplication>().Forms.Remove(this);
        RaiseDisposing();
    }

    #endregion

    #region Event Raisers

    void RaiseStateChanged() => StateChanged?.Invoke(this, EventArgs.Empty);

    void RaiseSizeChanged() => SizeChanged?.Invoke(this, new Size(Width, Height));

    void RaiseLocationChanged() => LocationChanged?.Invoke(this, new Point(Location.X, Location.Y));

    void RaiseMarginChanged() => MarginChanged?.Invoke(this, EventArgs.Empty);

    void RaiseThemePerformed() => ThemePerformed?.Invoke(this, EventArgs.Empty);

    void RaiseInvalidated() => Invalidated?.Invoke(this, EventArgs.Empty);

    void RaiseDisposing() => Disposing?.Invoke(this, EventArgs.Empty);

    #endregion
}
