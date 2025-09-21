using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

public class FormFactory : IFormFactory
{
    public IForm Create() => new Form();
}
