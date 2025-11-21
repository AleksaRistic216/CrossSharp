using CrossSharp.Ui;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;

namespace Demos.AllInOne.MainFormViews;

public sealed class ThemesView : StackedLayout
{
    const int LABEL_HEIGHT = 24;

    public ThemesView()
    {
        Initialize();
    }

    void Initialize()
    {
        Dock = DockStyle.Fill;
        var label1 = new Label();
        label1.Text = "Check out what you can do with themes";
        label1.Height = LABEL_HEIGHT;
        Add(label1);

        InitializeThemePicker();
    }

    void InitializeThemePicker()
    {
        var themePicker = new ThemePicker();
        themePicker.Width = 200;
        themePicker.CollapsedHeight = 50;
        Add(themePicker);
    }
}
