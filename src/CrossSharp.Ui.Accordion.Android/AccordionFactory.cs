using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class AccordionFactory : IAccordionFactory
{
    public IAccordion Create() => new Accordion();
}
