using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class StaticLayoutFactory : IStaticLayoutFactory
{
    public IStaticLayout Create() => new StaticLayout();
}
