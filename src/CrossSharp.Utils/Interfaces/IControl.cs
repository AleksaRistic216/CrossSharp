namespace CrossSharp.Utils.Interfaces;

public interface IControl : IDisposable {
    IntPtr Handle { get; }
    IntPtr ParentHandle { get; set; }
    void Initialize();
    void Show();
}