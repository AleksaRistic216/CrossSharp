using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Accordion()
    : CrossControl<IAccordion>(Services.GetSingleton<IAccordionFactory>().Create()),
        IAccordion
{
    public int DockIndex
    {
        get => Implementation.DockIndex;
        set => Implementation.DockIndex = value;
    }
    public DockStyle Dock
    {
        get => Implementation.Dock;
        set => Implementation.Dock = value;
    }
    public ColorRgba BackgroundColor
    {
        get => Implementation.BackgroundColor;
        set => Implementation.BackgroundColor = value;
    }
    public EventHandler? BackgroundColorChanged
    {
        get => Implementation.BackgroundColorChanged;
        set => Implementation.BackgroundColorChanged = value;
    }

    public AccordionState State
    {
        get => Implementation.State;
        set => Implementation.State = value;
    }
    public EventHandler? StateChanged
    {
        get => Implementation.StateChanged;
        set => Implementation.StateChanged = value;
    }
    public Orientation Orientation
    {
        get => Implementation.Orientation;
        set => Implementation.Orientation = value;
    }
    public EventHandler? OrientationChanged
    {
        get => Implementation.OrientationChanged;
        set => Implementation.OrientationChanged = value;
    }

    public void AddItem(IAccordionItem item) => Implementation.AddItem(item);
}
