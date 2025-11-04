using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class AccordionFactory : IAccordionFactory
{
    public IAccordion Create() => new Accordion();
}
