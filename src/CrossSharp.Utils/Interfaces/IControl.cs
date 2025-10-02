using CrossSharp.Utils.Drawing;

namespace CrossSharp.Utils.Interfaces;

public interface IControl
    : IDisposable,
        IRelativeHandle,
        IBorder,
        IClipLimiter,
        ILocationProvider,
        ISizeProvider
{
    int ZIndex { get; set; }
    bool Visible { get; set; }
    void Initialize();
    void Invalidate();
    void Show();
    void Redraw();
    void SuspendLayout();
    void ResumeLayout();
    void DrawShadows(ref Graphics g);
    void DrawBackground(ref Graphics g);
    void DrawBorders(ref Graphics g);
    void DrawContent(ref Graphics g);
}
