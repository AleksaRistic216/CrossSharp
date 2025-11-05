using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class StackedLayoutFactory : IStackedLayoutFactory
{
    public IStackedLayout Create()
    {
        var layout = new StackedLayout();
        layout.Initialize();
        return layout;
    }
}
