using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;
using Rectangle = System.Drawing.Rectangle;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
partial class FormSDL : IFormSDL
{
    #region Constructor

    protected FormSDL()
    {
        Debug.Log(LogCategory.Form, $"Creating form: {GetType().Name}");
        Handle = CreateWindow(Title, Width, Height + (_titleBar?.Height ?? 0));
        Debug.Log(LogCategory.Form, $"Window created, handle: 0x{Handle:X}");

        RecordWindowState();
        CreateRenderer();
        InitializeControls();
        RegisterHitTest();

        PerformTheme();
        Invalidate();
        Debug.Log(LogCategory.Form, $"Form initialized: {GetType().Name} ({Width}x{Height})");
    }

    #endregion

    #region Initialization

    void RecordWindowState()
    {
        ((IFormSDL)this).RecordLocation();
        ((IFormSDL)this).RecordSize();
        ((IFormSDL)this).RecordState();
    }

    void CreateRenderer()
    {
        Debug.Log(LogCategory.SDL, "Creating renderer");
        Renderer = SDLHelpers.SDL_CreateRenderer(Handle, null);

        if (Renderer == IntPtr.Zero)
            Debug.LogError("Failed to create SDL renderer");
        else
            Debug.Log(LogCategory.SDL, $"Renderer created, handle: 0x{Renderer:X}");
    }

    void InitializeControls()
    {
        Controls = Services.GetSingleton<IStaticLayoutFactory>().Create();
        Controls.Parent = this;
        Services.GetSingleton<IApplication>().Forms.Add(this);
        InvalidateTitleBar();
    }

    void RegisterHitTest()
    {
        if (Services.GetSingleton<IApplicationConfiguration>().FormsStyle is not FormStyle.CrossSharp)
            return;

        _hitTestHandler = new FormSDLHitTestHandler(this);
        _hitTestHandler.Register();
        Debug.Log(LogCategory.Form, "Hit test handler registered for resize/drag support");
    }

    #endregion

    #region IFormSDL Implementation

    void IFormSDL.RecordLocation()
    {
        SDLHelpers.SDL_GetWindowPosition(Handle, out int x, out int y);
        Location = new Point(x, y);
    }

    public void RecordSize()
    {
        SDLHelpers.SDL_GetWindowSize(Handle, out int w, out int h);
        Width = w;
        Height = h;
    }

    public void RecordState()
    {
        var state = SDLHelpers.SDL_GetWindowFlags(Handle);

        if ((state & SDLWindowFlags.MINIMIZED) != 0 || (state & SDLWindowFlags.HIDDEN) != 0)
            State = WindowState.Minimized;
        else if ((state & SDLWindowFlags.MAXIMIZED) != 0)
            State = WindowState.Maximized;
        else
            State = WindowState.Normal;
    }

    public void Initialize() { }

    #endregion

    #region Theme and Invalidation

    public void PerformTheme()
    {
        var theme = Services.GetSingleton<ITheme>();
        Debug.Log(LogCategory.Theme, $"Applying theme to form: {GetType().Name}");

        _titleBar?.PerformTheme();
        BackgroundColor = theme.LayoutBackgroundColor;

        foreach (var control in Controls)
            control.PerformTheme();

        OnThemePerformed();
    }

    public void Invalidate()
    {
        Controls.Width = Width;
        Controls.Height = Height;
        InvalidateTitleBar();

        foreach (var control in Controls)
            control.Invalidate();

        OnInvalidated();
    }

    void InvalidateTitleBar()
    {
        if (Services.GetSingleton<IApplicationConfiguration>().FormsStyle is not FormStyle.CrossSharp)
            return;

        _titleBar ??= new FormSDLTitleBar(this);
        Controls.Location = new Point(0, _titleBar.Height);
        _titleBar.Invalidate();
    }

    void InvalidateTitle()
    {
        SDLHelpers.SDL_SetWindowTitle(Handle, _title);
    }

    #endregion

    #region Window Operations

    public void Show()
    {
        Debug.Log(LogCategory.Form, $"Showing form: {GetType().Name}");
        Visible = true;
    }

    public void Move(Point location)
    {
        SDLHelpers.SDL_SetWindowPosition(Handle, location.X, location.Y);
    }

    public void Minimize()
    {
        Debug.Log(LogCategory.Form, $"Minimizing form: {GetType().Name}");
        SDLHelpers.SDL_MinimizeWindow(Handle);
    }

    public void Maximize()
    {
        Debug.Log(LogCategory.Form, $"Maximizing form: {GetType().Name}");
        SDLHelpers.SDL_MaximizeWindow(Handle);
    }

    public void Restore()
    {
        Debug.Log(LogCategory.Form, $"Restoring form: {GetType().Name}");
        SDLHelpers.SDL_RestoreWindow(Handle);
    }

    public void Close()
    {
        Debug.Log(LogCategory.Form, $"Closing form: {GetType().Name}");
        Dispose();
    }

    #endregion

    #region Layout

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    #endregion

    #region Drawing

    public void Redraw()
    {
        IGraphics g = new SDLGraphics(Renderer);
        Draw(ref g);
        g.Render();
        g.Dispose();
    }

    public void Draw(ref IGraphics graphics)
    {
        var clipState = ClipState.Create(
            ClipState.Max,
            new Rectangle(0, 0, Width, Height + (_titleBar?.Height ?? 0)),
            0
        );
        graphics.SetClip(clipState);
        graphics.SetOffset(new Point(0, _titleBar?.Height ?? 0));

        DrawShadows(ref graphics);
        DrawBackground(ref graphics);
        DrawBorders(ref graphics);
        DrawContent(ref graphics);
    }

    protected virtual void DrawShadows(ref IGraphics g) { }

    protected virtual void DrawBackground(ref IGraphics g)
    {
        g.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    protected virtual void DrawBorders(ref IGraphics g) { }

    protected virtual void DrawContent(ref IGraphics g)
    {
        _titleBar?.Draw(ref g);

        var oldOffset = g.GetOffset();
        foreach (var control in Controls.ToArray())
        {
            g.SetOffset(control.Location);
            control.Draw(ref g);
            g.SetOffset(oldOffset);
        }
    }

    #endregion

    #region Disposal

    public void Dispose() => OnDisposingInternal();

    void OnTickDispose(object? sender, EventArgs e)
    {
        Debug.Log(LogCategory.Form, $"Disposing form: {GetType().Name}");

        _hitTestHandler?.Unregister();
        Controls.Dispose();
        DestroyWindow();

        Handle = IntPtr.Zero;
        Renderer = IntPtr.Zero;

        Services.GetSingleton<IApplication>().Forms.Remove(this);
        Services.GetSingleton<IApplication>().Tick -= OnTickDispose;

        Debug.Log(LogCategory.Form, $"Form disposed: {GetType().Name}");
    }

    #endregion
}
