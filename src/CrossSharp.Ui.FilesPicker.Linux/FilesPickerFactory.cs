using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create() => new FilesPicker();
}
