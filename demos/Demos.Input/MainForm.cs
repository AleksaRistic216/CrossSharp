using CrossSharp.Ui;

namespace Demos.Input;

public class MainForm : Form
{
    public MainForm()
    {
        var input = new CrossSharp.Ui.Input();
        input.Width = 200;
        input.Height = 30;
        input.Location = new(100, 100);
        Controls.Add(input);
    }
}
