using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;

namespace Demos.Button;

public class MainForm : Form
{
    public MainForm()
    {
        var button = new CrossSharp.Ui.Button();
        button.Width = 100;
        button.Height = 50;
        button.Location = new Point(50, 50);
        button.BackgroundColor = ColorRgba.Gray;
        button.Text = "Click Me";
        Controls.Add(button);
    }
}
