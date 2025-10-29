using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;

namespace Demos.TextEditor;

public class MainForm : Form
{
    public MainForm()
    {
        InitializeMenuBar();

        var tabbedLayout = new TabbedLayout();
        tabbedLayout.Dock = DockPosition.Fill;
        tabbedLayout.DockIndex = 2;
        Controls.Add(tabbedLayout);
        tabbedLayout.AddTab("First Tab", typeof(TextEditTab));
        tabbedLayout.AddTabButton(
            "+",
            () =>
            {
                Notifications.Show("New Tab Clicked", "You clicked the new tab button.");
            }
        );
    }

    void InitializeMenuBar()
    {
        var menuBarHeight = 30;
        var menuBar = new StackedLayout();
        menuBar.Height = menuBarHeight;
        menuBar.BackgroundColor = ColorRgba.RandomColor;
        menuBar.Direction = Direction.Horizontal;
        menuBar.Dock = DockPosition.Top;
        menuBar.DockIndex = 0;
        Controls.Add(menuBar);

        var button1 = new Button();
        button1.Text = "New File";
        button1.AutoSize = true;
        button1.MinHeight = menuBarHeight;
        menuBar.Add(button1);
    }
}
