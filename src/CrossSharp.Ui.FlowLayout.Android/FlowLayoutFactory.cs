using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class FlowLayoutFactory : IFlowLayoutFactory
{
    public IFlowLayout Create() => new FlowLayout();
}
