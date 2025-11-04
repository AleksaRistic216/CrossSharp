using CrossSharp.Utils.DI;
using CrossSharp.Utils.Enums;
using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Linux;

class FormFactory : IFormFactory
{
    public IForm Create() => new FormSDL();
}
