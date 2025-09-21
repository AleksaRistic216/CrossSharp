using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;

namespace FirstDemo;

public class MainForm : Form
{
    public MainForm()
    {
        this.Width = 800;
        this.Height = 800;
        this.Location = new Point(0, 0);
        this.OnShow += MainForm_OnShow;
    }

    void MainForm_OnShow(object? sender, EventArgs e)
    {
        var panel = new PanelControl()
        {
            Width = 100,
            Height = 100,
            Location = new Point(0, 0),
            BackgroundColor = ColorRgba.Purple,
        };
        this.Controls.Add(panel);
        var panel1 = new PanelControl()
        {
            Width = 250,
            Height = 250,
            Location = new Point(200, 200),
            BackgroundColor = ColorRgba.Blue,
        };
        Controls.Add(panel1);
        var button = new Button()
        {
            Width = 100,
            Height = 50,
            Location = new Point(300, 300),
        };
        Controls.Add(button);
        button.OnClick += (s, e) =>
        {
            panel.BackgroundColor = ColorRgba.Red;
            panel.Invalidate();
        };
    }
}
