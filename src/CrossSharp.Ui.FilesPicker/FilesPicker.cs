using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class FilesPicker()
    : CrossControl<IFilesPicker>(Services.GetSingleton<IFilesPickerFactory>().Create()),
        IFilesPicker
{
    public void Show() => Implementation.Show();
}
