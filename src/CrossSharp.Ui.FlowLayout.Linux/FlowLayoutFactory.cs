using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FlowLayoutFactory : IFlowLayoutFactory
{
    public IFlowLayout Create() => new FlowLayout();
}
