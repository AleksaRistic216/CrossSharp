using CrossSharp.Utils.EventArgs;

namespace CrossSharp.Utils.Interfaces;

public interface IFilesPicker : IControl
{
    void Show();
    EventHandler<FilesSelectedEventArgs> FilesSelected { get; set; }
}
