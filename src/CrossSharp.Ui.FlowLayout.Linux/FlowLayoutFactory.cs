using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FlowLayoutFactory : IFlowLayoutFactory
{
    public IFlowLayout Create()
    {
        var layout = new FlowLayout();
        layout.Initialize();
        return layout;
    }
}
