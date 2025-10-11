using System.Drawing;
using CrossSharp.Ui;

namespace Demos.ModularForm;

public class MainForm : CrossSharp.Ui.ModularForm
{
    public MainForm()
    {
        RegisterContentSingleton(this);
        AddPageWithNavigation("First Page", typeof(FirstPage));
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
        // mainForm.OnSizeChanged += (s, e) =>
        // {
        //     Invalidate();
        // };
        Dock = CrossSharp.Utils.Enums.DockPosition.Fill;
        _label = new Label() { Text = "This is the first page" };
        Add(_label);
        _button = new Button()
        {
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
