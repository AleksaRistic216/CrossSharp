using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Accordion : StackedLayout, IAccordion
{
    public void AddItem(IAccordionItem item)
    {
        Add(item);
    }

    public override void PerformTheme()
    {
        base.PerformTheme();
        BackgroundColor = Theme.SecondaryBackgroundColor;
    }
}
