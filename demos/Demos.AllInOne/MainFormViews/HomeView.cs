using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.AllInOne.MainFormViews;

public sealed class HomeView : StackedLayout
{
    const int LABEL_HEIGHT = 24;

    public HomeView()
    {
        Dock = DockStyle.Fill;
        var label1 = new Label();
        label1.Text = "Welcome to the CrossSharp All-In-One Demo!";
        label1.Height = LABEL_HEIGHT;
        Add(label1);

        var label2 = new Label();
        label2.Text = "Use the side menu to navigate through different views.";
        label2.Height = LABEL_HEIGHT;
        Add(label2);
    }
}
