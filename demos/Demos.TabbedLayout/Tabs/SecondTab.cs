using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.TabbedLayout.Tabs;

public class SecondTab : StackedLayout
{
    Label _label;
    Button _button;

    public SecondTab()
    {
        Dock = DockPosition.Fill;
        _label = new Label() { Text = "This is the second page" };
        Add(_label);
        _button = new Button()
        {
            Style = RenderStyle.Contained,
            Text = "Button on second page",
            Height = 50,
        };
        Add(_button);
    }
}
