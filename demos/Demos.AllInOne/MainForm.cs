using CrossSharp.Ui;

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
    }
}
