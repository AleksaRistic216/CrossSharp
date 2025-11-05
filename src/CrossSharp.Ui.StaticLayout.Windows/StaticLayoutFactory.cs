using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class StaticLayoutFactory : IStaticLayoutFactory
{
    public IStaticLayout Create()
    {
        var layout = new StaticLayout();
        layout.Initialize();
        return layout;
    }
}
