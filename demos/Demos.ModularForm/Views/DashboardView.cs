using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.ModularForm.Views;

public class DashboardView : StackedLayout
{
    public DashboardView()
    {
        Dock = DockStyle.Fill;
        var button = new Button();
        button.Height = 50;
        button.Text = "Button";
        Add(button);
    }
}
