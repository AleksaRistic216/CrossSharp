using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class ThemePickerDropdownItem : StaticLayout, IDropdownItem, IClickable
{
    internal ThemePickerDropdownItem(ITheme itemTheme)
    {
        Location = Point.Empty;
        _itemTheme = itemTheme;
        _layout = new StackedLayout();
        _layout.Dock = DockStyle.Fill;
        _layout.SetMargin(8);
        Add(_layout);

        _themeNameLabel = new Label();
        _themeNameLabel.Text = itemTheme.GetType().Name;
        _themeNameLabel.Height = Convert.ToInt32(_itemHeight * _itemHeightScales[0]);
        _layout.Add(_themeNameLabel);

        _primaryColorPanel = new Panel();
        _primaryColorPanel.Height = Convert.ToInt32(_itemHeight * _itemHeightScales[1]);
        _layout.Add(_primaryColorPanel);

        _secondaryColorPanel = new Panel();
        _secondaryColorPanel.Height = Convert.ToInt32(_itemHeight * _itemHeightScales[2]);
        _layout.Add(_secondaryColorPanel);

        SubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        ThemePerformed += OnThemePerformed;
        Disposing += OnDispose;
        Services.GetSingleton<IInputHandler>().MouseMoved += OnMouseMoved;
        Services.GetSingleton<IInputHandler>().MousePressed += OnClick;
    }

    void UnsubscribeFromEvents()
    {
        ThemePerformed -= OnThemePerformed;
        Disposing -= OnDispose;
        Services.GetSingleton<IInputHandler>().MouseMoved -= OnMouseMoved;
        Services.GetSingleton<IInputHandler>().MousePressed -= OnClick;
    }

    void OnDispose(object? sender, EventArgs e)
    {
        UnsubscribeFromEvents();
    }

    void OnThemePerformed(object? sender, EventArgs e)
    {
        // control theme
        BackgroundColor = Theme.LayoutBackgroundColor;
        CornerRadius = Theme.DefaultCornerRadius;
        BorderColor = ColorRgba.DimGray;
        BorderWidth = 1;

        // theme preview
        _primaryColorPanel.BackgroundColor = _itemTheme.PrimaryColor;
        _primaryColorPanel.CornerRadius = _itemTheme.DefaultCornerRadius;
        _primaryColorPanel.BorderColor = ColorRgba.Gray;
        _primaryColorPanel.BorderWidth = 1;

        _secondaryColorPanel.BackgroundColor = _itemTheme.SecondaryColor;
        _secondaryColorPanel.CornerRadius = _itemTheme.DefaultCornerRadius;
        _secondaryColorPanel.BorderColor = ColorRgba.Gray;
        _secondaryColorPanel.BorderWidth = 1;

        var totalItemsSpacing = _layout.ItemsSpacing * 3;
        Height = _itemHeight + _layout.MarginTop + _layout.MarginBottom + totalItemsSpacing;
    }

    void SelectTheme()
    {
        if (Services.GetSingleton<ITheme>() == _itemTheme)
            return;
        Services.AddSingleton(_itemTheme, true);
        var form = this.GetForm();
        form!.PerformTheme();
        form!.Invalidate();
    }
}
