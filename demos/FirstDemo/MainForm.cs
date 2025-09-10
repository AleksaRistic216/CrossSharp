using System.Drawing;
using CrossSharp.Ui;

namespace FirstDemo;

public class MainForm : Form
{
    public MainForm()
    {
        this.Width = 800;
        this.Height = 800;
        this.OnShow += MainForm_OnShow;
    }

    void MainForm_OnShow(object? sender, EventArgs e)
    {
        // var panel = new PanelControl()
        // {
        //     Width = 100,
        //     Height = 100,
        //     Location = new Point(0, 0),
        //     BackgroundColor = Color.Blue,
        // };
        // this.Controls.Add(panel);
        var panel1 = new PanelControl()
        {
            Width = 250,
            Height = 250,
            Location = new Point(200, 200),
            BackgroundColor = Color.Orange,
        };
        Controls.Add(panel1);
    }
}
