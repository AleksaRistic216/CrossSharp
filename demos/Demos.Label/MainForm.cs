using CrossSharp.Ui;
using CrossSharp.Utils;

namespace Demos.Label;

public class MainForm : Form
{
    public MainForm()
    {
        var label = new CrossSharp.Ui.Label() { Text = "Hello World!" };
        Controls.Add(label);

        Task.Run(() =>
        {
            while (true)
            {
                label.Text = CrossSharp.Diagnostics.Ui.TicksPerSecond.ToString();
            }
        });
    }
}
