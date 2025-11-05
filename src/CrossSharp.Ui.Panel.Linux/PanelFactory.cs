using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class PanelFactory : IPanelFactory
{
    public IPanel Create()
    {
        var panel = new Panel();
        panel.Initialize();
        return panel;
    }
}
