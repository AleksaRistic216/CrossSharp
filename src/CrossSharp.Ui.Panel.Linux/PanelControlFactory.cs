using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class PanelControlFactory : IPanelControlFactory
{
    public IPanelControl Create()
    {
        return new PanelControl();
    }
}
