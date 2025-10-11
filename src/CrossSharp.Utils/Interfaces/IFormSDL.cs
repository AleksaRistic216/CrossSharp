namespace CrossSharp.Utils.Interfaces;

interface IFormSDL : IForm
{
    uint WindowId { get; }
    IntPtr Renderer { get; }
    void RecordLocation();
    void RecordSize();
}
