namespace CrossSharp.Utils.Interfaces;

public interface IControl : IDisposable, IRelativeHandle, IBorder, IClipLimiter
{
    int ZIndex { get; set; }
    bool Visible { get; set; }
    void Initialize();
    void Invalidate();
    void Show();
    void Redraw();
    void SuspendLayout();
    void ResumeLayout();
}
