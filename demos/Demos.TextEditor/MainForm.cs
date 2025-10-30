using CrossSharp.Desktop;
using CrossSharp.Ui;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.TextEditor;

public class MainForm : Form
{
    ITheme Theme => Services.GetSingleton<ITheme>();

    public MainForm()
    {
        InitializeMenuBar();

        var tabbedLayout = new TabbedLayout();
        tabbedLayout.Dock = DockStyle.Fill;
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
        menuBar.BackgroundColor = Theme.SecondaryBackgroundColor.Darkened;
        menuBar.Direction = Direction.Horizontal;
        menuBar.Dock = DockStyle.Top;
        menuBar.DockIndex = 0;
        Controls.Add(menuBar);

        var button1 = new Button();
        button1.Text = "New File";
        button1.AutoSize = true;
        button1.MinHeight = menuBarHeight;
        menuBar.Add(button1);

        var button2 = new Button();
        button2.Text = "Open File";
        button2.AutoSize = true;
        button2.MinHeight = menuBarHeight;
        menuBar.Add(button2);
    }
}
