using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class PanelFactory : IPanelFactory
{
    public IPanel Create()
    {
        return new Panel();
    }
}
