using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Windows;

class Form : IForm
{
    internal Form() { }

    void Initialize()
    {
        Handle = CreateWindowCore(Title ?? "CrossSharp Application", Width, Height);
        ((IFormSDL)this).RecordLocation();
        ((IFormSDL)this).RecordSize();
        CreateRenderer();
        Controls = Services.GetSingleton<IStaticLayoutFactory>().Create();
        Controls.Parent = this;
        Services.GetSingleton<IApplication>().Forms.Add(this);
        Invalidate();
    }

    IntPtr Renderer { get; set; }

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

    static IntPtr CreateWindowCore(string title, int width, int height)
    {
        var appConfig = Services.GetSingleton<IApplicationConfiguration>();
        uint flags = SDLWindowFlags.HIDDEN | SDLWindowFlags.RESIZABLE;
        if (appConfig.HighDpi)
            flags |= SDLWindowFlags.ALLOW_HIGHDPI;

        // Set to OPENGL, later can be changed to VULKAN or METAL or DIRECT3D based on configuration and platform
        flags |= SDLWindowFlags.OPENGL;

        var window = SDLHelpers.SDL_CreateWindow(
            title,
            SDLWindowPosition.CENTERED,
            SDLWindowPosition.CENTERED,
            width,
            height,
            flags
        );
        return window;
    }

    public void Dispose() { }

    public int BorderWidth { get; set; }

    public ColorRgba BorderColor { get; set; }

    public void LimitClip(ref IGraphics g) { }

    public Point Location { get; set; }
    public EventHandler<Point>? LocationChanged { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public EventHandler<Size>? SizeChanged { get; set; }
    public object? Parent { get; set; }
    public bool IsMouseOver { get; set; }
    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    public EventHandler? MarginChanged { get; set; }
    public bool Visible { get; set; }

    public void Invalidate() { }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public void Draw(ref IGraphics graphics) { }

    public EventHandler? Disposing { get; set; }
    public int Index { get; set; }
    public IControlsContainer Controls { get; private set; }
    public IntPtr Handle { get; private set; }
    public bool UseNativeTitleBar { get; set; }
    public string Title { get; set; }
    public EventHandler? TitleChanged { get; set; }
    public IApplication AppInstance { get; }

    public void Close() { }

    public EventHandler? Shown { get; set; }
    public EventHandler? OnClose { get; set; }

    public void Minimize() { }

    public void Maximize() { }

    public void Restore() { }

    public void Show()
    {
        Visible = true;
    }

    public void Redraw() { }

    public WindowState State { get; set; }
    public ITitleBar? TitleBar { get; }
    public ColorRgba BackgroundColor { get; set; }
    public EventHandler? BackgroundColorChanged { get; set; }
}
