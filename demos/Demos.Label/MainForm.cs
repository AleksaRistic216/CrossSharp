using CrossSharp.Ui;
using CrossSharp.Utils;

namespace Demos.Label;

public class MainForm : Form
{
    public MainForm()
    {
        var label = new CrossSharp.Ui.Label()
        {
            ForegroundColor = ColorRgba.White,
            Text = "Hello World!",
        };
        Controls.Add(label);
    }
}
