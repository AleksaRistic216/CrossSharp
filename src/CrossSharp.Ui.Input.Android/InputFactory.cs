using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Android;

class InputFactory : IInputFactory
{
    public IInput Create() => new Input();
}
