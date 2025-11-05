using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class TabbedLayoutFactory : ITabbedLayoutFactory
{
    public ITabbedLayout Create()
    {
        var layout = new TabbedLayout();
        return layout;
    }
}
