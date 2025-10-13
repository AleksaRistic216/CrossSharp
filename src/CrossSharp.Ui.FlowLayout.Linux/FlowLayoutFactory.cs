using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FlowLayoutFactory : IFlowLayoutFactory
{
    public IFlowLayout Create() => new FlowLayout();
}
