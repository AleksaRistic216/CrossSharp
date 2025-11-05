using CrossSharp.Utils.EventArgs;

namespace CrossSharp.Utils.Interfaces;

public interface IFilesPicker : IControl
{
    void Show();
    void Close();
    EventHandler<FilesSelectedEventArgs>? FilesSelected { get; set; }
}
