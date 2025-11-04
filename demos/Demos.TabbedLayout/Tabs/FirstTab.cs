using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace Demos.TabbedLayout.Tabs;

public class FirstTab : StackedLayout, ITabbedLayoutTab
{
    Label _label;
    Button _button;

    public FirstTab()
    {
        ItemsSpacing = 10;
        Dock = DockStyle.Fill;
        _label = new Label() { Text = "This is the first page" };
        Add(_label);
        _button = new Button()
        {
            Style = RenderStyle.Contained,
            Text = "Button on first page",
            Height = 50,
        };
        Add(_button);
    }

    public void OnTabFocusGained(string title) { }
}
