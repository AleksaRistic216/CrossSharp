using CrossSharp.Utils.Interfaces;

namespace CrossSharp.Ui.Windows;

public class InputFactory : IInputFactory
{
    public IInput Create()
    {
        var input = new Input();
        input.Initialize();
        return input;
    }
}
