using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IAccordion : IControl, IDockable, IBackgroundColorProvider, IRoundedCorners
{
    AccordionState State { get; set; }
    EventHandler? StateChanged { get; set; }
    Orientation Orientation { get; set; }
    EventHandler? OrientationChanged { get; set; }
    void AddItem(IAccordionItem item);
}
