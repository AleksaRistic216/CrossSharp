using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class PanelControlFactory : IPanelControlFactory
{
    public IPanelControl Create(IBackgroundColorProvider backgroundColorProvider)
    {
        return new PanelControl(backgroundColorProvider);
    }
}
