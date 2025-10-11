using System.Drawing;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.SDL;

namespace CrossSharp.Ui.Linux;

partial class FormSDL : IFormSDL
{
    public FormSDL()
    {
        Handle = CreateWindow(Title ?? "CrossSharp Application", Width, Height);
        ((IFormSDL)this).RecordLocation();
        ((IFormSDL)this).RecordSize();
        Services.GetSingleton<IApplication>().Forms.Add(this);
        Renderer = SDLHelpers.SDL_CreateRenderer(Handle, -1, 0);
        Controls = Services.GetSingleton<IStaticLayoutFactory>().Create();
        Controls.Parent = this;
    }

    /// <summary>
    /// Gets the current location of the window from the OS and updates the Location property.
    /// </summary>
    void IFormSDL.RecordLocation()
    {
        SDLHelpers.SDL_GetWindowPosition(Handle, out int x, out int y);
        Location = new System.Drawing.Point(x, y);
    }

    public void RecordSize()
    {
        SDLHelpers.SDL_GetWindowSize(Handle, out int w, out int h);
        Width = w;
        Height = h;
    }

    public void Initialize() { }

    public void Invalidate() { }

    public void Show() { }

    public void Redraw()
    {
        IGraphics g = new SDLGraphics(Renderer);
        Draw(ref g);
        g.ForceRender();
        g.Dispose();
    }

    public void SuspendLayout() { }

    public void ResumeLayout() { }

    public void DrawShadows(ref IGraphics g) { }

    public void DrawBackground(ref IGraphics g)
    {
        g.FillRectangle(0, 0, Width, Height, BackgroundColor);
    }

    public void DrawBorders(ref IGraphics g) { }

    public void DrawContent(ref IGraphics g)
    {
        foreach (var control in Controls)
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

    public IForm GetForm() => this;

    public void Close()
    {
        Dispose();
    }

    public void PerformLayout() { }

    public void Minimize() { }

    public void Maximize() { }

    public void Restore() { }

    public void Dispose()
    {
        Handle = IntPtr.Zero;
    }

    public void LimitClip(ref IGraphics g) { }
}
