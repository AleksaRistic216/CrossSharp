using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class StaticLayoutFactory : IStaticLayoutFactory
{
    public IStaticLayout Create() => new StaticLayout();
}
