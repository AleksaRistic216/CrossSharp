using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class StaticLayoutFactory : IStaticLayoutFactory
{
    public IStaticLayout Create() => new StaticLayout();
}
