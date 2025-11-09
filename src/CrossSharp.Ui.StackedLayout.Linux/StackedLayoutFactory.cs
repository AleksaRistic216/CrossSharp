using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class StackedLayoutFactory : IStackedLayoutFactory
{
    public IStackedLayout Create() => new StackedLayout();
}
