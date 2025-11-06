using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Dropdown
{
    public EventHandler? SelectedItemChanged { get; set; }

    void RaiseSelectedItemChanged()
    {
        SelectedItemChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnSelectedItemChanged()
    {
        InvalidatePlaceholder();
        RaiseSelectedItemChanged();
    }

    public EventHandler? CollapsedHeightChange { get; set; }

    void RaiseCollapsedHeightChange()
    {
        CollapsedHeightChange?.Invoke(this, EventArgs.Empty);
    }

    void OnCollapsedHeightChanged()
    {
        RaiseCollapsedHeightChange();
    }

    public EventHandler? StateChanged { get; set; }

    void RaiseStateChanged()
    {
        StateChanged?.Invoke(this, EventArgs.Empty);
    }

    void OnStateChanged()
    {
        if (_itemsLayout is not null)
            _itemsLayout.Visible = State is DropdownState.Expanded;
        // if (Parent is IStackedLayout sl)
        //     sl.Invalidate();
        // else
        Invalidate();
        RaiseStateChanged();
    }
}
