using CrossSharp.Utils.Enums;

namespace CrossSharp.Utils.Interfaces;

public interface IAccordion : IControl, IDockable, IBackgroundColorProvider
{
    Orientation Orientation { get; set; }
    EventHandler? OrientationChanged { get; set; }
    void AddItem(IAccordionItem item);
}
