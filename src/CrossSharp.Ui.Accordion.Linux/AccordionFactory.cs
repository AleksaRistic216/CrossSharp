using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class AccordionFactory : IAccordionFactory
{
    public IAccordion Create()
    {
        var accordion = new Accordion();
        accordion.Initialize();
        return accordion;
    }
}
