using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class AccordionFactory : IAccordionFactory
{
    public IAccordion Create() => new Accordion();
}
