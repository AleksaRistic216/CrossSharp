using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class Dropdown
{
    ITheme Theme => Services.GetSingleton<ITheme>();
    IButton? _expandBtn;
    DropdownState _state = DropdownState.Collapsed;
    Label? _placeholder;
    IStackedLayout? _headerLayout;
    IStackedLayout? _itemsLayout;
    public DropdownState State
    {
        get => _state;
        set
        {
            if (_state == value)
                return;
            _state = value;
            OnStateChanged();
        }
    }
    int _collapsedHeight;
    public int CollapsedHeight
    {
        get => _collapsedHeight;
        set
        {
            if (_collapsedHeight == value)
                return;
            _collapsedHeight = value;
            OnCollapsedHeightChanged();
        }
    }
    object? _selectedItem;
    public object? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (_selectedItem == value)
                return;
            _selectedItem = value;
            OnSelectedItemChanged();
        }
    }
    Margin? _minimalItemsMargin;
    public Margin? MinimumItemMargin
    {
        get => _minimalItemsMargin;
        set
        {
            if (_minimalItemsMargin == value)
                return;
            _minimalItemsMargin = value;
            OnMinimalItemsMarginChanged();
        }
    }
}
