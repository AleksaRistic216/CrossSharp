using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace Demos.TextEditor;

public class MainForm : Form
{
    public MainForm()
    {
        var menuBar = new StackedLayout();
        menuBar.Height = 30;
        menuBar.BackgroundColor = ColorRgba.RandomColor;
        menuBar.Direction = Direction.Horizontal;
        menuBar.Dock = DockPosition.Top;
        menuBar.DockIndex = 0;
        Controls.Add(menuBar);

        // var menuBar1 = new StackedLayout();
        // menuBar1.Height = 30;
        // menuBar1.BackgroundColor = ColorRgba.RandomColor;
        // menuBar1.Direction = Direction.Horizontal;
        // menuBar1.Dock = DockPosition.Top;
        // menuBar1.DockIndex = 1;
        // Controls.Add(menuBar1);

        var tabbedLayout = new TabbedLayout();
        tabbedLayout.Dock = DockPosition.Fill;
        tabbedLayout.DockIndex = 2;
        tabbedLayout.AddTab("Text Editor", typeof(TextEditTab));
        Controls.Add(tabbedLayout);
    }
}
