using System.Drawing;
using CrossSharp.Desktop;
using CrossSharp.Ui;

namespace ButtonDemo;

public class MainForm : Form
{
    public MainForm()
    {
        Width = 1000;
        Height = 800;

        var button = new Button
        {
            Text = "Click Me",
            Location = new Point(Width / 2 - 100, Height / 2 - 50),
            Width = 200,
            Height = 100,
        };
        button.OnClick += (s, e) =>
        {
            button.Text = "Clicked!";
            Notifications.Show("Hello", "You have clicked the button!");
        };
        Controls.Add(button);
    }
}
