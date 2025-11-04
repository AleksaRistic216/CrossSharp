using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.TabbedLayout.Tabs;

public class SecondTab : StackedLayout, ITabbedLayoutTab
{
    Label _label;
    Button _button;

    public SecondTab()
    {
        Dock = DockStyle.Fill;
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

    public void OnTabFocusGained(string title) { }
}
