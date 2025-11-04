using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

public class InputFactory : IInputFactory
{
    public IInput Create() => new Input();
}
