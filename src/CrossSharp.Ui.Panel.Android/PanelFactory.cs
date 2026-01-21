using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class PanelFactory : IPanelFactory
{
    public IPanel Create() => new Panel();
}
