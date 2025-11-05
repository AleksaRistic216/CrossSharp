using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create()
    {
        var filesPicker = new FilesPicker();
        filesPicker.Initialize();
        return filesPicker;
    }
}
