using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;

namespace FirstDemo;

public class MainForm : Form
{
    public MainForm()
    {
        Width = 1000;
        Height = 800;
        var panel = new Panel
        {
            Width = 100,
            Height = 100,
            Location = new Point(0, 0),
            BackgroundColor = ColorRgba.Purple,
        };
        Controls.Add(panel);
    }
}
