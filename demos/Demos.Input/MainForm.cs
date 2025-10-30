using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.Input;

public class MainForm : Form
{
    public MainForm()
    {
        var flowLayout = new FlowLayout();
        flowLayout.Dock = DockStyle.Fill;
        flowLayout.ItemsSpacing = 10;
        Controls.Add(flowLayout);

        var singleLineInput = new CrossSharp.Ui.Input();
        singleLineInput.Width = 200;
        singleLineInput.Height = 30;
        flowLayout.Add(singleLineInput);

        var multiLineInput = new CrossSharp.Ui.Input();
        multiLineInput.Width = 200;
        multiLineInput.Height = 100;
        multiLineInput.MultiLine = true;
        flowLayout.Add(multiLineInput);
    }
}
