using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

public class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create() => new FilesPicker();
}
