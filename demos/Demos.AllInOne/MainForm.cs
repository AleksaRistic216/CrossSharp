using CrossSharp.Ui;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace Demos.AllInOne;

public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeSideMenu();
        InitializeViewer();
        SubscribeEvents();
    }

    public override void Dispose()
    {
        UnsubscribeEvents();
        base.Dispose();
    }

    void SubscribeEvents()
    {
        ThemePerformed += OnThemePerformed;
    }

    void UnsubscribeEvents()
    {
        ThemePerformed -= OnThemePerformed;
    }

    void OnThemePerformed(object? sender, EventArgs e)
    {
        _viewer.PerformTheme();
        // You can handle theme changes here if needed
        // Usually not necessary if you are working with one theme, however if you allow runtime theme switching
        // theme driven properties will be updated according to theme and you can re-assign any custom property here
        if (Services.GetSingleton<ITheme>().DefaultLayoutItemSpacing == 0)
            _contentPane.Margin = new Margin(8);
    }
}
