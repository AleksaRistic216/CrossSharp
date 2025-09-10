namespace CrossSharp.Utils.Interfaces;

public interface IControl : IDisposable
{
    IntPtr Handle { get; internal set; }
    IntPtr ParentHandle { get; set; }
    bool Visible { get; set; }
    void Initialize();
    void Invalidate();
    void Show();
}
