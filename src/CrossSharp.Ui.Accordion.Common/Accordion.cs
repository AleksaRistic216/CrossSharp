using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Extensions;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Accordion : StackedLayout, IAccordion
{
    internal Accordion()
    {
        InitializeHeader();

        _itemsArea = new StackedLayout();
        _itemsArea.DockIndex = 1;
        _itemsArea.Orientation = Orientation.Vertical;
        _itemsArea.Dock = DockStyle.Fill;
        Add(_itemsArea);

        PerformTheme();
    }

    public sealed override void Add(params IControl[] controls)
    {
        base.Add(controls);
        foreach (var control in controls)
            control.Parent = this;
    }

    void InitializeHeader()
    {
        SizeChanged += (_, _) =>
        {
            if (State != AccordionState.Expanded)
                return;
            _lastWidth = Width;
            _lastHeight = Height;
        };
        _headerArea = new StackedLayout();
        _headerArea.Orientation = Orientation.Horizontal;
        _headerArea.Dock = DockStyle.Top;
        _headerArea.Height = 40;
        Add(_headerArea);

        _hamburgButton = new Button();
        _hamburgButton.Width = _headerArea.Height;
        _hamburgButton.Height = _headerArea.Height;
        _hamburgButton.Text = "☰";
        _hamburgButton.Click += (_, _) =>
        {
            State = State == AccordionState.Collapsed ? AccordionState.Expanded : AccordionState.Collapsed;
        };
        _headerArea.Add(_hamburgButton);
    }

    public override void Invalidate()
    {
        base.Invalidate();
        InvalidateItemsVisibility();
        InvalidateWidth();
        InvalidateHeight();
    }

    void InvalidateItemsVisibility()
    {
        if (Orientation == Orientation.Vertical)
            return;
        _itemsArea.Visible = _state == AccordionState.Expanded;
    }

    void InvalidateWidth()
    {
        if (Orientation != Orientation.Vertical)
            return;
        var collapsedWidth =
            _hamburgButton.Width + Padding.Horizontal + _headerArea.MarginLeft + _headerArea.MarginRight;
        Width = _state == AccordionState.Expanded ? _lastWidth : collapsedWidth;
    }

    void InvalidateHeight()
    {
        if (Orientation != Orientation.Horizontal)
            return;
        var collapsedHeight = _headerArea.Height;
        Height = _state == AccordionState.Expanded ? _lastHeight : collapsedHeight;
    }

    public void AddItem(IAccordionItem item)
    {
        _itemsArea.Add(item);
    }

    public sealed override void PerformTheme() // Shouldn't override method from
    {
        this.SetMargin(Theme.DefaultLayoutItemSpacing);
        _itemsArea.PerformTheme();
        _headerArea.PerformTheme();
        base.PerformTheme();
        BackgroundColor = Theme.PrimaryColor;
        _itemsArea.BackgroundColor = ColorRgba.Transparent;
        _headerArea.BackgroundColor = ColorRgba.Transparent;
        Invalidate();
    }
}
