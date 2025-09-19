namespace CrossSharp.Utils.Interfaces;

public interface IControl : IDisposable, IRelativeHandle
{
    bool Visible { get; set; }
    void Initialize();
    void Invalidate();
    void Show();
}
