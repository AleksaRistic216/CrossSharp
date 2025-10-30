using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using Demos.TabbedLayout.Tabs;

namespace Demos.TabbedLayout;

public class MainForm : Form
{
    public MainForm()
    {
        var tabbedLayout = new CrossSharp.Ui.TabbedLayout();
        tabbedLayout.Dock = DockStyle.Fill;
        tabbedLayout.AddTab("First Tab", typeof(FirstTab));
        tabbedLayout.AddTab("Second Tab", typeof(SecondTab));
        Controls.Add(tabbedLayout);
        tabbedLayout.SelectTab("First Tab");
    }
}
