using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Interfaces;

namespace FirstDemo;

public class MainForm : Form
{
    readonly IApplication _application;
    readonly ITheme _theme;

    public MainForm()
    {
        Width = 1000;
        Height = 800;
        OnShow += MainForm_OnShow;
    }

    void MainForm_OnShow(object? sender, EventArgs e)
    {
        var panel = new Panel()
        {
            Width = 100,
            Height = 100,
            Location = new Point(0, TitleBar.Height), // cant hold like this because of switch to native titlebar, coordinates will be fcked up
            // Either have some stack, then on top title bar, bellow fixed
            // Or offset everything by titlebar height
            BackgroundColor = ColorRgba.Purple,
        };
        Controls.Add(panel);
        // var panel1 = new Panel()
        // {
        //     Width = 250,
        //     Height = 250,
        //     Location = new Point(200, 200),
        //     BackgroundColor = ColorRgba.Blue,
        //     BorderWidth = 50,
        //     BorderColor = ColorRgba.DarkGray,
        // };
        // Controls.Add(panel1);
        // var button = new Button()
        // {
        //     Width = 100,
        //     Height = 50,
        //     Location = new Point(300, 300),
        // };
        // Controls.Add(button);
        // button.OnClick += (s, e) =>
        // {
        //     Location = new Point(new Random().Next(0, 201), new Random().Next(0, 201));
        // };
        //
        // var label = new Label() { Location = new Point(500, 500), Text = "Hello Motherfuckers!" };
        // Controls.Add(label);
        //
        // var centerPanel = new CenterPanel()
        // {
        //     Width = 100,
        //     Height = 100,
        //     Location = new Point(0, 100),
        //     BackgroundColor = ColorRgba.Green,
        //     Child = new Label() { Location = new Point(10, 10), Text = "I'm centered!" },
        // };
        // Controls.Add(centerPanel);
        //
        // var centerPanel1 = new CenterPanel()
        // {
        //     Width = 100,
        //     Height = 100,
        //     Location = new Point(0, 200),
        //     BackgroundColor = ColorRgba.Purple,
        //     Child = new Panel()
        //     {
        //         Location = new Point(10, 10),
        //         Width = 30,
        //         Height = 30,
        //         BackgroundColor = ColorRgba.Yellow,
        //     },
        // };
        // Controls.Add(centerPanel1);
    }
}
