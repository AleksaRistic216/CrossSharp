using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace Demos.FlowLayout;

public class MainForm : Form
{
    public MainForm()
    {
        Width = 800;
        Height = 600;
        var flowLayout = new CrossSharp.Ui.FlowLayout();
        flowLayout.Dock = DockPosition.Fill;
        flowLayout.BackgroundColor = ColorRgba.DarkGreen;
        Controls.Add(flowLayout);

        for (var i = 0; i < 50; i++)
        {
            var button = new Button()
            {
                Text = $"Hello {i}",
                Width = Random.Shared.Next(80, 300),
                Style = RenderStyle.Contained,
                Height = Random.Shared.Next(50, 100),
                BackgroundColor = ColorRgba.LightGray,
            };
            flowLayout.Add(button);
        }
    }
}
