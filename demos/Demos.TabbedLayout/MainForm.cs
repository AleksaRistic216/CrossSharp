using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.TabbedLayout;

public class MainForm : Form
{
    public MainForm()
    {
        var tabbedLayout = new CrossSharp.Ui.TabbedLayout();
        tabbedLayout.Dock = DockPosition.Fill;
        tabbedLayout.AddTab("First Tab", typeof(FirstTab));
        Controls.Add(tabbedLayout);
        tabbedLayout.SelectTab("First Tab");
    }
}

public class FirstTab : StackedLayout
{
    Label _label;
    Button _button;

    public FirstTab()
    {
        Dock = DockPosition.Fill;
        _label = new Label() { Text = "This is the first page" };
        Add(_label);
        _button = new Button()
        {
            Style = RenderStyle.Contained,
            Text = "Button on first page",
            Location = new Point(0, 205),
            Width = 200,
            Height = 50,
        };
        Add(_button);
    }

    public override void Invalidate()
    {
        base.Invalidate();
    }
}
