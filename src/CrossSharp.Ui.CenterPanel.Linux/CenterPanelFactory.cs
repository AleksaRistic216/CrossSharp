using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class CenterPanelFactory : ICenterPanelFactory
{
    public ICenterPanel Create() => new CenterPanel();
}
