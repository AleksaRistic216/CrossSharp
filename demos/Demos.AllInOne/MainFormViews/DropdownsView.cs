using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne.MainFormViews;

public sealed class DropdownsView : StackedLayout
{
    const int LABEL_HEIGHT = 24;

    public DropdownsView()
    {
        Dock = DockStyle.Fill;

        var label1 = new Label();
        label1.Text = "Here are examples of dropdown controls usage.";
        label1.Height = LABEL_HEIGHT;
        Add(label1);
    }
}
