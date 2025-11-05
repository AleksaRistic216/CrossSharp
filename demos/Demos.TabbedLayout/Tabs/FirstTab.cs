using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace Demos.TabbedLayout.Tabs;

public class FirstTab : StackedLayout, ITabbedLayoutTab
{
    ITheme Theme => Services.GetSingleton<ITheme>();
    IStackedLayout _navigationMenu = null!;

    public FirstTab()
    {
        Orientation = Orientation.Horizontal;
        Dock = DockStyle.Fill;
        InitializeNavigationMenu();
    }

    void InitializeNavigationMenu()
    {
        _navigationMenu = new StackedLayout();
        _navigationMenu.Orientation = Orientation.Vertical;
        _navigationMenu.BackgroundColor = Theme.BackgroundColor.Darkened;
        _navigationMenu.Width = 200;
        _navigationMenu.Dock = DockStyle.Left;
        Add(_navigationMenu);
    }

    public void OnTabFocusGained(string title) { }
}
