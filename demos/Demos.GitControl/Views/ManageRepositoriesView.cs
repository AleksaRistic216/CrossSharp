using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using Demos.GitControl.Controls;

namespace Demos.GitControl.Views;

public class ManageRepositoriesView : StackedLayout
{
    public ManageRepositoriesView()
    {
        Scrollable = ScrollableMode.Vertical;
        Dock = DockPosition.Fill;

        Add(new ManageRepositoriesItemControl());
    }
}
