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
    public FirstPage(MainForm mainForm)
    {
        Dock = CrossSharp.Utils.Enums.DockPosition.Fill;
        // Width = 500;
        // Height = 500;
        Add(
            new Label()
            {
                Text = "This is the first page",
                Height = 200,
                Width = 200,
            }
        );
        Add(
            new Button()
            {
                Text = "Button on first page",
                Location = new Point(0, 205),
                Width = 200,
                Height = 50,
            }
        );
    }
}
