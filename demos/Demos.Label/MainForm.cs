using CrossSharp.Ui;

namespace Demos.Label;

public class MainForm : Form
{
    public MainForm()
    {
        var label = new CrossSharp.Ui.Label();
        Controls.Add(label);
    }
}
