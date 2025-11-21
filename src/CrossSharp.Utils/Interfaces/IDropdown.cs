using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Utils.Interfaces;

public interface IDropdown : IControl
{
    void AddItem(params IDropdownItem[] item);
    void RemoveItem(params IDropdownItem[] item);
    void ClearItems();
    DropdownState State { get; set; }
    EventHandler? StateChanged { get; set; }
    int CollapsedHeight { get; set; }
    EventHandler? CollapsedHeightChange { get; set; }
    object? SelectedItem { get; set; }
    EventHandler? SelectedItemChanged { get; set; }
    Margin? MinimumItemMargin { get; set; }
    EventHandler? MinimumItemMarginChanged { get; set; }
}
