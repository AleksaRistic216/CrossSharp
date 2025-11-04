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

    /// <summary>
    /// Gets or sets the index of the control within its parent container.
    /// This determines the order in which controls are rendered and laid out.
    /// </summary>
    int Index { get; set; }
}
