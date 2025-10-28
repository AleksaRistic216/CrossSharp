using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class TabbedLayoutFactory : ITabbedLayoutFactory
{
    public ITabbedLayout Create() => new TabbedLayout();
}