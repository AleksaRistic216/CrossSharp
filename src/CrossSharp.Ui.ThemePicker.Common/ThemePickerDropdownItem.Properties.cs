using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ThemePickerDropdownItem
{
    int _itemHeight = 120;
    float[] _itemHeightScales = [0.2f, 0.4f, 0.4f];
    ITheme Theme => Services.GetSingleton<ITheme>();
    readonly ITheme _itemTheme;
    IStackedLayout _layout;
    ColorRgba _backgroundColor = ColorRgba.Transparent;
    IPanel _primaryColorPanel;
}
