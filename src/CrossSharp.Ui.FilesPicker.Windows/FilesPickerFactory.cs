using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create() => new FilesPicker();
}
