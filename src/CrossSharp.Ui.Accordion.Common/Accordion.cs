using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Drawing;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Helpers;
using CrossSharp.Utils.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace CrossSharp.Ui.Common;

partial class Accordion : StackedLayout, IAccordion
{
    protected Accordion()
    {
        Orientation = Orientation.Vertical;
        InitializeHeader();

        _itemsArea = new StackedLayout();
        _itemsArea.DockIndex = 1;
        _itemsArea.Orientation = Orientation.Vertical;
        _itemsArea.Dock = DockStyle.Fill;
        Add(_itemsArea);
    }

    void InitializeHeader()
    {
        SizeChanged += (s, e) =>
        {
            if (State == AccordionState.Expanded)
                _lastWidth = Width;
        };
        _headerArea = new StackedLayout();
        _headerArea.Orientation = Orientation.Horizontal;
        _headerArea.Dock = DockStyle.Top;
        _headerArea.Height = 35;
        Add(_headerArea);

        _hamburgButton = new Button();
        _hamburgButton.Width = _headerArea.Height;
        _hamburgButton.Height = _headerArea.Height;
        _hamburgButton.Text = "☰";
        _hamburgButton.BackgroundColor = ColorRgba.Transparent;
        _hamburgButton.Click += (s, e) =>
        {
            State =
                State == AccordionState.Collapsed
                    ? AccordionState.Expanded
                    : AccordionState.Collapsed;
        };
        _headerArea.Add(_hamburgButton);
    }

    public override void Invalidate()
    {
        base.Invalidate();
        var minWidth =
            _hamburgButton.Width
            + Padding.Horizontal
            + _headerArea.MarginLeft
            + _headerArea.MarginRight;
        Width = _state == AccordionState.Expanded ? _lastWidth : minWidth;
    }

    public void AddItem(IAccordionItem item)
    {
        _itemsArea.Add(item);
    }

    public override void PerformTheme()
    {
        BackgroundColor = Theme.SecondaryBackgroundColor;
        _itemsArea.PerformTheme();
        _headerArea.PerformTheme();
        base.PerformTheme();
    }
}
