using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ThemePicker : Dropdown, IThemePicker
{
    internal ThemePicker()
    {
        InitializeItems();
    }

    void InitializeItems()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        List<Type> themeTypes = [];
        foreach (var type in assemblies.SelectMany(x => x.GetTypes()))
            if (type is { IsClass: true, IsAbstract: false } && typeof(ITheme).IsAssignableFrom(type))
                themeTypes.Add(type);

        foreach (var themeType in themeTypes)
            if (Activator.CreateInstance(themeType) is ITheme themeInstance)
                AddItem(new ThemePickerDropdownItem(themeInstance));
    }
}
