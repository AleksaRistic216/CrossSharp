using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class StackedLayoutFactory : IStackedLayoutFactory
{
    public IStackedLayout Create() => new StackedLayout();
}
