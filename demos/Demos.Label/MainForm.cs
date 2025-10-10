using CrossSharp.Diagnostics;
using CrossSharp.Ui;

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
                label.Text = Ui.TicksPerSecond.ToString();
                Thread.Sleep(1000);
            }
        });
    }
}
