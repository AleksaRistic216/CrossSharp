using CrossSharp.Ui;
using CrossSharp.Utils.Enums;

namespace Demos.TextEditor;

public class MainForm : Form
{
    public MainForm()
    {
        var stackedLayout = new StackedLayout();
        stackedLayout.Dock = DockPosition.Fill;
        Controls.Add(stackedLayout);

        // var menuBar = new StackedLayout();
        // menuBar.Height = 30;
        // menuBar.Direction = Direction.Horizontal;
        // stackedLayout.Add(menuBar);

        var tabbedLayout = new TabbedLayout();
        tabbedLayout.Dock = DockPosition.Fill;
        tabbedLayout.AddTab("Text Editor", typeof(TextEditTab));
        stackedLayout.Add(tabbedLayout);
    }
}
