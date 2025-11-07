using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ThemePickerDropdownItem : StaticLayout, IDropdownItem
{
    internal ThemePickerDropdownItem(ITheme itemTheme)
    {
        _itemTheme = itemTheme;
        _layout = new StackedLayout();
        _layout.Dock = DockStyle.Fill;
        _layout.SetMargin(8);
        Add(_layout);

        _primaryColorPanel = new Panel();
        _primaryColorPanel.Height = 200;
        _layout.Add(_primaryColorPanel);

        ThemePerformed += (s, e) =>
        {
            // control theme
            BackgroundColor = Theme.LayoutBackgroundColor;
            CornerRadius = Theme.DefaultCornerRadius;
            BorderColor = ColorRgba.DimGray;
            BorderWidth = 1;

            // theme preview
            _primaryColorPanel.BackgroundColor = _itemTheme.PrimaryColor;
            _primaryColorPanel.CornerRadius = _itemTheme.DefaultCornerRadius;
        };
    }
}
