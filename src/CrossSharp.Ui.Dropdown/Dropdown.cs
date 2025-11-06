using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Dropdown() : CrossControl<IDropdown>(Services.GetSingleton<IDropdownFactory>().Create()), IDropdown
{
    public new int Height
    {
        get => Implementation.Height;
        set =>
            throw new InvalidOperationException(
                $"Height property is managed by Dropdown control itself. Use {nameof(CollapsedHeight)} instead."
            );
    }

    public void AddItem(params IDropdownItem[] item) => Implementation.AddItem(item);

    public void RemoveItem(params IDropdownItem[] item) => Implementation.RemoveItem(item);

    public void ClearItems() => Implementation.ClearItems();

    public DropdownState State
    {
        get => Implementation.State;
        set => Implementation.State = value;
    }
    public EventHandler? StateChanged
    {
        get => Implementation.StateChanged;
        set => Implementation.StateChanged = value;
    }
    public int CollapsedHeight
    {
        get => Implementation.CollapsedHeight;
        set => Implementation.CollapsedHeight = value;
    }
    public EventHandler? CollapsedHeightChange
    {
        get => Implementation.CollapsedHeightChange;
        set => Implementation.CollapsedHeightChange = value;
    }
    public object? SelectedItem
    {
        get => Implementation.SelectedItem;
        set => Implementation.SelectedItem = value;
    }
    public EventHandler? SelectedItemChanged
    {
        get => Implementation.SelectedItemChanged;
        set => Implementation.SelectedItemChanged = value;
    }
}
