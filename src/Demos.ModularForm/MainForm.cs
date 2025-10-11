using System.Drawing;
using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.ModularForm;

public class MainForm : CrossSharp.Ui.ModularForm
{
    public MainForm()
    {
        RegisterContentSingleton(this);
        AddPageWithNavigation("First Page", typeof(FirstPage));
        AddPageWithNavigation("Second Page", typeof(SecondPage));
    }
}

class FirstPage : StackedLayout
{
    Label _label;
    Button _button;
    MainForm _mainForm;

    public FirstPage(MainForm mainForm)
    {
        _mainForm = mainForm;
        Dock = DockPosition.Fill;
        _label = new Label() { Text = "This is the first page" };
        Add(_label);
        _button = new Button()
        {
            Style = RenderStyle.Contained,
            Text = "Button on first page",
            Location = new Point(0, 205),
            Width = mainForm.Width,
            Height = 50,
        };
        Add(_button);
    }

    public override void Invalidate()
    {
        Width = _mainForm.Width;
        base.Invalidate();
    }
}

class SecondPage : StackedLayout
{
    Label _label;
    Button _button;
    MainForm _mainForm;

    public SecondPage(MainForm mainForm)
    {
        _mainForm = mainForm;
        Dock = DockPosition.Fill;
        _label = new Label() { Text = "This is the second page" };
        Add(_label);
        _button = new Button()
        {
            Text = "Button on second page",
            Style = RenderStyle.Contained,
            Location = new Point(0, 205),
            Width = mainForm.Width,
            Height = 50,
        };
        Add(_button);
    }

    public override void Invalidate()
    {
        Width = _mainForm.Width;
        base.Invalidate();
    }
}
