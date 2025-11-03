using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.EventArgs;

namespace Demos.FilesPicker;

public class MainForm : Form
{
    CrossSharp.Ui.FilesPicker filesPicker;

    public MainForm()
    {
        var button = new Button();
        button.Text = "Open File Picker";
        button.AutoSize = true;
        button.Click += (s, e) =>
        {
            filesPicker = new CrossSharp.Ui.FilesPicker();
            filesPicker.FilesSelected += FilesPicker_OnFileSelected;
            filesPicker.Show();
        };
        Controls.Add(button);
    }

    void FilesPicker_OnFileSelected(object? sender, FilesSelectedEventArgs e)
    {
        Notifications.Show("Selected file", $"You selected: {e.SelectedFiles.Length} files.");
    }
}
