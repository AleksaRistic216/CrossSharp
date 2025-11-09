using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class StackedLayoutFactory : IStackedLayoutFactory
{
    public IStackedLayout Create() => new StackedLayout();
}
