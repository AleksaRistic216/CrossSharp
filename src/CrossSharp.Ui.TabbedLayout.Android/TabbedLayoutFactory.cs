using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class TabbedLayoutFactory : ITabbedLayoutFactory
{
    public ITabbedLayout Create() => new TabbedLayout();
}
