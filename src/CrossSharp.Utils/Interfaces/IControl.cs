namespace CrossSharp.Utils.Interfaces;

public interface IControl
    : IDisposable,
        IBorder,
        IClipLimiter,
        ILocationProvider,
        ISizeProvider,
        IChild,
        IIsMouseOverProvider,
        IMarginProvider
{
    bool Visible { get; set; }
    void Invalidate();
    void SuspendLayout();
    void ResumeLayout();
    void Draw(ref IGraphics graphics);
    EventHandler? Disposing { get; set; }
}
