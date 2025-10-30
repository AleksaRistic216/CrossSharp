using CrossSharp.Ui;
using CrossSharp.Utils;

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
            filesPicker.Show();
        };
        Controls.Add(button);
    }
}
