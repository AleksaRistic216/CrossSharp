using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class TabbedLayoutFactory : ITabbedLayoutFactory
{
    public ITabbedLayout Create()
    {
        var layout = new TabbedLayout();
        layout.Initialize();
        return layout;
    }
}
