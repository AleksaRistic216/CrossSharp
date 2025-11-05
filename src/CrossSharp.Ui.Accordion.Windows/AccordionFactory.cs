using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class AccordionFactory : IAccordionFactory
{
    public IAccordion Create()
    {
        var accordion = new Accordion();
        accordion.Initialize();
        return accordion;
    }
}
