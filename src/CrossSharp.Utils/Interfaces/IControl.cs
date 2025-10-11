namespace CrossSharp.Utils.Interfaces;

public interface IControl
    : IDisposable,
        IBorder,
        IClipLimiter,
        ILocationProvider,
        ISizeProvider,
        IChild,
        IIsMouseOverProvider
{
    bool Visible { get; set; }
    void Initialize();
    void Invalidate();
    void SuspendLayout();
    void ResumeLayout();
    void Draw(ref IGraphics graphics);
    IForm? GetForm();
}
