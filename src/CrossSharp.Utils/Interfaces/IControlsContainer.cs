namespace CrossSharp.Utils.Interfaces;

public interface IControlsContainer : IDisposable
{
    IntPtr Handle { get; }
    List<IControl> Items { get; }
    void Show();
    void Add(IControl control);
}
