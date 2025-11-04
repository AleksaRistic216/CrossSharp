using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create() => new FilesPicker();
}
