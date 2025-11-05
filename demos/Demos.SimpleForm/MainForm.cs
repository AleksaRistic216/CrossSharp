using CrossSharp.Ui;
using CrossSharp.Utils;

namespace Demos.SimpleForm;

public class MainForm : Form
{
    public MainForm()
    {
        var button = new Button() { BackgroundColor = ColorRgba.Red };
        // button.BackgroundColor = ColorRgba.Red;
        button.Text = "Click Me!";
        button.AutoSize = true;
        Controls.Add(button);
    }
}
