using CrossSharp.Utils;
using CrossSharp.Utils.DI;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui;

public class Accordion()
    : CrossControl<IAccordion>(Services.GetSingleton<IAccordionFactory>().Create()),
        IAccordion { }
