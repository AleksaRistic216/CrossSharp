using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace Demos.GitControl.Controls;

public class ManageRepositoriesItemControl : StackedLayout
{
    public ManageRepositoriesItemControl()
    {
        Dock = DockPosition.Fill;
        Add(
            new Button
            {
                Height = 50,
                BackgroundColor = ColorRgba.Blue,
                Text = "asd",
            }
        );
    }
}
