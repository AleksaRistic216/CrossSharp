using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class ModularFormFactory : IModularFormFactory
{
    public IForm Create() => new ModularForm();
}
