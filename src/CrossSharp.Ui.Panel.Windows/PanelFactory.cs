using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

public class PanelFactory : IPanelFactory
{
    public IPanel Create() => new Windows.Panel();
}
