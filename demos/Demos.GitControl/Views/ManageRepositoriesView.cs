using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.GitControl.Views;

public class ManageRepositoriesView : StackedLayout
{
    public ManageRepositoriesView()
    {
        Scrollable = ScrollableMode.Vertical;
        Dock = DockPosition.Fill;

        var button = new Button { Text = "Add Repository" };
        button.Width = 200;
        button.Height = 30;
        button.OnClick += (s, e) => { };
        Add(button);
    }
}
