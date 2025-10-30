using CrossSharp.Ui;

namespace Demos.FilesPicker;

public class MainForm : Form
{
    CrossSharp.Ui.FilesPicker filesPicker = new();

    public MainForm()
    {
        var button = new Button();
        button.Text = "Open File Picker";
        button.AutoSize = true;
        button.Click += (s, e) =>
        {
            filesPicker.Show();
        };
        Controls.Add(button);
    }
}
