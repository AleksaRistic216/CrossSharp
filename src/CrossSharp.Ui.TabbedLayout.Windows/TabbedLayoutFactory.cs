using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class TabbedLayoutFactory : ITabbedLayoutFactory
{
    public ITabbedLayout Create() => new TabbedLayout();
}
