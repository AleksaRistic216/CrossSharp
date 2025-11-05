using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;
using Rectangle = System.Drawing.Rectangle;

namespace CrossSharp.Ui.Common;

// ReSharper disable once InconsistentNaming
partial class FormSDL : IFormSDL
{
    internal FormSDL()
    {
        Initialize();
    }

    void CreateRenderer()
    {
        Renderer = SDLHelpers.SDL_CreateRenderer(
            Handle,
            -1,
            SDLRenderFlags.SDL_RENDERER_ACCELERATED | SDLRenderFlags.SDL_RENDERER_TARGETTEXTURE
        );
        if (Renderer == IntPtr.Zero) // Try again without acceleration
            Renderer = SDLHelpers.SDL_CreateRenderer(
                Handle,
                -1,
                SDLRenderFlags.SDL_RENDERER_SOFTWARE | SDLRenderFlags.SDL_RENDERER_TARGETTEXTURE
            );
    }

    /// <summary>
    /// Gets the current location of the window from the OS and updates the Location property.
    /// </summary>
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

    void Initialize()
    {
        Handle = CreateWindow(Title, Width, Height);
        ((IFormSDL)this).RecordLocation();
        ((IFormSDL)this).RecordSize();
        CreateRenderer();
        Controls = Services.GetSingleton<IStaticLayoutFactory>().Create();
        Controls.Parent = this;
        Services.GetSingleton<IApplication>().Forms.Add(this);
        Invalidate();
    }

    public void PerformTheme()
    {
        BackgroundColor = Services.GetSingleton<ITheme>().BackgroundColor;
        foreach (var control in Controls)
            control.PerformTheme();
    }

    public void Invalidate()
    {
        Controls.Width = Width;
        Controls.Height = Height;
        InvalidateTitleBar();
        foreach (var control in Controls)
            control.Invalidate();
    }

    FormSDLTitleBar? _titleBar;

    void InvalidateTitleBar()
    {
        if (Services.GetSingleton<IApplicationConfiguration>().FormsStyle is not FormStyle.CrossSharp)
            return;
        _titleBar ??= new FormSDLTitleBar(this);
        Controls.Location = new Point(0, _titleBar.Height);
    }

    void InvalidateTitle()
    {
        SDLHelpers.SDL_SetWindowTitle(Handle, _title);
    }

    public void Show()
    {
        Visible = true;
    }

    public void Redraw()
    {
        IGraphics g = new SDLGraphics(Renderer);
        Draw(ref g);
        g.Render();
        g.Dispose();
    }

    public void Move(Point location)
    {
        SDLHelpers.SDL_SetWindowPosition(Handle, location.X, location.Y);
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    protected virtual void DrawShadows(ref IGraphics g) { }

    protected virtual void DrawBackground(ref IGraphics g)
    {
        g.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    protected virtual void DrawBorders(ref IGraphics g) { }

    protected virtual void DrawContent(ref IGraphics g)
    {
        _titleBar?.Draw(ref g);
        foreach (var control in Controls.ToArray())
        {
            g.SetOffset(control.Location.X, control.Location.Y);
            control.Draw(ref g);
            g.ResetOffset();
        }
    }

    public void Draw(ref IGraphics graphics)
    {
        graphics.SetClip(new Rectangle(0, 0, Width, Height), 0);
        DrawShadows(ref graphics);
        DrawBackground(ref graphics);
        DrawBorders(ref graphics);
        DrawContent(ref graphics);
        // if (Services.GetSingleton<IApplication>().DevelopersMode)
        //     DrawDevelopersBorders(_g!);
    }

    public void Close()
    {
        Dispose();
    }

    public void Minimize() { }

    public void Maximize() { }

    public void Restore() { }

    public void Dispose() => OnDisposingInternal();

    void OnTickDispose(object? sender, EventArgs e)
    {
        Controls.Dispose();
        DestroyWindow();
        Handle = IntPtr.Zero;
        Renderer = IntPtr.Zero;
        Services.GetSingleton<IApplication>().Forms.Remove(this);
        Services.GetSingleton<IApplication>().Tick -= OnTickDispose;
    }

    public void LimitClip(ref IGraphics g) { }
}
