using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

partial class FormSDL : IFormSDL
{
    public FormSDL()
    {
        Services.GetSingleton<IApplication>().Forms.Add(this);
    }

    public void Initialize() { }

    public void Invalidate() { }

    public void Show() { }

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
        foreach (IControl control in Controls)
            control.Draw(ref g);
    }

    public void Draw(ref IGraphics graphics)
    {
        DrawShadows(ref graphics);
        DrawBackground(ref graphics);
        DrawBorders(ref graphics);
        DrawContent(ref graphics);
        foreach (var control in Controls)
            control.Draw(ref graphics);
        // if (Services.GetSingleton<IApplication>().DevelopersMode)
        //     DrawDevelopersBorders(_g!);
    }

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
