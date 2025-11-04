using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class FlowLayoutFactory : IFlowLayoutFactory
{
    public IFlowLayout Create() => new FlowLayout();
}
