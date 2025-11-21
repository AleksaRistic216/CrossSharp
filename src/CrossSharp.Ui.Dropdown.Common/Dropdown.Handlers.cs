using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Structs;

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

    void OnThemePerformed(object? sender, EventArgs e)
    {
        Orientation = Orientation.Vertical;
        BorderWidth = 1;
        BorderColor = ColorRgba.DimGray;
        Padding = Padding.Zero;
        ItemsSpacing = 0;

        if (CollapsedHeight == 0)
            CollapsedHeight = Theme.DefaultFontSize + Theme.DefaultLayoutItemSpacing;
    }

    void OnDispose(object? sender, EventArgs e)
    {
        UnsubscribeFromEvents();
    }
}
