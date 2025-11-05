using CrossSharp.Utils;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Common;

partial class Accordion : StackedLayout, IAccordion
{
    public Accordion()
    {
        BackgroundColor = Theme.SecondaryBackgroundColor;
    }

    public void AddItem(IAccordionItem item)
    {
        Add(item);
    }
}
