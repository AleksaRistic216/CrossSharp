using CrossSharp.Themes;
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
        var item = new ThemePickerDropdownItem(new DefaultTheme());
        item.Height = 30;
        AddItem(item);
        _items.Add(item);
    }

    public override void PerformTheme()
    {
        base.PerformTheme();
        foreach (var item in _items)
            item.PerformTheme();
    }
}
