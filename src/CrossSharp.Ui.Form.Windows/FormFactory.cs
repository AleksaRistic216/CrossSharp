using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

class FormFactory : IFormFactory
{
    public IForm Create() => new Form();
}
