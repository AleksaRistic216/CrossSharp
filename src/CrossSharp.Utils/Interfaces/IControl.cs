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
    void DrawShadows(Graphics g);
    void DrawBackground(Graphics g);
    void DrawBorders(Graphics g);
    void DrawContent(Graphics g);
}
