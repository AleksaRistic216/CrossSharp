using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class StackedLayoutFactory : IStackedLayoutFactory
{
    public IStackedLayout Create() => new StackedLayout();
}
