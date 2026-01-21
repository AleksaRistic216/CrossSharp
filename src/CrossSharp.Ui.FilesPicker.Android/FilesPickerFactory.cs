using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create() => new FilesPicker();
}
