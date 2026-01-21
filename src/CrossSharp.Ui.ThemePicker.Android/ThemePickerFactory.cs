using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class ThemePickerFactory : IThemePickerFactory
{
    public IThemePicker Create() => new ThemePicker();
}
