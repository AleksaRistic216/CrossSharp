using CrossSharp.Utils;
using CrossSharp.Utils.Attributes;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.EventArgs;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

[Control("Files Picker", [ControlGroup.Input], ControlIcon.FilesPicker)]
public class FilesPicker()
    : CrossControl<IFilesPicker>(Services.GetSingleton<IFilesPickerFactory>().Create()),
        IFilesPicker
{
    public void Show() => Implementation.Show();

    public void Close() => Implementation.Close();

    public EventHandler<FilesSelectedEventArgs>? FilesSelected
    {
        get => Implementation.FilesSelected;
        set => Implementation.FilesSelected = value;
    }
}
