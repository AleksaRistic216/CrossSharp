using System.Drawing;
using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using CrossSharp.Utils.Structs;

namespace CrossSharp.Ui.Common;

partial class Dropdown : StackedLayout, IDropdown
{
    protected Dropdown()
    {
        Initialize();
        PerformTheme();
    }

    void Initialize()
    {
        _headerLayout = new StackedLayout();
        _headerLayout.Orientation = Orientation.Horizontal;
        _headerLayout.ItemsSpacing = 4;
        Add(_headerLayout);
        InitializeExpandIcon();
        InitializePlaceholder();

        _itemsLayout = new StackedLayout();
        _itemsLayout.Scrollable = ScrollableMode.Vertical;
        _itemsLayout.Visible = State is DropdownState.Expanded;
        _itemsLayout.Orientation = Orientation.Vertical;
        _itemsLayout.MaxHeight = 400;
        _itemsLayout.ItemsSpacing = 4;
        Add(_itemsLayout);
    }

    void InitializePlaceholder()
    {
        _placeholder = new Label();
        _placeholder.Location = new Point(0, 0);
        _placeholder.Index = 0;
        _headerLayout!.Add(_placeholder);
    }

    void InvalidatePlaceholder()
    {
        if (_placeholder is null)
            return;
        var textSize = TextHelpers.MeasureText(this, _placeholder.Text, _placeholder.FontFamily, _placeholder.FontSize);
        _placeholder.Width = textSize.Width;
        _placeholder.Height = textSize.Height;
        var selectedItemText = _selectedItem is null ? "Select an option" : "(1)";
        if (_selectedItem is IButton btn)
            selectedItemText = btn.Text;
        _placeholder.Text = selectedItemText!;
    }

    public sealed override void PerformTheme()
    {
        base.PerformTheme();
        Orientation = Orientation.Vertical;
        BorderWidth = 1;
        BorderColor = ColorRgba.DimGray;
        Padding = Padding.Zero;
        ItemsSpacing = 0;
        if (CollapsedHeight == 0)
            CollapsedHeight = Theme.DefaultFontSize + Theme.DefaultLayoutItemSpacing;
    }

    void InitializeExpandIcon()
    {
        _expandBtn = new Button();
        _expandBtn.Width = 30;
        _expandBtn.Dock = DockStyle.Right;
        _expandBtn.DockIndex = 0;
        _expandBtn.Index = 1;
        _expandBtn.Click += (_, _) =>
        {
            State = State is DropdownState.Collapsed ? DropdownState.Expanded : DropdownState.Collapsed;
        };
        _headerLayout!.Add(_expandBtn);
    }

    public sealed override void Invalidate()
    {
        base.Invalidate();
        InvalidatePlaceholder();
        if (_expandBtn is not null)
            _expandBtn.Text = State is DropdownState.Collapsed ? "▼" : "▲";
        if (_itemsLayout is not null)
            _itemsLayout.Height = _itemsLayout.Sum(x =>
                x.Height + _itemsLayout.ItemsSpacing + x.MarginTop + x.MarginBottom
            );
        _headerLayout?.Invalidate();
        _itemsLayout?.Invalidate();
        InvalidateSize();
    }

    void InvalidateSize()
    {
        if (_headerLayout is not null)
            _headerLayout.Height = CollapsedHeight;
        if (State is DropdownState.Collapsed)
        {
            Height = CollapsedHeight;
            return;
        }
        Height = (_itemsLayout?.Height ?? 0) + CollapsedHeight;
    }

    public void AddItem(params IDropdownItem[] item)
    {
        foreach (var i in item)
            _itemsLayout!.Add(i);
        Invalidate();
    }

    public void RemoveItem(params IDropdownItem[] item)
    {
        foreach (var i in item)
            _itemsLayout!.Remove(i);
        Invalidate();
    }

    public void ClearItems()
    {
        foreach (var i in _itemsLayout!)
            _itemsLayout!.Remove(i);
        Invalidate();
    }
}
