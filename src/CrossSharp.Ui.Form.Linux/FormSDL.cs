using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;
using Rectangle = System.Drawing.Rectangle;

namespace CrossSharp.Ui.Linux;

partial class FormSDL : IFormSDL
{
    public FormSDL()
    {
        Initialize();
    }

    void CreateRenderer()
    {
        Renderer = SDLHelpers.SDL_CreateRenderer(
            Handle,
            -1,
            SDLRenderFlags.SDL_RENDERER_ACCELERATED
        );
        if (Renderer == IntPtr.Zero) // Try again without acceleration
            Renderer = SDLHelpers.SDL_CreateRenderer(
                Handle,
                -1,
                SDLRenderFlags.SDL_RENDERER_ACCELERATED
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

    public void Initialize()
    {
        Handle = CreateWindow(Title ?? "CrossSharp Application", Width, Height);
        ((IFormSDL)this).RecordLocation();
        ((IFormSDL)this).RecordSize();
        CreateRenderer();
        Controls = Services.GetSingleton<IStaticLayoutFactory>().Create();
        Controls.Parent = this;
        Services.GetSingleton<IApplication>().Forms.Add(this);
        Invalidate();
    }

    public void Invalidate()
    {
        Controls.Width = Width;
        Controls.Height = Height;
        foreach (var control in Controls)
            control.Invalidate();
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
        g.ForceRender();
        g.Dispose();
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public virtual void DrawShadows(ref IGraphics g) { }

    public virtual void DrawBackground(ref IGraphics g)
    {
        g.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    public virtual void DrawBorders(ref IGraphics g) { }

    public virtual void DrawContent(ref IGraphics g)
    {
        foreach (var control in Controls.ToArray())
        {
            g.SetOffset(control.Location.X, control.Location.Y);
            control.Draw(ref g);
            g.ResetOffset();
        }
    }

    public void Draw(ref IGraphics graphics)
    {
        graphics.SetClip(new Rectangle(0, 0, Width, Height));
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

    public void Dispose()
    {
        Services.GetSingleton<IApplication>().Tick += OnTickDispose;
    }

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
