using CrossSharp.Themes;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class ThemePicker : Dropdown, IThemePicker
{
    readonly HashSet<Type> _priorityThemes =
    [
        typeof(DefaultTheme),
        typeof(LightTheme),
        typeof(FlatBlueTheme),
        typeof(FlatPinkTheme),
        typeof(RoundedSpacedLimitlessSoftTheme),
        typeof(RoundedSpacedLightTheme),
    ];

    internal ThemePicker()
    {
        InitializeItems();
        MinimumItemMargin = new Margin(16, 8);
    }

    void InitializeItems()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        List<Type> themeTypes = [];
        foreach (var type in assemblies.SelectMany(x => x.GetTypes()))
            if (type is { IsClass: true, IsAbstract: false } && typeof(ITheme).IsAssignableFrom(type))
                themeTypes.Add(type);

        var priority = _priorityThemes.Where(themeTypes.Contains).ToList();

        var others = themeTypes.Where(t => !_priorityThemes.Contains(t)).ToList();

        foreach (var themeType in priority.Concat(others))
            if (Activator.CreateInstance(themeType) is ITheme themeInstance)
                AddItem(new ThemePickerDropdownItem(themeInstance));
    }
}
