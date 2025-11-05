using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IAccordion : IControl, IDockable, IBackgroundColorProvider
{
    AccordionState State { get; set; }
    EventHandler? StateChanged { get; set; }
    Orientation Orientation { get; set; }
    EventHandler? OrientationChanged { get; set; }
    void AddItem(IAccordionItem item);
}
