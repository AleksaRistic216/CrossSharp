using CrossSharp.Ui.Common;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

public class ModularFormFactory : IModularFormFactory
{
    public IForm Create() => new ModularForm();
}
