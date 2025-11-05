using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne.MainFormViews;

public class ThemesView : StackedLayout
{
    const int LABEL_HEIGHT = 24;

    public override void Initialize()
    {
        Dock = DockStyle.Fill;
        var label1 = new Label();
        label1.Text = "Check out what you can do with themes";
        label1.Height = LABEL_HEIGHT;
        Add(label1);
    }
}
