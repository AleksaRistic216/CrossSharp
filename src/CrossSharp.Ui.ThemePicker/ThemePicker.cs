using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class ThemePicker()
    : CrossControl<IThemePicker>(Services.GetSingleton<IThemePickerFactory>().Create()),
        IThemePicker
{
    public int CollapsedHeight
    {
        get => Implementation.CollapsedHeight;
        set => Implementation.CollapsedHeight = value;
    }
}
