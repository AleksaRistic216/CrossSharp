using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class FilesPickerFactory : IFilesPickerFactory
{
    public IFilesPicker Create()
    {
        var filesPicker = new FilesPicker();
        filesPicker.PerformTheme();
        return filesPicker;
    }
}
