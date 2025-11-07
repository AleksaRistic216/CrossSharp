using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class ThemePickerFactory : IThemePickerFactory
{
    public IThemePicker Create() => new ThemePicker();
}
