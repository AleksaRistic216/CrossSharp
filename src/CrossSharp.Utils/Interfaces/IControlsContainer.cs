namespace CrossSharp.Utils.Interfaces;

public interface IControlsContainer : IDisposable, IRelativeHandle
{
    List<IControl> Items { get; }
    void Show();
    void Add(IControl control);
    void Redraw();
}
